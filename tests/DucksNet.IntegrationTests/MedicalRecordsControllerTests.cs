using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DucksNet.API.Controllers;
using DucksNet.API.DTO;
using DucksNet.Application.Responses;
using DucksNet.Domain.Model;
using DucksNet.Domain.Model.Enums;

namespace DucksNet.IntegrationTests;
public class MedicalRecordsControllerTests : BaseIntegrationTests<MedicalRecordsController>
{
    private const string MedicalRecordUrl = "api/v1/medicalrecords";
    private const string AppointmentUrl = "api/v1/appointments";
    private const string ClientUrl = "api/v1/pets";
    private const string OfficesUrl = "api/v1/office";
    private const string PetsUrl = "api/v1/pets";

    [Fact]
    public async Task When_CreatedMedicalRecord_Then_ShouldReturnMedicalRecordInTheGetRequest()
    {
        //Arrange
        var petId = await SetupPet(Guid.NewGuid(), Size.Medium.Name);
        var officeId = await SetupOffice();
        var idAppointment = await SetupAppointment(petId, officeId);

        MedicalRecordDto medicalRecordDto = new MedicalRecordDto(idAppointment, petId);
        //Act
        var medicalRecordResponse = await TestingClient.PostAsJsonAsync(MedicalRecordUrl, medicalRecordDto);
        var medicalRecordResult = await TestingClient.GetAsync(MedicalRecordUrl);

        //Assert
        medicalRecordResponse.EnsureSuccessStatusCode();
        var medicalRecords = await medicalRecordResult.Content.ReadFromJsonAsync<List<MedicalRecordDto>>();
        medicalRecords.Should().NotBeNull();
        medicalRecords!.Count.Should().Be(1);
        foreach (var medicalRecord in medicalRecords!)
        {
            medicalRecord.IdAppointment.Should().Be(idAppointment);
            medicalRecord.IdClient.Should().Be(petId);
        }
    }

    [Fact]
    public async Task When_CreatedMedicalRecordWithEmptyIdAppointment_Then_ShouldReturnFail()
    {
        //Arrange
        var idAppointment = Guid.Empty;
        var idClient = Guid.NewGuid();

        MedicalRecordDto medicalRecordDto = new MedicalRecordDto(idAppointment, idClient);
        //Act
        var medicalRecordResponse = await TestingClient.PostAsJsonAsync(MedicalRecordUrl, medicalRecordDto);
        //Assert
        medicalRecordResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await medicalRecordResponse.Content.ReadAsStringAsync();
        errors.Should().NotBeEmpty();
        errors.Should().Contain("Entity of type Appointment was not found.");

    }
    [Fact]
    public async Task When_CreatedMedicalRecordWithEmptyIdClient_Then_ShouldReturnFail()
    {
        //Arrange
        var petId = await SetupPet(Guid.NewGuid(), Size.Medium.Name);
        var officeId = await SetupOffice();
        var idAppointment = await SetupAppointment(petId, officeId);
        petId = Guid.NewGuid();

        MedicalRecordDto medicalRecordDto = new MedicalRecordDto(idAppointment, petId);
        //Act
        var medicalRecordResponse = await TestingClient.PostAsJsonAsync(MedicalRecordUrl, medicalRecordDto);
        //Assert
        medicalRecordResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await medicalRecordResponse.Content.ReadAsStringAsync();
        errors.Should().NotBeEmpty();
        errors.Should().Contain("Entity of type Pet was not found.");
    }
    [Fact]
    public async Task When_CreatedMedicalRecordAndGetById_Then_ShoulSuccess()
    {
        //Arrange
        var petId = await SetupPet(Guid.NewGuid(), Size.Medium.Name);
        var officeId = await SetupOffice();
        var idAppointment = await SetupAppointment(petId, officeId);

        MedicalRecordDto medicalRecordDto = new MedicalRecordDto(idAppointment, petId);
        //Act
        var medicalRecordResponse = await TestingClient.PostAsJsonAsync(MedicalRecordUrl, medicalRecordDto);
        medicalRecordResponse.EnsureSuccessStatusCode();
        var medicalRecord = await medicalRecordResponse.Content.ReadFromJsonAsync<MedicalRecord>();
        var getMedicalRecordResponse = await TestingClient.GetAsync(MedicalRecordUrl + $"/{medicalRecord!.Id}");
        //Assert
        getMedicalRecordResponse.EnsureSuccessStatusCode();
        var getMedicalRecord = await getMedicalRecordResponse.Content.ReadFromJsonAsync<MedicalRecord>();
        medicalRecord.Id.Should().Be(getMedicalRecord!.Id);
        medicalRecord.IdAppointment.Should().Be(getMedicalRecord.IdAppointment);
        medicalRecord.IdClient.Should().Be(getMedicalRecord.IdClient);
    }
    [Fact]
    public async Task When_GetMedicalRecordWithValidId_Then_ShouldSuccess()
    {
        //Arrange
        var medicalRecordId = Guid.NewGuid();
        //Act
        var medicalRecordResponse = await TestingClient.GetAsync(MedicalRecordUrl + $"/{medicalRecordId}");
        //Assert
        medicalRecordResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        var errors = await medicalRecordResponse.Content.ReadAsStringAsync();
        errors.Should().NotBeEmpty();
        errors.Should().Contain("Entity of type MedicalRecord was not found.");
    }
    [Fact]
    public async Task When_CreatedMedicalRecordAndDeleteIt_Then_ShouldSuccess()
    {
        //Arrange
        var petId = await SetupPet(Guid.NewGuid(), Size.Medium.Name);
        var officeId = await SetupOffice();
        var idAppointment = await SetupAppointment(petId, officeId);

        MedicalRecordDto medicalRecordDto = new MedicalRecordDto(idAppointment, petId);
        //Act
        var medicalRecordResponse = await TestingClient.PostAsJsonAsync(MedicalRecordUrl, medicalRecordDto);
        medicalRecordResponse.EnsureSuccessStatusCode();
        var medicalRecord = await medicalRecordResponse.Content.ReadFromJsonAsync<MedicalRecord>();
        var getMedicalRecordResponse = await TestingClient.DeleteAsync(MedicalRecordUrl + $"/{medicalRecord!.Id}");
        //Assert
        getMedicalRecordResponse.EnsureSuccessStatusCode();
        var medicalRecordsResponse = await TestingClient.GetAsync(MedicalRecordUrl);
        var medicalRecords = await medicalRecordsResponse.Content.ReadFromJsonAsync<List<MedicalRecord>>();
        medicalRecords.Should().BeEmpty();
    }
    [Fact]
    public async Task When_DeleteMedicalRecordWithInvalidId_Then_ShouldFail()
    {
        //Arrange
        var medicalRecordId = Guid.NewGuid();
        //Act
        var medicalRecordResponse = await TestingClient.DeleteAsync(MedicalRecordUrl + $"/{medicalRecordId}");
        //Assert
        medicalRecordResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await medicalRecordResponse.Content.ReadAsStringAsync();
        errors.Should().NotBeEmpty();
        errors.Should().Contain("Entity of type MedicalRecord was not found.");
    }

    private async Task<Guid> SetupAppointment(Guid petId, Guid officeId)
    {
        var dto = new ScheduleAppointmentDto
        {
            TypeString = AppointmentType.Consultation.Name,
            PetID = petId,
            LocationID = officeId,
            StartTime = DateTime.Now.AddDays(1),
            EndTime = DateTime.Now.AddDays(1).AddHours(1)
        };
        var response = await TestingClient.PostAsJsonAsync(AppointmentUrl, dto);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<Appointment>();
        return result!.Id;
    }
    private async Task<Guid> SetupPet(Guid ownerId, string petSizeString)
    {
        var petDto = new PetDto
        {
            Name = "Test Pet",
            DateOfBirth = DateTime.Now.AddYears(-1),
            Species = "Dog",
            Breed = "Labrador",
            OwnerId = ownerId,
            Size = petSizeString
        };

        var petResponse = await TestingClient.PostAsJsonAsync(PetsUrl, petDto);
        petResponse.EnsureSuccessStatusCode();
        var pet = await petResponse.Content.ReadFromJsonAsync<Pet>();
        return pet!.Id;
    }
    private async Task<Guid> SetupOffice()
    {
        var officeDto = new OfficeDto
        {
            BusinessId = Guid.NewGuid(),
            Address = "123 Main St",
            AnimalCapacity = 10
        };

        var officeResponse = await TestingClient.PostAsJsonAsync(OfficesUrl, officeDto);
        officeResponse.EnsureSuccessStatusCode();
        var office = await officeResponse.Content.ReadFromJsonAsync<Office>();
        office.Should().NotBeNull();
        return office!.ID;
    }
}
