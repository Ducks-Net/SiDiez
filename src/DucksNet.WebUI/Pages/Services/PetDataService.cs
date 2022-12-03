using DucksNet.Domain.Model;
using System.Text.Json;

namespace DucksNet.WebUI.Pages.Services;

public class PetDataService : IPetDataService
{
    private const string ApiURL = "https://localhost:7115/api/v1/medicalrecords";
    private readonly HttpClient httpClient;

    public PetDataService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    public async Task<IEnumerable<Pet>> GetAllPets()
    {
#pragma warning disable CS8603 // Possible null reference return.

        return await JsonSerializer.DeserializeAsync<IEnumerable<Pet>>
            (await httpClient.GetStreamAsync(ApiURL), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true, });
#pragma warning restore CS8603 // Possible null reference return.
    }

    public async Task<Pet> GetPetDetail(Guid petId)
    {
        return null;
    }
}
