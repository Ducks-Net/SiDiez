using System;
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
        result.Value!.AssignToSediu(sut.Item1);
        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        var copy = result.Value;
        if (copy is not null)
        {
            copy.IdSediu.Should().Be(sut.Item1);
            copy.Surname.Should().Be(sut.Item2);
            copy.FirstName.Should().Be(sut.Item3);
            copy.Address.Should().Be(sut.Item4);
            copy.OwnerPhone.Should().Be(sut.Item5);
            copy.OwnerEmail.Should().Be(sut.Item6);
            copy.Id.Should().NotBeEmpty();
        }
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
    private Tuple<Guid, string, string, string, string, string> CreateSUT()
    {
        Tuple<Guid, string, string, string, string, string> sut = new(Guid.NewGuid(), "Mike", "Oxlong", "Blvd. Independentei", "0712123123", "uite@mail.com");
        return sut;
    }


}
