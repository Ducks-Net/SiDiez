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
    [HttpGet("byId/{medicalRecordId}")]
    public IActionResult GetById(Guid medicalRecordId)
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
        
        var medicalRecord = MedicalRecord.Create(dto.IdAppointment, dto.IdClient); //do not need, the checks are done above
        _medicalRecordRepository.Add(medicalRecord.Value!);
        return Ok(medicalRecord.Value);
    }
    [HttpPut("{medicalRecordId:guid}/{newIdAppointment:guid}")]
    public IActionResult UpdateAppointmentMedicalRecord(Guid medicalRecordId, Guid newIdAppointment)
    {
        var oldMedicalRecord = _medicalRecordRepository.Get(medicalRecordId);
        if (oldMedicalRecord.IsFailure)
        {
            return BadRequest(oldMedicalRecord.Errors);
        }
        var newAppointment = _appointmentRepository.Get(newIdAppointment);
        if (newAppointment.IsFailure)
        {
            return BadRequest(newAppointment.Errors);
        }
        if (oldMedicalRecord.Value!.IdAppointment != newIdAppointment)
            oldMedicalRecord.Value.AssignToAppointment(newIdAppointment);
        _medicalRecordRepository.Update(oldMedicalRecord.Value);
        return Ok(oldMedicalRecord.Value);
    }
    [HttpPut("{medicalRecordId:guid}/{newIdClient:guid}")]
    public IActionResult UpdateClientMedicalRecord(Guid medicalRecordId, Guid newIdClient)
    {
        var oldMedicalRecord = _medicalRecordRepository.Get(medicalRecordId);
        if (oldMedicalRecord.IsFailure)
        {
            return BadRequest(oldMedicalRecord.Errors);
        }
        var newAppointment = _clientRepository.Get(newIdClient);
        if (newAppointment.IsFailure)
        {
            return BadRequest(newAppointment.Errors);
        }
        if (oldMedicalRecord.Value!.IdClient != newIdClient)
            oldMedicalRecord.Value.AssignToClient(newIdClient);
        _medicalRecordRepository.Update(oldMedicalRecord.Value);
        return Ok(oldMedicalRecord.Value);
    }
    [HttpDelete("{medicalRecordId:guid}")]
    public IActionResult Delete(Guid medicalRecordId)
    {
        var medicalRecord = _medicalRecordRepository.Get(medicalRecordId);
        if (medicalRecord.IsFailure)
        {
            return BadRequest(medicalRecord.Errors);
        }
        _medicalRecordRepository.Delete(medicalRecord.Value!);
        return Ok();
    }
}
