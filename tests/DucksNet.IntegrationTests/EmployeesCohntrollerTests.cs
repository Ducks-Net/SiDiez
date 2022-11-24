using System.Collections.Generic;
using System.Net.Http.Json;
using DucksNet.API.DTO;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DucksNet.API.Integration_Tests;
public class EmployeesControllerTests
{

    private const string ApiURL = "api/v1/employees";

    [Fact]
    public async void When_CreatedEmployee_Then_ShouldReturnEmployeeInTheGetRequest()
    {
        await using var application = new WebApplicationFactory<DucksNet.API.Controllers.EmployeesController>();
        using var client = application.CreateClient();
        //Arrange
        EmployeeDTO employeeDTO = new EmployeeDTO(System.Guid.NewGuid(), "Mike", "Oxlong", "Bld. Independentei", "0712123123", "uite@mail.com");
        //Act
        var createEmployeeResponse = await client.PostAsJsonAsync(ApiURL, employeeDTO);
        var getEmployeeResult = await client.GetAsync(ApiURL);
        //Assert
        createEmployeeResponse.EnsureSuccessStatusCode();
        createEmployeeResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        getEmployeeResult.EnsureSuccessStatusCode();
        var employees = await getEmployeeResult.Content.ReadFromJsonAsync<List<EmployeeDTO>>();
        employees.Should().NotBeNull();
        if (employees is not null)
            employees.Count.Should().Be(1);
    }
}
