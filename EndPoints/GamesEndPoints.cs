
using GameStore.Api.Entities;
using GameStore.Api.Repository;

namespace GameStore.Api.EndPoints;

public static class GameEndPoints
{
    const string GetGameEndPointName = "GetGameById";

    public static RouteGroupBuilder MapGameEndPoints(this IEndpointRouteBuilder route)
    {
        IGamesRepository repo = new InMemGamesRepository();
        var gameGroup = route.MapGroup("/games")
                    .WithParameterValidation();

        gameGroup.MapGet("/", async () => await repo.GetAllAsync());

        gameGroup.MapPost("/", async (Game game) =>
        {
            if (game is null)
            {
                return Results.BadRequest();
            }
            
            await repo.CreateAsync(game);

            return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, game);
        });

        gameGroup.MapGet("/{id}", async (int id) =>
        {
            Game? game = await repo.GetAsync(id);

            if (game is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(game);
        }).WithName(GetGameEndPointName);

        return gameGroup;
    }
}