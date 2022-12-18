using DucksNet.Domain.Model;
using DucksNet.Domain.Model.Enums;
using DucksNet.SharedKernel.Utils;

namespace DucksNet.Infrastructure.Prelude;
public class AppointmentScheduleService
{
    private readonly IRepositoryAsync<Appointment> _appointmentsRepository;
    private readonly IRepositoryAsync<Office> _locationsRepository;
    private readonly IRepositoryAsync<Pet> _petsRepository;

    public AppointmentScheduleService(IRepositoryAsync<Appointment> appointmentsRepository, IRepositoryAsync<Office> locationsRepository, IRepositoryAsync<Pet> petsRepository)
    {
        _appointmentsRepository = appointmentsRepository;
        _locationsRepository = locationsRepository;
        _petsRepository = petsRepository;
    }

    public async Task<Result<List<Appointment>>> GetLocationSchedule(Guid locationId)
    {
        var res = await _appointmentsRepository.GetAllAsync();
        var resList = res.Where(a => a.LocationId == locationId).ToList();
        return Result<List<Appointment>>.Ok(resList);
    }

    public async Task<Result<Appointment>> ScheduleAppointment(string typeString, Guid petId, Guid locationId, DateTime startTime, DateTime endTime)
    {
        var pet = await _petsRepository.GetAsync(petId);
        if (pet.IsFailure || pet.Value == null)
            return Result<Appointment>.FromError(pet, "Pet not found.");
        
        var location = await _locationsRepository.GetAsync(locationId);
        if (location.IsFailure || location.Value == null)
            return Result<Appointment>.FromError(location, "Location not found.");

        var appointment = Appointment.Create(typeString, startTime, endTime);
        if (appointment.IsFailure || appointment.Value is null)
            return Result<Appointment>.ErrorList(appointment.Errors);

        var overlappingAppointments = await _appointmentsRepository.GetAllAsync();
            overlappingAppointments = overlappingAppointments
                                        .Where(a => a.StartTime < endTime && a.EndTime > startTime)
                                        .Where(a => a.PetId == petId);

        if (overlappingAppointments.Any())
            return Result<Appointment>.Error("There is already an appointment scheduled for this pet at this time.");

        appointment.Value.AssignToLocation(locationId);
        appointment.Value.AssignToPet(petId);

        _appointmentsRepository.AddAsync(appointment.Value!);
        return Result<Appointment>.Ok(appointment.Value!);
    }
}
