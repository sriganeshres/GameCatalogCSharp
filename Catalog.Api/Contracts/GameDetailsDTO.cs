namespace Catalog.Api.Contracts;

public record class GameDetailsDTO(
    int ID,
    string Name,
    int GenreID,
    decimal Price,
    DateOnly ReleaseDate
);

