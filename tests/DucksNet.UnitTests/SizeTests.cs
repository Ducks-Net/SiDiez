using DucksNet.Domain.Model.Enums;


namespace DucksNet.UnitTests;

public class SizeTests
{
    [Fact]
    public void When_CreateFromString_WithValidSize_Should_Succeed()
    {
        string size = "Small";

        var result = Size.CreateFromString(size);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.Name.Should().Be(size);
    }

    [Fact]
    public void When_CreateFromString_WithInvalidSize_should_Fail()
    {
        string size = "Invalid";

        var result = Size.CreateFromString(size);

        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Invalid size string. Valid values are: Small, Medium, Large.");
    }

    [Fact]
    public void When_CreateFromInt_WithValidSize_Should_Succeed()
    {
        Size size = Size.Small;

        var result = Size.CreateFromInt(size.Id);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.Should().Be(size);
    }

    [Fact]
    public void When_CreateFromInt_WithInvalidSize_should_Fail()
    {
        int size = 4;

        var result = Size.CreateFromInt(size);

        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Invalid size id. Valid values are: 1, 2, 3.");
    }
}
