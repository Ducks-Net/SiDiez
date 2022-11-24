using System.Net.Http.Json;
using System.Threading.Tasks;
using DucksNet.API.Controllers;
using DucksNet.API.DTO;

namespace DucksNet.API.Integration_Tests;
public class MedicalRecordsControllerTests : BaseIntegrationTests<MedicalRecordsController>
{
    private const string ApiURL = "api/medicalrecords";
    [Fact]
    public async Task When_CreatedMedicalRecord_Then_ShouldReturnMedicalRecordInTheGetRequest()
    {
        //Arrange
        //should get the an appointment and client using get and their id
        MedicalRecordDTO medicalRecordDTO = new MedicalRecordDTO
        {
            IdAppointment = System.Guid.NewGuid(),
            IdClient = System.Guid.NewGuid()
        };

        //Act
        var createShelterResponse = await TestingClient.PostAsJsonAsync(ApiURL, medicalRecordDTO);
        var getShelterResult = await TestingClient.GetAsync(ApiURL);

        //Assert
        createShelterResponse.EnsureSuccessStatusCode();
        createShelterResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
    }
}
