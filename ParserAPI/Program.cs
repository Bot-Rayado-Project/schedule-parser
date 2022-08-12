using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using ParserDAL.DataAccess;
using Parser.Core.ScheduleParser;
using Parser;

# region Configuration

ParserStates state = ParserStates.isStopped;

var builder = WebApplication.CreateBuilder(args);

// Create logs folder
if (!Directory.Exists("logs")) Directory.CreateDirectory("logs");

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logs/logger.log"));
builder.Logging.AddMail(Environment.GetEnvironmentVariable("EADRESS"), Environment.GetEnvironmentVariable("EPASSWORD"));

// Add services
builder.Services.AddDbContext<ScheduleContext>(options =>
{
    options.UseNpgsql("Host=localhost;Port=5432;Database=schedules;Username=postgres;Password=postgres");
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v2.2.3",
        Title = "Parser API",
        Description = "An ASP.NET Core Web API for managing schedule parsers' work",
        Contact = new OpenApiContact
        {
            Name = "VKontakte",
            Url = new Uri("https://vk.com/botrayado")
        }
    });
});
builder.Services.AddSingleton<IParserWorker>(new ParserWorker(new ScheduleParser(), new ScheduleParserSettings()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

#endregion

var parser = app.Services.GetRequiredService<IParserWorker>();

parser.OnNewData += (object _, string[] data) =>
{
    //var db = app.Services.GetRequiredService<ParserDALContext>();
    //ParserDALUtils.WriteSchedule(db, "бвт2103", "понедельник", "четная", "CUM!!!");
};

app.MapGet("/api/v1/start", (bool runOnce) =>
{
    if (runOnce) return RunOnce();
    else return RunForever();
});

app.MapGet("/api/v1/state", () =>
{
    return Results.Ok(new { State = state.ToString() });
});

app.MapGet("/api/v1/stop", () =>
{
    return Stop();
});

app.Run();

IResult RunOnce()
{
    if (state == ParserStates.isStopped || state == ParserStates.isSuspended)
    {
        state = ParserStates.isRunningOnce;
        RunOnceAsync();
        return Results.Ok(new { Message = "OK.", State = state.ToString() });
    }
    else return Results.BadRequest(new { Message = "Parser already running.", State = state.ToString() });
}

IResult RunForever()
{
    if (state == ParserStates.isStopped)
    {
        state = ParserStates.isRunningForever;
        RunForeverAsync();
        return Results.Ok(new { Message = "OK.", State = state.ToString() });
    }
    else return Results.BadRequest(new { Message = "Parser already running or suspended.", State = state.ToString() });
}

IResult Stop()
{
    if (state != ParserStates.isStopped)
    {
        parser.Stop();
        state = ParserStates.isStopped;
        return Results.Ok(new { Message = "OK.", State = state.ToString() });
    }
    else return Results.BadRequest(new { Message = "Parser already stopped.", State = state.ToString() });
}

async Task RunOnceAsync()
{
    try
    {
        await parser.StartAsync();
        state = ParserStates.isStopped;
    }
    catch (System.Exception ex)
    {
        app.Logger.LogCritical("An error occured in event loop: " + ex);
    }
}

async Task RunForeverAsync()
{
    while (state == ParserStates.isRunningForever)
    {
        try
        {
            await parser.StartAsync();
            app.Logger.LogInformation($"Sleeping for {parser.Settings.Delay} seconds...");
            await Task.Delay(parser.Settings.Delay);
        }
        catch (System.Exception ex)
        {
            app.Logger.LogCritical("An error occured in event loop: " + ex + $"\nSleeping for {parser.Settings.Delay} seconds...");
            await Task.Delay(parser.Settings.Delay);
        }
    }
}
