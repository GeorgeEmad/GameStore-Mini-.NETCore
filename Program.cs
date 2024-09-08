using GameStore.Api.Entities;

List<Game> games = new (){
    new Game{
        Id = 1,
        Name = "Game1",
        Genre = "Action",
        ImageUri = "https://placehold.co/100"
    },
    new Game{
        Id = 2,
        Name = "Game2",
        Genre = "Fantasy",
        ImageUri = "https://placehold.co/100"
    },
};

const string GetGameEndPointName = "GetGameById";
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");


var gameGroup = app.MapGroup("/games")
                    .WithParameterValidation();

gameGroup.MapGet("/", () => games);

gameGroup.MapPost("/", (Game game) => {
    if(game is null)
    {
        return Results.BadRequest();
    }
    int id = games.Max(x => x.Id) + 1;
    game.Id = id;
    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndPointName, new {id = game.Id}, game);
});

gameGroup.MapGet("/{id}", (int id) => {
    Game? game = games.Find((x) => x.Id == id);

    if(game is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(game);
}).WithName(GetGameEndPointName);





app.Run();
