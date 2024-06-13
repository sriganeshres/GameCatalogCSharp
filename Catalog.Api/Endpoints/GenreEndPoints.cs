using Catalog.Api.Contracts;
using Catalog.Api.Data;
using Catalog.Api.Entities;
using Catalog.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Endpoints;

public static class GenreEndpoints
{
    const string GETGENREENDPOINT = "genre";

    public static RouteGroupBuilder GetGenresEndpoints(this WebApplication app)
    {
        var group = app.MapGroup(GETGENREENDPOINT);

        group.MapGet("/hello", () =>
        {
            Console.WriteLine("Hello, world!");
            return "Hello World!";
        });

        group.MapGet("/", async (CatalogContext dbContext) =>
        {
            Console.WriteLine("Returning All Genres");
            return await dbContext.Genres
            .Select(g => g.ToDTO())
            .AsNoTracking()
            .ToListAsync();
        });

        group.MapGet("/{id}", async (CatalogContext dbContext, int id) =>
        {
            Console.WriteLine("Returning Genre");
            Genre? genre = await dbContext.Genres.FindAsync(id);
            return genre is null ? Results.NotFound() : Results.Ok(genre);
        }).WithName(GETGENREENDPOINT);

        group.MapPost("/", async (CreateGenre newGenre, CatalogContext dbContext) =>
        {
            Console.WriteLine("Creating Genre with name: {newGenre.Name}");
            Genre genre = newGenre.ToEntity();
            dbContext.Genres.Add(genre);
            await dbContext.SaveChangesAsync();

            GenreDTO genreDTO = genre.ToDTO();
            return Results.CreatedAtRoute(GETGENREENDPOINT, new { id = genre.ID }, genreDTO);
        });

        group.MapPut("/{id}", async (int id, UpdateGenreDTO updateGenre, CatalogContext dbContext) =>
        {
            Console.WriteLine("Updating Genre with ID: {ID}");
            var existingGenre = await dbContext.Genres.FindAsync(id);
            if (existingGenre is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingGenre)
            .CurrentValues
            .SetValues(updateGenre.ToEntity(id));
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, CatalogContext dbContext) =>
        {
            Console.WriteLine("Deleting Genre with ID: {ID}");
            await dbContext.Genres
            .Where(g => g.ID == id)
            .ExecuteDeleteAsync();

            return Results.NoContent();
        });

        return group;
    }
}