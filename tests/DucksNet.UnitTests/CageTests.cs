using DucksNet.Domain.Model;
using System;

public class CageTests
{
    [Fact]
    public void When_CreateCage_WithValidSize_Should_Succeed()
    {
        string size = "Small";

        var result = Cage.Create(size);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.Size.Name.Should().Be(size);
    }

    [Fact]
    public void When_CreateCage_WithInvalidSize_ShouldFail()
    {
        string size = "Invalid";

        var result = Cage.Create(size);

        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Failed to parse cage size.");
    }

    [Fact]
    public void When_AssignedToLocation_Should_Succeed()
    {
        string size = "Small";

        var cageResult = Cage.Create(size);
        var locationGuid = new Guid();

        var cage = cageResult.Value;

        cage!.AssignToLocation(locationGuid);

        cage.LocationId.Should().NotBeNull();
        cage.LocationId!.Value.Should().Be(locationGuid);
    }
}
