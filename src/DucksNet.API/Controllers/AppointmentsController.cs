using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using Microsoft.AspNetCore.Mvc;

namespace DucksNet.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IRepositoryAsync<Appointment> _appointmentsRepository;
    private readonly AppointmentScheduleService _appointmentScheduleService;

    public AppointmentsController(IRepositoryAsync<Appointment> repository, IRepositoryAsync<Office> officeRepository, IRepositoryAsync<Pet> animalRepository)
    {
        _appointmentsRepository = repository;
        _appointmentScheduleService = new AppointmentScheduleService(repository, officeRepository, animalRepository);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var appointments = await _appointmentsRepository.GetAllAsync();
        return Ok(appointments);
    }

    [HttpGet("byPet/{petId}")]
    public async Task<IActionResult> GetByPetID(Guid petId)
    {
        var appointments = await _appointmentsRepository.GetAllAsync();
        return Ok(appointments.Where(a => a.PetId == petId));
    }
    
    [HttpGet("byOffice/{locationId:guid}")]
    public async Task<IActionResult> GetByLocationId(Guid locationId)
    {
        var result = await _appointmentScheduleService.GetLocationSchedule(locationId);
        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> ScheduleAppointment([FromBody] ScheduleAppointmentDto appointment)
    {
        var res = await _appointmentScheduleService.ScheduleAppointment(appointment.TypeString, appointment.PetID, appointment.LocationID, appointment.StartTime, appointment.EndTime);
        if (res.IsFailure)
            return BadRequest(res.Errors);
        return Created(nameof(GetAll), res.Value);
    }
}
