using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Catalog.Frontend.Converters;

namespace Catalog.Frontend.Models;

public class GameClientDetails
{
    public int ID { get; set; }

    [Required]
    [StringLength(100)]
    public required string Name { get; set; }

    [Required(ErrorMessage = "The Genre Field is required")]
    [JsonConverter(typeof(StringConverter))]
    public string? GenreID { get; set; }

    [Range(1, 100)]
    public decimal Price { get; set; }

    public DateOnly ReleaseDate { get; set; }
}
