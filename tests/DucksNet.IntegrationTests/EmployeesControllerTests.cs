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

        var employees = await getEmployeeResult.Content.ReadFromJsonAsync<List<EmployeeDTO>>();
        employees.Should().NotBeNull();
        employees!.Count.Should().Be(1);
        foreach (var employee in employees!)
        {
            foreach (var office in offices!)
                employee.IdOffice.Should().Be(office.ID);
            employee.Surname.Should().Be(sut.Surname);
            employee.FirstName.Should().Be(sut.FirstName);
            employee.Address.Should().Be(sut.Address);
            employee.OwnerEmail.Should().Be(sut.OwnerEmail);
            employee.OwnerPhone.Should().Be(sut.OwnerPhone);
        }
    }
    [Fact]
    public async void When_CreatedEmployee_Then_ShouldReturnEmployeeByIdInTheGetRequest()
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
        foreach (var office in offices!)
            sut.IdOffice = office.ID;
        //Act 
        var employeeResponse = await TestingClient.PostAsJsonAsync(EmployeesUrl, sut);
        var employee = await employeeResponse.Content.ReadFromJsonAsync<Employee>();

        var getEmployeeResult = await TestingClient.GetAsync(EmployeesUrl + $"/{employee!.Id}");
        //Assert
        employeeResponse.EnsureSuccessStatusCode();

        var employeePost = await getEmployeeResult.Content.ReadFromJsonAsync<EmployeeDTO>();
        foreach (var office in offices!)
            employeePost!.IdOffice.Should().Be(office.ID);
        employeePost!.Surname.Should().Be(sut.Surname);
        employeePost.FirstName.Should().Be(sut.FirstName);
        employeePost.Address.Should().Be(sut.Address);
        employeePost.OwnerEmail.Should().Be(sut.OwnerEmail);
        employeePost.OwnerPhone.Should().Be(sut.OwnerPhone);
    }
    [Fact]
    public async void When_CreatedEmployeeAndDeleteIt_Then_ShouldReturnSucces()
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
        foreach (var office in offices!)
            sut.IdOffice = office.ID;
        //Act 
        var employeeResponse = await TestingClient.PostAsJsonAsync(EmployeesUrl, sut);
        var employee = await employeeResponse.Content.ReadFromJsonAsync<Employee>();

        var getEmployeeResult = await TestingClient.DeleteAsync(EmployeesUrl + $"/{employee!.Id}");
        //Assert
        employeeResponse.EnsureSuccessStatusCode();
        getEmployeeResult.Content.Headers.ContentLength.Should().Be(0);
    }
    [Fact]
    public async void When_CreatedEmployeeAndUpdateThePersonalInformation_Then_ShouldReturnModifiedEmployee()
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
        foreach (var office in offices!)
            sut.IdOffice = office.ID;
        var employeeResponse = await TestingClient.PostAsJsonAsync(EmployeesUrl, sut);
        var employee = await employeeResponse.Content.ReadFromJsonAsync<Employee>();
        employeeResponse.EnsureSuccessStatusCode();
        //Act
        var updatedEmployeeDTO = new EmployeeDTO(Guid.NewGuid(), "Ben", "Dova", "Blvd. Milei", "0769420666", "meme@mail.com");

        var updatedEmployeeResponse = await TestingClient.PutAsJsonAsync(EmployeesUrl + $"/{employee!.Id}", updatedEmployeeDTO);
        var updatedEmployee = await updatedEmployeeResponse.Content.ReadFromJsonAsync<Employee>();
        //Assert
        updatedEmployeeResponse.EnsureSuccessStatusCode();

        updatedEmployee.Id.Should().Be(employee.Id);
        updatedEmployee!.Surname.Should().Be(updatedEmployeeDTO.Surname);
        updatedEmployee.FirstName.Should().Be(updatedEmployeeDTO.FirstName);
        updatedEmployee.Address.Should().Be(updatedEmployeeDTO.Address);
        updatedEmployee.OwnerEmail.Should().Be(updatedEmployeeDTO.OwnerEmail);
        updatedEmployee.OwnerPhone.Should().Be(updatedEmployeeDTO.OwnerPhone);
    }

    private static EmployeeDTO CreateSUT()
    {
        return new EmployeeDTO(Guid.NewGuid(), "Mike", "Oxlong", "Blvd. Independentei", "0712123123", "ceva@mail.com");
    }
}
