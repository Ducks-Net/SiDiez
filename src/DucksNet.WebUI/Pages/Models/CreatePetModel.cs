using System.ComponentModel.DataAnnotations;

namespace DucksNet.WebUI.Pages.Models;

public class CreatePetModel
{
    [Required]
    [StringLength(120, MinimumLength = 2, ErrorMessage = "Name must be at least 2 characters long.")]
    public string? Name { get; set; }

    [Required]
    public DateTime? DateOfBirth { get; set; }

    [Required]
    [StringLength(120, MinimumLength = 2, ErrorMessage = "Species must be at least 2 characters long.")]
    public string? Species { get; set; }

    [Required]
    [StringLength(120, MinimumLength = 2, ErrorMessage = "Breed must be at least 2 characters long.")]
    public string? Breed { get; set; }

    [Required]
    [StringLength(36, MinimumLength = 36, ErrorMessage = "Owner id must be at least 36 characters long.")]
    public string? OwnerId { get; set; }

    [Required]
    public string? Size { get; set; }
}
