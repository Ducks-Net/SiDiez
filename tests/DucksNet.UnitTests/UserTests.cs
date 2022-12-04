using System;
using DucksNet.Domain.Model;

namespace DucksNet.UnitTests;
public class UserTests
{
    [Fact]
    public void When_CreateUser_Then_ShouldReturnUser()
    {
        Tuple<string, string, string, string, string, string> sut = CreateSUT();
        var result = User.Create(sut.Item1, sut.Item2, sut.Item3, sut.Item4, sut.Item5, sut.Item6);
        var copy = result.Value;
        copy.FirstName.Should().Be(sut.Item1);
        copy.LastName.Should().Be(sut.Item2);
        copy.Address.Should().Be(sut.Item3);
        copy.PhoneNumber.Should().Be(sut.Item4);
        copy.Email.Should().Be(sut.Item5);
        copy.Password.Should().Be(sut.Item6);
        copy.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void When_CreateUserWithEmptyFirstName_Then_ShouldFail()
    {
        var result = User.Create(" ", "Cats", "Paris, Bonfamille's Residence", "0700000000", "les_aristocats@yahoo.com", "CatsAreTheBest");
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("First name is required.");
    }

    [Fact]
    public void When_CreateUserWithEmptyLastName_Then_ShouldFail()
    {
        var result = User.Create("Aristo", " ", "Paris, Bonfamille's Residence", "0700000000", "les_aristocats@yahoo.com", "CatsAreTheBest");
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("Last name is required.");
    }

    [Fact]
    public void When_CreateUserWithEmptyAddress_Then_ShouldFail()
    {
        var result = User.Create("Aristo", "Cats", " ", "0700000000", "les_aristocats@yahoo.com", "CatsAreTheBest");
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("Address is required.");
    }

    [Fact]
    public void When_CreateUserWithInvalidPhoneNumber_Then_ShouldFail()
    {
        var result = User.Create("Aristo", "Cats", "Paris, Bonfamille's Residence", "070000000", "les_aristocats@yahoo.com", "CatsAreTheBest");
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("The phone number is not valid.");
    }

    [Fact]
    public void When_CreateUserWithInvalidEmail_Then_ShouldFail()
    {
        var result = User.Create("Aristo", "Cats", "Paris, Bonfamille's Residence", "0700000000", "les_aristocatsyahoo.com", "CatsAreTheBest");
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("The email is not valid.");
    }

    [Fact]
    public void When_CreateUserWithEmptyPassword_Then_ShouldFail()
    {
        var result = User.Create("Aristo", "Cats", "Paris, Bonfamille's Residence", "0700000000", "les_aristocats@yahoo.com", "");
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("Password is required.");
    }

    [Fact]
    public void When_AllInformationUpdatedInUser_Then_ShouldReturnUpdatedUser()
    {
        Tuple<string, string, string, string, string, string> sut = CreateSUT();
        var result = User.Create(sut.Item1, sut.Item2, sut.Item3, sut.Item4, sut.Item5, sut.Item6);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        Tuple<string, string, string, string, string, string> newSut = new ("Les", "Aristocats", "France, Paris, Bonfamille's Residence", "0700000001", "lesaristocats@yahoo.com", "CatsAreTheBest.");
        result.Value.UpdateFields(newSut.Item1, newSut.Item2, newSut.Item3, newSut.Item4, newSut.Item5, newSut.Item6);

        var copy = result.Value;
        copy.FirstName.Should().Be(newSut.Item1);
        copy.LastName.Should().Be(newSut.Item2);
        copy.Address.Should().Be(newSut.Item3);
        copy.PhoneNumber.Should().Be(newSut.Item4);
        copy.Email.Should().Be(newSut.Item5);
        copy.Password.Should().Be(newSut.Item6);
        copy.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void When_UpdateUserFirstNameIsEmpty_Then_ShouldNotUpdateUserFirstName()
    {
        Tuple<string, string, string, string, string, string> sut = CreateSUT();
        var result = User.Create(sut.Item1, sut.Item2, sut.Item3, sut.Item4, sut.Item5, sut.Item6);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        Tuple<string, string, string, string, string, string> newSut = new(" ", "Aristocats", "France, Paris, Bonfamille's Residence", "0700000001", "lesaristocats@yahoo.com", "CatsAreTheBest.");
        result.Value.UpdateFields(newSut.Item1, newSut.Item2, newSut.Item3, newSut.Item4, newSut.Item5, newSut.Item6);

        var copy = result.Value;
        copy.FirstName.Should().Be(sut.Item1);
        copy.LastName.Should().Be(newSut.Item2);
        copy.Address.Should().Be(newSut.Item3);
        copy.PhoneNumber.Should().Be(newSut.Item4);
        copy.Email.Should().Be(newSut.Item5);
        copy.Password.Should().Be(newSut.Item6);
        copy.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void When_UpdateUserLastNameIsEmpty_Then_ShouldNotUpdateUserLastName()
    {
        Tuple<string, string, string, string, string, string> sut = CreateSUT();
        var result = User.Create(sut.Item1, sut.Item2, sut.Item3, sut.Item4, sut.Item5, sut.Item6);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        Tuple<string, string, string, string, string, string> newSut = new("Les", " ", "France, Paris, Bonfamille's Residence", "0700000001", "lesaristocats@yahoo.com", "CatsAreTheBest.");
        result.Value.UpdateFields(newSut.Item1, newSut.Item2, newSut.Item3, newSut.Item4, newSut.Item5, newSut.Item6);

        var copy = result.Value;
        copy.FirstName.Should().Be(newSut.Item1);
        copy.LastName.Should().Be(sut.Item2);
        copy.Address.Should().Be(newSut.Item3);
        copy.PhoneNumber.Should().Be(newSut.Item4);
        copy.Email.Should().Be(newSut.Item5);
        copy.Password.Should().Be(newSut.Item6);
        copy.Id.Should().NotBeEmpty();
    }   
    
    [Fact]
    public void When_UpdateUserAddressIsEmpty_Then_ShouldNotUpdateUserAddress()
    {
        Tuple<string, string, string, string, string, string> sut = CreateSUT();
        var result = User.Create(sut.Item1, sut.Item2, sut.Item3, sut.Item4, sut.Item5, sut.Item6);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        Tuple<string, string, string, string, string, string> newSut = new("Les", "Aristocats", " ", "0700000001", "lesaristocats@yahoo.com", "CatsAreTheBest.");
        result.Value.UpdateFields(newSut.Item1, newSut.Item2, newSut.Item3, newSut.Item4, newSut.Item5, newSut.Item6);

        var copy = result.Value;
        copy.FirstName.Should().Be(newSut.Item1);
        copy.LastName.Should().Be(newSut.Item2);
        copy.Address.Should().Be(sut.Item3);
        copy.PhoneNumber.Should().Be(newSut.Item4);
        copy.Email.Should().Be(newSut.Item5);
        copy.Password.Should().Be(newSut.Item6);
        copy.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void When_UpdateUserPhoneNumberIsInvalid_Then_ShouldNotUpdateUserPhoneNumber()
    {
        Tuple<string, string, string, string, string, string> sut = CreateSUT();
        var result = User.Create(sut.Item1, sut.Item2, sut.Item3, sut.Item4, sut.Item5, sut.Item6);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        Tuple<string, string, string, string, string, string> newSut = new("Les", "Aristocats", "France, Paris, Bonfamille's Residence", "070000001", "lesaristocats@yahoo.com", "CatsAreTheBest.");
        result.Value.UpdateFields(newSut.Item1, newSut.Item2, newSut.Item3, newSut.Item4, newSut.Item5, newSut.Item6);

        var copy = result.Value;
        copy.FirstName.Should().Be(newSut.Item1);
        copy.LastName.Should().Be(newSut.Item2);
        copy.Address.Should().Be(newSut.Item3);
        copy.PhoneNumber.Should().Be(sut.Item4);
        copy.Email.Should().Be(newSut.Item5);
        copy.Password.Should().Be(newSut.Item6);
        copy.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void When_UpdateUserEmailIsInvalid_Then_ShouldNotUpdateUserEmail()
    {
        Tuple<string, string, string, string, string, string> sut = CreateSUT();
        var result = User.Create(sut.Item1, sut.Item2, sut.Item3, sut.Item4, sut.Item5, sut.Item6);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        Tuple<string, string, string, string, string, string> newSut = new("Les", "Aristocats", "France, Paris, Bonfamille's Residence", "0700000001", "lesaristocatsyahoo.com", "CatsAreTheBest.");
        result.Value.UpdateFields(newSut.Item1, newSut.Item2, newSut.Item3, newSut.Item4, newSut.Item5, newSut.Item6);

        var copy = result.Value;
        copy.FirstName.Should().Be(newSut.Item1);
        copy.LastName.Should().Be(newSut.Item2);
        copy.Address.Should().Be(newSut.Item3);
        copy.PhoneNumber.Should().Be(newSut.Item4);
        copy.Email.Should().Be(sut.Item5);
        copy.Password.Should().Be(newSut.Item6);
        copy.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void When_UpdateUserPasswordIsEmpty_Then_ShouldNotUpdateUserEmpty()
    {
        Tuple<string, string, string, string, string, string> sut = CreateSUT();
        var result = User.Create(sut.Item1, sut.Item2, sut.Item3, sut.Item4, sut.Item5, sut.Item6);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        Tuple<string, string, string, string, string, string> newSut = new("Les", "Aristocats", "France, Paris, Bonfamille's Residence", "0700000001", "lesaristocats@yahoo.com", " ");
        result.Value.UpdateFields(newSut.Item1, newSut.Item2, newSut.Item3, newSut.Item4, newSut.Item5, newSut.Item6);

        var copy = result.Value;
        copy.FirstName.Should().Be(newSut.Item1);
        copy.LastName.Should().Be(newSut.Item2);
        copy.Address.Should().Be(newSut.Item3);
        copy.PhoneNumber.Should().Be(newSut.Item4);
        copy.Email.Should().Be(newSut.Item5);
        copy.Password.Should().Be(sut.Item6);
        copy.Id.Should().NotBeEmpty();
    }

    private Tuple<string, string, string, string, string, string> CreateSUT()
    {
        Tuple<string, string, string, string, string, string> sut = new ("Aristo", "Cats", "Paris, Bonfamille's Residence", "0700000000", "les_aristocats@yahoo.com", "CatsAreTheBest");
        return sut;
    }
}
