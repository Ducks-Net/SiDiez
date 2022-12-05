using System;
using DucksNet.Domain.Model;
using DucksNet.Domain.Model.Enums;

namespace DucksNet.UnitTests;

public class MedicineTests
{
    [Fact]
    public void When_CreateMedicineWithEmptyName_Then_ShouldFail()
    {
        var result = Medicine.Create("", "Flu", 18, "Oral");
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("The name should contain at least one character.");
    }

    [Fact]
    public void When_CreateMedicineWithEmptyDescription_Then_ShouldFail()
    {
        var result = Medicine.Create("Paracetamol", "", 18, "Oral");
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("The description should contain at least one character.");
    }

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
    public void When_CreateMedicine_WithValidAdministrationByInt_Should_Succeed()
    {
        int drugAdministration = 1;
        var result = DrugAdministration.createMedicineByInt(drugAdministration);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }
    [Fact]
    public void When_CreateMedicine_WithInvalidAdministrationByInt_Should_Fail()
    {
        int drugAdministration = 174;
        var result = DrugAdministration.createMedicineByInt(drugAdministration);
        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Wrong type of drug administration");
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

    [Fact]
    public void When_UpdateNameIsNotValid_Then_ShouldNotUpateMedicineName()
    {
        Tuple<string, string, double, string> sut = CreateSUT();
        var result = Medicine.Create(sut.Item1, sut.Item2, sut.Item3, sut.Item4);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        Tuple<string, string, double, string> newSut = new("", "Flu", 30, "Oral");
        var resultMedicine = result.Value!.UpdateMedicineFields(newSut.Item1, newSut.Item2, newSut.Item3, newSut.Item4);
        resultMedicine.Errors.Should().Contain("Name is empty.");

        var copy = result.Value;
        copy.Name.Should().Be(sut.Item1);
        copy.Description.Should().Be(newSut.Item2);
        copy.Price.Should().Be(newSut.Item3);
        copy.DrugAdministration.Name.Should().Be(newSut.Item4);
        copy.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void When_UpdateDescriptionIsNotValid_Then_ShouldNotUpateMedicineDescription()
    {
        Tuple<string, string, double, string> sut = CreateSUT();
        var result = Medicine.Create(sut.Item1, sut.Item2, sut.Item3, sut.Item4);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        Tuple<string, string, double, string> newSut = new("Ibuprofen", "", 30, "Oral");
        var resultMedicine = result.Value!.UpdateMedicineFields(newSut.Item1, newSut.Item2, newSut.Item3, newSut.Item4);
        resultMedicine.Errors.Should().Contain("Description is empty.");

        var copy = result.Value;
        copy.Name.Should().Be(newSut.Item1);
        copy.Description.Should().Be(sut.Item2);
        copy.Price.Should().Be(newSut.Item3);
        copy.DrugAdministration.Name.Should().Be(newSut.Item4);
        copy.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void When_UpdatePriceIsNotValid_Then_ShouldNotUpateMedicinePrice()
    {
        Tuple<string, string, double, string> sut = CreateSUT();
        var result = Medicine.Create(sut.Item1, sut.Item2, sut.Item3, sut.Item4);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        Tuple<string, string, double, string> newSut = new("Ibuprofen", "Flu", -5, "Oral");
        var resultMedicine = result.Value!.UpdateMedicineFields(newSut.Item1, newSut.Item2, newSut.Item3, newSut.Item4);
        resultMedicine.Errors.Should().Contain("Invalid price.");

        var copy = result.Value;
        copy.Name.Should().Be(newSut.Item1);
        copy.Description.Should().Be(newSut.Item2);
        copy.Price.Should().Be(sut.Item3);
        copy.DrugAdministration.Name.Should().Be(newSut.Item4);
        copy.Id.Should().NotBeEmpty();
    }
    [Fact]
    public void When_AllInformationUpdatedInMedicine_Then_ShouldReturnUpdatedMedicine()
    {
        Tuple<string,string, double, string> sut = CreateSUT();
        var result = Medicine.Create(sut.Item1, sut.Item2, sut.Item3, sut.Item4);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        Tuple<string, string, double, string> newSut = new("Ibuprofen", "Headache", 20, "Intradermal");
        result.Value!.UpdateMedicineFields(newSut.Item1, newSut.Item2, newSut.Item3, newSut.Item4);

        var copy = result.Value;
        copy.Name.Should().Be(newSut.Item1);
        copy.Description.Should().Be(newSut.Item2);
        copy.Price.Should().Be(newSut.Item3);
        copy.DrugAdministration.Name.Should().Be(newSut.Item4);
        copy.Id.Should().NotBeEmpty();
    }
    [Fact]
    public void When_UpdateDrugAdministrationIsNotValid_Then_ShouldNotUpateMedicineDrugAdministration()
    {
        Tuple<string, string, double, string> sut = CreateSUT();
        var result = Medicine.Create(sut.Item1, sut.Item2, sut.Item3, sut.Item4);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        Tuple<string, string, double, string> newSut = new("Ibuprofen", "Flu", 30, "Invalid");
        var resultMedicine = result.Value!.UpdateMedicineFields(newSut.Item1, newSut.Item2, newSut.Item3, newSut.Item4);

        resultMedicine.Errors.Should().Contain("Invalid type of drug administration.");
        var copy = result.Value;
        copy.Name.Should().Be(newSut.Item1);
        copy.Description.Should().Be(newSut.Item2);
        copy.Price.Should().Be(newSut.Item3);
        copy.DrugAdministration.Name.Should().Be(sut.Item4);
        copy.Id.Should().NotBeEmpty();
    }
    private static Tuple<string, string, double, string> CreateSUT()
    {
        Tuple<string, string, double, string> sut = new("Naldorex", "Flu", 30, "Oral");
        return sut;
    }

}
