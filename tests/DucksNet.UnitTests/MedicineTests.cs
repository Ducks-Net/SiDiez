using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DucksNet.Domain.Model;
using DucksNet.Domain.Model.Enums;
using FluentAssertions;
using Xunit;

namespace DucksNet.UnitTests;

public class MedicineTests
{
    [Fact]
    public void When_CreateMedicine_WithValidAdministration_Should_Succeed()
    {
        string name = "Name";
        string description = "Description";
        double price = 1;
        string drugAdministration = "Oral";
        var result = Medicine.Create(name, description, price, drugAdministration);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.DrugAdministration.Name.Should().Be(drugAdministration);
    }

    [Fact]
    public void When_CreateMedicine_WitInvalidAdministration_Should_Fail()
    {
        string name = "Name";
        string description = "Description";
        double price = 1;
        string drugAdministration = "Invalid";
        var result = Medicine.Create(name, description, price, drugAdministration);
        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Failed to parse type of medicine administration.");
    }


}
