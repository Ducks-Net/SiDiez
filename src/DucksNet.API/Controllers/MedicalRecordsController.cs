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
    private readonly IRepository<Pet> _clientRepository;
    private readonly IRepository<Appointment> _appointmentRepository;

    public MedicalRecordsController(IRepository<MedicalRecord> medicalRepository, IRepository<Pet> clientRepository, IRepository<Appointment> appointmentRepository)
    {
        _medicalRecordRepository = medicalRepository;
        _clientRepository = clientRepository;
        _appointmentRepository = appointmentRepository;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        var cages = _medicalRecordRepository.GetAll();
        return Ok(cages);
    }
    [HttpGet("{medicalRecordId:guid}")]
    public IActionResult GetByLocationID(Guid medicalRecordId)
    {
        var medicalRecord = _medicalRecordRepository.Get(medicalRecordId);
        if (medicalRecord.IsFailure)
        {
            return NotFound(medicalRecord.Errors);
        }
        return Ok(medicalRecord.Value);
    }
    [HttpPost]
    public IActionResult Create([FromBody] MedicalRecordDTO dto)
    {
        // TODO (RO): need some help for integration test
        /*
        var appointment = _appointmentRepository.Get(dto.IdAppointment);
        if (appointment.IsFailure)
        {
            return BadRequest(appointment.Errors);
        }
        var client = _clientRepository.Get(dto.IdClient);
        if (client.IsFailure)
        {
            return BadRequest(client.Errors);
        }
        */
        var medicalRecord = MedicalRecord.Create(dto.IdAppointment, dto.IdClient);
        if (medicalRecord.IsFailure)
        {
            return BadRequest(medicalRecord.Errors);
        }
        var result = _medicalRecordRepository.Add(medicalRecord!.Value);
        if (result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok(medicalRecord.Value);
    }
    // TODO (RO): apis for update
    [HttpDelete("{token:guid}")]
    public IActionResult Delete(Guid token)
    {
        //TODO(RO): find a better way to delete a medical record
        var medicalRecord = _medicalRecordRepository.Get(token);
        if (medicalRecord.IsFailure)
        {
            return BadRequest(medicalRecord.Errors);
        }
        var result = _medicalRecordRepository.Delete(medicalRecord.Value!);
        if (result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok();
    }
}
