using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DucksNet.API.Controllers;
using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using DucksNet.Domain.Model.Enums;
using DucksNet.SharedKernel.Utils;

namespace DucksNet.IntegrationTests;

public class AppointmentsControllerTests : BaseIntegrationTests<AppointmentsController>
{
    private const string CagesUrl = "api/v1/cages";
    private const string OfficesUrl = "api/v1/office";
    private const string PetsUrl = "api/v1/pets";
    private const string AppointmentsUrl = "api/v1/appointments";

    [Fact]
    public async Task When_Post_WithValidDta_Should_ReturnAppointment()
    {
        var officeId = await SetupOffice();
        var petId = await SetupPet(Guid.NewGuid(), Size.Medium.Name);

        var appointment = new ScheduleAppointmentDto
        {
            TypeString = AppointmentType.Consultation.Name,
            PetID = petId,
            LocationID = officeId,
            StartTime = DateTime.Now.AddDays(1),
            EndTime = DateTime.Now.AddDays(1).AddHours(1)
        };

        var response = await TestingClient.PostAsJsonAsync(AppointmentsUrl, appointment);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<Appointment>();
        Assert.NotNull(result);
        result!.LocationId.Should().Be(officeId);
        result.PetId.Should().Be(petId);
        result.StartTime.Should().Be(appointment.StartTime);
        result.EndTime.Should().Be(appointment.EndTime);
    }

    [Fact]
    public async Task When_Post_WithInvalidData_Should_ReturnBadRequest()
    {
        var appointment = new ScheduleAppointmentDto
        {
            TypeString = AppointmentType.Consultation.Name,
            PetID = Guid.NewGuid(),
            LocationID = Guid.NewGuid(),
            StartTime = DateTime.Now.AddDays(1),
            EndTime = DateTime.Now.AddDays(1).AddHours(1)
        };

        var response = await TestingClient.PostAsJsonAsync(AppointmentsUrl, appointment);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task When_GetAll_Should_ReturnAllAppointments()
    {
        var officeId = await SetupOffice();
        var petId = await SetupPet(Guid.NewGuid(), Size.Medium.Name);

        var appointment = new ScheduleAppointmentDto
        {
            TypeString = AppointmentType.Consultation.Name,
            PetID = petId,
            LocationID = officeId,
            StartTime = DateTime.Now.AddDays(1),
            EndTime = DateTime.Now.AddDays(1).AddHours(1)
        };

        var response = await TestingClient.PostAsJsonAsync(AppointmentsUrl, appointment);
        response.EnsureSuccessStatusCode();

        var result = await TestingClient.GetAsync(AppointmentsUrl);
        result.EnsureSuccessStatusCode();

        var appointments = await result.Content.ReadFromJsonAsync<List<Appointment>>();
        Assert.NotNull(appointments);
        appointments!.Count.Should().Be(1);
    }

    [Fact]
    public async Task When_GetByOffice_Should_ReturnAppointments()
    {
        var officeId = await SetupOffice();
        var petId = await SetupPet(Guid.NewGuid(), Size.Medium.Name);

        var appointment = new ScheduleAppointmentDto
        {
            TypeString = AppointmentType.Consultation.Name,
            PetID = petId,
            LocationID = officeId,
            StartTime = DateTime.Now.AddDays(1),
            EndTime = DateTime.Now.AddDays(1).AddHours(1)
        };

        var response = await TestingClient.PostAsJsonAsync(AppointmentsUrl, appointment);
        response.EnsureSuccessStatusCode();

        var result = await TestingClient.GetAsync($"{AppointmentsUrl}/byOffice/{officeId}");
        result.EnsureSuccessStatusCode();

        var appointments = await result.Content.ReadFromJsonAsync<List<Appointment>>();
        appointments!.Count.Should().Be(1);
    }

    [Fact]
    public async Task When_Get_ByPet_Should_ReturnAppointments()
    {
        var officeId = await SetupOffice();
        var petId = await SetupPet(Guid.NewGuid(), Size.Medium.Name);

        var appointment = new ScheduleAppointmentDto
        {
            TypeString = AppointmentType.Consultation.Name,
            PetID = petId,
            LocationID = officeId,
            StartTime = DateTime.Now.AddDays(1),
            EndTime = DateTime.Now.AddDays(1).AddHours(1)
        };

        var response = await TestingClient.PostAsJsonAsync(AppointmentsUrl, appointment);
        response.EnsureSuccessStatusCode();

        var result = await TestingClient.GetAsync($"{AppointmentsUrl}/byPet/{petId}");
        result.EnsureSuccessStatusCode();

        var appointments = await result.Content.ReadFromJsonAsync<List<Appointment>>();
        Assert.NotNull(appointments);
        appointments!.Count.Should().Be(1);
    }

    private async Task<Guid> SetupPet(Guid ownerId ,string petSizeString)
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
