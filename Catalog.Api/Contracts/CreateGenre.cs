using System.ComponentModel.DataAnnotations;

namespace Catalog.Api.Contracts;

public record class CreateGenre(
    [Required][StringLength(50)] string Name
);