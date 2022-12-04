using DucksNet.Domain.Model;
using DucksNet.WebUI.Pages.Services;
using Microsoft.AspNetCore.Components;

namespace DucksNet.WebUI.Pages;

public partial class PetsOverview : ComponentBase
{
    [Inject]
    public IPetDataService? PetDataService { get; set; }
    public List<Pet> Pets { get; set; } = default!;
    protected async override Task OnInitializedAsync()
    {
        Pets = (await PetDataService!.GetAllPets()).ToList();
    }
}
