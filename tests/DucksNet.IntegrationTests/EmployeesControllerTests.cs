using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using DucksNet.API.Controllers;
using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using DucksNet.IntegrationTests;

namespace DucksNet.API.IntegrationTests;
public class EmployeesControllerTests : BaseIntegrationTests<EmployeesController>
{
    private const string EmployeesUrl = "api/v1/employees";
    private const string OfficeUrl = "api/v1/office";

    [Fact]
    public async void When_CreatedEmployee_Then_ShouldReturnEmployeeInTheGetRequest()
    {
        //Arrange
        var sut = CreateSUT();
        var officeDTO = new OfficeDTO
        {
            BusinessId = Guid.NewGuid(),
            Address = "123 Main St",
            AnimalCapacity = 10
        };

        var officeResponse = await TestingClient.PostAsJsonAsync(OfficeUrl, officeDTO);
        var officeResult = await TestingClient.GetAsync(OfficeUrl);
        officeResponse.EnsureSuccessStatusCode();
        var offices = await officeResult.Content.ReadFromJsonAsync<List<Office>>();
        offices.Should().NotBeNull();
        offices!.Count.Should().Be(1);
        foreach(var office in offices!)
            sut.IdOffice = office.ID;
       //Act 
        var employeeResponse = await TestingClient.PostAsJsonAsync(EmployeesUrl, sut);
        var getEmployeeResult = await TestingClient.GetAsync(EmployeesUrl);
        //Assert
        employeeResponse.EnsureSuccessStatusCode();

        var employees = await getEmployeeResult.Content.ReadFromJsonAsync<List<Employee>>();
        employees.Should().NotBeNull();
        employees!.Count.Should().Be(1);
        foreach (var employee in employees!)
        {
            employee.Surname.Should().Be(sut.Surname);
            employee.FirstName.Should().Be(sut.FirstName);
            employee.Address.Should().Be(sut.Address);
            employee.OwnerEmail.Should().Be(sut.OwnerEmail);
            employee.OwnerPhone.Should().Be(sut.OwnerPhone);
        }
    }

    
    private static EmployeeDTO CreateSUT()
    {
        return new EmployeeDTO(Guid.NewGuid(), "Mike", "Oxlong", "Blvd. Independentei", "0712123123", "ceva@mail.com");
    }
}
