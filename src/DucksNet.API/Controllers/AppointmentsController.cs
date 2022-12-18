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

    [HttpGet("byEmployee/{employeeId}")]
    public async Task<IActionResult> GetByVetID(Guid vetId)
    {
        var appointments = await _appointmentsRepository.GetAllAsync();
        return Ok(appointments.Where(a => a.VetId == vetId));
    }

    [HttpGet("byOffice/{locationId}")]
    public async Task<IActionResult> GetByLocationID(Guid? locationId)
    {
        var appointments = await _appointmentsRepository.GetAllAsync();
        return Ok(appointments.Where(a => a.LocationId == locationId));
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
