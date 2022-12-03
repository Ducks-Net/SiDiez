using System.Text.Json;
using DucksNet.Domain.Model;

namespace DucksNet.WebUI.Pages.Services;

public class MedicineDataService : IMedicineDataService
{
    private const string ApiURL = "https://localhost:7115/api/v1/medicine";
    private readonly HttpClient httpClient;

    public MedicineDataService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    public async Task<IEnumerable<Medicine>> GetAllMedicine()
    {
#pragma warning disable CS8603 // Possible null reference return.

        return await JsonSerializer.DeserializeAsync<IEnumerable<Medicine>>
            (await httpClient.GetStreamAsync(ApiURL), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true, });
#pragma warning restore CS8603 // Possible null reference return.
    }

    public async Task<Medicine> GetMedicineDetail(Guid medicineId)
    {
        //throw new NotImplementedException();
        return null;
    }
}
