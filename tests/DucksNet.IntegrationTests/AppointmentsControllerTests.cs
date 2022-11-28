using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using DucksNet.API.Controllers;
using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using DucksNet.Domain.Model.Enums;

namespace  DucksNet.IntegrationTests;

public class AppointmentsControllerTests : BaseIntegrationTests<AppointmentsController>
{
    private const string CagesUrl = "api/v1/cages";
    private const string OfficesUrl = "api/v1/office";
    private const string PetsUrl = "api/v1/pets";
    private const string AppointmentsUrl = "api/v1/appointments";


    [Fact]
    public void When_Post_WithValidDta_Should_ReturnAppointment()
    {
        var officeId = SetupOffice();
        var petId = SetupPet(Size.Medium.Name);

        var appointment = new ScheduleAppointmentDTO
        {
            TypeString = AppointmentType.Consultation.Name,
            PetID = petId,
            LocationID = officeId,
            StartTime = DateTime.Now.AddDays(1),
            EndTime = DateTime.Now.AddDays(1).AddHours(1)
        };

        var response = TestingClient.PostAsJsonAsync(AppointmentsUrl, appointment).Result;
        response.EnsureSuccessStatusCode();

        var result = response.Content.ReadFromJsonAsync<Appointment>().Result;
        Assert.NotNull(result);
        result!.LocationId.Should().Be(officeId);
        result.PetId.Should().Be(petId);
        result.StartTime.Should().Be(appointment.StartTime);
        result.EndTime.Should().Be(appointment.EndTime);
    }

    [Fact]
    public void When_Post_WithInvalidData_Should_ReturnBadRequest()
    {
        var appointment = new ScheduleAppointmentDTO
        {
            TypeString = AppointmentType.Consultation.Name,
            PetID = Guid.NewGuid(),
            LocationID = Guid.NewGuid(),
            StartTime = DateTime.Now.AddDays(1),
            EndTime = DateTime.Now.AddDays(1).AddHours(1)
        };

        var response = TestingClient.PostAsJsonAsync(AppointmentsUrl, appointment).Result;
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public void When_GetAll_Should_ReturnAllAppointments()
    {
        var officeId = SetupOffice();
        var petId = SetupPet(Size.Medium.Name);

        var appointment = new ScheduleAppointmentDTO
        {
            TypeString = AppointmentType.Consultation.Name,
            PetID = petId,
            LocationID = officeId,
            StartTime = DateTime.Now.AddDays(1),
            EndTime = DateTime.Now.AddDays(1).AddHours(1)
        };

        var response = TestingClient.PostAsJsonAsync(AppointmentsUrl, appointment).Result;
        response.EnsureSuccessStatusCode();

        var result = TestingClient.GetAsync(AppointmentsUrl).Result;
        result.EnsureSuccessStatusCode();

        var appointments = result.Content.ReadFromJsonAsync<List<Appointment>>().Result;
        Assert.NotNull(appointments);
        appointments!.Count.Should().Be(1);
    }

    [Fact]
    public void When_GetByOffice_Should_ReturnAppointments()
    {
        var officeId = SetupOffice();
        var petId = SetupPet(Size.Medium.Name);

        var appointment = new ScheduleAppointmentDTO
        {
            TypeString = AppointmentType.Consultation.Name,
            PetID = petId,
            LocationID = officeId,
            StartTime = DateTime.Now.AddDays(1),
            EndTime = DateTime.Now.AddDays(1).AddHours(1)
        };

        var response = TestingClient.PostAsJsonAsync(AppointmentsUrl, appointment).Result;
        response.EnsureSuccessStatusCode();

        var result = TestingClient.GetAsync($"{AppointmentsUrl}/byOffice/{officeId}").Result;
        result.EnsureSuccessStatusCode();

        var appointments = result.Content.ReadFromJsonAsync<List<Appointment>>().Result;
        Assert.NotNull(appointments);
        appointments!.Count.Should().Be(1);
    }

    [Fact]
    public void When_Get_ByPet_Should_ReturnAppointments()
    {
        var officeId = SetupOffice();
        var petId = SetupPet(Size.Medium.Name);

        var appointment = new ScheduleAppointmentDTO
        {
            TypeString = AppointmentType.Consultation.Name,
            PetID = petId,
            LocationID = officeId,
            StartTime = DateTime.Now.AddDays(1),
            EndTime = DateTime.Now.AddDays(1).AddHours(1)
        };

        var response = TestingClient.PostAsJsonAsync(AppointmentsUrl, appointment).Result;
        response.EnsureSuccessStatusCode();

        var result = TestingClient.GetAsync($"{AppointmentsUrl}/byPet/{petId}").Result;
        result.EnsureSuccessStatusCode();

        var appointments = result.Content.ReadFromJsonAsync<List<Appointment>>().Result;
        Assert.NotNull(appointments);
        appointments!.Count.Should().Be(1);
    }

    // [Fact]
    // public void When_GetByPet_Should_ReturnAppointmentsForPet()
    // {
    //     var officeId = SetupOffice();
    //     var petId = SetupPet(Size.Medium.Name);

    //     var appointment = new ScheduleAppointmentDTO
    //     {
    //         TypeString = AppointmentType.Consultation.Name,
    //         PetID = petId,
    //         LocationID = officeId,
    //         StartTime = DateTime.Now.AddDays(1),
    //         EndTime = DateTime.Now.AddDays(1).AddHours(1)
    //     };

    //     var response = TestingClient.PostAsJsonAsync(AppointmentsUrl, appointment).Result;
    //     response.EnsureSuccessStatusCode();

    //     var result = TestingClient.GetAsync($"{AppointmentsUrl}/byPet/{petId}").Result;
    //     result.EnsureSuccessStatusCode();

    //     var appointments = result.Content.ReadFromJsonAsync<List<Appointment>>().Result;
    //     Assert.NotNull(appointments);
    //     appointments!.Count.Should().Be(1);
    // }

    // [Fact]
    // public void When_GetByOffice_Should_ReturnAppointmentsForOffice()
    // {
    //     var officeId = SetupOffice();
    //     var petId = SetupPet(Size.Medium.Name);

    //     var appointment = new ScheduleAppointmentDTO
    //     {
    //         TypeString = AppointmentType.Consultation.Name,
    //         PetID = petId,
    //         LocationID = officeId,
    //         StartTime = DateTime.Now.AddDays(1),
    //         EndTime = DateTime.Now.AddDays(1).AddHours(1)
    //     };

    //     var response = TestingClient.PostAsJsonAsync(AppointmentsUrl, appointment).Result;
    //     response.EnsureSuccessStatusCode();

    //     var result = TestingClient.GetAsync($"{AppointmentsUrl}/byOffice/{officeId}").Result;
    //     result.EnsureSuccessStatusCode();

    //     var appointments = result.Content.ReadFromJsonAsync<List<Appointment>>().Result;
    //     Assert.NotNull(appointments);
    //     appointments!.Count.Should().Be(1);
    // }

    private Guid SetupPet(string petSizeString)
    { 
        var petDTO = new PetDTO
        {
            OwnerId = Guid.NewGuid(),
            Size = petSizeString
        };

        var petResponse = TestingClient.PostAsJsonAsync(PetsUrl, petDTO).Result;
        petResponse.EnsureSuccessStatusCode();
        var pet = petResponse.Content.ReadFromJsonAsync<Pet>().Result;
        return pet!.ID;
    }

    private Guid SetupCage(Guid officeId, string size)
    {        
        var sut = new CreateCageDTO {
            LocationId = officeId,
            SizeString = size
        };

        //Act
        var cageResponse = TestingClient.PostAsJsonAsync(CagesUrl, sut).Result;

        //Assert
        cageResponse.EnsureSuccessStatusCode();
        var cage = cageResponse.Content.ReadFromJsonAsync<Cage>().Result;
        return cage!.ID;
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
