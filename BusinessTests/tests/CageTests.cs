using Xunit;
using System;
using FluentAssertions;

using VetAppointment.Model;
using VetAppointment.Util;

public class CageTests
{
    [Fact]
    public void When_GivenProperSize_Should_ReturnCage()
    {
        string small = "Small";
        Result<Cage> cageResult = Cage.Create(small);

        cageResult.IsSuccess.Should().BeTrue();
        cageResult.IsFailure.Should().BeFalse();
        cageResult.Error.Should().BeNull();

        cageResult.Entity.Should().NotBeNull();
        cageResult.Entity!.Size.Should().Be(Size.Small);
    }

    [Fact]
    public void When_GivenBadSize_Should_ReturnError()
    {
        string wrong = "Smoll";
        Result<Cage> cageResult = Cage.Create(wrong);

        cageResult.IsSuccess.Should().BeFalse();
        cageResult.IsFailure.Should().BeTrue();
        cageResult.Error.Should().Be("Failed to create Cage with size 'Smoll'. Failed to parse 'Smoll' as Size. Possible values for Size are: [Small,Medium,Big]");
        cageResult.Entity.Should().Be(default(Cage));
    }

    [Fact]
    public void Wehen_AssignedToClinic_Should_HavePropperClinicID()
    {
        string small = "Small";
        Result<Cage> cageResult = Cage.Create(small);
        Cage cage = cageResult.Entity!;

        Guid fauxClinicID = new Guid();
        cage.AssignToClinic(fauxClinicID);

        cage.clinicId.Should().Be(fauxClinicID);
    }

}