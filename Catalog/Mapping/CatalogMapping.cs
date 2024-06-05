using Catalog.Contracts;
using Catalog.Entities;

namespace Catalog.Mapping;

public static class CatalogMapping
{
    public static Game ToEntity(this CreateGame newGame) {
        return new Game()
        {
            Name = newGame.Name,
            // Genre = dbContext.Genres.Find(newGame.GenreID),
            GenreID = newGame.GenreID,
            Price = newGame.Price,
            ReleaseDate = newGame.ReleaseDate,
        };
    }

    public static GameDTO ToDTO(this Game game) { 
        return new GameDTO(
            game.ID,
            game.Name,
            game.Genre!.Name,
            game.Price,
            game.ReleaseDate
        );
    }

    public static GameDetailsDTO ToDetailsDTO(this Game game) {
        return new GameDetailsDTO(
            game.ID,
            game.Name,
            game.GenreID,
            game.Price,
            game.ReleaseDate
        );
    }

    public static Game ToEntity(this UpdateGameDTO newGame, int id)
    {
        return new Game()
        {
            ID = id,
            Name = newGame.Name,
            GenreID = newGame.GenreID,
            Price = newGame.Price,
            ReleaseDate = newGame.ReleaseDate,
        };
    }
}
