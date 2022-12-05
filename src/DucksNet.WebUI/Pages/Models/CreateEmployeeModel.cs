using System.ComponentModel.DataAnnotations;

namespace DucksNet.WebUI.Pages.Models;

public class CreateEmployeeModel
{
    [Required]
    [StringLength(36, MinimumLength = 36, ErrorMessage = "Guid must be 36 characters long.")]
    public string? IdOffice { get; set; }
    [Required]
    [StringLength(120, MinimumLength = 2, ErrorMessage = "Surname must be at least 5 characters long.")]
    public string? Surname { get; set; }
    [Required]
    [StringLength(120, MinimumLength = 2, ErrorMessage = "First Name must be at least 5 characters long.")]
    public string? FirstName { get; set; }
    [Required]
    [StringLength(120, MinimumLength = 2, ErrorMessage = "Addres must be at least 5 characters long.")]
    public string? Address { get; set; }
    [Required]
    public string? OwnerPhone { get; set; }
    [Required]
    public string? OwnerEmail { get; set; }
}
