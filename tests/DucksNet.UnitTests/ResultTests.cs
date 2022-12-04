using System;
using System.Linq;
using DucksNet.SharedKernel.Utils;

namespace DucksNet.UnitTests;

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
    public void Result_ShouldBeFailure_WhenConstructedByErrorList()
    {
        string[] errrorMessages = {"Error", "Another error"};
        var result = Result.ErrorList(errrorMessages.ToList());
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain(errrorMessages);
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
    public void AddResult_ShouldThrowException_When_AddingErrorToSuccessResult()
    {
        var result = Result.Ok();
        Action action = () => result.AddError("Error");
        action.Should().Throw<InvalidOperationException>();
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

    [Fact]
    public void Result_ShouldContainAllErrors_When_MultipleErrorsAddedWithValue()
    {
        string[] errrorMessages = new string[] { "Error1", "Error2" };
        var result = Result<string>.Error(errrorMessages[0]);
        result.AddError(errrorMessages[1]);
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain(errrorMessages);
        result.Value.Should().BeNull();
    }

    [Fact]
    public void Result_ShouldBeFailure_WhenConstructedByErrorListWithValue()
    {
        string[] errrorMessages = new string[] { "Error1", "Error2" };
        var result = Result<string>.ErrorList(errrorMessages.ToList());
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain(errrorMessages);
        result.Value.Should().BeNull();
    }

    [Fact]
    public void CreateFromError_ShouldContainAllErrros_WhenConstructedByErrorListWithValue()
    {
        string[] errrorMessages = new string[] { "Error1", "Error2" };
        var result = Result<string>.ErrorList(errrorMessages.ToList());
        var result2 = Result<int>.FromError(result, "ExtraError");
        result2.IsFailure.Should().BeTrue();
        result2.Errors.Should().Contain(errrorMessages);
        result2.Errors.Should().Contain("ExtraError");
        result2.Value.Should().Be(0);
    }

    [Fact]
    public void CreateFromError_ShouldThrow_WhenConstructedByOkWithValue()
    {
        string value = "Result";
        var result = Result<string>.Ok(value);
        Action action = () => Result<int>.FromError(result, "ExtraError");
        action.Should().Throw<InvalidOperationException>();
    }
}
