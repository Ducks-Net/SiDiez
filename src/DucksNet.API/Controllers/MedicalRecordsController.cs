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
    private readonly IRepositoryAsync<MedicalRecord> _medicalRecordRepository;
    private readonly IRepositoryAsync<Pet> _clientRepository;
    private readonly IRepositoryAsync<Appointment> _appointmentRepository;

    public MedicalRecordsController(IRepositoryAsync<MedicalRecord> medicalRepository, IRepositoryAsync<Pet> clientRepository, IRepositoryAsync<Appointment> appointmentRepository)
    {
        _medicalRecordRepository = medicalRepository;
        _clientRepository = clientRepository;
        _appointmentRepository = appointmentRepository;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var cages = await _medicalRecordRepository.GetAllAsync();
        return Ok(cages);
    }
    [HttpGet("{medicalRecordId:guid}")]
    public async Task<IActionResult> GetByLocationID(Guid medicalRecordId)
    {
        var medicalRecord = await _medicalRecordRepository.GetAsync(medicalRecordId);
        if (medicalRecord.IsFailure)
        {
            return NotFound(medicalRecord.Errors);
        }
        return Ok(medicalRecord.Value);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MedicalRecordDto dto)
    {

        var appointment = await _appointmentRepository.GetAsync(dto.IdAppointment);
        if (appointment.IsFailure)
        {
            return BadRequest(appointment.Errors);
        }
        var client = await _clientRepository.GetAsync(dto.IdClient);
        if (client.IsFailure)
        {
            return BadRequest(client.Errors);
        }
        
        var medicalRecord = MedicalRecord.Create(dto.IdAppointment, dto.IdClient); //do not need, the checks are done above
        await _medicalRecordRepository.AddAsync(medicalRecord.Value!);
        return Ok(medicalRecord.Value);
    }
    [HttpPut("{medicalRecordId:guid}")]
    public async Task<IActionResult> UpdateAppointmentMedicalRecord(Guid medicalRecordId, [FromBody] Guid newIdAppointment)
    {
        var oldMedicalRecord = await _medicalRecordRepository.GetAsync(medicalRecordId);
        if (oldMedicalRecord.IsFailure)
        {
            return BadRequest(oldMedicalRecord.Errors);
        }
        var newAppointment = await _appointmentRepository.GetAsync(newIdAppointment);
        if (newAppointment.IsFailure)
        {
            return BadRequest(newAppointment.Errors);
        }
        oldMedicalRecord.Value!.AssignToAppointment(newIdAppointment);
        await _medicalRecordRepository.UpdateAsync(oldMedicalRecord.Value);
        return Ok(oldMedicalRecord.Value);
    }
    [HttpPut("{medicalRecordId:guid}/{newIdClient:guid}")]
    public async Task<IActionResult> UpdateClientMedicalRecord(Guid medicalRecordId, Guid newIdClient)
    {
        var oldMedicalRecord = await _medicalRecordRepository.GetAsync(medicalRecordId);
        if (oldMedicalRecord.IsFailure)
        {
            return BadRequest(oldMedicalRecord.Errors);
        }
        var newAppointment = await _clientRepository.GetAsync(newIdClient);
        if (newAppointment.IsFailure)
        {
            return BadRequest(newAppointment.Errors);
        }
        oldMedicalRecord.Value.AssignToClient(newIdClient);
        await _medicalRecordRepository.UpdateAsync(oldMedicalRecord.Value);
        return Ok(oldMedicalRecord.Value);
    }
    [HttpDelete("{medicalRecordId:guid}")]
    public async Task<IActionResult> Delete(Guid medicalRecordId)
    {
        var medicalRecord = await _medicalRecordRepository.GetAsync(medicalRecordId);
        if (medicalRecord.IsFailure)
        {
            return BadRequest(medicalRecord.Errors);
        }
        await _medicalRecordRepository.DeleteAsync(medicalRecord.Value!);
        return Ok();
    }
}
