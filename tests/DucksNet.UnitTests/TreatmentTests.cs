using Xunit;
using FluentAssertions;

using DucksNet.Domain.Model;
using System;
using System.Collections.Generic;
using DucksNet.Domain.Model.Enums;

public class TreatmentTests
{
    [Fact]
    public void When_CreateTreatments_WithAtLeastOneMedicine_Should_Succeed()
    {
        var listOfMedicine = new List<Medicine>();
        string name = "Name";
        string description = "Description";
        double price = 1;
        DrugAdministration drugAdministration = new DrugAdministration(1,"Oral");
        var medicine = new Medicine(name, description, price, drugAdministration);
        listOfMedicine.Add(medicine);
        var result = Treatment.CreateTreatment(listOfMedicine);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public void When_CreateTreatments_WithNoMedicine_Should_Fail()
    {
        var listOfMedicine = new List<Medicine>();
        var result = Treatment.CreateTreatment(listOfMedicine);

        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Cannot create treatment as the medicine list is empty.");
    }
}
