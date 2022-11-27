using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using DucksNet.API.Controllers;
using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using DucksNet.Infrastructure.Sqlite;
using DucksNet.IntegrationTests;
using DucksNet.SharedKernel.Utils;

namespace DucksNet.API.IntegrationTests;
public class EmployeesControllerTests : BaseIntegrationTests<EmployeesController>
{
    private const string ApiURL = "api/v1/employees";

    [Fact]
    public async void When_CreatedEmployee_Then_ShouldReturnEmployeeInTheGetRequest()
    {
        var sut = CreateSUT();
        var officeDTO = new OfficeDTO
        {
            BusinessId = Guid.NewGuid(),
            Address = "123 Main St",
            AnimalCapacity = 10
        };

        var office = await TestingClient.PostAsJsonAsync("api/v1/office", officeDTO);
        var officeResult = await office.Content.ReadFromJsonAsync<Office>();
        var officeId = officeResult!.ID;
        sut.IdOffice = officeId;

        var employeeResult = await TestingClient.PostAsJsonAsync(ApiURL, sut);
        var employee = await employeeResult.Content.ReadFromJsonAsync<Employee>();
    }
    
    private static EmployeeDTO CreateSUT()
    {
        return new EmployeeDTO(Guid.NewGuid(), "Mike", "Oxlong", "Blvd. Independentei", "0712123123", "ceva@mail.com");
    }
}
