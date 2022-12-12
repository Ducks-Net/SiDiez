using System.Net.Http.Json;
using DucksNet.Domain.Model;
using DucksNet.WebUI.Pages.Models;

namespace DucksNet.WebUI.Pages.Services;

public class CageDataService : ICageDataService
{
#pragma warning disable S1075 // URIs should not be hardcoded  
    private const string ApiURL = "https://localhost:7115/api/v1/cages";
#pragma warning disable S1075 // URIs should not be hardcoded  
    private readonly HttpClient httpClient;

    public CageDataService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<IEnumerable<Cage>> GetAllCages()
    {
        var cages = await httpClient.GetFromJsonAsync<IEnumerable<Cage>>(ApiURL);
        return cages!;
    }

    public Task<Cage> GetCageDetail(Guid cageId)
    {
        throw new NotImplementedException();
    }

    public async Task CreateCage(CageCreateModel cageCreateModel)
    {
        var result = await httpClient.PostAsJsonAsync(ApiURL, cageCreateModel);
        await result.Content.ReadFromJsonAsync<Cage>();
    }

    public async Task DeleteCage(string cageId)
    {
        await httpClient.DeleteAsync($"{ApiURL}/{cageId}");
    }
}
