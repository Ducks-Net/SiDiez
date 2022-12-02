using DucksNet.Domain.Model;
using System.Text.Json;

namespace DucksNet.WebUI.Pages.Services;

public class MedicalRecordDataService : IMedicalRecordDataService
{
    private const string ApiURL = "https://localhost:7115/api/v1/medicalrecords";
    private readonly HttpClient httpClient;

    public MedicalRecordDataService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    public async Task<IEnumerable<MedicalRecord>> GetAllPersons()
    {
#pragma warning disable CS8603 // Possible null reference return.

        return await JsonSerializer.DeserializeAsync<IEnumerable<MedicalRecord>>
            (await httpClient.GetStreamAsync(ApiURL), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true, });
#pragma warning restore CS8603 // Possible null reference return.
    }

    public async Task<MedicalRecord> GetPersonDetail(Guid personId)
    {
        //throw new NotImplementedException();
        return null;
    }
}
