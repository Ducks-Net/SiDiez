using DucksNet.Domain.Model;
using DucksNet.WebUI.Pages.Services;
using Microsoft.AspNetCore.Components;

namespace DucksNet.WebUI.Pages;

public partial class EmployeesOverview : ComponentBase
{
    [Inject]
    public IEmployeeDataService EmployeeDataService { get; set; }
    public List<Employee> Employees { get; set; } = default!;
    protected async override Task OnInitializedAsync()
    {
        Employees = (await EmployeeDataService.GetAllPersons()).ToList();
    }
}
