using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DucksNet.API.Controllers;
using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using DucksNet.Domain.Model.Enums;

namespace DucksNet.IntegrationTests;
public class MedicineControllerTests : BaseIntegrationTests<MedicineController>
{
    private const string MedicineUrl = "api/v1/medicine";

    [Fact]
    public async void When_CreatedMedicine_Then_ShouldReturnMedicineInTheGetRequest()
    {
        ClearDatabase();
        //Arrange
        var sut = CreateSut();
        //Act 
        var medicineResponse = await TestingClient.PostAsJsonAsync(MedicineUrl, sut);
        medicineResponse.EnsureSuccessStatusCode();
        var getMedicineResult = await TestingClient.GetAsync(MedicineUrl);
        //Assert

        var medicines = await getMedicineResult.Content.ReadFromJsonAsync<List<Medicine>>();
        medicines.Should().NotBeNull();
        medicines!.Count.Should().Be(1);
        foreach (var medicine in medicines!)
        {
            medicine.Name.Should().Be(sut.Name);
            medicine.Description.Should().Be(sut.Description);
            medicine.Price.Should().Be(sut.Price);
            medicine.DrugAdministration.Name.Should().Be(sut.DrugAdministrationString);
        }
    }

    [Fact]
    public async void When_CreatedMedicine_Then_ShouldReturnMedicineByNameInTheGetRequest()
    {
        ClearDatabase();
        //Arrange
        var sut = CreateSut();
        //Act 
        var medicineResponse = await TestingClient.PostAsJsonAsync(MedicineUrl, sut);
        var medicine = await medicineResponse.Content.ReadFromJsonAsync<Medicine>();
        var getMedicineResult = await TestingClient.GetAsync(MedicineUrl + $"/byName/{medicine!.Name}");
        //Assert
        medicineResponse.EnsureSuccessStatusCode();

        var medicines = await getMedicineResult.Content.ReadFromJsonAsync<List<Medicine>>();
        medicines.Should().NotBeNull();
        medicines!.Count.Should().Be(1);
        foreach (var med in medicines!)
        {
            med.Name.Should().Be(sut.Name);
            med.Description.Should().Be(sut.Description);
            med.Price.Should().Be(sut.Price);
            med.DrugAdministration.Name.Should().Be(sut.DrugAdministrationString);
        }
    }

    [Fact]
    public async void When_CreatedMedicine_Then_ShouldReturnMedicineByDescriptionInTheGetRequest()
    {
        ClearDatabase();
        //Arrange
        var sut = CreateSut();
        //Act 
        var medicineResponse = await TestingClient.PostAsJsonAsync(MedicineUrl, sut);
        var medicine = await medicineResponse.Content.ReadFromJsonAsync<Medicine>();
        var getMedicineResult = await TestingClient.GetAsync(MedicineUrl + $"/byDescription/{medicine!.Description}");
        //Assert
        medicineResponse.EnsureSuccessStatusCode();
        var medicines = await getMedicineResult.Content.ReadFromJsonAsync<List<Medicine>>();
        medicines.Should().NotBeNull();
        medicines!.Count.Should().Be(1);
        foreach (var med in medicines!)
        {
            med.Name.Should().Be(sut.Name);
            med.Description.Should().Be(sut.Description);
            med.Price.Should().Be(sut.Price);
            med.DrugAdministration.Name.Should().Be(sut.DrugAdministrationString);
        }
    }

    [Fact]
    public async void When_CreatedMedicine_Then_ShouldReturnMedicineByDrugAdministrationInTheGetRequest()
    {
        ClearDatabase();
        //Arrange
        var sut = CreateSut();
        //Act 
        var medicineResponse = await TestingClient.PostAsJsonAsync(MedicineUrl, sut);
        medicineResponse.EnsureSuccessStatusCode();
        var medicine = await medicineResponse.Content.ReadFromJsonAsync<Medicine>();
        var getMedicineResult = await TestingClient.GetAsync(MedicineUrl + $"/byDrugAdministration/{medicine!.DrugAdministration.Name}");
        //Assert

        var medicines = await getMedicineResult.Content.ReadFromJsonAsync<List<Medicine>>();
        medicines.Should().NotBeNull();
        medicines!.Count.Should().Be(1);
        foreach (var med in medicines!)
        {
            med.Name.Should().Be(sut.Name);
            med.Description.Should().Be(sut.Description);
            med.Price.Should().Be(sut.Price);
            med.DrugAdministration.Name.Should().Be(sut.DrugAdministrationString);
        }
    }

    [Fact]
    public async Task When_CreatedMedicineAndDeleteIt_Then_ShouldReturnSucces()
    {
        ClearDatabase();
        //Arrange
        var sut = CreateSut();
        //Act 
        var medicineResponse = await TestingClient.PostAsJsonAsync(MedicineUrl, sut);
        var medicine = await medicineResponse.Content.ReadFromJsonAsync<Medicine>();
        var getMedicineResult = await TestingClient.DeleteAsync(MedicineUrl + $"/{medicine!.Id}");
        //Assert
        medicineResponse.EnsureSuccessStatusCode();
        getMedicineResult.Content.Headers.ContentLength.Should().Be(0);
    }

    [Fact]
    public async Task When_CreatedMedicineWithInvalidPrice_Then_ShouldReturnFail()
    {
        ClearDatabase();
        //Arrange
        Guid id = Guid.NewGuid();
        Medicine medicine = new Medicine(id, "Naldorex", "Migraine", -999, DrugAdministration.Oral);
        //Act
        var medicineResponse = await TestingClient.PostAsJsonAsync(MedicineUrl, medicine);
        //Assert
        medicineResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task When_SearchingMedicineThatDoesntExist_Then_ShouldReturnFail()
    {
        ClearDatabase();
        //Arrange
        Guid randomId= Guid.NewGuid();
        //Act 
        var getMedicineResult = await TestingClient.DeleteAsync(MedicineUrl + $"/{randomId}");
        //Assert
        getMedicineResult.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    private static MedicineDTO CreateSut()
    {
        return new MedicineDTO("Nurofen", "Flu", 30, "Oral");
    }
}
