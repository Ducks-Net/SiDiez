using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
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
        var taskEmployeeDTO = GetEmployeeDTO(0);
        var sut = taskEmployeeDTO.Result;
        var officeResult = await TestingClient.GetAsync(OfficeUrl);
        var offices = await officeResult.Content.ReadFromJsonAsync<List<Office>>();
        //Act 
        var employeeResponse = await TestingClient.PostAsJsonAsync(EmployeesUrl, sut);
        var getEmployeeResult = await TestingClient.GetAsync(EmployeesUrl);
        //Assert
        employeeResponse.EnsureSuccessStatusCode();

        //TODO (RO): resolve problem for employee idOffice
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
    public async void When_CreateEmployeeWithNoOffice_Then_ShouldFail()
    {
        //Arrange
        var taskEmployeeDTO = GetEmployeeDTO(0);
        var sut = taskEmployeeDTO.Result;
        var officeResult = await TestingClient.GetAsync(OfficeUrl);
        sut.IdOffice = Guid.NewGuid();
        //Act 
        var employeeResponse = await TestingClient.PostAsJsonAsync(EmployeesUrl, sut);
        //Assert
        employeeResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = employeeResponse.Content.ReadFromJsonAsync<List<string>>().Result;
        errors.Should().NotBeEmpty();
        errors.Should().Contain("Office not found.");
    }
    [Fact]
    public async void When_CreateEmployeeWithEmptySurname_Then_ShouldFail()
    {
        //Arrange
        var taskEmployeeDTO = GetEmployeeDTO(0);
        var sut = taskEmployeeDTO.Result;
        sut.Surname = "";
        //Act 
        var employeeResponse = await TestingClient.PostAsJsonAsync(EmployeesUrl, sut);
        //Assert
        employeeResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = employeeResponse.Content.ReadFromJsonAsync<List<string>>().Result;
        errors.Should().NotBeEmpty();
        errors.Should().Contain("Surname can not be empty");
    }
    [Fact]
    public async void When_CreateEmployeeWithEmptyFirstName_Then_ShouldFail()
    {
        //Arrange
        var taskEmployeeDTO = GetEmployeeDTO(0);
        var sut = taskEmployeeDTO.Result;
        sut.FirstName = "";
        //Act 
        var employeeResponse = await TestingClient.PostAsJsonAsync(EmployeesUrl, sut);
        //Assert
        employeeResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = employeeResponse.Content.ReadFromJsonAsync<List<string>>().Result;
        errors.Should().NotBeEmpty();
        errors.Should().Contain("First name can not be empty");
    }
    [Fact]
    public async void When_CreateEmployeeWithEmptyAddress_Then_ShouldFail()
    {
        //Arrange
        var taskEmployeeDTO = GetEmployeeDTO(0);
        var sut = taskEmployeeDTO.Result;
        sut.Address = "";
        //Act 
        var employeeResponse = await TestingClient.PostAsJsonAsync(EmployeesUrl, sut);
        //Assert
        employeeResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = employeeResponse.Content.ReadFromJsonAsync<List<string>>().Result;
        errors.Should().NotBeEmpty();
        errors.Should().Contain("Address can not be empty");
    }
    [Fact]
    public async void When_CreateEmployeeWithEmptyEmail_Then_ShouldFail()
    {
        //Arrange
        var taskEmployeeDTO = GetEmployeeDTO(0);
        var sut = taskEmployeeDTO.Result;
        sut.OwnerEmail = "";
        //Act 
        var employeeResponse = await TestingClient.PostAsJsonAsync(EmployeesUrl, sut);
        //Assert
        employeeResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = employeeResponse.Content.ReadFromJsonAsync<List<string>>().Result;
        errors.Should().NotBeEmpty();
        errors.Should().Contain("Email can not be empty");
    }
    [Fact]
    public async void When_CreateEmployeeWithEmptyTelephone_Then_ShouldFail()
    {
        //Arrange
        var taskEmployeeDTO = GetEmployeeDTO(0);
        var sut = taskEmployeeDTO.Result;
        sut.OwnerPhone = "";
        //Act 
        var employeeResponse = await TestingClient.PostAsJsonAsync(EmployeesUrl, sut);
        //Assert
        employeeResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = employeeResponse.Content.ReadFromJsonAsync<List<string>>().Result;
        errors.Should().NotBeEmpty();
        errors.Should().Contain("Telephone can not be empty");
    }
    [Fact]
    public async void When_CreateEmployeeWithInvalidTelephone_Then_ShouldFail()
    {
        //Arrange
        var taskEmployeeDTO = GetEmployeeDTO(0);
        var sut = taskEmployeeDTO.Result;
        sut.OwnerPhone = "0123";
        //Act 
        var employeeResponse = await TestingClient.PostAsJsonAsync(EmployeesUrl, sut);
        //Assert
        employeeResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = employeeResponse.Content.ReadFromJsonAsync<List<string>>().Result;
        errors.Should().NotBeEmpty();
        errors.Should().Contain("The telephone number is not valid");
    }
    [Fact]
    public async void When_CreateEmployeeWithInvalidEmail_Then_ShouldFail()
    {
        //Arrange
        var taskEmployeeDTO = GetEmployeeDTO(0);
        var sut = taskEmployeeDTO.Result;
        sut.OwnerEmail = "gunoi_test";
        //Act 
        var employeeResponse = await TestingClient.PostAsJsonAsync(EmployeesUrl, sut);
        //Assert
        employeeResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = employeeResponse.Content.ReadFromJsonAsync<List<string>>().Result;
        errors.Should().NotBeEmpty();
        errors.Should().Contain("The email is not valid");
    }
    [Fact]
    public async void When_CreateTwoEmployeeWithSameEmail_Then_ShouldFail()
    {
        //Arrange
        var sut1 = GetEmployeeDTO(0).Result;
        var sut2 = GetEmployeeDTO(1).Result;
        sut2.OwnerEmail = sut1.OwnerEmail;
        //Act 
        var employeeResponse = await TestingClient.PostAsJsonAsync(EmployeesUrl, sut1);
        var duplicateEmployeeEmail = await TestingClient.PostAsJsonAsync(EmployeesUrl, sut2);
        //Assert
        employeeResponse.EnsureSuccessStatusCode();
        duplicateEmployeeEmail.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = duplicateEmployeeEmail.Content.ReadAsStringAsync().Result;
        errors.Should().NotBeEmpty();
        errors.Should().Contain("The email already exists");
    }
    [Fact]
    public async void When_CreateTwoEmployeeWithSameTelephoneNumber_Then_ShouldFail()
    {
        //Arrange
        var sut1 = GetEmployeeDTO(0).Result;
        var sut2 = GetEmployeeDTO(1).Result;
        sut2.OwnerPhone = sut1.OwnerPhone;
        //Act 
        var employeeResponse = await TestingClient.PostAsJsonAsync(EmployeesUrl, sut1);
        var duplicateEmployeeEmail = await TestingClient.PostAsJsonAsync(EmployeesUrl, sut2);
        //Assert
        employeeResponse.EnsureSuccessStatusCode();
        duplicateEmployeeEmail.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = duplicateEmployeeEmail.Content.ReadAsStringAsync().Result;
        errors.Should().NotBeEmpty();
        errors.Should().Contain("The telephone number already exists");
    }
    [Fact]
    public async void When_CreatedEmployee_Then_ShouldReturnEmployeeByIdInTheGetRequest()
    {
        //Arrange
        var taskEmployeeDTO = GetEmployeeDTO(0);
        var sut = taskEmployeeDTO.Result;
        var officeResult = await TestingClient.GetAsync(OfficeUrl);
        var offices = await officeResult.Content.ReadFromJsonAsync<List<Office>>();
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
    public async void When_GetEmployeeWithInvalidGuid_Then_ShouldFail()
    {
        //Arrange
        var idEmployee = Guid.NewGuid();
        //Act 
        var getEmployeeResult = await TestingClient.GetAsync(EmployeesUrl + $"/{idEmployee}");
        //Assert
        getEmployeeResult.StatusCode.Should().Be(HttpStatusCode.NotFound);
        var errors = getEmployeeResult.Content.ReadAsStringAsync().Result;
        errors.Should().NotBeEmpty();
        errors.Should().Contain("Employee not found");
    }
    [Fact]
    public async void When_CreatedEmployeeAndDeleteIt_Then_ShouldReturnSucces()
    {
        //Arrange
        var taskEmployeeDTO = GetEmployeeDTO(0);
        var sut = taskEmployeeDTO.Result;
   
        //Act 
        var employeeResponse = await TestingClient.PostAsJsonAsync(EmployeesUrl, sut);
        var employee = await employeeResponse.Content.ReadFromJsonAsync<Employee>();

        var getEmployeeResult = await TestingClient.DeleteAsync(EmployeesUrl + $"/{employee!.Id}");
        //Assert
        //getById ->
        employeeResponse.EnsureSuccessStatusCode();
        getEmployeeResult.Content.Headers.ContentLength.Should().Be(0);
        var employees = TestingClient.GetAsync(EmployeesUrl).Result.Content.ReadFromJsonAsync<List<Employee>>().Result;
        employees.Should().BeEmpty();
    }
    [Fact]
    public async void When_CreatedEmployeeAndDeleteOtherGuid_Then_ShouldFail()
    {
        //Arrange
        var taskEmployeeDTO = GetEmployeeDTO(0);
        var sut = taskEmployeeDTO.Result;
        //Act 
        var employeeResponse = await TestingClient.PostAsJsonAsync(EmployeesUrl, sut);
        var getEmployeeResult = await TestingClient.DeleteAsync(EmployeesUrl + $"/{Guid.NewGuid()}");
        //Assert
        employeeResponse.EnsureSuccessStatusCode();
        getEmployeeResult.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = getEmployeeResult.Content.ReadFromJsonAsync<List<string>>().Result;
        errors.Should().NotBeEmpty();
        errors.Should().Contain("Employee not found");
    }
    [Fact]
    public async void When_CreatedEmployeeAndUpdateThePersonalInformation_Then_ShouldReturnModifiedEmployee()
    {
        //Arrange
        var taskEmployeeDTO = GetEmployeeDTO(0);
        var sut = taskEmployeeDTO.Result;
        var employeeResponse = await TestingClient.PostAsJsonAsync(EmployeesUrl, sut);
        var employee = await employeeResponse.Content.ReadFromJsonAsync<Employee>();
        employeeResponse.EnsureSuccessStatusCode();
        //Act
        var updatedEmployeeDTO = CreateSUT2();
        var updatedEmployeeResponse = await TestingClient.PutAsJsonAsync(EmployeesUrl + $"/{employee!.Id}", updatedEmployeeDTO);
        var updates = updatedEmployeeResponse.Content.ReadAsStringAsync().Result;
        //Assert
        updatedEmployeeResponse.EnsureSuccessStatusCode();

        updates.Should().Contain("The information has been updated");
    }
    [Fact]
    public async void When_CreatedEmployeeAndGetInvalidGuid_Then_ShouldFail()
    {
        //Arrange
        var taskEmployeeDTO = GetEmployeeDTO(0);
        var sut = taskEmployeeDTO.Result;
        var employeeResponse = await TestingClient.PostAsJsonAsync(EmployeesUrl, sut);
        var employee = await employeeResponse.Content.ReadFromJsonAsync<Employee>();
        employeeResponse.EnsureSuccessStatusCode();
        //Act
        var updatedEmployeeDTO = CreateSUT2();
        var updatedEmployeeResponse = await TestingClient.PutAsJsonAsync(EmployeesUrl + $"/{Guid.NewGuid()}", updatedEmployeeDTO);
        var errors = updatedEmployeeResponse.Content.ReadAsStringAsync().Result;
        //Assert
        updatedEmployeeResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        errors.Should().Contain("Employee not found");
    }
    [Fact]
    public async void When_CreatedEmployeeAndUpdateDuplicateEmail_Then_ShouldFail()
    {
        //Arrange
        var taskEmployeeDTO = GetEmployeeDTO(0);
        var sut = taskEmployeeDTO.Result;
        var employeeResponse = await TestingClient.PostAsJsonAsync(EmployeesUrl, sut);
        var employee = await employeeResponse.Content.ReadFromJsonAsync<Employee>();
        employeeResponse.EnsureSuccessStatusCode();
        //Act
        var updatedEmployeeDTO = CreateSUT2();
        updatedEmployeeDTO.OwnerEmail = sut.OwnerEmail;
        var updatedEmployeeResponse = await TestingClient.PutAsJsonAsync(EmployeesUrl + $"/{employee!.Id}", updatedEmployeeDTO);
        var errors = updatedEmployeeResponse.Content.ReadAsStringAsync().Result;
        //Assert
        updatedEmployeeResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        errors.Should().Contain("The updated email already exists");
    }
    [Fact]
    public async void When_CreatedEmployeeAndUpdateDuplicatePhoneNumber_Then_ShouldFail()
    {
        //Arrange
        var taskEmployeeDTO = GetEmployeeDTO(0);
        var sut = taskEmployeeDTO.Result;
        var employeeResponse = await TestingClient.PostAsJsonAsync(EmployeesUrl, sut);
        var employee = await employeeResponse.Content.ReadFromJsonAsync<Employee>();
        employeeResponse.EnsureSuccessStatusCode();
        //Act
        var updatedEmployeeDTO = CreateSUT2();
        updatedEmployeeDTO.OwnerPhone = sut.OwnerPhone;
        var updatedEmployeeResponse = await TestingClient.PutAsJsonAsync(EmployeesUrl + $"/{employee!.Id}", updatedEmployeeDTO);
        var errors = updatedEmployeeResponse.Content.ReadAsStringAsync().Result;
        //Assert
        updatedEmployeeResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        errors.Should().Contain("The updated telephone number already exists");
    }
    [Fact]
    public async void When_CreatedEmployeeAndUpdateInvalidPhoneNumber_Then_ShouldFail()
    {
        //Arrange
        var taskEmployeeDTO = GetEmployeeDTO(0);
        var sut = taskEmployeeDTO.Result;
        var employeeResponse = await TestingClient.PostAsJsonAsync(EmployeesUrl, sut);
        var employee = await employeeResponse.Content.ReadFromJsonAsync<Employee>();
        employeeResponse.EnsureSuccessStatusCode();
        //Act
        var updatedEmployeeDTO = CreateSUT2();
        updatedEmployeeDTO.OwnerPhone = "0123";
        var updatedEmployeeResponse = await TestingClient.PutAsJsonAsync(EmployeesUrl + $"/{employee!.Id}", updatedEmployeeDTO);
        var errors = updatedEmployeeResponse.Content.ReadAsStringAsync().Result;
        //Assert
        updatedEmployeeResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        errors.Should().Contain("The telephone number is not valid");
    }
    [Fact]
    public async void When_CreatedEmployeeAndUpdateInvalidEmail_Then_ShouldFail()
    {
        //Arrange
        var taskEmployeeDTO = GetEmployeeDTO(0);
        var sut = taskEmployeeDTO.Result;
        var employeeResponse = await TestingClient.PostAsJsonAsync(EmployeesUrl, sut);
        var employee = await employeeResponse.Content.ReadFromJsonAsync<Employee>();
        employeeResponse.EnsureSuccessStatusCode();
        //Act
        var updatedEmployeeDTO = CreateSUT2();
        updatedEmployeeDTO.OwnerEmail = "gunoi_test";
        var updatedEmployeeResponse = await TestingClient.PutAsJsonAsync(EmployeesUrl + $"/{employee!.Id}", updatedEmployeeDTO);
        var errors = updatedEmployeeResponse.Content.ReadAsStringAsync().Result;
        //Assert
        updatedEmployeeResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        errors.Should().Contain("The email is not valid");
    }
    [Fact]
    public async void When_CreatedEmployeeAndUpdateWithNoValues_Then_ShouldFail()
    {
        //Arrange
        var taskEmployeeDTO = GetEmployeeDTO(0);
        var sut = taskEmployeeDTO.Result;
        var employeeResponse = await TestingClient.PostAsJsonAsync(EmployeesUrl, sut);
        var employee = await employeeResponse.Content.ReadFromJsonAsync<Employee>();
        employeeResponse.EnsureSuccessStatusCode();
        //Act
        var updatedEmployeeDTO = new EmployeeDTO(Guid.NewGuid(), "", "", "", "", "");

        var updatedEmployeeResponse = await TestingClient.PutAsJsonAsync(EmployeesUrl + $"/{employee!.Id}", updatedEmployeeDTO);
        var errors = updatedEmployeeResponse.Content.ReadAsStringAsync().Result;
        //Assert
        updatedEmployeeResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        errors.Should().Contain("You need to add at least one value");
    }
    [Fact]
    public async void When_CreatedEmployeeAndUpdateIdNewOffice_Then_ShouldReturnModifiedEmployee()
    {
        //Arrange
        var sut = GetEmployeeDTO(0).Result;
        var sut2 = GetEmployeeDTO(1).Result;
        var employeeResponse = await TestingClient.PostAsJsonAsync(EmployeesUrl, sut);
        var employeeResponse2 = await TestingClient.PostAsJsonAsync(EmployeesUrl, sut2);

        var employee = await employeeResponse.Content.ReadFromJsonAsync<Employee>();
        var employee2 = await employeeResponse2.Content.ReadFromJsonAsync<EmployeeDTO>();
        employeeResponse.EnsureSuccessStatusCode();
        employeeResponse2.EnsureSuccessStatusCode();
        //Act
        var updatedEmployeeResponse = await TestingClient.PutAsJsonAsync(EmployeesUrl + $"/{employee!.Id}/{employee2!.IdOffice}", employee2.IdOffice);
        var employeeUpdate = await updatedEmployeeResponse.Content.ReadFromJsonAsync<EmployeeDTO>();
        //Assert
        updatedEmployeeResponse.EnsureSuccessStatusCode();
        employeeUpdate!.IdOffice.Should().Be(sut2.IdOffice);
        employeeUpdate!.Surname.Should().Be(sut.Surname);
        employeeUpdate.FirstName.Should().Be(sut.FirstName);
        employeeUpdate.Address.Should().Be(sut.Address);
        employeeUpdate.OwnerEmail.Should().Be(sut.OwnerEmail);
        employeeUpdate.OwnerPhone.Should().Be(sut.OwnerPhone);
    }
    [Fact]
    public async void When_CreatedEmployeeAndGetEmployeeWithInvalidGuid_Then_ShouldReturnModifiedEmployee()
    {
        //Arrange
        var sut = GetEmployeeDTO(0).Result;
        var sut2 = GetEmployeeDTO(1).Result;
        var employeeResponse = await TestingClient.PostAsJsonAsync(EmployeesUrl, sut);
        var employeeResponse2 = await TestingClient.PostAsJsonAsync(EmployeesUrl, sut2);

        var employee = await employeeResponse.Content.ReadFromJsonAsync<Employee>();
        var employee2 = await employeeResponse2.Content.ReadFromJsonAsync<EmployeeDTO>();
        employeeResponse.EnsureSuccessStatusCode();
        employeeResponse2.EnsureSuccessStatusCode();
        //Act
        var updatedEmployeeResponse = await TestingClient.PutAsJsonAsync(EmployeesUrl + $"/{employee!.Id}/{Guid.NewGuid()}", employee2!.IdOffice);
        //Assert
        updatedEmployeeResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = updatedEmployeeResponse.Content.ReadFromJsonAsync<List<string>>().Result;
        errors.Should().NotBeEmpty();
        errors.Should().Contain("Office not found.");
    }
    [Fact]
    public async void When_CreatedEmployeeAndUpdateOfficeWithInvalidGuid_Then_ShouldReturnModifiedEmployee()
    {
        //Arrange
        var sut = GetEmployeeDTO(0).Result;
        var sut2 = GetEmployeeDTO(1).Result;
        var employeeResponse = await TestingClient.PostAsJsonAsync(EmployeesUrl, sut);
        var employeeResponse2 = await TestingClient.PostAsJsonAsync(EmployeesUrl, sut2);

        var employee = await employeeResponse.Content.ReadFromJsonAsync<Employee>();
        var employee2 = await employeeResponse2.Content.ReadFromJsonAsync<EmployeeDTO>();
        employeeResponse.EnsureSuccessStatusCode();
        employeeResponse2.EnsureSuccessStatusCode();
        //Act
        var updatedEmployeeResponse = await TestingClient.PutAsJsonAsync(EmployeesUrl + $"/{Guid.NewGuid()}/{employee2!.IdOffice}", employee2.IdOffice);
        //Assert
        updatedEmployeeResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = updatedEmployeeResponse.Content.ReadAsStringAsync().Result;
        errors.Should().NotBeEmpty();
        errors.Should().Contain("Employee not found");
    }
    private static EmployeeDTO CreateSUT()
    {
        return new EmployeeDTO(Guid.NewGuid(), "Mike", "Oxlong", "Blvd. Independentei", "0712123123", "ceva@mail.com");
    }
    private static EmployeeDTO CreateSUT2()
    {
        return new EmployeeDTO(Guid.NewGuid(), "Ben", "Dova", "Blvd. Soarelui", "0769420666", "update@mail.com");
    }
    private static OfficeDTO CreateSUTOffice()
    {
        return new OfficeDTO
        {
            BusinessId = Guid.NewGuid(),
            Address = "123 Main St",
            AnimalCapacity = 10
        };
    }
    private static OfficeDTO CreateSUTOffice2()
    {
        return new OfficeDTO
        {
            BusinessId = Guid.NewGuid(),
            Address = "Bld. Socola",
            AnimalCapacity = 10
        };
    }
    private async Task<EmployeeDTO> GetEmployeeDTO(int numberSut)
    {

        var sut = numberSut == 0 ? CreateSUT() : CreateSUT2();
        var officeDTO = numberSut == 0 ? CreateSUTOffice() : CreateSUTOffice2();

        var officeResponse = await TestingClient.PostAsJsonAsync(OfficeUrl, officeDTO);
        var officeResult = await TestingClient.GetAsync(OfficeUrl);
        officeResponse.EnsureSuccessStatusCode();
        var offices = await officeResult.Content.ReadFromJsonAsync<List<Office>>();
        offices.Should().NotBeNull();
        offices!.Count.Should().BeLessThan(3);
        foreach (var office in offices!)
            sut.IdOffice = office.ID;
        return sut;
    }
}
