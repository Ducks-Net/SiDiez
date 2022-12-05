﻿using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using DucksNet.Infrastructure.Sqlite;
using Microsoft.AspNetCore.Mvc;

namespace DucksNet.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TreatmentController : ControllerBase
{
    //TODO(MG): Redo everything using List<Medicine> when it's fixed
    private readonly IRepository<Treatment> _treatmentRepository;
    public TreatmentController(IRepository<Treatment> treatmentRepository) // + IRepository<Medicine> medicineRepository
    {
        _treatmentRepository = treatmentRepository;
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
        var treatmentPost = Treatment.CreateTreatment(dto.OwnerID, dto.ClientID, dto.ClinicID);
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
