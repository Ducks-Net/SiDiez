using System;
using DucksNet.Domain.Model;

namespace DucksNet.UnitTests;

public class BusinessTests
{
    [Fact]
    public void When_CreateBusiness_Should_Succeed()
    {
        var result = Business.Create("DucksNet", "Ion", "Ion1", "Strada", "0723456789", "ion@e.com");
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.BusinessName.Should().Be("DucksNet");
        result.Value!.Surname.Should().Be("Ion");
        result.Value!.FirstName.Should().Be("Ion1");
        result.Value!.Address.Should().Be("Strada");
        result.Value!.OwnerPhone.Should().Be("0723456789");
        result.Value!.OwnerEmail.Should().Be("ion@e.com");
    }
    [Fact]
    public void When_CreateBusiness_WithInvalidBusinessName_ShouldFail()
    {
        var result = Business.Create(string.Empty, "Ion", "Ion1", "Strada", "0723456789", "ion@e.com");
        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Business name is required");
    }
    [Fact]
    public void When_CreateBusiness_WithInvalidSurname_ShouldFail()
    {
        var result = Business.Create("DucksNet", string.Empty, "Ion1", "Strada", "0723456789", "ion@e,com");
        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Surname is required");
    }
    [Fact]
    public void When_CreateBusiness_WithInvalidFirstName_ShouldFail()
    {
        var result = Business.Create("DucksNet", "Ion", string.Empty, "Strada", "0723456789", "ion@e.com");
        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("First name is required");
    }
    [Fact]
    public void When_CreateBusiness_WithInvalidAddress_ShouldFail()
    {
        var result = Business.Create("DucksNet", "Ion", "Ion1", string.Empty, "0723456789", "ion@e.com");
        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Address is required");
    }
    [Fact]
    public void When_CreateBusiness_WithInvalidOwnerPhone_ShouldFail()
    {
        var result = Business.Create("DucksNet", "Ion", "Ion1", "Strada", string.Empty, "ion@e.coM");
        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Owner phone is required");
    }
    [Fact]
    public void When_CreateBusiness_WithInvaliOwnerPhone_ShouldFail_2()
    {
        var result = Business.Create("DucksNet", "Ion", "Ion1", "Strada", "072345678", "ion@e.com");
        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Phone number must be 10 digits");
    }
    [Fact]
    public void When_CreateBusiness_WithInvaliOwnerPhone_ShouldFail_3()
    {
        var result = Business.Create("DucksNet", "Ion", "Ion1", "Strada", "07a345678", "ion@e.com");
        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Phone number must be numeric");
    }
    [Fact]
    public void When_CreateBusiness_WithInvalidOwnerEmail_ShouldFail()
    {
        var result = Business.Create("DucksNet", "Ion", "Ion1", "Strada", "0723456789", string.Empty);
        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Owner email is required");
    }
    [Fact]
    public void When_CreateBusiness_WithInvalidOwnerEmail_ShouldFail_2()
    {
        var result = Business.Create("DucksNet", "Ion", "Ion1", "Strada", "0723456789", "ion@e");
        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Owner email is invalid");
    }
}
