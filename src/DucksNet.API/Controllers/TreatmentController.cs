using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using Microsoft.AspNetCore.Mvc;

namespace DucksNet.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TreatmentController : ControllerBase
{
    private readonly IRepository<Treatment> _treatmentRepository;
    public TreatmentController(IRepository<Treatment> repository)
    {
        _treatmentRepository = repository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var treatments = _treatmentRepository.GetAll();
        return Ok(treatments);
    }

    [HttpGet("{ownerId}")]
    public IActionResult GetByOwnerID(Guid ownerId)
    {
        var treatment = _treatmentRepository.GetAll().Where(t => t.OwnerID == ownerId).ToList();
        return Ok(treatment);
    }

    [HttpGet("{clientId}")]
    public IActionResult GetByClientID(Guid clientId)
    {
        var treatment = _treatmentRepository.GetAll().Where(t => t.ClientID == clientId).ToList();
        return Ok(treatment);
    }

    [HttpGet("{clinicId}")]
    public IActionResult GetByClinicID(Guid clinicId)
    {
        var treatment = _treatmentRepository.GetAll().Where(t => t.ClinicID == clinicId).ToList();
        return Ok(treatment);
    }
}
