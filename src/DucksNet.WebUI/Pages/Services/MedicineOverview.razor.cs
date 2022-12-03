using DucksNet.Domain.Model;
using DucksNet.WebUI.Pages.Services;
using Microsoft.AspNetCore.Components;

namespace DucksNet.WebUI.Pages;

public partial class MedicineOverview : ComponentBase
{
    [Inject]
    public IMedicineDataService MedicineDataService { get; set; }
    public List<Medicine> Medicines { get; set; } = default!;
    protected async override Task OnInitializedAsync()
    {
        Medicines = (await MedicineDataService.GetAllMedicine()).ToList();
    }
}
