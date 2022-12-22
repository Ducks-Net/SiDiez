using DucksNet.API.DTO;
using DucksNet.API.Validators;
using DucksNet.Application.Handlers.EmployeeHandlers;
using DucksNet.Application.Requests.EmployeeRequests;
using DucksNet.Application.Responses;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using DucksNet.SharedKernel.Utils;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DucksNet.API.Controllers;
[EnableCors("VetPolicyCors")]
[Route("api/v1/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<EmployeeDto> _createValidator;

    public EmployeesController(IValidator<EmployeeDto> validator, IMediator mediator)
    {
        _createValidator = validator;
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var employees = await _mediator.Send(new GetAllEmployeesRequest());
        return Ok(employees);
    }
    [HttpGet("{employeeId:guid}")]
    public async Task<IActionResult> GetByEmployeeID(Guid employeeId)
    {
        var employee = await _mediator.Send(new GetEmployeeRequest { EmployeeId = employeeId});
        if (employee.TypeRequest == ETypeRequests.NOT_FOUND)
        {
            return NotFound(employee.Errors);
        }
        return Ok(employee.Value);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EmployeeDto dto)
    {
        ValidationResult resultValidate = await _createValidator.ValidateAsync(dto, 
            options => options.IncludeRuleSets("CreateEmployee"));
        if (!resultValidate.IsValid)
        {
            List<string> errorsList = new List<string>();
            foreach(var error in resultValidate.Errors)
            {
                errorsList.Add(error.ErrorMessage);
            }
            return BadRequest(errorsList);
        }
        var result = await _mediator.Send(dto);
        if (result.TypeRequest == ETypeRequests.NOT_FOUND)
        {
            return NotFound(result.Errors);
        }
        if (result.TypeRequest == ETypeRequests.BAD_REQUEST)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Value);
    }
    [HttpPut("{employeeId:guid}")]
    public async Task<IActionResult> UpdatePersonalInformationEmployee(Guid employeeId, [FromBody] EmployeeDto dto)
    {
        ValidationResult resultValidate = await _createValidator.ValidateAsync(dto,
            options => options.IncludeRuleSets("UpdateEmployee"));
        if (!resultValidate.IsValid)
        {
            List<string> errorsList = new List<string>();
            foreach (var error in resultValidate.Errors)
            {
                errorsList.Add(error.ErrorMessage);
            }
            return BadRequest(errorsList);
        }
        var result = await _mediator.Send(new UpdateEmployeeRequest { EmployeeId = employeeId, Value = dto });
        if (result.TypeRequest == ETypeRequests.NOT_FOUND)
        {
            return NotFound(result.Errors);
        }
        if (result.TypeRequest == ETypeRequests.BAD_REQUEST)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Errors);
    }
    [HttpPut("{employeeId:guid}/{newOfficeId:guid}")]
    public async Task<IActionResult> UpdateOfficeEmployee(Guid employeeId, Guid newOfficeId)
    {
        var result = await _mediator.Send(new UpdateEmployeeOfficeRequest { EmployeeId = employeeId, OfficeId = newOfficeId });
        if (result.TypeRequest == ETypeRequests.BAD_REQUEST)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Value);
    }
    [HttpDelete("{employeeId:guid}")]
    public async Task<IActionResult> Delete(Guid employeeId)
    {
        var result = await _mediator.Send(new DeleteEmployeeRequest { EmployeeId = employeeId});
        if (result.TypeRequest == ETypeRequests.BAD_REQUEST)
        {
            return BadRequest(result.Errors);
        }
        return Ok();
    }
}
