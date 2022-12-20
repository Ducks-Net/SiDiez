using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Net.Http.Json;
using DucksNet.API.Controllers;
using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using System.Net;

namespace DucksNet.IntegrationTests;
public class PetsControllerTests : BaseIntegrationTests<PetsController>
{
    private const string PetUrl = "api/v1/pets";

    [Fact]
    public async Task When_CreatedPet_Then_ShouldReturnPetInTheGetRequest()
    {
        ClearDatabase();
        //Arrange
        var sut = CreateSut();
        //Act 
        var petResponse = await TestingClient.PostAsJsonAsync(PetUrl, sut);
        petResponse.EnsureSuccessStatusCode();
        var getPetResult = await TestingClient.GetAsync(PetUrl);
        //Assert

        var pets = await getPetResult.Content.ReadFromJsonAsync<List<Pet>>();
        pets.Should().NotBeNull();
        pets!.Count.Should().Be(1);
        foreach (var pet in pets!)
        {
            pet.Name.Should().Be(sut.Name);
            pet.DateOfBirth.Should().Be(sut.DateOfBirth);
            pet.Species.Should().Be(sut.Species);
            pet.Breed.Should().Be(sut.Breed);
            pet.OwnerId.Should().Be(sut.OwnerId);
            pet.Size.Name.Should().Be(sut.Size);
        }
    }

    [Fact]
    public async Task When_CreatePetWithEmptyName_Then_ShouldFail()
    {
        ClearDatabase();
        //Arrange
        var sut = CreateInvalidSut(" ", new DateTime(2021, 06, 04), "Cat", "European", Guid.NewGuid(), "Small");

        //Act 
        var petResponse = await TestingClient.PostAsJsonAsync(PetUrl, sut);
        //Assert
        petResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await petResponse.Content.ReadFromJsonAsync<List<string>>();
        errors.Should().NotBeEmpty();
        errors.Should().Contain("The name should contain at least one character.");
    }

    [Fact]
    public async Task When_CreatePetWithInvalidDateOfBirth_Then_ShouldFail()
    {
        ClearDatabase();
        //Arrange
        var sut = CreateInvalidSut("Cleo", new DateTime(2023, 06, 04), "Cat", "European", Guid.NewGuid(), "Small");

        //Act 
        var petResponse = await TestingClient.PostAsJsonAsync(PetUrl, sut);
        //Assert
        petResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await petResponse.Content.ReadFromJsonAsync<List<string>>();
        errors.Should().NotBeEmpty();
        errors.Should().Contain("This date is not a valid date of birth.");
    }

    [Fact]
    public async Task When_CreatePetWithEmptySpecies_Then_ShouldFail()
    {
        ClearDatabase();
        //Arrange
        var sut = CreateInvalidSut("Cleo", new DateTime(2021, 06, 04), " ", "European", Guid.NewGuid(), "Small");

        //Act 
        var petResponse = await TestingClient.PostAsJsonAsync(PetUrl, sut);
        //Assert
        petResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await petResponse.Content.ReadFromJsonAsync<List<string>>();
        errors.Should().NotBeEmpty();
        errors.Should().Contain("The species field can not be empty.");
    }

    [Fact]
    public async Task When_CreatePetWithEmptyBreed_Then_ShouldFail()
    {
        ClearDatabase();
        //Arrange
        var sut = CreateInvalidSut("Cleo", new DateTime(2021, 06, 04), "Cat", " ", Guid.NewGuid(), "Small");

        //Act 
        var petResponse = await TestingClient.PostAsJsonAsync(PetUrl, sut);
        //Assert
        petResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await petResponse.Content.ReadFromJsonAsync<List<string>>();
        errors.Should().NotBeEmpty();
        errors.Should().Contain("The breed field can not be empty.");
    }

    [Fact]
    public async Task When_CreatePetWithNullOwnerId_Then_ShouldFail()
    {
        ClearDatabase();
        //Arrange
        var sut = CreateInvalidSut("Cleo", new DateTime(2021, 06, 04), "Cat", "European", Guid.Empty, "Small");

        //Act 
        var petResponse = await TestingClient.PostAsJsonAsync(PetUrl, sut);
        //Assert
        petResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await petResponse.Content.ReadFromJsonAsync<List<string>>();
        errors.Should().NotBeEmpty();
        errors.Should().Contain("The onwer id can not be null.");
    }

    [Fact]
    public async Task When_CreatePetWithInvalidSize_Then_ShouldFail()
    {
        ClearDatabase();
        //Arrange
        var sut = CreateInvalidSut("Cleo", new DateTime(2021, 06, 04), "Cat", "European", Guid.NewGuid(), "Big");

        //Act 
        var petResponse = await TestingClient.PostAsJsonAsync(PetUrl, sut);
        //Assert
        petResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await petResponse.Content.ReadFromJsonAsync<List<string>>();
        errors.Should().NotBeEmpty();
        errors.Should().Contain("Failed to parse pet size.");
    }

    private static PetDto CreateSut()
    {
        return new PetDto
        {
            Name = "Cleo",
            DateOfBirth = new DateTime(2021, 06, 04),
            Species = "Cat",
            Breed = "European",
            OwnerId = Guid.NewGuid(),
            Size = "Small"
        };
    }

    private static PetDto CreateInvalidSut(string name, DateTime dateOfBirth, string species, string breed, Guid ownerId, string size)
    {
        return new PetDto
        {
            Name = name,
            DateOfBirth = dateOfBirth,
            Species = species,
            Breed = breed,
            OwnerId = ownerId,
            Size = size
        };
    }
}
