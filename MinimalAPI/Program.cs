using MinimalAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<NameService>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpLogging();

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", (NameService nameService) =>
{
    app.Logger.LogInformation("/weatherforecastcalled");
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

// status codes
app.MapGet("/Statuscodes", (bool ok) => ok ? Results.Ok("Everything is ok!") : Results.BadRequest("Bad Request!"));

// rounting 
app.MapGet("/", () => get);
app.MapPost("/", () => "Post ccalled");
app.MapPut("/", () => "Put ccalled");
app.MapDelete("/", () => "Delete ccalled");

var personHandler = new PersonHandler();
app.MapGet("/Persons", personHandler.HandlerGet);

// route parameters
//app.MapGet("/Persons/{id}", personHandler.HandlerGetById);

// route parameter constrains
app.MapGet("/Persons/{id:int}", personHandler.HandlerGetById);

// parameter binding
// person json from body to person object automatically
app.MapPost("Persons", (Person person) => person.FirstName + ", " + person.LastName);

app.Run();

string get() => "Get called";

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
