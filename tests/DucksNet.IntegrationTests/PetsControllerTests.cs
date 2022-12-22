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
        errors.Should().Contain("The owner id can not be null.");
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

    [Fact]
    public async Task When_CreatedPet_Then_ShouldReturnPetByIdInTheGetRequest()
    {
        //Arrange
        var sut = CreateSut();

        //Act 
        var petResponse = await TestingClient.PostAsJsonAsync(PetUrl, sut);
        var pet = await petResponse.Content.ReadFromJsonAsync<Pet>();
        var getPetResult = await TestingClient.GetAsync(PetUrl + $"/{pet!.Id}");

        //Assert
        petResponse.EnsureSuccessStatusCode();
        var petPost = await getPetResult.Content.ReadFromJsonAsync<Pet>();
        petPost!.Name.Should().Be(sut.Name);
        petPost!.DateOfBirth.Should().Be(sut.DateOfBirth);
        petPost!.Species.Should().Be(sut.Species);
        petPost!.Breed.Should().Be(sut.Breed);
        petPost!.OwnerId.Should().Be(sut.OwnerId);
        petPost!.Size.Name.Should().Be(sut.Size);
    }

    [Fact]
    public async Task When_CreatedPetAndUpdateInformation_Then_ShouldReturnUpdatedPet()
    {
        //Arrange
        var sut = CreateSut();
        var petResponse = await TestingClient.PostAsJsonAsync(PetUrl, sut);
        var pet = await petResponse.Content.ReadFromJsonAsync<Pet>();
        petResponse.EnsureSuccessStatusCode();

        //Act
        var newSut = CreateNewSut();
        var newPetResponse = await TestingClient.PutAsJsonAsync(PetUrl + $"/{pet!.Id}", newSut);
        var updates = await newPetResponse.Content.ReadAsStringAsync();
        Console.WriteLine(updates);

        //Assert
        newPetResponse.EnsureSuccessStatusCode();
        updates.Should().Contain("The information has been updated.");
    }

    [Fact]
    public async Task When_CreatedPetAndDeleteIt_Then_ShouldReturnSuccess()
    {
        //Arrange
        var sut = CreateSut();

        //Act 
        var petResponse = await TestingClient.PostAsJsonAsync(PetUrl, sut);
        var pet = await petResponse.Content.ReadFromJsonAsync<Pet>();
        var getPetResult = await TestingClient.DeleteAsync(PetUrl + $"/{pet!.Id}");

        //Assert
        petResponse.EnsureSuccessStatusCode();
        getPetResult.Content.Headers.ContentLength.Should().Be(0);
        var petsResponse = await TestingClient.GetAsync(PetUrl);
        var pets = await petsResponse.Content.ReadFromJsonAsync<List<Pet>>();
        pets.Should().BeEmpty();
    }

    [Fact]
    public async Task When_CreatedPetAndDeleteOtherGuid_Then_ShouldFail()
    {
        ///Arrange
        var sut = CreateSut();

        //Act 
        var petResponse = await TestingClient.PostAsJsonAsync(PetUrl, sut);
        var getPetResult = await TestingClient.DeleteAsync(PetUrl + $"/{Guid.NewGuid()}");
        //Assert
        petResponse.EnsureSuccessStatusCode();
        getPetResult.StatusCode.Should().Be(HttpStatusCode.NotFound);
        var errors = await getPetResult.Content.ReadAsStringAsync();
        errors.Should().NotBeEmpty();
        errors.Should().Contain("Entity of type Pet was not found.");
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

    private static PetDto CreateNewSut()
    {
        return new PetDto
        {
            Name = "CleoTheCat",
            DateOfBirth = new DateTime(2022, 06, 04),
            Species = "Fluffy Cat",
            Breed = "Romanian",
            OwnerId = Guid.NewGuid(),
            Size = "Medium"
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
