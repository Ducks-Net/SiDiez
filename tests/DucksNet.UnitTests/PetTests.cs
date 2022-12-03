using DucksNet.Domain.Model;
using System;

namespace DucksNet.UnitTests;
public class PetTests
{
    [Fact]
    public void When_CreatePet_Then_ShouldReturnPet()
    {
        Tuple<string, DateTime, string, string, Guid, string> sut = CreateSUT();
        var result = Pet.Create(sut.Item1, sut.Item2, sut.Item3, sut.Item4, sut.Item5, sut.Item6);
        var copy = result.Value;
        copy.Name.Should().Be(sut.Item1);
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

    private Tuple<string, DateTime, string, string, Guid, string> CreateSUT()
    {
        Tuple<string, DateTime, string, string, Guid, string> sut = new("Cleo", new DateTime(2021, 06, 04), "Cat", "European", Guid.NewGuid(), "Small");
        return sut;
    }
}
