using DucksNet.Domain.Model;
using DucksNet.WebUI.Pages.Services;
using Microsoft.AspNetCore.Components;

namespace DucksNet.WebUI.Pages;
public partial class UsersOverview : ComponentBase
{
    [Inject]
    public IUserDataService? UserDataService { get; set; }
    public List<User> Users { get; set; } = default!;
    protected async override Task OnInitializedAsync()
    {
        Users = (await UserDataService.GetAllUsers()).ToList();
    }
}

