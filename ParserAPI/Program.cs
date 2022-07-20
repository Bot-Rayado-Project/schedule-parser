using Parser;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1.1",
        Title = "Parser API",
        Description = "An ASP.NET Core Web API for managing schedule parsers' work",
        Contact = new OpenApiContact
        {
            Name = "VKontakte",
            Url = new Uri("https://vk.com/botrayado")
        }
    });
});

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

// Create parser instance
IParserWorker parser = new ParserWorker(new ParserSettings() /*{Delay = 5000}*/);


app.MapGet("/api/v1/start", (bool runOnce) =>
{
    if (parser.State == ParserStates.isStopped)
    {
        if (runOnce) parser.RunOnce();
        else parser.RunForever();
        return Results.Ok(new { Message = "OK" });
    }
    else if (parser.State == ParserStates.isSuspended && runOnce) { parser.RunOnce(); return Results.Ok(new { Message = "OK" }); }
    return Results.BadRequest(new { Message = "Another instance of parser is already running" });
});

app.MapGet("/api/v1/state", () =>
{
    return Results.Ok(new { Message = parser.State.ToString() });
});

app.MapGet("/api/v1/stop", () =>
{
    if (parser.State != ParserStates.isStopped) { parser.Stop(); return Results.Ok(new { Message = "OK" }); }
    return Results.Ok(new { Message = "No running instance detected" });
});

app.Run();
