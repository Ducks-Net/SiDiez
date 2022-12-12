using System.Net.Http.Json;
using System.Text.Json;
using DucksNet.Domain.Model;
using DucksNet.WebUI.Pages.Models;

namespace DucksNet.WebUI.Pages.Services;

public class TreatmentDataService : ITreatmentDataService
{
#pragma warning disable S1075 // URIs should not be hardcoded  
    private const string ApiURL = "https://localhost:7115/api/v1/treatment";
    private readonly HttpClient httpClient;
#pragma warning disable S1075 // URIs should not be hardcoded  

    public TreatmentDataService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    public async Task CreateTreatment(CreateTreatmentModel createTreatmentModel)
    {
        var result = await httpClient.PostAsJsonAsync(ApiURL, createTreatmentModel);
        await result.Content.ReadFromJsonAsync<Treatment>();
    }
    public async Task<IEnumerable<Treatment>> GetAllTreatment()
    {
#pragma warning disable CS8603 // Possible null reference return.

        return await JsonSerializer.DeserializeAsync<IEnumerable<Treatment>>
            (await httpClient.GetStreamAsync(ApiURL), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true, });
#pragma warning restore CS8603 // Possible null reference return.
    }

    public async Task DeleteTreatment(string treatmentId)
    {
        await httpClient.DeleteAsync($"{ApiURL}/{treatmentId}");
    }

    public async Task UpdateTreatment(string treatmentId, UpdateTreatmentModel updateTreatmentModel)
    {
        await httpClient.PutAsJsonAsync($"{ApiURL}/{treatmentId}", updateTreatmentModel);
    }
}
