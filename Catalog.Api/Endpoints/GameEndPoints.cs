using Catalog.Api.Contracts;
using Catalog.Api.Data;
using Catalog.Api.Entities;
using Catalog.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Endpoints;

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

        group.MapGet("/", async (CatalogContext dbContext) =>
        {
            Console.WriteLine("Returning All Games");

            return await dbContext.Games
                .Include(game => game.Genre)
                .Select(g => g.ToDTO())
                .AsNoTracking()
                .ToListAsync();
        });

        group.MapGet("/{id}", async (int id, CatalogContext dbContext) =>
        {
            Console.WriteLine($"Returning Game {id}");
            Game? game = await dbContext.Games.FindAsync(id);
            return game is null ? Results.NotFound() : Results.Ok(game.ToDetailsDTO());
        }).WithName(GETGAMEENDPOINT);


        group.MapPost("/", async (CreateGame newGame, CatalogContext dbContext) =>
        {
            Console.WriteLine($"Creating new Game {newGame.Name}");

            Game game = newGame.ToEntity();

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            GameDetailsDTO gameDTO = game.ToDetailsDTO();

            return Results.CreatedAtRoute(GETGAMEENDPOINT, new { id = game.ID }, gameDTO);
        });

        group.MapPut("/{id}", async (int id, UpdateGameDTO updateGame, CatalogContext dbContext) =>
        {
            Console.WriteLine($"Updating Game at ID: {id}");
            var existingGame = await dbContext.Games.FindAsync(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingGame)
            .CurrentValues
            .SetValues(updateGame.ToEntity(id));
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, CatalogContext dbContext) =>
        {
            Console.WriteLine($"Deleting a Game at {id}");
            await dbContext.Games
            .Where(g => g.ID == id)
            .ExecuteDeleteAsync();
            return Results.NoContent();
        });

        return group;
    }
}
