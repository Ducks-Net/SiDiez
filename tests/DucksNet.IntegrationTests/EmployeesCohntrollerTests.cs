using System.Collections.Generic;
using System.Net.Http.Json;
using DucksNet.API.Controllers;
using DucksNet.API.DTO;

namespace DucksNet.API.Integration_Tests;
public class EmployeesControllerTests : BaseIntegrationTests<EmployeesController>
{
    private const string ApiURL = "api/v1/employees";

    [Fact]
    public async void When_CreatedEmployee_Then_ShouldReturnEmployeeInTheGetRequest()
    {
        //Arrange
        EmployeeDTO employeeDTO = new EmployeeDTO(System.Guid.NewGuid(), "Mike", "Oxlong", "Bld. Independentei", "0712123123", "uite@mail.com");
        //Act
        var createEmployeeResponse = await TestingClient.PostAsJsonAsync(ApiURL, employeeDTO);
        var getEmployeeResult = await TestingClient.GetAsync(ApiURL);
        //Assert
        createEmployeeResponse.EnsureSuccessStatusCode();
        createEmployeeResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        getEmployeeResult.EnsureSuccessStatusCode();
        var employees = await getEmployeeResult.Content.ReadFromJsonAsync<List<EmployeeDTO>>();
        employees.Should().NotBeNull();
        employees.Should().NotBeEmpty();
    }
}
