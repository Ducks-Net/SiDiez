using System.Net.Http.Json;
using DucksNet.Domain.Model;
using DucksNet.WebUI.Models;

namespace DucksNet.WebUI.Pages.Services;

public class CageDataService : ICageDataService
{
    private const string ApiURL = "https://localhost:7115/api/v1/cages";
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
        var cage = await result.Content.ReadFromJsonAsync<Cage>();
    }

    public async Task DeleteCage(string cageId)
    {
        await httpClient.DeleteAsync($"{ApiURL}/{cageId}");
    }
}
