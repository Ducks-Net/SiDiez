using DucksNet.Domain.Model;
using DucksNet.WebUI.Pages.Models;

namespace DucksNet.WebUI.Pages.Services;
public interface IPetDataService
{
    Task CreatePet(CreatePetModel petCreateModel);
    Task DeletePet(string petId);
    Task<IEnumerable<Pet>> GetAllPets();
    Task<Pet> GetPetDetail(Guid petId);
    Task UpdatePet(string petId, UpdatePetModel updatePetModel);
}