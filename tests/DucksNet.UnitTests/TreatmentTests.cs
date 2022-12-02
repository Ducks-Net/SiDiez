using System;
using System.Collections.Generic;
using DucksNet.Domain.Model;

public class TreatmentTests
{
    [Fact]
    public void When_CreateTreatments_WithAtLeastOneMedicine_Should_Succeed()
    {
        List<Medicine> medicineList = new List<Medicine>();
        string name = "Name";
        string description = "Description";
        double price = 1;
        string administration = "Oral";
        Medicine medicine = new Medicine(name, description, price, administration);
        medicineList.Add(medicine);
        var result = Treatment.CreateTreatment(medicineList);
        result.Value!.AddMedicineToTreatment(medicine);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public void When_CreateTreatments_WithNoMedicine_Should_Fail()
    {
        List<Medicine> medicineList = new List<Medicine>();
        var result = Treatment.CreateTreatment(medicineList);
        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Can't create treatment with no medicine.");
    }
}
