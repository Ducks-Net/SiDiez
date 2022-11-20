using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;

using Microsoft.AspNetCore.Mvc;

namespace DucksNet.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IRepository<Appointment> _appointmentsRepository;
    public AppointmentsController(IRepository<Appointment> repository)
    {
        _appointmentsRepository = repository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var appointments = _appointmentsRepository.GetAll();
        return Ok(appointments);
    }

    [HttpGet("{petId}")]
    public IActionResult GetByPetID(Guid petId)
    {
        var appointments = _appointmentsRepository.GetAll().Where(a => a.PetId == petId).ToList();
        return Ok(appointments);
    }

    [HttpGet("{vetId}")]
    public IActionResult GetByVetID(Guid vetId)
    {
        var appointments = _appointmentsRepository.GetAll().Where(a => a.VetId == vetId).ToList();
        return Ok(appointments);
    }

    [HttpGet("{locationId}")]
    public IActionResult GetByLocationID(Guid locationId)
    {
        var appointments = _appointmentsRepository.GetAll().Where(a => a.LocationId == locationId).ToList();
        return Ok(appointments);
    }

    [HttpPost]
    public IActionResult ScheduleAppointment(Guid petId, DateTime startTime, DateTime endTime, string type)
    {
        return Ok();
    }
}
