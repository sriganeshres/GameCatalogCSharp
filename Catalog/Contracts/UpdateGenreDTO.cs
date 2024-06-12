using System.ComponentModel.DataAnnotations;

namespace Catalog;

public record class UpdateGenreDTO
(
    [Required][StringLength(50)] string Name
);
