using System.ComponentModel.DataAnnotations;

namespace Catalog;

public record class CreateGenre(
    [Required][StringLength(50)] string Name
);
