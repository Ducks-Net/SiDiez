using System.ComponentModel.DataAnnotations;

namespace DucksNet.WebUI.Pages.Models;

public class DeleteCreateModel
{
    [Required]
    [StringLength(36, MinimumLength = 36, ErrorMessage = "Guid must be 36 characters long.")]
    public string? CageId { get; set; }
}
