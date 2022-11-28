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
        var cages = httpClient.GetFromJsonAsync<IEnumerable<Cage>>(ApiURL);
        return cages;
    }

    public Task<Cage> GetCageDetail(Guid cageId)
    {
        throw new NotImplementedException();
    }
}
