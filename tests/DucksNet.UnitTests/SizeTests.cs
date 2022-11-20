using DucksNet.Domain.Model.Enums;

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
}
