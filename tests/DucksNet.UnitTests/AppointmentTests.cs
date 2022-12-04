using System;
using DucksNet.Domain.Model;


namespace DucksNet.UnitTests;

public class AppointmentTests
{
    [Fact]
    public void When_CreateAppointment_WithValidTypeAndDate_Should_Succeed()
    {
        string type = "Consultation";
        DateTime dateStart = DateTime.Now.AddDays(1);
        DateTime dateEnd = DateTime.Now.AddDays(1).AddHours(1);

        var result = Appointment.Create(type, dateStart, dateEnd);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.Type.Name.Should().Be(type);
    }

    [Fact]
    public void When_CreateAppointment_WithInvalidType_Should_Fail()
    {
        string type = "InvalidType";
        DateTime dateStart = DateTime.Now;
        DateTime dateEnd = DateTime.Now.AddHours(1);

        var result = Appointment.Create(type, dateStart, dateEnd);

        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("Invalid appointment type.");
    }

    [Fact]
    public void When_CreateAppointment_WithInvalidEndDate_Should_Fail()
    {
        string type = "Consultation";
        DateTime dateStart = DateTime.Now.AddDays(1);
        DateTime dateEnd = DateTime.Now.AddDays(1).AddHours(-1);

        var result = Appointment.Create(type, dateStart, dateEnd);

        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("Start time cannot be after end time.");
    }

    [Fact]
    public void When_CreateAppointment_WithInvalidStartDate_Should_Fail()
    {
        string type = "Consultation";
        DateTime dateStart = DateTime.Now.AddHours(-2);
        DateTime dateEnd = DateTime.Now;

        var result = Appointment.Create(type, dateStart, dateEnd);

        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("Start time cannot be in the past.");
    }

    [Fact]
    public void When_AssignToLocation_WithValidLocationId_Should_Succeed()
    {
        string type = "Consultation";
        DateTime dateStart = DateTime.Now.AddDays(1);
        DateTime dateEnd = DateTime.Now.AddDays(1).AddHours(1);
        Guid locationId = new Guid();

        var result = Appointment.Create(type, dateStart, dateEnd);
        result.Value!.AssignToLocation(locationId);

        result.IsSuccess.Should().BeTrue();
        result.Value.LocationId.Should().Be(locationId);
    }

    [Fact]
    public void When_AssignToPet_WithValidPetId_Should_Succeed()
    {
        string type = "Consultation";
        DateTime dateStart = DateTime.Now.AddDays(1);
        DateTime dateEnd = DateTime.Now.AddDays(1).AddHours(1);
        Guid petId = new Guid();

        var result = Appointment.Create(type, dateStart, dateEnd);
        result.Value!.AssignToPet(petId);

        result.IsSuccess.Should().BeTrue();
        result.Value.PetId.Should().Be(petId);
    }

    [Fact]
    public void When_AssignToVet_WithValidVetId_Should_Succeed()
    {
        string type = "Consultation";
        DateTime dateStart = DateTime.Now.AddDays(1);
        DateTime dateEnd = DateTime.Now.AddDays(1).AddHours(1);
        Guid vetId = new Guid();

        var result = Appointment.Create(type, dateStart, dateEnd);
        result.Value!.AssignToVet(vetId);

        result.IsSuccess.Should().BeTrue();
        result.Value.VetId.Should().Be(vetId);
    }

    [Fact]
    public void When_AssignAll_WithValidIds_Should_Succeed()
    {
        string type = "Consultation";
        DateTime dateStart = DateTime.Now.AddDays(1);
        DateTime dateEnd = DateTime.Now.AddDays(1).AddHours(1);
        Guid locationId = new Guid();
        Guid petId = new Guid();
        Guid vetId = new Guid();

        var result = Appointment.Create(type, dateStart, dateEnd);
        result.Value!.AssignAll(locationId, petId, vetId);

        result.IsSuccess.Should().BeTrue();
        result.Value.LocationId.Should().Be(locationId);
        result.Value.PetId.Should().Be(petId);
        result.Value.VetId.Should().Be(vetId);
    }

    [Fact]
    public void When_NeedsCage_Should_Succeed()
    {
        string type = "Consultation";
        DateTime dateStart = DateTime.Now.AddDays(1);
        DateTime dateEnd = DateTime.Now.AddDays(1).AddHours(1);

        var result = Appointment.Create(type, dateStart, dateEnd);
        result.Value!.NeedsCage.Should().BeFalse();
        result.Value!.DoesNeedCage();

        result.IsSuccess.Should().BeTrue();
        result.Value!.NeedsCage.Should().BeTrue();
    }

    [Fact]
    public void When_DoesNotNeedCage_Should_Succeed()
    {
        string type = "Consultation";
        DateTime dateStart = DateTime.Now.AddDays(1);
        DateTime dateEnd = DateTime.Now.AddDays(1).AddHours(1);

        var result = Appointment.Create(type, dateStart, dateEnd);
        result.Value!.NeedsCage.Should().BeFalse();
        result.Value!.DoesNotNeedCage();

        result.IsSuccess.Should().BeTrue();
        result.Value!.NeedsCage.Should().BeFalse();
    }
}
