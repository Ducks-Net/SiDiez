using DucksNet.Domain.Model;
using System;
using System.Linq;

namespace DucksNet.UnitTests;
public class PetTests
{
    [Fact]
    public void When_CreatePet_Then_ShouldReturnPet()
    {
        Tuple<string, DateTime, string, string, Guid, string> sut = new("Cleo", new DateTime(2021, 06, 04), "Cat", "European", Guid.NewGuid(), "Small");
        var result = Pet.Create(sut.Item1, sut.Item2, sut.Item3, sut.Item4, sut.Item5, sut.Item6);
        var copy = result.Value;
        copy!.Name.Should().Be(sut.Item1);
        copy.DateOfBirth.Should().Be(sut.Item2);
        copy.Species.Should().Be(sut.Item3);
        copy.Breed.Should().Be(sut.Item4);
        copy.OwnerId.Should().Be(sut.Item5);
        copy.Size.Name.Should().Be(sut.Item6);
        copy.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void When_CreatePetWithEmptyName_Then_ShouldFail()
    {
        var result = Pet.Create("", new DateTime(2021, 06, 04), "Cat", "European", Guid.NewGuid(), "Small");
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("The name should contain at least one character.");
    }

    [Fact]
    public void When_CreatePetWithInvalidSize_Then_ShouldFail()
    {
        var result = Pet.Create("Cleo", new DateTime(2021, 06, 04), "Cat", "European", Guid.NewGuid(), "little");
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("Failed to parse pet size.");
    }

    [Fact]
    public void When_CreatePetWithInvalidDate_Then_ShouldFail()
    {
        var result = Pet.Create("Cleo", new DateTime(2023, 06, 04), "Cat", "European", Guid.NewGuid(), "Small");
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("This date is not a valid date of birth.");
    }

    [Fact]
    public void When_CreatePetWithEmptySpecies_Then_ShouldFail()
    {
        var result = Pet.Create("Cleo", new DateTime(2021, 06, 04), " ", "European", Guid.NewGuid(), "Small");
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("The species field can not be empty.");
    }

    [Fact]
    public void When_CreatePetWithEmptyBreed_Then_ShouldFail()
    {
        var result = Pet.Create("Cleo", new DateTime(2021, 06, 04), "Cat", " ", Guid.NewGuid(), "Small");
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("The breed field can not be empty.");
    }

    [Fact]
    public void When_AllInformationUpdatedInPet_Then_ShouldReturnUpdatedPet()
    {
        Tuple<string, DateTime, string, string, Guid, string> sut = CreateSUT();
        var result = Pet.Create(sut.Item1, sut.Item2, sut.Item3, sut.Item4, sut.Item5, sut.Item6);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        Tuple<string, DateTime, string, string, Guid, string> newSut = new("CleoTheCat", new DateTime(2022, 06, 04), "Fluffy Cat", "Romanian", Guid.NewGuid(), "Medium");
        result.Value!.UpdateFields(newSut.Item1, newSut.Item2, newSut.Item3, newSut.Item4, newSut.Item5, newSut.Item6);

        var copy = result.Value;
        copy.Name.Should().Be(newSut.Item1);
        copy.DateOfBirth.Should().Be(newSut.Item2);
        copy.Species.Should().Be(newSut.Item3);
        copy.Breed.Should().Be(newSut.Item4);
        copy.OwnerId.Should().Be(newSut.Item5);
        copy.Size.Name.Should().Be(newSut.Item6);
        copy.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void When_UpdatePetNameIsEmpty_Then_ShouldNotUpatePetName()
    {
        Tuple<string, DateTime, string, string, Guid, string> sut = CreateSUT();
        var result = Pet.Create(sut.Item1, sut.Item2, sut.Item3, sut.Item4, sut.Item5, sut.Item6);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        Tuple<string, DateTime, string, string, Guid, string> newSut = new (" ", new DateTime(2022, 06, 04), "Fluffy Cat", "Romanian", Guid.NewGuid(), "Medium");
        result.Value!.UpdateFields(newSut.Item1, newSut.Item2, newSut.Item3, newSut.Item4, newSut.Item5, newSut.Item6);

        var copy = result.Value;
        copy.Name.Should().Be(sut.Item1);
        copy.DateOfBirth.Should().Be(newSut.Item2);
        copy.Species.Should().Be(newSut.Item3);
        copy.Breed.Should().Be(newSut.Item4);
        copy.OwnerId.Should().Be(newSut.Item5);
        copy.Size.Name.Should().Be(newSut.Item6);
        copy.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void When_UpdatePetDateOfBirthIsNotValid_Then_ShouldNotUpatePetDateOfBirth()
    {
        Tuple<string, DateTime, string, string, Guid, string> sut = CreateSUT();
        var result = Pet.Create(sut.Item1, sut.Item2, sut.Item3, sut.Item4, sut.Item5, sut.Item6);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        Tuple<string, DateTime, string, string, Guid, string> newSut = new("Cleo", new DateTime(2023, 06, 04), "Fluffy Cat", "Romanian", Guid.NewGuid(), "Medium");
        result.Value!.UpdateFields(newSut.Item1, newSut.Item2, newSut.Item3, newSut.Item4, newSut.Item5, newSut.Item6);

        var copy = result.Value;
        copy.Name.Should().Be(newSut.Item1);
        copy.DateOfBirth.Should().Be(sut.Item2);
        copy.Species.Should().Be(newSut.Item3);
        copy.Breed.Should().Be(newSut.Item4);
        copy.OwnerId.Should().Be(newSut.Item5);
        copy.Size.Name.Should().Be(newSut.Item6);
        copy.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void When_UpdatePetSpeciesIsEmpty_Then_ShouldNotUpatePetSpecies()
    {
        Tuple<string, DateTime, string, string, Guid, string> sut = CreateSUT();
        var result = Pet.Create(sut.Item1, sut.Item2, sut.Item3, sut.Item4, sut.Item5, sut.Item6);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        Tuple<string, DateTime, string, string, Guid, string> newSut = new("Cleo", new DateTime(2022, 06, 04), " ", "Romanian", Guid.NewGuid(), "Medium");
        result.Value!.UpdateFields(newSut.Item1, newSut.Item2, newSut.Item3, newSut.Item4, newSut.Item5, newSut.Item6);

        var copy = result.Value;
        copy.Name.Should().Be(newSut.Item1);
        copy.DateOfBirth.Should().Be(newSut.Item2);
        copy.Species.Should().Be(sut.Item3);
        copy.Breed.Should().Be(newSut.Item4);
        copy.OwnerId.Should().Be(newSut.Item5);
        copy.Size.Name.Should().Be(newSut.Item6);
        copy.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void When_UpdatePetBreedIsEmpty_Then_ShouldNotUpatePetBreed()
    {
        Tuple<string, DateTime, string, string, Guid, string> sut = CreateSUT();
        var result = Pet.Create(sut.Item1, sut.Item2, sut.Item3, sut.Item4, sut.Item5, sut.Item6);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        Tuple<string, DateTime, string, string, Guid, string> newSut = new("Cleo", new DateTime(2022, 06, 04), "Fluffy Cat", " ", Guid.NewGuid(), "Medium");
        result.Value!.UpdateFields(newSut.Item1, newSut.Item2, newSut.Item3, newSut.Item4, newSut.Item5, newSut.Item6);

        var copy = result.Value;
        copy.Name.Should().Be(newSut.Item1);
        copy.DateOfBirth.Should().Be(newSut.Item2);
        copy.Species.Should().Be(newSut.Item3);
        copy.Breed.Should().Be(sut.Item4);
        copy.OwnerId.Should().Be(newSut.Item5);
        copy.Size.Name.Should().Be(newSut.Item6);
        copy.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void When_UpdatePetOwnerIdIsEmpty_Then_ShouldNotUpatePetOwnerId()
    {
        Tuple<string, DateTime, string, string, Guid, string> sut = CreateSUT();
        var result = Pet.Create(sut.Item1, sut.Item2, sut.Item3, sut.Item4, sut.Item5, sut.Item6);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        Tuple<string, DateTime, string, string, Guid, string> newSut = new("Cleo", new DateTime(2022, 06, 04), "Fluffy Cat", "Romanian", Guid.Empty, "Medium");
        result.Value!.UpdateFields(newSut.Item1, newSut.Item2, newSut.Item3, newSut.Item4, newSut.Item5, newSut.Item6);

        var copy = result.Value;
        copy.Name.Should().Be(newSut.Item1);
        copy.DateOfBirth.Should().Be(newSut.Item2);
        copy.Species.Should().Be(newSut.Item3);
        copy.Breed.Should().Be(newSut.Item4);
        copy.OwnerId.Should().Be(sut.Item5);
        copy.Size.Name.Should().Be(newSut.Item6);
        copy.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void When_UpdatePetSizeIsInvalid_Then_ShouldNotUpatePetSize()
    {
        Tuple<string, DateTime, string, string, Guid, string> sut = CreateSUT();
        var result = Pet.Create(sut.Item1, sut.Item2, sut.Item3, sut.Item4, sut.Item5, sut.Item6);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        Tuple<string, DateTime, string, string, Guid, string> newSut = new("Cleo", new DateTime(2022, 06, 04), "Fluffy Cat", "Romanian", Guid.NewGuid(), "medium");
        result.Value!.UpdateFields(newSut.Item1, newSut.Item2, newSut.Item3, newSut.Item4, newSut.Item5, newSut.Item6);

        var copy = result.Value;
        copy.Name.Should().Be(newSut.Item1);
        copy.DateOfBirth.Should().Be(newSut.Item2);
        copy.Species.Should().Be(newSut.Item3);
        copy.Breed.Should().Be(newSut.Item4);
        copy.OwnerId.Should().Be(newSut.Item5);
        copy.Size.Name.Should().Be(sut.Item6);
        copy.Id.Should().NotBeEmpty();
    }

    private static Tuple<string, DateTime, string, string, Guid, string> CreateSUT()
    {
        Tuple<string, DateTime, string, string, Guid, string> sut = new("CleoTheCat", new DateTime(2022, 06, 04), "Fluffy Cat", "Romanian", Guid.NewGuid(), "Medium");
        return sut;
    }
}
