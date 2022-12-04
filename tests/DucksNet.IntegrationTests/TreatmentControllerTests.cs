using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using DucksNet.API.Controllers;
using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using DucksNet.Domain.Model.Enums;
using DucksNet.Infrastructure.Prelude;

namespace DucksNet.IntegrationTests;
public class TreatmentControllerTests : BaseIntegrationTests<TreatmentController>
{
    private const string TreatmentUrl = "api/v1/treatment";

    [Fact]
    public async void When_CreatedTreatment_Then_ShouldReturnTreatmentByOwnerIDInTheGetRequest()
    {
        ClearDatabase();
        //Arrange
        var sut = CreateSUT();
        //Act 
        var treatmentResponse = await TestingClient.PostAsJsonAsync(TreatmentUrl, sut);
        var treatment = await treatmentResponse.Content.ReadFromJsonAsync<Treatment>();
        var getTreatmentResult = await TestingClient.GetAsync(TreatmentUrl + $"/byOwnerId/{treatment!.OwnerID}");
        //Assert
        treatmentResponse.EnsureSuccessStatusCode();

        var treatments = await getTreatmentResult.Content.ReadFromJsonAsync<List<Treatment>>();
        treatments.Should().NotBeNull();
        treatments!.Count.Should().Be(1);
        foreach (var trtm in treatments!)
        {
            trtm.OwnerID.Should().Be(sut.OwnerID);
            trtm.ClientID.Should().Be(sut.ClientID);
            trtm.ClinicID.Should().Be(sut.ClinicID);
        }
    }

    [Fact]
    public async void When_CreatedMedicine_Then_ShouldReturnMedicineByClientIDInTheGetRequest()
    {
        ClearDatabase();
        //Arrange
        var sut = CreateSUT();
        //Act 
        var treatmentResponse = await TestingClient.PostAsJsonAsync(TreatmentUrl, sut);
        var treatment = await treatmentResponse.Content.ReadFromJsonAsync<Treatment>();
        var getTreatmentResult = await TestingClient.GetAsync(TreatmentUrl + $"/byClientId/{treatment!.ClientID}");
        //Assert
        treatmentResponse.EnsureSuccessStatusCode();

        var treatments = await getTreatmentResult.Content.ReadFromJsonAsync<List<Treatment>>();
        treatments.Should().NotBeNull();
        treatments!.Count.Should().Be(1);
        foreach (var trtm in treatments!)
        {
            trtm.OwnerID.Should().Be(sut.OwnerID);
            trtm.ClientID.Should().Be(sut.ClientID);
            trtm.ClinicID.Should().Be(sut.ClinicID);
        }
    }

    [Fact]
    public async void When_CreatedMedicine_Then_ShouldReturnMedicineByClinicIDInTheGetRequest()
    {
        ClearDatabase();
        //Arrange
        var sut = CreateSUT();
        //Act 
        var treatmentResponse = await TestingClient.PostAsJsonAsync(TreatmentUrl, sut);
        var treatment = await treatmentResponse.Content.ReadFromJsonAsync<Treatment>();
        var getTreatmentResult = await TestingClient.GetAsync(TreatmentUrl + $"/byClinicId/{treatment!.ClinicID}");
        //Assert
        treatmentResponse.EnsureSuccessStatusCode();

        var treatments = await getTreatmentResult.Content.ReadFromJsonAsync<List<Treatment>>();
        treatments.Should().NotBeNull();
        treatments!.Count.Should().Be(1);
        foreach (var trtm in treatments!)
        {
            trtm.OwnerID.Should().Be(sut.OwnerID);
            trtm.ClientID.Should().Be(sut.ClientID);
            trtm.ClinicID.Should().Be(sut.ClinicID);
        }
    }

    [Fact]
    public async void When_CreatedTreatmentAndDeleteIt_Then_ShouldReturnSucces()
    {
        ClearDatabase();
        //Arrange
        var sut = CreateSUT();
        //Act 
        var treatmentResponse = await TestingClient.PostAsJsonAsync(TreatmentUrl, sut);
        var treatment = await treatmentResponse.Content.ReadFromJsonAsync<Treatment>();
        var getTreatmentResult = await TestingClient.DeleteAsync(TreatmentUrl + $"{treatment!.ID}");
        //Assert
        treatmentResponse.EnsureSuccessStatusCode();
        getTreatmentResult.Content.Headers.ContentLength.Should().Be(0);
    }

    [Fact]
    public async Task When_SearchingTreatmentThatDoesntExist_Then_ShouldReturnFail()
    {
        ClearDatabase();
        //Arrange
        Guid randomID = Guid.NewGuid();
        //Act 
        var getTreatmentResult = await TestingClient.DeleteAsync(TreatmentUrl + $"/{randomID}");
        //Assert
        getTreatmentResult.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    private static TreatmentDTO CreateSUT()
    {
        return new TreatmentDTO(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
    }
}
