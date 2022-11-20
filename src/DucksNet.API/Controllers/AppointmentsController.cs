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

    [HttpGet("byLocation/{locationId}")]
    public IActionResult GetByLocationID(Guid? locationId)
    {
        var appointments = _appointmentsRepository.GetAll().Where(a => a.LocationId == locationId).ToList();
        return Ok(appointments);
    }

    // TODO (AL): Schedule an appointment. Needs a service similar to CagesScheduleService.
}
