using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using Microsoft.AspNetCore.Mvc;

namespace DucksNet.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IRepository<Appointment> _appointmentsRepository;
    private readonly AppointmentScheduleService _appointmentScheduleService;
    private readonly IRepository<Office> _officeRepository;
    private readonly IRepository<Pet> _animalRepository;

    public AppointmentsController(IRepository<Appointment> repository, IRepository<Office> officeRepository, IRepository<Pet> animalRepository)
    {
        _appointmentsRepository = repository;
        _officeRepository = officeRepository;
        _animalRepository = animalRepository;
        _appointmentScheduleService = new AppointmentScheduleService(repository, officeRepository, animalRepository);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var appointments = _appointmentsRepository.GetAll();
        return Ok(appointments);
    }

    [HttpGet("byPet/{petId}")]
    public IActionResult GetByPetID(Guid petId)
    {
        var appointments = _appointmentsRepository.GetAll().Where(a => a.PetId == petId).ToList();
        return Ok(appointments);
    }

    [HttpGet("byEmployee/{employeeId}")]
    public IActionResult GetByVetID(Guid vetId)
    {
        var appointments = _appointmentsRepository.GetAll().Where(a => a.VetId == vetId).ToList();
        return Ok(appointments);
    }

    [HttpGet("byOffice/{locationId}")]
    public IActionResult GetByLocationID(Guid? locationId)
    {
        var appointments = _appointmentsRepository.GetAll().Where(a => a.LocationId == locationId).ToList();
        return Ok(appointments);
    }

    [HttpPost]
    public IActionResult ScheduleAppointment([FromBody] ScheduleAppointmentDTO appointment)
    {
        var res = _appointmentScheduleService.ScheduleAppointment(appointment.TypeString, appointment.PetID, appointment.LocationID, appointment.StartTime, appointment.EndTime);
        if (res.IsFailure)
            return BadRequest(res.Errors);
        return Created(nameof(GetAll), res.Value);
    }
}
