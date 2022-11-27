using System;
using System.Net.Sockets;
using DucksNet.Domain.Model;
using DucksNet.SharedKernel.Utils;

namespace DucksNet.UnitTests;
public class EmployeeTests
{
    [Fact]
    public void When_CreateEmployee_Then_ShouldReturnEmployee()
    {
        //Arrange
        var idSediu = Guid.NewGuid();
        var surname = "Mike";
        var firstName = "Oxlong";
        var address = "Blvd. Independentei";
        var ownerPhone = "0712123123";
        var ownerEmail = "uite@mail.com";
        //Act
        var result = Employee.Create(surname, firstName, address, ownerPhone, ownerEmail);
        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        var copy = result.Value;
        if (copy is not null)
        {
            copy.IdSediu.Should().Be(idSediu);
            copy.Surname.Should().Be(surname);
            copy.FirstName.Should().Be(firstName);
            copy.Address.Should().Be(address);
            copy.OwnerPhone.Should().Be(ownerPhone);
            copy.OwnerEmail.Should().Be(ownerEmail);
            copy.Id.Should().NotBeEmpty();
        }
    }
}
