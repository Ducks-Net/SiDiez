using System.Text.Json;
using System;
using DucksNet.Domain.Model;

namespace DucksNet.WebUI.Pages.Services;

public class EmployeeDataService : IEmployeeDataService
{
    private const string ApiURL = "https://localhost:7115/api/v1/employees";
    private readonly HttpClient httpClient;

    public EmployeeDataService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    public async Task<IEnumerable<Employee>> GetAllPersons()
    {
#pragma warning disable CS8603 // Possible null reference return.

        return await JsonSerializer.DeserializeAsync<IEnumerable<Employee>>
            (await httpClient.GetStreamAsync(ApiURL), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true, });
#pragma warning restore CS8603 // Possible null reference return.
    }

    public async Task<Employee> GetPersonDetail(Guid personId)
    {
        //throw new NotImplementedException();
        return null;
    }
}
