using System.ComponentModel.DataAnnotations;

namespace DucksNet.WebUI.Pages.Models;

public class UpdateUserModel
{
    [Required]
    [StringLength(36, MinimumLength = 36, ErrorMessage = "User id must be 36 characters long.")]
    public string? UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}
