using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using DucksNet.API.Controllers;
using DucksNet.API.DTO;
using DucksNet.IntegrationTests;

namespace DucksNet.API.IntegrationTests;
public class EmployeesControllerTests : BaseIntegrationTests<EmployeesController>
{
    private const string ApiURL = "api/v1/employees";

    [Fact]
    public async void When_CreatedEmployee_Then_ShouldReturnEmployeeInTheGetRequest()
    {
        //Arrange
        EmployeeDTO sut = CreateSUT();
        //Act
        var createEmployeeResponse = await TestingClient.PostAsJsonAsync(ApiURL, sut);
        var getEmployeeResult = await TestingClient.GetAsync(ApiURL);
        //Assert
        createEmployeeResponse.EnsureSuccessStatusCode();
        createEmployeeResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        getEmployeeResult.EnsureSuccessStatusCode();
        var employees = await getEmployeeResult.Content.ReadFromJsonAsync<List<EmployeeDTO>>();
        employees.Should().NotBeNull();
        employees.Count.Should().Be(1);
        employees.Should().NotBeEmpty();
    }
    private static EmployeeDTO CreateSUT()
    {
        return new EmployeeDTO(Guid.NewGuid(), "Mike", "Oxlong", "Blvd. Independentei", "0712123123", "uite@mail.com");
    }
}
