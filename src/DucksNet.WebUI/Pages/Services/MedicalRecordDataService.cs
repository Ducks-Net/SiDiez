using DucksNet.Domain.Model;
using DucksNet.WebUI.Pages.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace DucksNet.WebUI.Pages.Services;

public class MedicalRecordDataService : IMedicalRecordDataService
{
    private const string ApiURL = "https://localhost:7115/api/v1/MedicalRecords";
    private readonly HttpClient httpClient;

    public MedicalRecordDataService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    public async Task<IEnumerable<MedicalRecord>> GetAllMedicalRecords()
    {
#pragma warning disable CS8603 // Possible null reference return.

        return await JsonSerializer.DeserializeAsync<IEnumerable<MedicalRecord>>
            (await httpClient.GetStreamAsync(ApiURL), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true, });
#pragma warning restore CS8603 // Possible null reference return.
    }

    public async Task CreateMedicalRecord(CreateMedicalRecord createMedicalRecord)
    {
        var result = await httpClient.PostAsJsonAsync(ApiURL, createMedicalRecord);
        var medicalRecord = await result.Content.ReadFromJsonAsync<MedicalRecord>();
    }

    public async Task DeleteMedicalRecord(string medicalRecordId)
    {
        await httpClient.DeleteAsync($"{ApiURL}/{medicalRecordId}");
    }

    public async Task UpdateEmployee(string medicalRecordId, UpdateMedicalRecord updateMedicalRecordModel)
    {
        await httpClient.PutAsJsonAsync($"{ApiURL}/{medicalRecordId}", updateMedicalRecordModel);
    }
}
