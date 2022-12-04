using System.ComponentModel.DataAnnotations;

namespace DucksNet.WebUI.Pages.Models;

public class DeleteMedicalRecord
{
    [Required]
    [StringLength(36, MinimumLength = 36, ErrorMessage = "Guid must be 36 characters long.")]
    public string? EmployeeId { get; set; }
}
