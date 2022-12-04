using DucksNet.Domain.Model;
using DucksNet.WebUI.Pages.Models;
using System.Net.Http.Json;
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
        var users = await httpClient.GetFromJsonAsync<IEnumerable<User>>(ApiURL);
        return users!;
    }

    public Task<User> GetUserDetail(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task CreateUser(CreateUserModel userCreateModel)
    {
        var result = await httpClient.PostAsJsonAsync(ApiURL, userCreateModel);
        var user = await result.Content.ReadFromJsonAsync<User>();
    }

    public async Task UpdateUser(string userId, UpdateUserModel updateUserModel)
    {
        await httpClient.PutAsJsonAsync($"{ApiURL}/{userId}", updateUserModel);
    }

    public async Task DeleteUser(string userId)
    {
        await httpClient.DeleteAsync($"{ApiURL}/{userId}");
    }
}
