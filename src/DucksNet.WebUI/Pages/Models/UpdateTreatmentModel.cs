using System.ComponentModel.DataAnnotations;

namespace DucksNet.WebUI.Pages.Models;

public class UpdateTreatmentModel
{
    [Required]
    [StringLength(36, MinimumLength = 36, ErrorMessage = "Guid must be 36 characters long.")]
    public string? TreatmentId { get; set; }
    [Required]
    public string? OwnerId { get; set; }
    [Required]
    public string? ClientId { get; set; }
    [Required]
    public string? ClinicId { get; set; }
}
