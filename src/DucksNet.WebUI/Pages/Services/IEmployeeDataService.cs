using DucksNet.Domain.Model;

namespace DucksNet.WebUI.Pages.Services;
public interface IEmployeeDataService
{
    Task<IEnumerable<Employee>> GetAllPersons();
    Task<Employee> GetPersonDetail(Guid personId);
}