using DucksNet.Domain.Model;
using DucksNet.WebUI.Pages.Models;
using DucksNet.WebUI.Pages.Services;
using Microsoft.AspNetCore.Components;

namespace DucksNet.WebUI.Pages;

public partial class PetsOverview : ComponentBase
{
    [Inject]
    public IPetDataService? PetDataService { get; set; }
    public List<Pet> Pets { get; set; } = default!;

    protected async Task ReloadAllPets()
    {
        Pets = (await PetDataService!.GetAllPets()).ToList();
        if (Pets.Count == 0)
        {
            Pets = default!;
        }
    }

    protected async Task CreatePet(CreatePetModel petCreateModel)
    {
        await PetDataService!.CreatePet(petCreateModel);
    }

    protected async Task UpdatePet(string petId, UpdatePetModel updatePetModel)
    {
        await PetDataService!.UpdatePet(petId, updatePetModel);
    }

    protected async Task DeletePet(string petId)
    {
        await PetDataService!.DeletePet(petId);
    }
}
