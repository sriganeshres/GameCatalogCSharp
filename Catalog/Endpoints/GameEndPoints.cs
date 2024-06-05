using Catalog.Contracts;
using Catalog.Data;
using Catalog.Entities;
using Catalog.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Endpoints;

public static class GameEndPoints
{
    const string GETGAMEENDPOINT = "GetGame";

    public static RouteGroupBuilder GetGameEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();
        group.MapGet("/hello", () =>
        {
            Console.WriteLine("Hello, world!");
            return "Hello World!";
        });

        group.MapGet("/", (CatalogContext dbContext) =>
        {
            Console.WriteLine("Returning All Games");
            GameDTO[] games = [];
            if (dbContext.Games.Any())
            {
                games = [
                    .. dbContext.Games
                .Include(game => game.Genre)
                .Select(g => g.ToDTO())
                .AsNoTracking()
                ];
            }
            return games;
        });

        group.MapGet("/{id}", (int id, CatalogContext dbContext) =>
        {
            Console.WriteLine($"Returning Game {id}");
            Game? game = dbContext.Games.Find(id);
            return game is null ? Results.NotFound() : Results.Ok(game.ToDetailsDTO());
        }).WithName(GETGAMEENDPOINT);


        group.MapPost("/", (CreateGame newGame, CatalogContext dbContext) =>
        {
            Console.WriteLine($"Creating new Game {newGame.Name}");

            Game game = newGame.ToEntity();

            dbContext.Games.Add(game);
            dbContext.SaveChanges();

            GameDetailsDTO gameDTO = game.ToDetailsDTO();

            return Results.CreatedAtRoute(GETGAMEENDPOINT, new { id = game.ID }, gameDTO);
        });

        group.MapPut("/{id}", (int id, UpdateGameDTO updateGame, CatalogContext dbContext) =>
        {
            Console.WriteLine($"Updating Game at ID: {id}");
            var existingGame = dbContext.Games.Find(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingGame)
            .CurrentValues
            .SetValues(updateGame.ToEntity(id));
            dbContext.SaveChanges();

            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id, CatalogContext dbContext) =>
        {
            Console.WriteLine($"Deleting a Game at {id}");
            dbContext.Games
            .Where(g => g.ID == id)
            .ExecuteDelete();
            return Results.NoContent();
        });

        return group;
    }
}
