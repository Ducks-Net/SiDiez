using System.ComponentModel.DataAnnotations;

namespace DucksNet.WebUI.Pages.Models;

public class UpdateEmployeeModel
{
    [Required]
    [StringLength(36, MinimumLength = 36, ErrorMessage = "Guid must be 36 characters long.")]
    public string? EmployeeId { get; set; }
    public string? Surname { get; set; }
    public string? FirstName { get; set; }
    public string? Address { get; set; }
    public string? OwnerPhone { get; set; }
    public string? OwnerEmail { get; set; }
}
