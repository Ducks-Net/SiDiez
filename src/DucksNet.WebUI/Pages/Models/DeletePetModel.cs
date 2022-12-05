using System.ComponentModel.DataAnnotations;

namespace DucksNet.WebUI.Pages.Models;

public class DeletePetModel
{
    [Required]
    [StringLength(36, MinimumLength = 36, ErrorMessage = "Pet id must be 36 characters long.")]
    public string? PetId { get; set; }
}
