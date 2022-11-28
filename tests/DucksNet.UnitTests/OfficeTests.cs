using System;
using DucksNet.Domain.Model;

public class OfficeTests
{
    [Fact]
    public void When_CreateOffice_Should_Succeed()
    {
        var businessGuid = new Guid();
        var address = "Strada";
        var animalCapacity = 10;

        var result = Office.Create(businessGuid, address, animalCapacity);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.BusinessId.Should().Be(businessGuid);
        result.Value!.Address.Should().Be(address);
        result.Value!.AnimalCapacity.Should().Be(animalCapacity);
    }
    [Fact]
    public void When_CreateOffice_WithInvalidBusinessId_ShouldFail()
    {
        var businessGuid = new Guid();
        var address = "Strada";
        var animalCapacity = 10;

        var result = Office.Create(businessGuid, address, animalCapacity);

        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Business id is required");
    }
    [Fact]
    public void When_CreateOffice_WithInvalidAddress_ShouldFail()
    {
        var businessGuid = new Guid();
        var address = "Strada";
        var animalCapacity = 10;

        var result = Office.Create(businessGuid, address, animalCapacity);

        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Address is required");
    }
    [Fact]
    public void When_CreateOffice_WithInvalidAnimalCapacity_ShouldFail()
    {
        var businessGuid = new Guid();
        var address = "Strada";
        var animalCapacity = 10;

        var result = Office.Create(businessGuid, address, animalCapacity);

        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Animal capacity is required");
    }
    
        
}
