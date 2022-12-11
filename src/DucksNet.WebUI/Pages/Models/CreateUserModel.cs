using System.ComponentModel.DataAnnotations;

namespace DucksNet.WebUI.Pages.Models;

public class CreateUserModel
{
    [Required]
    [StringLength(120, MinimumLength = 2, ErrorMessage = "First Name must be at least 2 characters long.")]
    public string? FirstName { get; set; }

    [Required]
    [StringLength(120, MinimumLength = 2, ErrorMessage = "Last Name must be at least 2 characters long.")]
    public string? LastName { get; set; }

    [Required]
    [StringLength(120, MinimumLength = 8, ErrorMessage = "Address must be at least 8 characters long.")]
    public string? Address { get; set; }

    [Required]
    [StringLength(120, MinimumLength = 2, ErrorMessage = "Phone number must be at least 2 characters long.")]
    public string? PhoneNumber { get; set; }

    [Required]
    [StringLength(120, MinimumLength = 5, ErrorMessage = "Email must be at least 5 characters long.")]
    public string? Email { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Password must be at least 5 characters long.")]
    public string? Password { get; set; }

    [Required]
    [Compare(nameof(Password), ErrorMessage = "Both passwords must be the same")]
    public string? ConfirmedPassword { get; set; }
}
