using System.ComponentModel.DataAnnotations;

namespace DucksNet.WebUI.Pages.Models;

public class UpdateMedicineModel
{
    [Required]
    [StringLength(36, MinimumLength = 36, ErrorMessage = "Guid must be 36 characters long.")]
    public string? MedicineId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double? Price { get; set; }
    public string? DrugAdministration { get; set; }
}
