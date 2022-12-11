using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
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

    public EmployeesController(IRepositoryAsync<Employee> employeeRepository, IRepositoryAsync<Office> officeRepository)
    {
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
        var employeePost = Employee.Create(dto.Surname, dto.FirstName, dto.Address, dto.OwnerPhone, dto.OwnerEmail);
        if (employeePost.IsFailure)
        {
            return BadRequest(employeePost.Errors);
        }
        employeePost.Value!.AssignToOffice(office.Value!.ID);
        var employees = await _employeesRepository.GetAllAsync();
        foreach (var employee in employees)
        {
            if(employee.OwnerEmail == dto.OwnerEmail)
            {
                return BadRequest("The email already exists");
            }
            if (employee.OwnerPhone == dto.OwnerPhone)
            {
                return BadRequest("The telephone number already exists");
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
                return BadRequest("The updated email already exists");
            }
            if (employee.OwnerPhone == dto.OwnerPhone)
            {
                return BadRequest("The updated telephone number already exists");
            }
        }
        var resultUpdated = oldEmployee.Value!.UpdateFields(dto.Surname, dto.FirstName, dto.Address, dto.OwnerPhone, dto.OwnerEmail);
        if(resultUpdated.First() != "The information has been updated")
        {
            return BadRequest(resultUpdated.First());
        }
        await _employeesRepository.UpdateAsync(oldEmployee.Value);
        return Ok(resultUpdated.First());
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
