using Xunit;
using System;
using FluentAssertions;

using VetAppointment.Util;
using VetAppointment.Model;

public class SizeTests
{
    [Fact]
    public void When_GivenCorrectString_ShouldSucceedCreating()
    {

        string small = "Small";
        string medium = "Medium";
        string big = "Big";

        Result<Size> smallResult = SizeExtensions.FromString(small);
        Result<Size> mediumResult = SizeExtensions.FromString(medium);
        Result<Size> bigResult = SizeExtensions.FromString(big);

        smallResult.IsSuccess.Should().BeTrue();
        smallResult.IsFailure.Should().BeFalse();
        smallResult.Error.Should().BeNull();
        smallResult.Entity.Should().Be(Size.Small);

        mediumResult.IsSuccess.Should().BeTrue();
        mediumResult.IsFailure.Should().BeFalse();
        mediumResult.Error.Should().BeNull();
        mediumResult.Entity.Should().Be(Size.Medium);

        bigResult.IsSuccess.Should().BeTrue();
        bigResult.IsFailure.Should().BeFalse();
        bigResult.Error.Should().BeNull();
        bigResult.Entity.Should().Be(Size.Big);
    }

    [Fact]
    public void When_GivenWrongString_ShouldReturnErrorMessage()
    {
        string wrong = "Smoll";

        Result<Size> result = SizeExtensions.FromString(wrong);

        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Failed to parse 'Smoll' as Size. Possible values for Size are: [Small,Medium,Big]");
        result.Entity.Should().Be(default(Size));
    }
}