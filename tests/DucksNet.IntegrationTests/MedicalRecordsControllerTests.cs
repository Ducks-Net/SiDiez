using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DucksNet.API.Controllers;
using DucksNet.API.DTO;
using DucksNet.Domain.Model;

namespace DucksNet.IntegrationTests;
public class MedicalRecordsControllerTests : BaseIntegrationTests<MedicalRecordsController>
{
    private const string MedicalRecordURL = "api/v1/medicalrecords";
    private const string AppointmentURL = "api/v1/appointments";
    private const string ClientURL = "api/v1/pets";
    [Fact]
    public async Task When_CreatedMedicalRecord_Then_ShouldReturnMedicalRecordInTheGetRequest()
    {
        //Arrange
        var idAppointment = Guid.NewGuid();
        var idClient = Guid.NewGuid();

        MedicalRecordDTO medicalRecordDTO = new MedicalRecordDTO(idAppointment, idClient);
        //Act
        var medicalRecordResponse = await TestingClient.PostAsJsonAsync(MedicalRecordURL, medicalRecordDTO);
        var medicalRecordResult = await TestingClient.GetAsync(MedicalRecordURL);
        //Assert
        medicalRecordResponse.EnsureSuccessStatusCode();
        var medicalRecords = await medicalRecordResult.Content.ReadFromJsonAsync<List<MedicalRecordDTO>>();
        medicalRecords.Should().NotBeNull();
        medicalRecords!.Count.Should().Be(1);
        foreach (var medicalRecord in medicalRecords!)
        {
            medicalRecord.IdAppointment.Should().Be(idAppointment);
            medicalRecord.IdClient.Should().Be(idClient);
        }
    }
    [Fact]
    public async Task When_CreatedMedicalRecordWithEmptyIdAppointment_Then_ShouldReturnFail()
    {
        //Arrange
        var idAppointment = Guid.Empty;
        var idClient = Guid.NewGuid();

        MedicalRecordDTO medicalRecordDTO = new MedicalRecordDTO(idAppointment, idClient);
        //Act
        var medicalRecordResponse = await TestingClient.PostAsJsonAsync(MedicalRecordURL, medicalRecordDTO);
        var medicalRecordResult = await TestingClient.GetAsync(MedicalRecordURL);
        //Assert
        medicalRecordResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    [Fact]
    public async Task When_CreatedMedicalRecordWithEmptyIdClient_Then_ShouldReturnFail()
    {
        //Arrange
        var idAppointment = Guid.NewGuid();
        var idClient = Guid.Empty;

        MedicalRecordDTO medicalRecordDTO = new MedicalRecordDTO(idAppointment, idClient);
        //Act
        var medicalRecordResponse = await TestingClient.PostAsJsonAsync(MedicalRecordURL, medicalRecordDTO);
        var medicalRecordResult = await TestingClient.GetAsync(MedicalRecordURL);
        //Assert
        medicalRecordResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    // TODO(RO) : after push of pet, can start here
}
