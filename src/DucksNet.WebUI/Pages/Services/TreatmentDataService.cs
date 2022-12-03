using System.Text.Json;
using DucksNet.Domain.Model;

namespace DucksNet.WebUI.Pages.Services;

public class TreatmentDataService : ITreatmentDataService
{
    private const string ApiURL = "https://localhost:7115/api/v1/treatment";
    private readonly HttpClient httpClient;

    public TreatmentDataService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    public async Task<IEnumerable<Treatment>> GetAllTreatments()
    {
#pragma warning disable CS8603 // Possible null reference return.

        return await JsonSerializer.DeserializeAsync<IEnumerable<Treatment>>
            (await httpClient.GetStreamAsync(ApiURL), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true, });
#pragma warning restore CS8603 // Possible null reference return.
    }

    public async Task<Treatment> GetTreatmentDetail(Guid treatmentId)
    {
        //throw new NotImplementedException();
        return null;
    }
}
