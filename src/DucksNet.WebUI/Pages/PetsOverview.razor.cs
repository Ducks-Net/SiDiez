using DucksNet.Domain.Model;
using DucksNet.SharedKernel.Utils;
using DucksNet.WebUI.Pages.Models;
using DucksNet.WebUI.Pages.Services;
using Microsoft.AspNetCore.Components;

namespace DucksNet.WebUI.Pages;

public partial class PetsOverview : ComponentBase
{
    [Inject]
    public IPetDataService? PetDataService { get; set; }
    [Inject]
    public IUserDataService? UserDataService { get; set; }
    public List<Pet> Pets { get; set; } = default!;
    public IEnumerable<User> Owners { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await ReloadAllPets();
        Owners = (await GetUsers())!;
    }

    protected async Task ReloadAllPets()
    {
        Pets = (await PetDataService!.GetAllPets()).Value!.ToList();
        if (Pets.Count == 0)
        {
            Pets = default!;
        }
    }

    protected Task<Result> CreatePet(CreatePetModel petCreateModel)
    {
        return PetDataService!.CreatePet(petCreateModel);
    }

    protected async Task<Result> UpdatePet(string petId, UpdatePetModel updatePetModel)
    {
        return await PetDataService!.UpdatePet(petId, updatePetModel);
    }

    protected async Task<Result> DeletePet(string petId)
    {
        return await PetDataService!.DeletePet(petId);
    }

    protected async Task<IEnumerable<User>> GetUsers()
    {
        return (await UserDataService!.GetAllUsers())!;
    }
}
