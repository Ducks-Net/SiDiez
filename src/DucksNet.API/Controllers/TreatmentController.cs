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
    //NOTE(MG): Redo everything using List<Medicine> when it's fixed
    private readonly IRepositoryAsync<Treatment> _treatmentRepository;
    public TreatmentController(IRepositoryAsync<Treatment> treatmentRepository) // + IRepository<Medicine> medicineRepository
    {
        _treatmentRepository = treatmentRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var treatments = await _treatmentRepository.GetAllAsync();
        return Ok(treatments);
    }

    [HttpGet("byOwnerId/{ownerId:guid}")]
    public async Task<IActionResult> GetByOwnerId(Guid ownerId)
    {
        var treatment = await _treatmentRepository.GetAllAsync();
        treatment = treatment.Where(t => t.OwnerID == ownerId).ToList();
        return Ok(treatment);
    }

    [HttpGet("byClientId/{clientId:guid}")]
    public async Task<IActionResult> GetByClientId(Guid clientId)
    {
        var treatment = await _treatmentRepository.GetAllAsync();
        treatment = treatment.Where(t => t.ClientID == clientId).ToList();
        return Ok(treatment);
    }

    [HttpGet("byClinicId/{clinicId:guid}")]
    public async Task<IActionResult> GetByClinicId(Guid clinicId)
    {
        var treatment = await _treatmentRepository.GetAllAsync();
        treatment = treatment.Where(t => t.ClinicID == clinicId).ToList();
        return Ok(treatment);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TreatmentDto dto)
    {
        var treatmentPost = Treatment.CreateTreatment(dto.OwnerID, dto.ClientID, dto.ClinicID);
        if (treatmentPost.IsFailure)
        {
            return BadRequest(treatmentPost.Errors);
        }

        var result = await _treatmentRepository.AddAsync(treatmentPost.Value!);
        if (result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok(treatmentPost.Value);
    }

    [HttpDelete("{token:guid}")]
    public async Task<IActionResult> Delete(Guid token)
    {
        var treatment = await _treatmentRepository.GetAsync(token);
        if (treatment.IsFailure)
        {
            return BadRequest(treatment.Errors);
        }
        var result = await _treatmentRepository.DeleteAsync(treatment.Value!);
        if (result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok();
    }
}
