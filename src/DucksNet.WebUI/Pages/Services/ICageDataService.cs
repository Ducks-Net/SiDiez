using DucksNet.Domain.Model;
using DucksNet.WebUI.Models;

namespace DucksNet.WebUI.Pages.Services;
public interface ICageDataService
{
    Task<IEnumerable<Cage>> GetAllCages();
    Task<Cage> GetCageDetail(Guid cageId);
    Task CreateCage(CageCreateModel cageCreateModel);
    Task DeleteCage(string cageId);
}
