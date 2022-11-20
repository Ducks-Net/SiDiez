using Xunit;
using System;
using FluentAssertions;

using DucksNet.Domain.Model;

public class CageTimeBlockTests
{
    [Fact]
    public void When_CreateCageTimeBlock_WithValidTime_Should_Succeed()
    {
        DateTime start = new DateTime(2021, 1, 1, 0, 0, 0);
        DateTime end = new DateTime(2021, 1, 1, 1, 0, 0);

        var result = CageTimeBlock.Create(start, end);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.Start.Should().Be(start);
        result.Value!.End.Should().Be(end);
    }

    [Fact]
    public void When_CreateCageTimeBlock_WithIvalidTime_Should_Fail()
    {
        DateTime start = new DateTime(2021, 1, 1, 0, 0, 0);
        DateTime end = new DateTime(2021, 1, 1, 0, 0, 0);

        var result = CageTimeBlock.Create(start, end);

        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Time block start time must be before end time.");
    }

    [Fact]
    public void When_AssignToCage_Should_Succeed()
    {
        DateTime start = new DateTime(2021, 1, 1, 0, 0, 0);
        DateTime end = new DateTime(2021, 1, 1, 1, 0, 0);

        var cageTimeBlockResult = CageTimeBlock.Create(start, end);
        var cageGuid = new Guid();

        var cageTimeBlock = cageTimeBlockResult.Value;

        cageTimeBlock!.AssignToCage(cageGuid);

        cageTimeBlock.CageId.Should().NotBeNull();
        cageTimeBlock.CageId!.Value.Should().Be(cageGuid);
    }

    [Fact]
    public void When_AssignedToOwner_Should_Succeed()
    {
        DateTime start = new DateTime(2021, 1, 1, 0, 0, 0);
        DateTime end = new DateTime(2021, 1, 1, 1, 0, 0);

        var cageTimeBlockResult = CageTimeBlock.Create(start, end);
        var ownerGuid = new Guid();

        var cageTimeBlock = cageTimeBlockResult.Value;

        cageTimeBlock!.AssignToOccupant(ownerGuid);

        cageTimeBlock.OccupantId.Should().NotBeNull();
        cageTimeBlock.OccupantId!.Value.Should().Be(ownerGuid);
    }
}
