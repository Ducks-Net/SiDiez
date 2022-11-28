using System.Net.Http.Json;
using DucksNet.Domain.Model;

namespace DucksNet.WebUI.Pages.Services;

public class CageDataService : ICageDataService
{
    private const string ApiURL = "https://localhost:7115/api/v1/cages";
    private readonly HttpClient httpClient;

    public CageDataService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public Task<IEnumerable<Cage>> GetAllCages()
    {
        return httpClient.GetAsync(ApiURL).Result.Content.ReadFromJsonAsync<IEnumerable<Cage>>()!;
    }

    public Task<Cage> GetCageDetail(Guid cageId)
    {
        throw new NotImplementedException();
    }
}
