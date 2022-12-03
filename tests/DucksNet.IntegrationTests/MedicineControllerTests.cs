using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using DucksNet.API.Controllers;
using DucksNet.API.DTO;
using DucksNet.Domain.Model;

namespace DucksNet.IntegrationTests;
public class MedicineControllerTests : BaseIntegrationTests<MedicineController>
{
    private const string MedicineUrl = "api/v1/medicine";

    [Fact]
    public async void When_CreatedMedicine_Then_ShouldReturnMedicineInTheGetRequest()
    {
        //Arrange
        var sut = CreateSUT();
        //Act 
        var medicineResponse = await TestingClient.PostAsJsonAsync(MedicineUrl, sut);
        Console.WriteLine(medicineResponse.Content.ReadAsStringAsync().Result);
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
        //Arrange
        var sut = CreateSUT();
        //Act 
        var medicineResponse = await TestingClient.PostAsJsonAsync(MedicineUrl, sut);
        var medicine = await medicineResponse.Content.ReadFromJsonAsync<Medicine>();
        var getMedicineResult = await TestingClient.GetAsync(MedicineUrl + $"/byName/{medicine!.Name}");
        //Assert
        medicineResponse.EnsureSuccessStatusCode();

        var medicines = await getMedicineResult.Content.ReadFromJsonAsync<List<MedicineDTO>>();
        medicines.Should().NotBeNull();
        medicines!.Count.Should().Be(1);
        foreach (var med in medicines!)
        {
            med.Name.Should().Be(sut.Name);
            med.Description.Should().Be(sut.Description);
            med.Price.Should().Be(sut.Price);
            med.DrugAdministrationString.Should().Be(sut.DrugAdministrationString);
        }
    }

    [Fact]
    public async void When_CreatedMedicine_Then_ShouldReturnMedicineByDescriptionInTheGetRequest()
    {
        //Arrange
        var sut = CreateSUT();
        //Act 
        var medicineResponse = await TestingClient.PostAsJsonAsync(MedicineUrl, sut);
        var medicine = await medicineResponse.Content.ReadFromJsonAsync<Medicine>();
        var getMedicineResult = await TestingClient.GetAsync(MedicineUrl + $"/byDescription/{medicine!.Description}");
        //Assert
        medicineResponse.EnsureSuccessStatusCode();
        Console.WriteLine(medicineResponse.Content.ReadAsStringAsync().Result);
        var medicines = await getMedicineResult.Content.ReadFromJsonAsync<List<MedicineDTO>>();
        medicines.Should().NotBeNull();
        medicines!.Count.Should().Be(1);
        foreach (var med in medicines!)
        {
            med.Name.Should().Be(sut.Name);
            med.Description.Should().Be(sut.Description);
            med.Price.Should().Be(sut.Price);
            med.DrugAdministrationString.Should().Be(sut.DrugAdministrationString);
        }
    }

    [Fact]
    public async void When_CreatedMedicine_Then_ShouldReturnMedicineByDrugAdministrationInTheGetRequest()
    {
        //Arrange
        var sut = CreateSUT();
        //Act 
        var medicineResponse = await TestingClient.PostAsJsonAsync(MedicineUrl, sut);
        var medicine = await medicineResponse.Content.ReadFromJsonAsync<Medicine>();
        var getMedicineResult = await TestingClient.GetAsync(MedicineUrl + $"/byDrugAdministration/{medicine!.DrugAdministration}");
        //Assert
        medicineResponse.EnsureSuccessStatusCode();

        var medicines = await getMedicineResult.Content.ReadFromJsonAsync<List<MedicineDTO>>();
        medicines.Should().NotBeNull();
        medicines!.Count.Should().Be(1);
        foreach (var med in medicines!)
        {
            med.Name.Should().Be(sut.Name);
            med.Description.Should().Be(sut.Description);
            med.Price.Should().Be(sut.Price);
            med.DrugAdministrationString.Should().Be(sut.DrugAdministrationString);
        }
    }

    [Fact]
    public async void When_CreatedMedicineAndDeleteIt_Then_ShouldReturnSucces()
    {
        //Arrange
        var sut = CreateSUT();
        //Act 
        var medicineResponse = await TestingClient.PostAsJsonAsync(MedicineUrl, sut);
        var medicine = await medicineResponse.Content.ReadFromJsonAsync<Medicine>();
        var getMedicineResult = await TestingClient.DeleteAsync(MedicineUrl + $"/{medicine!.Id}");
        //Assert
        medicineResponse.EnsureSuccessStatusCode();
        getMedicineResult.Content.Headers.ContentLength.Should().Be(0);
    }

    private static MedicineDTO CreateSUT()
    {
        return new MedicineDTO("Nurofen", "Flu", 30, "Oral");
    }
}
