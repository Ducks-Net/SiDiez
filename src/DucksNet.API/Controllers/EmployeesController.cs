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
    private readonly IRepositoryAsync<Employee> _employeesRepository;
    private readonly IRepositoryAsync<Office> _officeRepository;
    private readonly IValidator<EmployeeDTO> _createValidator;

    public EmployeesController(IValidator<EmployeeDTO> validator, IRepositoryAsync<Employee> employeeRepository, IRepositoryAsync<Office> officeRepository)
    {
        _createValidator = validator;
        _employeesRepository = employeeRepository;
        _officeRepository = officeRepository;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var employees = await _employeesRepository.GetAllAsync();
        return Ok(employees);
    }
    [HttpGet("{employeeId:guid}")]
    public async Task<IActionResult> GetByEmployeeID(Guid employeeId)
    {
        var employee = await _employeesRepository.GetAsync(employeeId);
        if (employee.IsFailure)
        {
            return NotFound(employee.Errors);
        }
        return Ok(employee.Value);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EmployeeDTO dto)
    {
        var office = await _officeRepository.GetAsync(dto.IdOffice);
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
        var employees = await _employeesRepository.GetAllAsync();
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
        await _employeesRepository.AddAsync(employeePost.Value);
        return Ok(employeePost.Value);
    }
    [HttpPut("{employeeId:guid}")]
    public async Task<IActionResult> UpdatePersonalInformationEmployee(Guid employeeId, [FromBody] EmployeeDTO dto)
    {
        var oldEmployee = await _employeesRepository.GetAsync(employeeId);
 
        if (oldEmployee.IsFailure)
        {
            return BadRequest(oldEmployee.Errors);
        }
        var employees = await _employeesRepository.GetAllAsync();
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
        await _employeesRepository.UpdateAsync(oldEmployee!.Value);
        return Ok("The information has been updated");
    }
    [HttpPut("{employeeId:guid}/{newOfficeId:guid}")]
    public async Task<IActionResult> UpdateOfficeEmployee(Guid employeeId, Guid newOfficeId)
    {
        var oldEmployee = await _employeesRepository.GetAsync(employeeId);
        if (oldEmployee.IsFailure)
        {
            return BadRequest(oldEmployee.Errors);
        }
        var newOffice = await _officeRepository.GetAsync(newOfficeId);
        if (newOffice.IsFailure)
        {
            return BadRequest(newOffice.Errors);
        }
        oldEmployee.Value!.AssignToOffice(newOfficeId);
        await _employeesRepository.UpdateAsync(oldEmployee.Value);
        return Ok(oldEmployee.Value);
    }
    [HttpDelete("{employeeId:guid}")]
    public async Task<IActionResult> Delete(Guid employeeId)
    {
        var employee = await _employeesRepository.GetAsync(employeeId);
        if (employee.IsFailure)
        {
            return BadRequest(employee.Errors);
        }
        await _employeesRepository.DeleteAsync(employee.Value!);
        return Ok();
    }
}
