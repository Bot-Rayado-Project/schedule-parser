using Parser;
using ParserAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Create parser instance
IParserWorker parser = new ParserWorker(new ParserSettings() /*{Delay = 5000}*/);

app.MapGet("/api/v1/start", (bool runOnce) =>
{
    if (parser.State == ParserStates.isStopped)
    {
        if (runOnce) parser.RunOnce();
        parser.RunForever();
        return Results.Ok(new { Message = "OK" });
    }
    return Results.BadRequest(new { Message = "Another instance of parser is already running!" });
});

app.MapGet("/api/v1/state", () =>
{
    return Results.Ok(new { Message = parser.State.ToString() });
});

app.Run();
