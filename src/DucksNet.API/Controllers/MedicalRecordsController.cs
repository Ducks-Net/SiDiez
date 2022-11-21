using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using DucksNet.Infrastructure.Sqlite;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DucksNet.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class MedicalRecordsController : ControllerBase
{
    private readonly IRepository<MedicalRecord> _medicalRecordRepository;
    public MedicalRecordsController(IRepository<MedicalRecord> repository)
    {
        _medicalRecordRepository = repository;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        var cages = _medicalRecordRepository.GetAll();
        return Ok(cages);
    }
    [HttpGet("{medicalRecordId}")]
    public IActionResult GetByLocationID(Guid medicalRecordId)
    {
        var medicalRecord = _medicalRecordRepository.GetAll().Where(c => c.Id == medicalRecordId).ToList();
        return Ok(medicalRecord);
    }
    [HttpPost]
    public IActionResult Create([FromBody] MedicalRecordDTO dto)
    {
        var medicalRecord = MedicalRecord.Create(dto.IdAppointment, dto.IdClient);
        if (medicalRecord.IsSuccess && medicalRecord.Value is not null)
        {
            _medicalRecordRepository.Add(medicalRecord.Value);
            return Created(nameof(GetAll), medicalRecord);
        }
        return BadRequest();
    }

}
