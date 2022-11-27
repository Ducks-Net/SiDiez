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

namespace DucksNet.API.IntegrationTests;
public class EmployeesControllerTests : BaseIntegrationTests<EmployeesController>
{
    private const string ApiURL = "api/v1/employees";

    [Fact]
    public async void When_CreatedEmployee_Then_ShouldReturnEmployeeInTheGetRequest()
    {
        //Arrange
        IRepository<Office> repository = new OfficesRepository(TestingDb);
      
        var office = Office.Create(Guid.NewGuid(), "Bld. Smecher", 10);
       
        office.IsSuccess.Should().BeTrue();
        office.Value.Should().NotBeNull();
        repository.Add(office.Value!);
        EmployeeDTO sut = CreateSUT();
        sut.IdOffice = office.Value!.ID;
        //Act
        var createEmployeeResponse = await TestingClient.PostAsJsonAsync(ApiURL, sut);
        
        var getEmployeeResult = await TestingClient.GetAsync(ApiURL);
        Console.WriteLine(await getEmployeeResult.Content.ReadAsStringAsync());
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
        return new EmployeeDTO(Guid.NewGuid(), "Mike", "Oxlong", "Blvd. Independentei", "071", "uite@mail.com");
    }
}
