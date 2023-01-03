using DucksNet.API.DTO;
using DucksNet.Application.Requests.MedicalRecordRequests;
using DucksNet.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DucksNet.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class MedicalRecordsController : ControllerBase
{
    private readonly IMediator _mediator;

    public MedicalRecordsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var medicalRecords = await _mediator.Send(new GetAllMedicalRecordRequest());
        return Ok(medicalRecords);
    }
    [HttpGet("{medicalRecordId:guid}")]
    public async Task<IActionResult> GetByLocationID(Guid medicalRecordId)
    {
        var medicalRecord = await _mediator.Send(new GetMedicalRecordRequest { MedicalRecordId = medicalRecordId });
        if (medicalRecord.TypeRequest == ETypeRequests.NOT_FOUND)
        {
            return NotFound(medicalRecord.Errors);
        }
        return Ok(medicalRecord.Value);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MedicalRecordDto dto)
    {
        var result = await _mediator.Send(dto);
        if (result.TypeRequest == ETypeRequests.BAD_REQUEST)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Value);
    }
    [HttpPut("{medicalRecordId:guid}")]
    public async Task<IActionResult> UpdateAppointmentMedicalRecord(Guid medicalRecordId, [FromBody] Guid newIdAppointment)
    {
        var result = await _mediator.Send(new UpdateMedicalRecordAppointmentRequest { MedicalRecordId = medicalRecordId, NewAppointmentId = newIdAppointment });
        if (result.TypeRequest == ETypeRequests.BAD_REQUEST)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Value);
    }
    [HttpPut("{medicalRecordId:guid}/{newIdClient:guid}")]
    public async Task<IActionResult> UpdateClientMedicalRecord(Guid medicalRecordId, Guid newIdClient)
    {
        var result = await _mediator.Send(new UpdateMedicalRecordClientRequest { MedicalRecordId = medicalRecordId, NewClientId = newIdClient });
        if (result.TypeRequest == ETypeRequests.BAD_REQUEST)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Value);
    }
    [HttpDelete("{medicalRecordId:guid}")]
    public async Task<IActionResult> Delete(Guid medicalRecordId)
    {
        var result = await _mediator.Send(new DeleteMedicalRecordRequest { MedicalRecordId = medicalRecordId });
        if (result.TypeRequest == ETypeRequests.BAD_REQUEST)
        {
            return BadRequest(result.Errors);
        }
        return Ok();
    }
}
