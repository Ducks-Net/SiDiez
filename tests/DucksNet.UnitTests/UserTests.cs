using DucksNet.Domain.Model;
using System;

namespace DucksNet.UnitTests;
public class UserTests
{
    //// TODO (Ad): see why id is null and the test fails
    //[Fact]
    //public void When_CreateUser_Then_ShouldReturnUser()
    //{
    //    Tuple<string, string, string, string, string, string> sut = CreateSUT();
    //    var result = User.Create(sut.Item1, sut.Item2, sut.Item3, sut.Item4, sut.Item5, sut.Item6);
    //    var copy = result.Value;
    //    copy.FirstName.Should().Be(sut.Item1);
    //    copy.LastName.Should().Be(sut.Item2);
    //    copy.Address.Should().Be(sut.Item3);
    //    copy.PhoneNumber.Should().Be(sut.Item4);
    //    copy.Email.Should().Be(sut.Item5);
    //    copy.Password.Should().Be(sut.Item6);
    //    copy.Id.Should().NotBeEmpty();
    //}

    [Fact]
    public void When_CreateUserWithEmptyFirstName_Then_ShouldFail()
    {
        var result = User.Create(" ", "Cats", "0700000000", "Paris, Bonfamille's Residence", "les_aristocats@yahoo.com", "CatsAreTheBest");
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("First name is required.");
    }

    [Fact]
    public void When_CreateUserWithEmptyLastName_Then_ShouldFail()
    {
        var result = User.Create("Aristo", " ", "0700000000", "Paris, Bonfamille's Residence", "les_aristocats@yahoo.com", "CatsAreTheBest");
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("Last name is required.");
    }

    [Fact]
    public void When_CreateUserWithInvalidPhoneNumber_Then_ShouldFail()
    {
        var result = User.Create("Aristo", "Cats", "070000000", "Paris, Bonfamille's Residence", "les_aristocats@yahoo.com", "CatsAreTheBest");
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("The phone number is not valid.");
    }

    [Fact]
    public void When_CreateUserWithEmptyAddress_Then_ShouldFail()
    {
        var result = User.Create("Aristo", "Cats", "0700000000", " ", "les_aristocats@yahoo.com", "CatsAreTheBest");
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("Address is required.");
    }

    [Fact]
    public void When_CreateUserWithInvalidEmail_Then_ShouldFail()
    {
        var result = User.Create("Aristo", "Cats", "0700000000", "Paris, Bonfamille's Residence", "les_aristocatsyahoo.com", "CatsAreTheBest");
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("The email is not valid.");
    }

    [Fact]
    public void When_CreateUserWithEmptyPassword_Then_ShouldFail()
    {
        var result = User.Create("Aristo", "Cats", "0700000000", "Paris, Bonfamille's Residence", "les_aristocats@yahoo.com", "");
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("Password is required.");
    }

    private Tuple<string, string, string, string, string, string> CreateSUT()
    {
        Tuple<string, string, string, string, string, string> sut = new("Aristo", "Cats", "0700000000", "Paris, Bonfamille's Residence", "les_aristocats@yahoo.com", "CatsAreTheBest");
        return sut;
    }
}
