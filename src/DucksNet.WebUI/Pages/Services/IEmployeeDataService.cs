using DucksNet.Domain.Model;
using DucksNet.WebUI.Pages.Models;

namespace DucksNet.WebUI.Pages.Services;
public interface IEmployeeDataService
{
    Task CreateEmployee(CreateEmployeeModel createEmployeeModel);
    Task DeleteEmployee(string employeeId);
    Task<IEnumerable<Employee>> GetAllPersons();
    Task UpdateEmployee(string employeeId, UpdateEmployeeModel updateEmployeeModel);
}