using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using DucksNet.Infrastructure.Sqlite;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DucksNet.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MedicalRecordsController : ControllerBase
{
    private readonly IRepository<MedicalRecord> _medicalRecordRepository;
    //TODO: To add AppointmentRepository and ClientRepository
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
    [HttpPost]
    public IActionResult Create([FromBody] MedicalRecordDTO dto)
    {
        //var medicalRecord = MedicalRecord.Create(dto.IdAppointment, dto.IdClient);
        //if (medicalRecord.IsSuccess)
        //{
        //    _medicalRecordRepository.Add(medicalRecord.Value);
        //    return Created(nameof(GetAll), medicalRecord);
        //}
        //return BadRequest(medicalRecord.Errors);
        var medicalRecord = new MedicalRecord(dto.IdAppointment, dto.IdClient);
        _medicalRecordRepository.Add(medicalRecord);
        return Created(nameof(GetAll), medicalRecord);
    }

}
