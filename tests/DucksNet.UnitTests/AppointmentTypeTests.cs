using DucksNet.Domain.Model.Enums;

namespace DucksNet.UnitTests;

public class AppointmentTypeTests
{
    [Fact]
    public void When_CreateFromString_WithValidAppointmentType_Should_Succeed()
    {
        string appointmentType = "Consultation";

        var result = AppointmentType.CreateFromString(appointmentType);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.Name.Should().Be(appointmentType);
    }

    [Fact]
    public void When_CreateFromString_WithInvalidAppointmentType_should_Fail()
    {
        string appointmentType = "Invalid";

        var result = AppointmentType.CreateFromString(appointmentType);

        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Invalid AppointmentType string. Valid values are: Consultation, Vaccination, Surgery.");
    }

    [Fact]
    public void When_CreateFromInt_WithValidAppointmentType_Should_Succeed()
    {
        AppointmentType appointmentType = AppointmentType.Consultation;

        var result = AppointmentType.CreateFromInt(appointmentType.Id);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.Should().Be(appointmentType);
    }

    [Fact]
    public void When_CreateFromInt_WithInvalidAppointmentType_should_Fail()
    {
        int appointmentType = 4;

        var result = AppointmentType.CreateFromInt(appointmentType);

        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain("Invalid AppointmentType id. Valid values are: 1, 2, 3.");
    }
}
