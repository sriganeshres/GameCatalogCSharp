using System.ComponentModel.DataAnnotations;

namespace Catalog.Api.Contracts;

public record class UpdateGameDTO(
    [Required][StringLength(50)] string Name,
    int GenreID,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate
    );

