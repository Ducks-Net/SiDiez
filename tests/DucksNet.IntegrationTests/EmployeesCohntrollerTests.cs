using System.Collections.Generic;
using System.Net.Http.Json;
using DucksNet.API.DTO;
using DucksNet.Infrastructure.Prelude;
using DucksNet.Infrastructure.Sqlite;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace DucksNet.API.Integration_Tests;
public class EmployeesControllerTests
{

    private const string ApiURL = "api/v1/employees";

    [Fact]
    public async void When_CreatedEmployee_Then_ShouldReturnEmployeeInTheGetRequest()
    {
        await using var application = new WebApplicationFactory<DucksNet.API.Controllers.EmployeesController>().WithWebHostBuilder(builder => { 
            builder.ConfigureTestServices(services => {
                services.AddScoped<IDatabaseContext>(provider => new TestDbContext("EmployeesControllerTests"));
            });
        });

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
        employees.Should().NotBeEmpty();
    }
}
