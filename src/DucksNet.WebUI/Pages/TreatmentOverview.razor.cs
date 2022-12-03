using DucksNet.Domain.Model;
using DucksNet.WebUI.Pages.Services;
using Microsoft.AspNetCore.Components;

namespace DucksNet.WebUI.Pages;

public partial class TreatmentOverview : ComponentBase
{
    [Inject]
    public ITreatmentDataService TreatmentDataService { get; set; }
    public List<Treatment> Treatments { get; set; } = default!;
    protected async override Task OnInitializedAsync()
    {
        Treatments = (await TreatmentDataService.GetAllTreatments()).ToList();
    }
}
