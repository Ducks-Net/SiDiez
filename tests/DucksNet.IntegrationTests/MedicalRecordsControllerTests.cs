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
public class MedicalRecordsControllerTests : BaseIntegrationTests<MedicalRecordsController>
{
    private const string MedicalRecordURL = "api/v1/medicalrecords";
    private const string AppointmentURL = "api/v1/appointments";
    private const string ClientURL = "api/v1/pets";
    private const string OfficesUrl = "api/v1/office";
    private const string PetsUrl = "api/v1/pets";

    [Fact]
    public async Task When_CreatedMedicalRecord_Then_ShouldReturnMedicalRecordInTheGetRequest()
    {
        //Arrange
        var petId = SetupPet(Guid.NewGuid(), Size.Medium.Name);
        var officeId = SetupOffice();
        var idAppointment = SetupAppointment(petId, officeId);

        MedicalRecordDTO medicalRecordDTO = new MedicalRecordDTO(idAppointment, petId);
        //Act
        var medicalRecordResponse = await TestingClient.PostAsJsonAsync(MedicalRecordURL, medicalRecordDTO);
        var medicalRecordResult = await TestingClient.GetAsync(MedicalRecordURL);

        Console.WriteLine(medicalRecordResponse.Content.ReadAsStringAsync().Result);
        //Assert
        medicalRecordResponse.EnsureSuccessStatusCode();
        var medicalRecords = await medicalRecordResult.Content.ReadFromJsonAsync<List<MedicalRecordDTO>>();
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

        MedicalRecordDTO medicalRecordDTO = new MedicalRecordDTO(idAppointment, idClient);
        //Act
        var medicalRecordResponse = await TestingClient.PostAsJsonAsync(MedicalRecordURL, medicalRecordDTO);
        var _ = await TestingClient.GetAsync(MedicalRecordURL);
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

    private Guid SetupAppointment(Guid petId, Guid officeId)
    {
        var DTO = new ScheduleAppointmentDTO
        {
            TypeString = AppointmentType.Consultation.Name,
            PetID = petId,
            LocationID = officeId,
            StartTime = DateTime.Now.AddDays(1),
            EndTime = DateTime.Now.AddDays(1).AddHours(1)
        };
        var response = TestingClient.PostAsJsonAsync(AppointmentURL, DTO).Result;
        response.EnsureSuccessStatusCode();
        var result = response.Content.ReadFromJsonAsync<Appointment>().Result;
        return result!.ID;
    }
    private Guid SetupPet(Guid ownerId, string petSizeString)
    {
        var petDTO = new PetDTO
        {
            Name = "Test Pet",
            DateOfBirth = DateTime.Now.AddYears(-1),
            Species = "Dog",
            Breed = "Labrador",
            OwnerId = ownerId,
            Size = petSizeString
        };

        var petResponse = TestingClient.PostAsJsonAsync(PetsUrl, petDTO).Result;
        petResponse.EnsureSuccessStatusCode();
        var pet = petResponse.Content.ReadFromJsonAsync<Pet>().Result;
        return pet!.Id;
    }
    private Guid SetupOffice()
    {
        var officeDTO = new OfficeDTO
        {
            BusinessId = Guid.NewGuid(),
            Address = "123 Main St",
            AnimalCapacity = 10
        };

        var officeResponse = TestingClient.PostAsJsonAsync(OfficesUrl, officeDTO).Result;
        officeResponse.EnsureSuccessStatusCode();
        var office = officeResponse.Content.ReadFromJsonAsync<Office>().Result;
        office.Should().NotBeNull();
        return office!.ID;
    }
}
