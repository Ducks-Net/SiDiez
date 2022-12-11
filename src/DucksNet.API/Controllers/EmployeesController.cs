using DucksNet.API.DTO;
using DucksNet.API.Validators;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using DucksNet.SharedKernel.Utils;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DucksNet.API.Controllers;
[EnableCors("VetPolicyCors")]
[Route("api/v1/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IValidator<EmployeeDTO> _createValidator;
    private readonly IRepository<Employee> _employeesRepository;
    private readonly IRepository<Office> _officeRepository;

    public EmployeesController(IValidator<EmployeeDTO> validator, IRepository<Employee> employeeRepository, IRepository<Office> officeRepository)
    {
        _createValidator = validator;
        _employeesRepository = employeeRepository;
        _officeRepository = officeRepository;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        var employees = _employeesRepository.GetAll();
        return Ok(employees);
    }
    [HttpGet("{employeeId:guid}")]
    public IActionResult GetByEmployeeID(Guid employeeId)
    {
        var employee = _employeesRepository.Get(employeeId);
        if (employee.IsFailure)
        {
            return NotFound(employee.Errors);
        }
        return Ok(employee.Value);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EmployeeDTO dto)
    {
        var office = _officeRepository.Get(dto.IdOffice);
        if (office.IsFailure)
        {
            return BadRequest(office.Errors);
        }
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
        var employeePost = Employee.Create(dto.Surname, dto.FirstName, dto.Address, dto.OwnerPhone, dto.OwnerEmail);
        employeePost.Value!.AssignToOffice(office.Value!.ID);
        var employees = _employeesRepository.GetAll();
        foreach (var employee in employees)
        {
            if (employee.OwnerEmail == dto.OwnerEmail)
            {
                return BadRequest(new List<string> { "The email already exists" });
            }
            if (employee.OwnerPhone == dto.OwnerPhone)
            {
                return BadRequest(new List<string> { "The telephone number already exists" });
            }
        }
        _employeesRepository.Add(employeePost.Value);
        return Ok(employeePost.Value);
    }
    [HttpPut("{employeeId:guid}")]
    public async Task<IActionResult> UpdatePersonalInformationEmployee(Guid employeeId, [FromBody] EmployeeDTO dto)
    {
        var oldEmployee = _employeesRepository.Get(employeeId);
 
        if (oldEmployee.IsFailure)
        {
            return BadRequest(oldEmployee.Errors);
        }
        var employees = _employeesRepository.GetAll();
        foreach (var employee in employees)
        {
            if (employee.OwnerEmail == dto.OwnerEmail)
            {
                return BadRequest(new List<string> { "The updated email already exists" });
            }
            if (employee.OwnerPhone == dto.OwnerPhone)
            {
                return BadRequest(new List<string> { "The updated telephone number already exists" });
            }
        }
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
        oldEmployee.Value!.UpdateFields(dto.Surname, dto.FirstName, dto.Address, dto.OwnerPhone, dto.OwnerEmail);
        _employeesRepository.Update(oldEmployee.Value);
        return Ok("The information has been updated");
    }
    [HttpPut("{employeeId:guid}/{newOfficeId:guid}")]
    public IActionResult UpdateOfficeEmployee(Guid employeeId, Guid newOfficeId)
    {
        var oldEmployee = _employeesRepository.Get(employeeId);
        if (oldEmployee.IsFailure)
        {
            return BadRequest(oldEmployee.Errors);
        }
        var newOffice = _officeRepository.Get(newOfficeId);
        if (newOffice.IsFailure)
        {
            return BadRequest(newOffice.Errors);
        }
        oldEmployee.Value!.AssignToOffice(newOfficeId);
        _employeesRepository.Update(oldEmployee.Value);
        return Ok(oldEmployee.Value);
    }
    [HttpDelete("{employeeId:guid}")]
    public IActionResult Delete(Guid employeeId)
    {
        var employee = _employeesRepository.Get(employeeId);
        if (employee.IsFailure)
        {
            return BadRequest(employee.Errors);
        }
        _employeesRepository.Delete(employee.Value!);
        return Ok();
    }
}
