using System.ComponentModel.DataAnnotations;

namespace DucksNet.WebUI.Pages.Models;

public class CreateMedicineModel
{
    [Required]
    public string IdMedicine { get; set; }
    [Required]
    [StringLength(120, MinimumLength = 3, ErrorMessage = "Name must be at least 3 characters long.")]
    public string? Name { get; set; }

    [Required]
    [StringLength(120, MinimumLength = 3, ErrorMessage = "Description must be at least 5 characters long.")]
    public string? Description { get; set; }

    [Required]
    public double? Price { get; set; }

    [Required]
    [StringLength(120, MinimumLength = 4, ErrorMessage = "Drug Administration must be at least 4 characters long.")]
    public string? DrugAdministration { get; set; }
}
