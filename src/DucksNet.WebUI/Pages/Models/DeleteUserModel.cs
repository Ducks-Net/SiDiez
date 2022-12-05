using System.ComponentModel.DataAnnotations;

namespace DucksNet.WebUI.Pages.Models;

public class DeleteUserModel
{
    [Required]
    [StringLength(36, MinimumLength = 36, ErrorMessage = "User id must be 36 characters long.")]
    public string? UserId { get; set; }
}
