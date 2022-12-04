using System;
using System.Linq;
using DucksNet.Domain.Model;
namespace DucksNet.UnitTests;
public class EmployeeTests
{
    [Fact]
    public void When_CreateEmployee_Then_ShouldReturnEmployee()
    {
        //Arrange
        Tuple<Guid, string, string, string, string, string> sut = CreateSUT();
        var result = Employee.Create(sut.Item2, sut.Item3, sut.Item4, sut.Item5, sut.Item6);
        result.Value!.AssignToOffice(sut.Item1);
        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        var copy = result.Value;
        copy.IdOffice.Should().Be(sut.Item1);
        copy.Surname.Should().Be(sut.Item2);
        copy.FirstName.Should().Be(sut.Item3);
        copy.Address.Should().Be(sut.Item4);
        copy.OwnerPhone.Should().Be(sut.Item5);
        copy.OwnerEmail.Should().Be(sut.Item6);
        copy.Id.Should().NotBeEmpty();
    }
    [Fact]
    public void When_CreateEmployeeWithEmptySurname_Then_ShouldFail()
    {
        Tuple<Guid, string, string, string, string, string> sut = CreateSUT();
        var result = Employee.Create("", sut.Item3, sut.Item4, sut.Item5, sut.Item6);
        //Assert
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("Surname can not be empty");
    }
    [Fact]
    public void When_CreateEmployeeWithEmptyFirstName_Then_ShouldFail()
    {
        Tuple<Guid, string, string, string, string, string> sut = CreateSUT();
        var result = Employee.Create(sut.Item2, "", sut.Item4, sut.Item5, sut.Item6);
        //Assert
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("First name can not be empty");
    }
    [Fact]
    public void When_CreateEmployeeWithEmptyAddress_Then_ShouldFail()
    {
        Tuple<Guid, string, string, string, string, string> sut = CreateSUT();
        var result = Employee.Create(sut.Item2, sut.Item3, "", sut.Item5, sut.Item6);
        //Assert
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("Address can not be empty");
    }
    [Fact]
    public void When_CreateEmployeeWithEmptyTelephoneNumber_Then_ShouldFail()
    {
        Tuple<Guid, string, string, string, string, string> sut = CreateSUT();
        var result = Employee.Create(sut.Item2, sut.Item3, sut.Item4, "", sut.Item6);
        //Assert
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("Telephone can not be empty");
    }
    [Fact]
    public void When_CreateEmployeeWithEmptyEmail_Then_ShouldFail()
    {
        Tuple<Guid, string, string, string, string, string> sut = CreateSUT();
        var result = Employee.Create(sut.Item2, sut.Item3, sut.Item4, sut.Item5, "");
        //Assert
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("Email can not be empty");
    }
    [Fact]
    public void When_CreateEmployeeWithInvalidTelephoneNumber_Then_ShouldFail()
    {
        Tuple<Guid, string, string, string, string, string> sut = CreateSUT();
        var result = Employee.Create(sut.Item2, sut.Item3, sut.Item4, "123456789", sut.Item6);
        //Assert
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("The telephone number is not valid");
    }
    [Fact]
    public void When_CreateEmployeeWithInvalidEmail_Then_ShouldFail()
    {
        Tuple<Guid, string, string, string, string, string> sut = CreateSUT();
        var result = Employee.Create(sut.Item2, sut.Item3, sut.Item4, sut.Item5, "gunoi_test");
        //Assert
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("The email is not valid");
    }
    [Fact]
    public void When_UpdateAllInformationEmployee_Then_ShouldReturnUpdatedEmployee()
    {
        //Arrange
        Tuple<Guid, string, string, string, string, string> sut = CreateSUT();
        var result = Employee.Create(sut.Item2, sut.Item3, sut.Item4, sut.Item5, sut.Item6);
        result.Value!.AssignToOffice(sut.Item1);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        Tuple<string, string, string, string, string> updatedSut = CreateUpdatedSUT();
        var newResult = result.Value.UpdateFields(updatedSut.Item1, updatedSut.Item2, updatedSut.Item3, updatedSut.Item4, updatedSut.Item5);

        //Assert
        newResult.Count.Should().Be(1);
        newResult.First().Should().Contain("The information has been updated");
        var copy = result.Value;
        copy.IdOffice.Should().Be(sut.Item1);
        copy.Surname.Should().Be(updatedSut.Item1);
        copy.FirstName.Should().Be(updatedSut.Item2);
        copy.Address.Should().Be(updatedSut.Item3);
        copy.OwnerPhone.Should().Be(updatedSut.Item4);
        copy.OwnerEmail.Should().Be(updatedSut.Item5);
        copy.Id.Should().NotBeEmpty();
    }
    [Fact]
    public void When_SurnameOfUpdatedEmployeeIsEmpty_Then_ShoulNotUpdateSurnameEmployee()
    {
        //Arrange
        Tuple<Guid, string, string, string, string, string> sut = CreateSUT();
        var result = Employee.Create(sut.Item2, sut.Item3, sut.Item4, sut.Item5, sut.Item6);
        result.Value!.AssignToOffice(sut.Item1);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        Tuple<string, string, string, string, string> updatedSut = CreateUpdatedSUT();
        var newResult = result.Value.UpdateFields("", updatedSut.Item2, updatedSut.Item3, updatedSut.Item4, updatedSut.Item5);

        //Assert
        newResult.Count.Should().Be(1);
        newResult.First().Should().Contain("The information has been updated");
        var copy = result.Value;
        copy.IdOffice.Should().Be(sut.Item1);
        copy.Surname.Should().Be(copy.Surname);
        copy.FirstName.Should().Be(updatedSut.Item2);
        copy.Address.Should().Be(updatedSut.Item3);
        copy.OwnerPhone.Should().Be(updatedSut.Item4);
        copy.OwnerEmail.Should().Be(updatedSut.Item5);
        copy.Id.Should().NotBeEmpty();
    }
    [Fact]
    public void When_FirstNameOfUpdatedEmployeeIsEmpty_Then_ShoulNotUpdateFirstNameEmployee()
    {
        //Arrange
        Tuple<Guid, string, string, string, string, string> sut = CreateSUT();
        var result = Employee.Create(sut.Item2, sut.Item3, sut.Item4, sut.Item5, sut.Item6);
        result.Value!.AssignToOffice(sut.Item1);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        Tuple<string, string, string, string, string> updatedSut = CreateUpdatedSUT();
        var newResult = result.Value.UpdateFields(updatedSut.Item1, "", updatedSut.Item3, updatedSut.Item4, updatedSut.Item5);

        //Assert
        newResult.Count.Should().Be(1);
        newResult.First().Should().Contain("The information has been updated");
        var copy = result.Value;
        copy.IdOffice.Should().Be(sut.Item1);
        copy.Surname.Should().Be(updatedSut.Item1);
        copy.FirstName.Should().Be(copy.FirstName);
        copy.Address.Should().Be(updatedSut.Item3);
        copy.OwnerPhone.Should().Be(updatedSut.Item4);
        copy.OwnerEmail.Should().Be(updatedSut.Item5);
        copy.Id.Should().NotBeEmpty();
    }
    [Fact]
    public void When_AddressOfUpdatedEmployeeIsEmpty_Then_ShoulNotUpdateAddressEmployee()
    {
        //Arrange
        Tuple<Guid, string, string, string, string, string> sut = CreateSUT();
        var result = Employee.Create(sut.Item2, sut.Item3, sut.Item4, sut.Item5, sut.Item6);
        result.Value!.AssignToOffice(sut.Item1);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        Tuple<string, string, string, string, string> updatedSut = CreateUpdatedSUT();
        var newResult = result.Value.UpdateFields(updatedSut.Item1, updatedSut.Item2, "", updatedSut.Item4, updatedSut.Item5);

        //Assert
        newResult.Count.Should().Be(1);
        newResult.First().Should().Contain("The information has been updated");
        var copy = result.Value;
        copy.IdOffice.Should().Be(sut.Item1);
        copy.Surname.Should().Be(updatedSut.Item1);
        copy.FirstName.Should().Be(updatedSut.Item2);
        copy.Address.Should().Be(copy.Address);
        copy.OwnerPhone.Should().Be(updatedSut.Item4);
        copy.OwnerEmail.Should().Be(updatedSut.Item5);
        copy.Id.Should().NotBeEmpty();
    }
    [Fact]
    public void When_ValidTelephoneOfUpdatedEmployeeIsEmpty_Then_ShoulNotUpdateTelephoneEmployee()
    {
        //Arrange
        Tuple<Guid, string, string, string, string, string> sut = CreateSUT();
        var result = Employee.Create(sut.Item2, sut.Item3, sut.Item4, sut.Item5, sut.Item6);
        result.Value!.AssignToOffice(sut.Item1);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        Tuple<string, string, string, string, string> updatedSut = CreateUpdatedSUT();
        var newResult = result.Value.UpdateFields(updatedSut.Item1, updatedSut.Item2, updatedSut.Item3, "", updatedSut.Item5);

        //Assert
        newResult.Count.Should().Be(1);
        newResult.First().Should().Contain("The information has been updated");
        var copy = result.Value;
        copy.IdOffice.Should().Be(sut.Item1);
        copy.Surname.Should().Be(updatedSut.Item1);
        copy.FirstName.Should().Be(updatedSut.Item2);
        copy.Address.Should().Be(updatedSut.Item3);
        copy.OwnerPhone.Should().Be(copy.OwnerPhone);
        copy.OwnerEmail.Should().Be(updatedSut.Item5);
        copy.Id.Should().NotBeEmpty();
    }
    [Fact]
    public void When_VaildEmailOfUpdatedEmployeeIsEmpty_Then_ShoulNotUpdateEmailEmployee()
    {
        //Arrange
        Tuple<Guid, string, string, string, string, string> sut = CreateSUT();
        var result = Employee.Create(sut.Item2, sut.Item3, sut.Item4, sut.Item5, sut.Item6);
        result.Value!.AssignToOffice(sut.Item1);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        Tuple<string, string, string, string, string> updatedSut = CreateUpdatedSUT();
        var newResult = result.Value.UpdateFields(updatedSut.Item1, updatedSut.Item2, updatedSut.Item3, updatedSut.Item4, "");

        //Assert
        newResult.Count.Should().Be(1);
        newResult.First().Should().Contain("The information has been updated");
        var copy = result.Value;
        copy.IdOffice.Should().Be(sut.Item1);
        copy.Surname.Should().Be(updatedSut.Item1);
        copy.FirstName.Should().Be(updatedSut.Item2);
        copy.Address.Should().Be(updatedSut.Item3);
        copy.OwnerPhone.Should().Be(updatedSut.Item4);
        copy.OwnerEmail.Should().Be(copy.OwnerEmail);
        copy.Id.Should().NotBeEmpty();
    }
    [Fact]
    public void When_InvalidTelephoneOfUpdatedEmployeeIsEmpty_Then_ShoulFail()
    {
        //Arrange
        Tuple<Guid, string, string, string, string, string> sut = CreateSUT();
        var result = Employee.Create(sut.Item2, sut.Item3, sut.Item4, sut.Item5, sut.Item6);
        result.Value!.AssignToOffice(sut.Item1);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        Tuple<string, string, string, string, string> updatedSut = CreateUpdatedSUT();
        var newResult = result.Value.UpdateFields(updatedSut.Item1, updatedSut.Item2, updatedSut.Item3, "9123", updatedSut.Item5);

        //Assert
        newResult.Count.Should().Be(1);
        newResult.First().Should().Contain("The telephone number is not valid");
        var copy = result.Value;
        copy.IdOffice.Should().Be(sut.Item1);
        copy.Surname.Should().Be(sut.Item2);
        copy.FirstName.Should().Be(sut.Item3);
        copy.Address.Should().Be(sut.Item4);
        copy.OwnerPhone.Should().Be(sut.Item5);
        copy.OwnerEmail.Should().Be(sut.Item6);
        copy.Id.Should().NotBeEmpty();
    }
    [Fact]
    public void When_InvaildEmailOfUpdatedEmployeeIsEmpty_Then_ShoulNotUpdateEmailEmployee()
    {
        //Arrange
        Tuple<Guid, string, string, string, string, string> sut = CreateSUT();
        var result = Employee.Create(sut.Item2, sut.Item3, sut.Item4, sut.Item5, sut.Item6);
        result.Value!.AssignToOffice(sut.Item1);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        Tuple<string, string, string, string, string> updatedSut = CreateUpdatedSUT();
        var newResult = result.Value.UpdateFields(updatedSut.Item1, updatedSut.Item2, updatedSut.Item3, updatedSut.Item4, "gunoi_test");

        //Assert
        newResult.Count.Should().Be(1);
        newResult.First().Should().Contain("The email is not valid");
        var copy = result.Value;
        copy.IdOffice.Should().Be(sut.Item1);
        copy.Surname.Should().Be(sut.Item2);
        copy.FirstName.Should().Be(sut.Item3);
        copy.Address.Should().Be(sut.Item4);
        copy.OwnerPhone.Should().Be(sut.Item5);
        copy.OwnerEmail.Should().Be(sut.Item6);
        copy.Id.Should().NotBeEmpty();
    }
    [Fact]
    public void When_UpdatedInformationIsEmpty_Then_ShoulNotUpdateEmailEmployee()
    {
        //Arrange
        Tuple<Guid, string, string, string, string, string> sut = CreateSUT();
        var result = Employee.Create(sut.Item2, sut.Item3, sut.Item4, sut.Item5, sut.Item6);
        result.Value!.AssignToOffice(sut.Item1);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        Tuple<string, string, string, string, string> updatedSut = CreateUpdatedSUT();
        var newResult = result.Value.UpdateFields("", "", "", "", "");

        //Assert
        newResult.Count.Should().Be(1);
        newResult.First().Should().Contain("You need to add at least one value");
        
        var copy = result.Value;
        copy.IdOffice.Should().Be(sut.Item1);
        copy.Surname.Should().Be(sut.Item2);
        copy.FirstName.Should().Be(sut.Item3);
        copy.Address.Should().Be(sut.Item4);
        copy.OwnerPhone.Should().Be(sut.Item5);
        copy.OwnerEmail.Should().Be(sut.Item6);
        copy.Id.Should().NotBeEmpty();
    }
    private static Tuple<Guid, string, string, string, string, string> CreateSUT()
    {
        Tuple<Guid, string, string, string, string, string> sut = new(Guid.NewGuid(), "Mike", "Oxlong", "Blvd. Independentei", "0712123123", "uite@mail.com");
        return sut;
    }
    private static Tuple<string, string, string, string, string> CreateUpdatedSUT()
    {
        Tuple<string, string, string, string, string> sut = new("NewMike", "NewOxlong", "Blvd. Soarelui", "0769420666", "test@gmail.com");
        return sut;
    }

}
