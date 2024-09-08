using GameStore.Api.EndPoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGameEndPoints();

app.Run();
