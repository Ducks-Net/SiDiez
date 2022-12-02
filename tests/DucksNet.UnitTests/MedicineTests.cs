﻿using DucksNet.Domain.Model;

namespace DucksNet.UnitTests;

public class MedicineTests
{
    [Fact]
    public void When_CreateMedicine_WithValidAdministration_Should_Succeed()
    {
        string name = "Name";
        string description = "Description";
        double price = 1;
        string drugAdministration = "Inhalation";
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
        result.Errors.Should().Contain("Failed to parse type of medicine administration by string.");
    }
    
    [Fact]
    public void When_CreateMedicine_WitInvalidPrice_Should_Fail()
    {
        string name = "Name";
        string description = "Description";
        double price = -1; 
        string drugAdministration = "Subcutaneous";
        var result = Medicine.Create(name, description, price, drugAdministration);
        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Invalid price");
    }

    [Fact]
    public void When_CreateMedicine_WithValidPrice_Should_Succeed()
    {
        string name = "Name";
        string description = "Description";
        double price = 5; 
        string drugAdministration = "Subcutaneous";
        var result = Medicine.Create(name, description, price, drugAdministration);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Errors.Should().BeEmpty();
    }
}
