using System.ComponentModel.DataAnnotations;

namespace DucksNet.WebUI.Pages.Models;

public class UpdateMedicalRecord
{
    [Required]
    [StringLength(36, MinimumLength = 36, ErrorMessage = "Guid must be 36 characters long.")]
    public string? MedicalRecordId { get; set; }

    [StringLength(36, MinimumLength = 36, ErrorMessage = "Id Appointment must be 36 characters long.")]
    public string? IdAppintment { get; set; }
    [StringLength(36, MinimumLength = 36, ErrorMessage = "Id Client must be 36 characters long.")]
    public string? IdClient { get; set; }
}
