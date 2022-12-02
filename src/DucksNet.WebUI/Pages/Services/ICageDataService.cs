using DucksNet.Domain.Model;

namespace DucksNet.WebUI.Pages.Services;
public interface ICageDataService
{
    Task<IEnumerable<Cage>> GetAllCages();
    Task<Cage> GetCageDetail(Guid cageId);
}
