using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Net.Http.Json;
using DucksNet.API.Controllers;
using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using System.Net;

namespace DucksNet.IntegrationTests;
public class UsersControllerTests : BaseIntegrationTests<UsersController>
{
    private const string UserUrl = "api/v1/users";

    [Fact]
    public async Task When_CreatedUser_Then_ShouldReturnPetInTheGetRequest()
    {
        ClearDatabase();
        //Arrange
        var sut = CreateSut();
        //Act 
        var userResponse = await TestingClient.PostAsJsonAsync(UserUrl, sut);
        userResponse.EnsureSuccessStatusCode();
        var getUserResult = await TestingClient.GetAsync(UserUrl);
        //Assert

        var users = await getUserResult.Content.ReadFromJsonAsync<List<User>>();
        users.Should().NotBeNull();
        users!.Count.Should().Be(1);
        foreach (var user in users!)
        {
            user.FirstName.Should().Be(sut.FirstName);
            user.LastName.Should().Be(sut.LastName);
            user.Address.Should().Be(sut.Address);
            user.PhoneNumber.Should().Be(sut.PhoneNumber);
            user.Email.Should().Be(sut.Email);
            user.Password.Should().Be(sut.Password);
        }
    }

    [Fact]
    public async Task When_CreateUserWithEmptyFirstName_Then_ShouldFail()
    {
        ClearDatabase();
        //Arrange
        var sut = CreateInvalidSut(" ", "Cats", "Paris, Bonfamille's Residence", "0700000000", "les_aristocats@yahoo.com", "CatsAreTheBest");

        //Act 
        var userResponse = await TestingClient.PostAsJsonAsync(UserUrl, sut);
        //Assert
        userResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await userResponse.Content.ReadFromJsonAsync<List<string>>();
        errors.Should().NotBeEmpty();
        errors.Should().Contain("First name is required.");
    }

    [Fact]
    public async Task When_CreateUserWithEmptyLastName_Then_ShouldFail()
    {
        ClearDatabase();
        //Arrange
        var sut = CreateInvalidSut("Aristo", " ", "Paris, Bonfamille's Residence", "0700000000", "les_aristocats@yahoo.com", "CatsAreTheBest");

        //Act 
        var userResponse = await TestingClient.PostAsJsonAsync(UserUrl, sut);
        //Assert
        userResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await userResponse.Content.ReadFromJsonAsync<List<string>>();
        errors.Should().NotBeEmpty();
        errors.Should().Contain("Last name is required.");
    }

    [Fact]
    public async Task When_CreateUserWithEmptyAddress_Then_ShouldFail()
    {
        ClearDatabase();
        //Arrange
        var sut = CreateInvalidSut("Aristo", "Cats", " ", "0700000000", "les_aristocats@yahoo.com", "CatsAreTheBest");

        //Act 
        var userResponse = await TestingClient.PostAsJsonAsync(UserUrl, sut);
        //Assert
        userResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await userResponse.Content.ReadFromJsonAsync<List<string>>();
        errors.Should().NotBeEmpty();
        errors.Should().Contain("Address is required.");
    }

    [Fact]
    public async Task When_CreateUserWithInvalidPhoneNumber_Then_ShouldFail()
    {
        ClearDatabase();
        //Arrange
        var sut = CreateInvalidSut("Aristo", "Cats", "Paris, Bonfamille's Residence", "070000000", "les_aristocats@yahoo.com", "CatsAreTheBest");

        //Act 
        var userResponse = await TestingClient.PostAsJsonAsync(UserUrl, sut);
        //Assert
        userResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await userResponse.Content.ReadFromJsonAsync<List<string>>();
        errors.Should().NotBeEmpty();
        errors.Should().Contain("The phone number is not valid.");
    }

    [Fact]
    public async Task When_CreateUserWithInvalidEmail_Then_ShouldFail()
    {
        ClearDatabase();
        //Arrange
        var sut = CreateInvalidSut("Aristo", "Cats", "Paris, Bonfamille's Residence", "0700000000", "les_aristocatsyahoo.com", "CatsAreTheBest");

        //Act 
        var userResponse = await TestingClient.PostAsJsonAsync(UserUrl, sut);
        //Assert
        userResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await userResponse.Content.ReadFromJsonAsync<List<string>>();
        errors.Should().NotBeEmpty();
        errors.Should().Contain("The email is not valid.");
    }

    [Fact]
    public async Task When_CreateUserWithEmptyPassword_Then_ShouldFail()
    {
        ClearDatabase();
        //Arrange
        var sut = CreateInvalidSut("Aristo", "Cats", "Paris, Bonfamille's Residence", "0700000000", "les_aristocats@yahoo.com", " ");

        //Act 
        var userResponse = await TestingClient.PostAsJsonAsync(UserUrl, sut);
        //Assert
        userResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await userResponse.Content.ReadFromJsonAsync<List<string>>();
        errors.Should().NotBeEmpty();
        errors.Should().Contain("Password is required.");
    }

    private static UserDto CreateSut()
    {
        return new UserDto
        {
            FirstName = "Aristo",
            LastName = "Cats",
            Address = "Paris, Bonfamille's Residence",
            PhoneNumber = "0700000000",
            Email = "les_aristocats@yahoo.com",
            Password = "CatsAreTheBest"
        };
    }

    private static UserDto CreateInvalidSut(string firstName, string lastName, string address, string phoneNumber, string email, string password)
    {
        return new UserDto
        {
            FirstName = firstName,
            LastName = lastName,
            Address = address,
            PhoneNumber = phoneNumber,
            Email = email,
            Password = password
        };
    }
}

