using DucksNet.Domain.Model;
using DucksNet.WebUI.Pages.Models;
using DucksNet.WebUI.Pages.Services;
using Microsoft.AspNetCore.Components;

namespace DucksNet.WebUI.Pages;
public partial class TreatmentOverview : ComponentBase
{
    [Inject]
    public ITreatmentDataService? TreatmentDataService { get; set; }
    public List<Treatment> Treatments { get; set; } = default!;
    protected async Task CreateTreatment(CreateTreatmentModel createTreatmentModel)
    {
        await TreatmentDataService!.CreateTreatment(createTreatmentModel);
    }
    protected async Task UpdateTreatment(string treatmentId, UpdateTreatmentModel updateTreatmentModel)
    {
        await TreatmentDataService!.UpdateTreatment(treatmentId, updateTreatmentModel);
    }
    protected async Task ReloadAllTreatment()
    {
        Treatments = (await TreatmentDataService!.GetAllTreatment()).ToList();
        if (Treatments.Count == 0)
        {
            Treatments = default!;
        }
    }
    protected async Task DeleteTreatment(string treatmentId)
    {
        await TreatmentDataService!.DeleteTreatment(treatmentId);
    }
}
