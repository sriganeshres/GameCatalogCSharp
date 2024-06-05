using Catalog.Contracts;
using Catalog.Entities;

namespace Catalog.Mapping;

public static class GenreMapping
{
    public static GenreDTO ToDTO(this Genre genre)
    {
        return new GenreDTO(
            genre.ID,
            genre.Name
        );
    }

    public static Genre ToEntity(this CreateGenre newGenre)
    {
        return new Genre()
        {
            Name = newGenre.Name
        };
    }

    public static Genre ToEntity(this UpdateGenreDTO newGenre, int id)
    {
        return new Genre()
        {
            ID = id,
            Name = newGenre.Name
        };
    }
}
