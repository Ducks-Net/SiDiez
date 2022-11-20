using DucksNet.SharedKernel.Utils;

public class ResultTests 
{
    [Fact]
    public void Result_ShouldBeSuccess_WhenConstructedByOk()
    {
        var result = Result.Ok();
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void Result_ShouldBeFailure_WhenConstructedByError()
    {
        string errrorMessage = "Error";
        var result = Result.Error(errrorMessage);
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain(errrorMessage);
    }

    [Fact]
    public void Result_ShouldContainAllErrors_When_MultipleErrorsAdded()
    {
        string[] errrorMessages = new string[] { "Error1", "Error2" };
        var result = Result.Error(errrorMessages[0]);
        result.AddError(errrorMessages[1]);
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain(errrorMessages);
    }

    [Fact]
    public void Result_ShouldBeSuccess_WhenConstructedByOkWithValue()
    {
        string value = "Result";
        var result = Result<string>.Ok(value);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(value);
    }

    [Fact]
    public void Result_ShouldBeFailure_WhenConstructedByErrorWithValue()
    {
        string errrorMessage = "Error";
        var result = Result<string>.Error(errrorMessage);
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain(errrorMessage);
        result.Value.Should().BeNull();
    }
}
