using DucksNet.Domain.Model;

namespace DucksNet.WebUI.Pages.Services;
public interface IPetDataService
{
    Task<IEnumerable<Pet>> GetAllPets();
    Task<Pet> GetPetDetail(Guid petId);
}