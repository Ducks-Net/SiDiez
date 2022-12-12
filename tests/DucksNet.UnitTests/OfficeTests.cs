using System;
using DucksNet.Domain.Model;


namespace DucksNet.UnitTests;

public class OfficeTests
{
    [Fact]
    public void When_OfficeCreated_Should_Succeed()
    {
        Guid dummyGUID = Guid.NewGuid();
        var result = Office.Create(dummyGUID, "Adresa", 10);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.BusinessId.Should().Be(dummyGUID);
        result.Value!.Address.Should().Be("Adresa");
        result.Value!.AnimalCapacity.Should().Be(10);
    }

    [Fact]
    public void When_OfficeCreatedWithEmptyAddress_Should_Fail()
    {
        Guid dummyGUID = Guid.NewGuid();
        string adress = string.Empty;
        var result = Office.Create(dummyGUID, adress, 10);

        result.IsFailure.Should().BeTrue();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Address is required");
    }
    [Fact]
    public void When_OfficeCreatedWithNegativeAnimalCapacity_Should_Fail()
    {
        Guid dummyGUID = Guid.NewGuid();
        var result = Office.Create(dummyGUID, "Adresa", -1);

        result.IsFailure.Should().BeTrue();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Animal capacity is required");
    }
    [Fact]
    public void When_OfficeCreatedWithZeroAnimalCapacity_Should_Fail()
    {
        Guid dummyGUID = Guid.NewGuid();
        var result = Office.Create(dummyGUID, "Adresa", 0);

        result.IsFailure.Should().BeTrue();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Animal capacity is required");
    }
    [Fact]
    public void When_OfficeCreatedWithEmptyBusinessId_Should_Fail()
    {
        Guid dummyGUID = Guid.NewGuid();
        var result = Office.Create(dummyGUID, "Adresa", 10);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.BusinessId.Should().Be(dummyGUID);
        result.Value!.Address.Should().Be("Adresa");
        result.Value!.AnimalCapacity.Should().Be(10);
    }
    
}
