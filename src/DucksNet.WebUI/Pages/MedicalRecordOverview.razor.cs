using DucksNet.Domain.Model;
using DucksNet.WebUI.Pages.Services;
using Microsoft.AspNetCore.Components;

namespace DucksNet.WebUI.Pages;

public partial class MedicalRecordOverview : ComponentBase
{
    [Inject]
    public IMedicalRecordDataService MedicalRecordDataService { get; set; }
    public List<MedicalRecord> MedicalRecords { get; set; } = default!;
    protected async override Task OnInitializedAsync()
    {
        MedicalRecords = (await MedicalRecordDataService.GetAllPersons()).ToList();
    }
}
