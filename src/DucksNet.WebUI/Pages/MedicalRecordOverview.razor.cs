using DucksNet.Domain.Model;
using DucksNet.WebUI.Pages.Models;
using DucksNet.WebUI.Pages.Services;
using Microsoft.AspNetCore.Components;

namespace DucksNet.WebUI.Pages;

public partial class MedicalRecordOverview : ComponentBase
{
    [Inject]
    public IMedicalRecordDataService? MedicalRecordDataService { get; set; }
    public List<MedicalRecord> MedicalRecords { get; set; } = default!;
    protected async Task CreateMedicalRecord(CreateMedicalRecord createMedicalRecord)
    {
        await MedicalRecordDataService!.CreateMedicalRecord(createMedicalRecord);
    }
    protected async Task UpdateMedicalRecord(string medicalRecordId, UpdateMedicalRecord updateMedicalRecord)
    {
        await MedicalRecordDataService!.UpdateEmployee(medicalRecordId, updateMedicalRecord);
    }
    protected async Task ReloadAllMedicalRecords()
    {
        MedicalRecords = (await MedicalRecordDataService!.GetAllMedicalRecords()).ToList();
        if (MedicalRecords.Count == 0)
        {
            MedicalRecords = default!;
        }
    }
    protected async Task DeleteMedicalRecord(string medicalRecordId)
    {
        await MedicalRecordDataService!.DeleteMedicalRecord(medicalRecordId);
    }
}
