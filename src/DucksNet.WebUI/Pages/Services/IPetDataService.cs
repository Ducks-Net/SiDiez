using DucksNet.Domain.Model;
using DucksNet.SharedKernel.Utils;
using DucksNet.WebUI.Pages.Models;

namespace DucksNet.WebUI.Pages.Services;
public interface IPetDataService
{
    Task<Result> CreatePet(CreatePetModel petCreateModel);
    Task<Result> DeletePet(string petId);
    Task<Result<IEnumerable<Pet>>> GetAllPets();
    Task<Result> UpdatePet(string petId, UpdatePetModel updatePetModel);
}
