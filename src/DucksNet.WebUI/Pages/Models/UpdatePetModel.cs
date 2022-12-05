using System.ComponentModel.DataAnnotations;

namespace DucksNet.WebUI.Pages.Models;

public class UpdatePetModel
{
    [Required]
    [StringLength(36, MinimumLength = 36, ErrorMessage = "Pet id must be 36 characters long.")]
    public string? PetId { get; set; }
    public string? Name { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Species { get; set; }
    public string? Breed { get; set; }

    [StringLength(36, MinimumLength = 36, ErrorMessage = "Owner id must be 36 characters long.")]
    public string? OwnerId { get; set; }
    public string? Size { get; set; }
}
