using System;
using DucksNet.Domain.Model;

public class BusinessTests
{
    //string businessName, string surname, string firstName, string address, string ownerPhone, string ownerEmail
    //test business creation
    [Fact]
    public void When_CreateBusiness_Should_Succeed()
    {
        var result = Business.Create("DucksNet", "Ion", "Ion1", "Strada", "123456789", "ion@e.com");
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.BusinessName.Should().Be("DucksNet");
        result.Value!.Surname.Should().Be("Ion");
        result.Value!.FirstName.Should().Be("Ion1");
        result.Value!.Address.Should().Be("Strada");
        result.Value!.OwnerPhone.Should().Be("123456789");
        result.Value!.OwnerEmail.Should().Be("ion@e.com");
    }
    [Fact]
    public void When_CreateBusiness_WithInvalidBusinessName_ShouldFail()
    {
        var result = Business.Create(null, "Ion", "Ion1", "Strada", "123456789", "ion@e.com");
        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Business name is required");
    }
    [Fact]
    public void When_CreateBusiness_WithInvalidSurname_ShouldFail()
    {
        var result = Business.Create("DucksNet", null, "Ion1", "Strada", "123456789", "ion@e,com");
        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Surname is required");
    }
    [Fact]
    public void When_CreateBusiness_WithInvalidFirstName_ShouldFail()
    {
        var result = Business.Create("DucksNet", "Ion", null, "Strada", "123456789", "ion@e.com");
        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("First name is required");
    }
    [Fact]
    public void When_CreateBusiness_WithInvalidAddress_ShouldFail()
    {
        var result = Business.Create("DucksNet", "Ion", "Ion1", null, "123456789", "ion@e.com");
        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Address is required");
    }
    [Fact]
    public void When_CreateBusiness_WithInvalidOwnerPhone_ShouldFail()
    {
        var result = Business.Create("DucksNet", "Ion", "Ion1", "Strada", null, "ion@e.coM");
        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Owner phone is required");
    }
    [Fact]
    public void When_CreateBusiness_WithInvalidOwnerEmail_ShouldFail()
    {
        var result = Business.Create("DucksNet", "Ion", "Ion1", "Strada", "123456789", null);
        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Owner email is required");
    }

}
