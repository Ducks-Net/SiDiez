using DucksNet.Domain.Model;
using DucksNet.WebUI.Pages.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace DucksNet.WebUI.Pages.Services;

public class PetDataService : IPetDataService
{
    private const string ApiURL = "https://localhost:7115/api/v1/pets";
    private readonly HttpClient httpClient;

    public PetDataService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<IEnumerable<Pet>> GetAllPets()
    {
        var pets = await httpClient.GetFromJsonAsync<IEnumerable<Pet>>(ApiURL);
        return pets!;
    }
    public async Task CreatePet(CreatePetModel petCreateModel)
    {
        var result = await httpClient.PostAsJsonAsync(ApiURL, petCreateModel);
        var pet = await result.Content.ReadFromJsonAsync<Pet>();
    }

    public async Task UpdatePet(string petId, UpdatePetModel updatePetModel)
    {
        await httpClient.PutAsJsonAsync($"{ApiURL}/{petId}", updatePetModel);
    }

    public async Task DeletePet(string petId)
    {
        await httpClient.DeleteAsync($"{ApiURL}/{petId}");
    }
}
