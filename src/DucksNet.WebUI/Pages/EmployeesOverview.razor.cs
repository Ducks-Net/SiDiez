using DucksNet.Domain.Model;
using DucksNet.WebUI.Pages.Models;
using DucksNet.WebUI.Pages.Services;
using Microsoft.AspNetCore.Components;

namespace DucksNet.WebUI.Pages;

public partial class EmployeesOverview : ComponentBase
{
    [Inject]
    public IEmployeeDataService? EmployeeDataService { get; set; }
    public List<Employee> Employees { get; set; } = default!;
    protected async Task CreateEmployee(CreateEmployeeModel createEmployeeModel)
    {
        await EmployeeDataService!.CreateEmployee(createEmployeeModel);
    }
    protected async Task UpdateEmployee(string employeeId, UpdateEmployeeModel updateEmployeeModel)
    {
        await EmployeeDataService!.UpdateEmployee(employeeId, updateEmployeeModel);
    }
    protected async Task ReloadAllEmployees()
    {
        Employees = (await EmployeeDataService!.GetAllPersons()).ToList();
        if (Employees.Count == 0)
        {
            Employees = default!;
        }
    }
    protected async Task DeleteEmployee(string employeeId)
    {
        await EmployeeDataService!.DeleteEmployee(employeeId);
    }
}
