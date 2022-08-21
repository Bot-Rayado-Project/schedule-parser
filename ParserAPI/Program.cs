using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using ParserDAL.DataAccess;
using ParserDAL.Models;
using Parser.Core.ScheduleParser;
using Parser;

# region Configuration

var builder = WebApplication.CreateBuilder(args);

// Get required environment variables
string connectionString = builder.Configuration["ConnectionStrings:Db"] ?? throw new Exception("DB Connection string not given.");
string logsFileName = builder.Configuration["logsFileName"] ?? "log";
string? emailAdress = builder.Configuration["EADRESS"];
string? emailPassword = builder.Configuration["EPASSWORD"];
string? url = builder.Configuration["url"];
string? downloadPath = builder.Configuration["downloadPath"];
int? delay = builder.Configuration.GetValue<int>("delay");

// Create logs folder
if (!Directory.Exists("logs")) Directory.CreateDirectory("logs");

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddFile(Path.Combine(Directory.GetCurrentDirectory(), $"logs/{logsFileName}.log"));
builder.Logging.AddMail(emailAdress, emailPassword);

// Add services
builder.Services.AddSingleton(new ParserWorker<Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<int, string?>>>>>(
                            new ScheduleParser(),
                            new ScheduleParserSettings(url, downloadPath)
                        ));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v2.3.4",
        Title = "Parser API",
        Description = "An ASP.NET Core Web API for managing schedule parsers' work",
        Contact = new OpenApiContact
        {
            Name = "VK",
            Url = new Uri("https://vk.com/botrayado")
        }
    });
});
builder.Services.AddDbContext<ScheduleContext>(options =>
{
    options.UseNpgsql(connectionString);
});

#endregion

// Parser state variable to control parserworker
ParserStates state = ParserStates.isStopped;

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

// Create DB
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ScheduleContext>();
    db.Database.EnsureCreated();
}

var parser = app.Services.GetRequiredService<ParserWorker<Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<int, string?>>>>>>();

parser.OnNewData += (object _, Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<int, string?>>>> data) =>
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetService<ScheduleContext>();
        foreach (var kvp in data)
        {
            string group = kvp.Key;
            foreach (var days in kvp.Value)
            {

            }
        }
        // ЛОГИКА ЗАПИСИ В БД ТУТ!!! МОЖНО ВСТАВИТЬ ДЕЛЕГАТ!!!!
        db.SharedSchedules.Add(new SharedSchedule()
        {
            stream_group = "бвт2103",
            day = "понедельник",
            parity = "четная",
            schedule = "CUM!!!",
        });
        db.SaveChanges();
    }
};

app.MapGet("/api/v1/start", (bool runOnce) => runOnce ? RunOnce() : RunForever());

app.MapGet("/api/v1/state", () => Results.Ok(new { State = state.ToString() }));

app.MapGet("/api/v1/stop", Stop);

app.Run();

IResult RunOnce()
{
    if (state is ParserStates.isStopped or ParserStates.isSuspended)
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
    catch (Exception ex)
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
            app.Logger.LogInformation($"Sleeping for {Convert.ToInt32(delay)} seconds...");
            await Task.Delay(Convert.ToInt32(delay));
        }
        catch (Exception ex)
        {
            app.Logger.LogCritical("An error occured in event loop: " + ex + $"\nSleeping for {Convert.ToInt32(delay)} seconds...");
            await Task.Delay(Convert.ToInt32(delay));
        }
    }
}
