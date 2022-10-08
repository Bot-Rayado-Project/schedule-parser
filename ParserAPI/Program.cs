using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using ParserDAL.DataAccess;
using ParserDAL.Models;
using Parser.Core.ScheduleParser;
using Parser.Core.Models;
using Parser;
using Newtonsoft.Json;
using UnidecodeSharpFork;

# region Configuration

var builder = WebApplication.CreateBuilder(args);

// Get required environment variables
string connectionString = builder.Configuration["ConnectionString"] ?? throw new Exception("DB Connection string not given.");
string logsFileName = builder.Configuration["logsFileName"] ?? "log";
string? emailAdress = builder.Configuration["EADRESS"];
string? emailPassword = builder.Configuration["EPASSWORD"];
string? url = builder.Configuration["url"];
string? downloadPath = builder.Configuration["downloadPath"];
int? delay = builder.Configuration.GetValue<int>("delay");
string projectJsonContent = "{\n" + GetSectionContent(builder.Configuration.GetSection("streamsMatchesFaculties")) + "}";
var streamsMatchesFaculties = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, int>>>(projectJsonContent);

// Create logs folder
if (!Directory.Exists("logs")) Directory.CreateDirectory("logs");

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddFile(Path.Combine(Directory.GetCurrentDirectory(), $"logs/{logsFileName}.log"));
builder.Logging.AddMail(emailAdress, emailPassword);

// Add services
builder.Services.AddSingleton(new ParserWorker<Dictionary<string, Dictionary<int, Dictionary<DayOfWeekRussian, string>>>>(
                              new ScheduleParser(),
                              new ScheduleParserSettings(url, downloadPath, streamsMatchesFaculties)
                            ));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v2.7.9",
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
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
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

var parser = app.Services.GetRequiredService<ParserWorker<Dictionary<string, Dictionary<int, Dictionary<DayOfWeekRussian, string>>>>>();

parser.OnNewData += (object _, Dictionary<string, Dictionary<int, Dictionary<DayOfWeekRussian, string>>> data) =>
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetService<ScheduleContext>();
        foreach (var _data in data)
        {
            string _group = _data.Key.ToLower();
            _group = _group.Unidecode();
            foreach (var parity in _data.Value)
            {
                string _parity = parity.Key == 1 ? "нечетная" : "четная";
                foreach (var day in parity.Value)
                {
                    string _day = day.Key.ToString();
                    string schedule = day.Value;
                    var selectedSchedule = (from s in db.SharedSchedules
                                            where (s.stream_group.Equals(_group) && s.parity.Equals(_parity) && s.day.Equals(_day))
                                            select s).FirstOrDefault();
                    if (selectedSchedule != null)
                    {
                        selectedSchedule.schedule = schedule;
                    }
                    else
                        db.SharedSchedules.Add(new SharedSchedule()
                        {
                            stream_group = _group,
                            day = _day,
                            parity = _parity,
                            schedule = schedule
                        });
                }
            }
        }
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
            await Task.Delay(Convert.ToInt32(delay) * 1000);
        }
        catch (Exception ex)
        {
            app.Logger.LogCritical("An error occured in event loop: " + ex + $"\nSleeping for {Convert.ToInt32(delay)} seconds...");
            await Task.Delay(Convert.ToInt32(delay) * 1000);
        }
    }
}

string GetSectionContent(IConfiguration configSection)
{
    string sectionContent = "";
    foreach (var section in configSection.GetChildren())
    {
        sectionContent += "\"" + section.Key + "\":";
        if (section.Value == null)
        {
            string subSectionContent = GetSectionContent(section);
            sectionContent += "{\n" + subSectionContent + "},\n";
        }
        else
        {
            sectionContent += "\"" + section.Value + "\",\n";
        }
    }
    return sectionContent;
}