using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DucksNet.Domain.Model;
using FluentAssertions;
using Xunit;

namespace DucksNet.UnitTests;

public class MedicineTests
{
    [Fact]
    public void When_CreateMedicine_WithValidAdministration_Should_Succeed()
    {
        string administration = "Intramuscular";
        var result = Medicine.Create(administration);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.DrugAdministration.Name.Should().Be(administration);
    }

    [Fact]
    public void When_CreateMedicine_WitInvalidAdministration_Should_Fail()
    {
        string administration = "Invalid";
        var result = Medicine.Create(administration);
        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Failed to parse type of medicine administration.");
    }


}
