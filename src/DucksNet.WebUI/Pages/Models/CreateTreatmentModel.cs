using System.ComponentModel.DataAnnotations;

namespace DucksNet.WebUI.Pages.Models;

public class CreateTreatmentModel
{
    [Required]
    [StringLength(36, MinimumLength = 36, ErrorMessage = "Guid must be 36 characters long.")]
    public string? OwnerId { get; set; }
    [Required]
    [StringLength(36, MinimumLength = 36, ErrorMessage = "Guid must be 36 characters long.")]
    public string? ClientId { get; set; }
    [Required]
    [StringLength(36, MinimumLength = 36, ErrorMessage = "Guid must be 36 characters long.")]
    public string? ClinicId { get; set; }
}
