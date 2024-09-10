using GameStore.Api.EndPoints;
using GameStore.Api.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IGamesRepository, InMemGamesRepository>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGameEndPoints();

app.Run();
