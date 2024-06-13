using System.ComponentModel.DataAnnotations;

namespace Catalog.Api.Contracts;

public record class UpdateGenreDTO
(
    [Required][StringLength(50)] string Name
);