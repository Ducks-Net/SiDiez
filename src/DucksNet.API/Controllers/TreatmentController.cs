using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using DucksNet.Infrastructure.Sqlite;
using Microsoft.AspNetCore.Mvc;

namespace DucksNet.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TreatmentController : ControllerBase
{
    private readonly IRepository<Treatment> _treatmentRepository;
    //private readonly IRepository<Medicine> _medicineRepository;
    public TreatmentController(IRepository<Treatment> treatmentRepository) // + IRepository<Medicine> medicineRepository
    {
        _treatmentRepository = treatmentRepository;
        //_medicineRepository = medicineRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var treatments = _treatmentRepository.GetAll();
        return Ok(treatments);
    }

    [HttpGet("byOwnerId/{ownerId}")]
    public IActionResult GetByOwnerID(Guid ownerId)
    {
        var treatment = _treatmentRepository.GetAll().Where(t => t.OwnerID == ownerId).ToList();
        return Ok(treatment);
    }

    [HttpGet("byClientId/{clientId}")]
    public IActionResult GetByClientID(Guid clientId)
    {
        var treatment = _treatmentRepository.GetAll().Where(t => t.ClientID == clientId).ToList();
        return Ok(treatment);
    }

    [HttpGet("byClinicId/{clinicId}")]
    public IActionResult GetByClinicID(Guid clinicId)
    {
        var treatment = _treatmentRepository.GetAll().Where(t => t.ClinicID == clinicId).ToList();
        return Ok(treatment);
    }

    [HttpPost]
    public IActionResult Create([FromBody] TreatmentDTO dto)
    {
        //var treatmentPost = Treatment.CreateTreatment(dto.MedicineIDList.ToList());
        var treatmentPost = Treatment.CreateTreatment();
        if (treatmentPost.IsFailure)
        {
            return BadRequest(treatmentPost.Errors);
        }

        var result = _treatmentRepository.Add(treatmentPost.Value!);
        if (result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok(treatmentPost.Value);
    }

    [HttpDelete("{token:guid}")]
    public IActionResult Delete(Guid token)
    {
        var treatment = _treatmentRepository.Get(token);
        if (treatment.IsFailure)
        {
            return BadRequest(treatment.Errors);
        }
        var result = _treatmentRepository.Delete(treatment.Value!);
        if (result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok();
    }
}
