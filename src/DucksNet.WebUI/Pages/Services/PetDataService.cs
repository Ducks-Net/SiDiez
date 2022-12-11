using DucksNet.Domain.Model;
using DucksNet.SharedKernel.Utils;
using DucksNet.WebUI.Pages.Models;
using System.Diagnostics.Tracing;
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

    public async Task<Result<IEnumerable<Pet>>> GetAllPets()
    {
        var petsResult = await httpClient.GetAsync(ApiURL);
        if(petsResult.IsSuccessStatusCode) {
            var pets = await petsResult.Content.ReadFromJsonAsync<IEnumerable<Pet>>();
            return Result<IEnumerable<Pet>>.Ok(pets!);
        } else {
            var errors = await petsResult.Content.ReadFromJsonAsync<IEnumerable<string>>();
            return Result<IEnumerable<Pet>>.ErrorList(errors!.ToList());
        }
    }
    public async Task<Result> CreatePet(CreatePetModel petCreateModel)
    {
        var result = await httpClient.PostAsJsonAsync(ApiURL, petCreateModel);
        if (result.IsSuccessStatusCode)
        {
            return Result.Ok();
        }
        else
        {
            var errors = await result.Content.ReadFromJsonAsync<IEnumerable<string>>();
            return Result.ErrorList(errors!.ToList());
        }
    }

    public async Task<Result> UpdatePet(string petId, UpdatePetModel updatePetModel)
    {
        Console.WriteLine($"PDS UpdatePet: {petId} | {updatePetModel.Name} | {updatePetModel.Breed} | {updatePetModel.DateOfBirth}");
        var result = await httpClient.PutAsJsonAsync($"{ApiURL}/{petId}", updatePetModel);
        if (result.IsSuccessStatusCode)
        {
            return Result.Ok();
        }
        else
        {
            var errors = await result.Content.ReadAsStringAsync();
            return Result.Error(errors);
        }
    }

    public async Task<Result> DeletePet(string petId)
    {
        var result = await httpClient.DeleteAsync($"{ApiURL}/{petId}");
        if (result.IsSuccessStatusCode)
        {
            return Result.Ok();
        }
        else
        {
            var errors = await result.Content.ReadFromJsonAsync<IEnumerable<string>>();
            return Result.ErrorList(errors!.ToList());
        }
    }
}
