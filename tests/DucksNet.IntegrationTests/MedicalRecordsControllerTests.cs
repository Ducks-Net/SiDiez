using System.Collections.Generic;
using System.Net.Http.Json;

using DucksNet.API.Controllers;
using DucksNet.API.DTO;

namespace DucksNet.IntegrationTests;
public class MedicalRecordsControllerTests : BaseIntegrationTests<MedicalRecordsController>
{
    private const string ApiURL = "api/v1/medicalrecords";
    /*
    [Fact]
    public async void When_CreatedMedicalRecord_Then_ShouldReturnMedicalRecordInTheGetRequest()
    {
        //Arrange
        var idAppointment = System.Guid.NewGuid();
        var idClient = System.Guid.NewGuid();
        MedicalRecordDTO medicalRecordDTO = new MedicalRecordDTO
        {
            IdAppointment = idAppointment,
            IdClient = idClient
        };

        //Act
        var createShelterResponse = await HttpClient.PostAsJsonAsync(ApiURL, medicalRecordDTO);
        var getMedicalRecord = await HttpClient.GetAsync(ApiURL);

        //Assert
        createShelterResponse.EnsureSuccessStatusCode();
        createShelterResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        getMedicalRecord.EnsureSuccessStatusCode();
        var medicalRecords = await getMedicalRecord.Content.ReadFromJsonAsync<List<MedicalRecordDTO>>();
        
        medicalRecords.Should().NotBeNull();
        if (medicalRecords is not null)
            medicalRecords.Count.Should().Be(1);
    }
    */
}
