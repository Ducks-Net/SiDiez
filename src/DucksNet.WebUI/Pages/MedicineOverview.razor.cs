using DucksNet.Domain.Model;
using DucksNet.WebUI.Pages.Models;
using DucksNet.WebUI.Pages.Services;
using Microsoft.AspNetCore.Components;

namespace DucksNet.WebUI.Pages;
public partial class MedicineOverview : ComponentBase
{
    [Inject]
    public IMedicineDataService? MedicineDataService { get; set; }
    public List<Medicine> Medicines { get; set; } = default!;
    protected async Task CreateMedicine(CreateMedicineModel createMedicineModel) { 
        await MedicineDataService!.CreateMedicine(createMedicineModel);
    }
    protected async Task UpdateMedicine(string medicineId, UpdateMedicineModel updateMedicineModel)
    {
        await MedicineDataService!.UpdateMedicine(medicineId, updateMedicineModel);
    }
    protected async Task ReloadAllMedicine()
    {
        Medicines = (await MedicineDataService!.GetAllMedicine()).ToList();
        if (Medicines.Count == 0)
        {
            Medicines = default!;
        }
    }
    protected async Task DeleteMedicine(string medicineId)
    {
        await MedicineDataService!.DeleteMedicine(medicineId);
    }
}
