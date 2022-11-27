using DucksNet.Domain.Model;
using DucksNet.Domain.Model.Enums;
using DucksNet.SharedKernel.Utils;

namespace DucksNet.Infrastructure.Prelude;
public class AppointmentScheduleService
{
    private readonly IRepository<Appointment> _appointmentsRepository;
    private readonly IRepository<Office> _locationsRepository;
    private readonly IRepository<Pet> _petsRepository;

    public AppointmentScheduleService(IRepository<Appointment> appointmentsRepository, IRepository<Office> locationsRepository, IRepository<Pet> petsRepository)
    {
        _appointmentsRepository = appointmentsRepository;
        _locationsRepository = locationsRepository;
        _petsRepository = petsRepository;
    }

    public Result<List<Appointment>> GetLocationSchedule(Guid locationId)
    {
        var res = _appointmentsRepository.GetAll().Where(a => a.LocationId == locationId).ToList();
        return Result<List<Appointment>>.Ok(res);
    }

    public Result<Appointment> ScheduleAppointment(string typeString, Guid petID, Guid locationID, DateTime startTime, DateTime endTime)
    {
        var pet = _petsRepository.Get(petID);
        if (pet.IsFailure || pet.Value == null)
            return Result<Appointment>.FromError(pet, "Pet not found.");
        
        var location = _locationsRepository.Get(locationID);
        if (location.IsFailure || location.Value == null)
            return Result<Appointment>.FromError(location, "Location not found.");

        var appointment = Appointment.Create(typeString, startTime, endTime);
        if (appointment.IsFailure || appointment.Value is null)
            return Result<Appointment>.ErrorList(appointment.Errors);

        var overlappingAppointments = _appointmentsRepository.GetAll()
            .Where(a => a.StartTime < endTime && a.EndTime > startTime)
            .Where(a => a.PetId == petID);

        if (overlappingAppointments.Any())
            return Result<Appointment>.Error("There is already an appointment scheduled for this pet at this time.");

        appointment.Value.AssignToLocation(locationID);
        appointment.Value.AssignToPet(petID);

        _appointmentsRepository.Add(appointment.Value!);
        return Result<Appointment>.Ok(appointment.Value!);
    }
}
