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

}
