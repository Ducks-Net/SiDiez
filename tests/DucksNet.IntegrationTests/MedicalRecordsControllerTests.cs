using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DucksNet.API.Controllers;
using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using DucksNet.IntegrationTests;

namespace DucksNet.API.Integration_Tests;
public class MedicalRecordsControllerTests : BaseIntegrationTests<MedicalRecordsController>
{
    private const string ApiURL = "api/v1/medicalrecords";
    [Fact]
    public async Task When_CreatedMedicalRecord_Then_ShouldReturnMedicalRecordInTheGetRequest()
    {
        //Arrange
        var idAppointment = Guid.NewGuid();
        var idClient = Guid.NewGuid();

        MedicalRecordDTO medicalRecordDTO = new MedicalRecordDTO(idAppointment, idClient);
        //Act
        var medicalRecordResponse = await TestingClient.PostAsJsonAsync(ApiURL, medicalRecordDTO);
        var medicalRecordResult = await TestingClient.GetAsync(ApiURL);
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
        var medicalRecordResponse = await TestingClient.PostAsJsonAsync(ApiURL, medicalRecordDTO);
        var medicalRecordResult = await TestingClient.GetAsync(ApiURL);
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
        var medicalRecordResponse = await TestingClient.PostAsJsonAsync(ApiURL, medicalRecordDTO);
        var medicalRecordResult = await TestingClient.GetAsync(ApiURL);
        //Assert
        medicalRecordResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
