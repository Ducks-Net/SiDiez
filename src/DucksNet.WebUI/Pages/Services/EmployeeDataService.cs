using System.Net.Http.Json;
using System.Text.Json;
using DucksNet.Domain.Model;
using DucksNet.WebUI.Pages.Models;

namespace DucksNet.WebUI.Pages.Services;

public class EmployeeDataService : IEmployeeDataService
{
#pragma warning disable S1075 // URIs should not be hardcoded  
    private const string ApiURL = "https://localhost:7115/api/v1/employees";
#pragma warning disable S1075 // URIs should not be hardcoded  
    private readonly HttpClient httpClient;

    public EmployeeDataService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    public async Task CreateEmployee(CreateEmployeeModel createEmployeeModel)
    {
        var result = await httpClient.PostAsJsonAsync(ApiURL, createEmployeeModel);
        await result.Content.ReadFromJsonAsync<Employee>();
    }
    public async Task<IEnumerable<Employee>> GetAllPersons()
    {
#pragma warning disable CS8603 // Possible null reference return.

        return await JsonSerializer.DeserializeAsync<IEnumerable<Employee>>
            (await httpClient.GetStreamAsync(ApiURL), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true, });
#pragma warning restore CS8603 // Possible null reference return.
    }
    public async Task DeleteEmployee(string employeeId)
    {
        await httpClient.DeleteAsync($"{ApiURL}/{employeeId}");
    }

    public async Task UpdateEmployee(string employeeId, UpdateEmployeeModel updateEmployeeModel)
    {
        await httpClient.PutAsJsonAsync($"{ApiURL}/{employeeId}", updateEmployeeModel);
    }
}
