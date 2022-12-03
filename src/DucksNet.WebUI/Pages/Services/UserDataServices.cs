using DucksNet.Domain.Model;
using System.Text.Json;

namespace DucksNet.WebUI.Pages.Services;

public class UserDataService : IUserDataService
{
    private const string ApiURL = "https://localhost:7115/api/v1/users";
    private readonly HttpClient httpClient;

    public UserDataService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    public async Task<IEnumerable<User>> GetAllUsers()
    {
#pragma warning disable CS8603 // Possible null reference return.

        return await JsonSerializer.DeserializeAsync<IEnumerable<User>>
            (await httpClient.GetStreamAsync(ApiURL), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true, });
#pragma warning restore CS8603 // Possible null reference return.
    }

    public async Task<User> GetUserDetail(Guid userId)
    {
        return null;
    }
}
