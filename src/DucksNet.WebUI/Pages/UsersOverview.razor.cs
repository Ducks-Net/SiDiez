using DucksNet.Domain.Model;
using DucksNet.WebUI.Pages.Models;
using DucksNet.WebUI.Pages.Services;
using Microsoft.AspNetCore.Components;

namespace DucksNet.WebUI.Pages;
public partial class UsersOverview : ComponentBase
{
    [Inject]
    public IUserDataService? UserDataService { get; set; }
    public List<User> Users { get; set; } = default!;

    protected async Task ReloadAllUsers()
    {
        Users = (await UserDataService!.GetAllUsers()).ToList();
        if (Users.Count == 0)
        {
            Users = default!;
        }
    }

    protected async Task CreateUser(CreateUserModel userCreateModel)
    {
        await UserDataService!.CreateUser(userCreateModel);
    }

    protected async Task UpdateUser(string userId, UpdateUserModel updateuserModel)
    {
        await UserDataService!.UpdateUser(userId, updateuserModel);
    }

    protected async Task DeleteUser(string userId)
    {
        await UserDataService!.DeleteUser(userId);
    }
}
