using System.Net.Http.Json;
using DucksNet.API.Controllers;
using DucksNet.API.DTO;

namespace DucksNet.API.Integration_Tests;
public class MedicalRecordsControllerTests : BaseIntegrationTests<MedicalRecordsController>
{
    private const string ApiURL = "api/medicalrecords";
    [Fact]
    public async void When_CreatedMedicalRecord_Then_ShouldReturnMedicalRecordInTheGetRequest()
    {
        //Arrange
        //should get the an appointment and client using get and their id
        MedicalRecordDTO medicalRecordDTO = new MedicalRecordDTO
        {
            IdAppointment = Guid.NewGuid(),
            IdClient = Guid.NewGuid()
        };

        //Act
        var createShelterResponse = await HttpClient.PostAsJsonAsync(ApiURL, medicalRecordDTO);
        var getShelterResult = await HttpClient.GetAsync(ApiURL);

        //Assert
        createShelterResponse.EnsureSuccessStatusCode();
        createShelterResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
    }
}
