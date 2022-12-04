using System.ComponentModel.DataAnnotations;

namespace DucksNet.WebUI.Pages.Models;

public class CageCreateModel
{
    [Required]
    public string? SizeString { get; set; }
    [Required]
    [StringLength(36, MinimumLength = 36, ErrorMessage = "LocationGuid must be 36 characters long.")]
    public string? LocationId { get; set; }
}
