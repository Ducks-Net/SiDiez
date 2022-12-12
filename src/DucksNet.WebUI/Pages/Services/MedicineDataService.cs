using System.Net.Http.Json;
using System.Text.Json;
using DucksNet.Domain.Model;
using DucksNet.WebUI.Pages.Models;

namespace DucksNet.WebUI.Pages.Services;

public class MedicineDataService : IMedicineDataService
{
#pragma warning disable S1075 // URIs should not be hardcoded  
    private const string ApiURL = "https://localhost:7115/api/v1/medicine";
#pragma warning disable S1075 // URIs should not be hardcoded  
    private readonly HttpClient httpClient;

    public MedicineDataService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    public async Task CreateMedicine(CreateMedicineModel createMedicineModel)
    {
        var result = await httpClient.PostAsJsonAsync(ApiURL, createMedicineModel);
        await result.Content.ReadFromJsonAsync<Medicine>();
    }
    public async Task<IEnumerable<Medicine>> GetAllMedicine()
    {
#pragma warning disable CS8603 // Possible null reference return.

        return await JsonSerializer.DeserializeAsync<IEnumerable<Medicine>>
            (await httpClient.GetStreamAsync(ApiURL), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true, });
#pragma warning restore CS8603 // Possible null reference return.
    }

    public async Task DeleteMedicine(string medicineId)
    {
        await httpClient.DeleteAsync($"{ApiURL}/{medicineId}");
    }

    public async Task UpdateMedicine(string medicineId, UpdateMedicineModel updateMedicineModel)
    {
        await httpClient.PutAsJsonAsync($"{ApiURL}/{medicineId}", updateMedicineModel);
    }
}
