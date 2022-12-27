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
    [Fact]
    public async Task When_UpdateMedicalRecordWithValidAppointment_Then_ShouldSuccess()
    {
        //Arrange
        var petId1 = await SetupPet(Guid.NewGuid(), Size.Medium.Name);
        var officeId1 = await SetupOffice();
        var idAppointment1 = await SetupAppointment(petId1, officeId1);
        MedicalRecordDto medicalRecordDto = new MedicalRecordDto(idAppointment1, petId1);

        var petId2 = await SetupPet(Guid.NewGuid(), Size.Medium.Name, 1);
        var officeId2 = await SetupOffice(1);
        var idAppointment2 = await SetupAppointment(petId2, officeId2);

        //Act
        var medicalRecordResponse = await TestingClient.PostAsJsonAsync(MedicalRecordUrl, medicalRecordDto);
        var oldMedicalRecord = await medicalRecordResponse.Content.ReadFromJsonAsync<MedicalRecord>();
        medicalRecordResponse.EnsureSuccessStatusCode();
        var updateMedicalRecordResponse = await TestingClient.PutAsJsonAsync(MedicalRecordUrl + $"/{oldMedicalRecord!.Id}", idAppointment2);
        
        //Assert
        updateMedicalRecordResponse.EnsureSuccessStatusCode();
        var updatedMedicalRecord = await updateMedicalRecordResponse.Content.ReadFromJsonAsync<MedicalRecord>();
        updatedMedicalRecord!.Id.Should().Be(oldMedicalRecord.Id);
        updatedMedicalRecord.IdAppointment.Should().Be(idAppointment2);
    }
    [Fact]
    public async Task When_UpdateMedicalRecordWithInvalidMedicalRecordId_Then_ShouldFail()
    {
        //Arrange
        var oldMedicalRecordId = Guid.NewGuid();
        var idAppointment = Guid.NewGuid();
        //Act
        var updateMedicalRecordResponse = await TestingClient.PutAsJsonAsync(MedicalRecordUrl + $"/{oldMedicalRecordId}", idAppointment);
        //Assert
        updateMedicalRecordResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await updateMedicalRecordResponse.Content.ReadAsStringAsync();
        errors.Should().NotBeEmpty();
        errors.Should().Contain("Entity of type MedicalRecord was not found.");
    }
    [Fact]
    public async Task When_UpdateMedicalRecordWithInvalidIdAppointment_Then_ShouldFail()
    {
        //Arrange
        var petId = await SetupPet(Guid.NewGuid(), Size.Medium.Name);
        var officeId = await SetupOffice();
        var idAppointment = await SetupAppointment(petId, officeId);
        MedicalRecordDto medicalRecordDto = new MedicalRecordDto(idAppointment, petId);
        var idAppointment1 = Guid.NewGuid();

        //Act
        var medicalRecordResponse = await TestingClient.PostAsJsonAsync(MedicalRecordUrl, medicalRecordDto);
        var oldMedicalRecord = await medicalRecordResponse.Content.ReadFromJsonAsync<MedicalRecord>();
        medicalRecordResponse.EnsureSuccessStatusCode();
        var updateMedicalRecordResponse = await TestingClient.PutAsJsonAsync(MedicalRecordUrl + $"/{oldMedicalRecord!.Id}", idAppointment1);

        //Assert
        updateMedicalRecordResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var errors = await updateMedicalRecordResponse.Content.ReadAsStringAsync();
        errors.Should().NotBeEmpty();
        errors.Should().Contain("Entity of type Appointment was not found.");
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
    private async Task<Guid> SetupPet(Guid ownerId, string petSizeString, int whichPet = 0)
    {
        PetDto petDto;
        if (whichPet == 0)
        {
            petDto = new PetDto
            {
                Name = "Test Pet",
                DateOfBirth = DateTime.Now.AddYears(-1),
                Species = "Dog",
                Breed = "Labrador",
                OwnerId = ownerId,
                Size = petSizeString
            };
        }
        else
        {
            petDto = new PetDto
            {
                Name = "Update Pet",
                DateOfBirth = DateTime.Now.AddYears(-1),
                Species = "Cat",
                Breed = "British Short hair",
                OwnerId = ownerId,
                Size = petSizeString
            };
        }

        var petResponse = await TestingClient.PostAsJsonAsync(PetsUrl, petDto);
        petResponse.EnsureSuccessStatusCode();
        var pet = await petResponse.Content.ReadFromJsonAsync<Pet>();
        return pet!.Id;
    }
    private async Task<Guid> SetupOffice(int whichOffice = 0)
    {
        OfficeDto officeDto;
        if (whichOffice == 0)
        {
            officeDto = new OfficeDto
            {
                BusinessId = Guid.NewGuid(),
                Address = "123 Main St",
                AnimalCapacity = 10
            };
        }
        else
        {
            officeDto = new OfficeDto
            {
                BusinessId = Guid.NewGuid(),
                Address = "Blvd. Independentei",
                AnimalCapacity = 100
            };
        }

        var officeResponse = await TestingClient.PostAsJsonAsync(OfficesUrl, officeDto);
        officeResponse.EnsureSuccessStatusCode();
        var office = await officeResponse.Content.ReadFromJsonAsync<Office>();
        office.Should().NotBeNull();
        return office!.ID;
    }
}
