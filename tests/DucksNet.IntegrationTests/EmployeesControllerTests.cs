using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using DucksNet.API.DTO;
using DucksNet.API.Integration_Tests;
using DucksNet.API.Controllers;

namespace DucksNet.IntegrationTests;
public class EmployeesControllerTests : BaseIntegrationTests<EmployeesController>
{
    private const string ApiURL = "api/v1/employees";
    [Fact]
    public async void When_CreatedEmployee_Then_ShouldReturnEmployeeInTheGetRequest()
    {
        //Arrange
        EmployeeDTO employeeDTO = new EmployeeDTO(Guid.NewGuid(), "Mike", "Oxlong", "Bld. Independentei", "0712123123", "uite@mail.com");
        //Act
        var createEmployeeResponse = await HttpClient.PostAsJsonAsync(ApiURL, employeeDTO);
        var getEmployeeResult = await HttpClient.GetAsync(ApiURL);
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
