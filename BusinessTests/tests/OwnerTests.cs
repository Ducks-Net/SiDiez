using Xunit;
using System;
using FluentAssertions;

using VetAppointment.Model;


public class OwnerTests
{
    class OwnerSut
    {
        public string Name = "Joe";
        public string Surname = "Doe";
        public string Address = "Street St. 24th";
        public string EmailAddress = "joedoe@joedoe.xyz";
        public string Phone = "0733222111";
        public DateOnly Birthday = new DateOnly(2001, 12, 12);
    }

    [Fact]
    public void When_Create_Owner_ShouldReturnOwner()
    {
        OwnerSut sut = new OwnerSut();
        var gender = "Male";
        var CNP = "5011212212123";

        var result = Owner.Create(
            CNP,
            sut.Name,
            sut.Surname,
            gender,
            sut.Birthday,
            sut.Address,
            sut.EmailAddress,
            sut.Phone);

        result.IsSuccess.Should().Be(true);
        result.IsFailure.Should().Be(false);
        result.Error.Should().BeNull();

        var owner = result.Entity;
        owner.CNP.Should().Be(CNP);
        owner.Name.Should().Be(sut.Name);
        owner.Surname.Should().Be(sut.Surname);
        owner.Gender.Should().Be(Gender.Male);
        owner.Birthday.Should().Be(sut.Birthday);
        owner.Address.Should().Be(sut.Address);
        owner.EmailAddress.Should().Be(sut.EmailAddress);
        owner.Phone.Should().Be(sut.Phone);
    }

    [Fact]
    public void Wehen_Created_With_BadGender_Should_ReturnError()
    {
        OwnerSut sut = new OwnerSut();
        var gender = "Non Binary";
        var CNP = "5011212212123";

        var result = Owner.Create(
            CNP,
            sut.Name,
            sut.Surname,
            gender,
            sut.Birthday,
            sut.Address,
            sut.EmailAddress,
            sut.Phone);
        
        result.IsSuccess.Should().Be(false);
        result.IsFailure.Should().Be(true);
        result.Entity.Should().BeNull();
    }
}