using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using DucksNet.Infrastructure.Sqlite;
using DucksNet.SharedKernel;
using Microsoft.AspNetCore.Mvc;

namespace DucksNet.API.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IRepository<Employee> _employeesRepository;
    private readonly IRepository<Office> _officeRepository;

    public EmployeesController(IRepository<Employee> employeeRepository, IRepository<Office> officeRepository)
    {
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
    public IActionResult Create([FromBody] EmployeeDTO dto)
    {
        var office = _officeRepository.Get(dto.IdOffice);
        if (office.IsFailure)
        {
            return BadRequest(office.Errors);
        }
        var employeePost = Employee.Create(dto.Surname, dto.FirstName, dto.Address, dto.OwnerPhone, dto.OwnerEmail);
        if (employeePost.IsFailure)
        {
            return BadRequest(employeePost.Errors);
        }
        var employees = _employeesRepository.GetAll();
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
        var result = _employeesRepository.Add(employeePost.Value);
        if (result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok(employeePost.Value);
    }
    [HttpPut("{token:guid}")]
    public IActionResult UpdatePersonalInformationEmployee(Guid token, [FromBody] EmployeeDTO dto)
    {
        //TODO(RO): token must be based from the authentification token, not from employee id
        var oldEmployee = _employeesRepository.Get(token);
        if (oldEmployee.IsFailure)
        {
            return BadRequest(oldEmployee.Errors);
        }
        oldEmployee.Value!.UpdateFields(dto.Surname, dto.FirstName, dto.Address, dto.OwnerPhone, dto.OwnerEmail);
        var result = _employeesRepository.Update(oldEmployee.Value);
        if (result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok(oldEmployee.Value);
    }
    [HttpPut("{token:guid}/{newOfficeId:guid}")]
    public IActionResult UpdateOfficeEmployee(Guid token, Guid newOfficeId)
    {
        //TODO(RO): token must be based from the authentification token, not from employee id
        var oldEmployee = _employeesRepository.Get(token);
        if (oldEmployee.IsFailure)
        {
            return BadRequest(oldEmployee.Errors);
        }
        var newOffice = _officeRepository.Get(newOfficeId);
        if (newOffice.IsFailure)
        {
            return BadRequest(newOffice.Errors);
        }
        if (oldEmployee.Value!.IdSediu != newOfficeId)
            oldEmployee.Value.AssignToSediu(newOfficeId);
        var result = _employeesRepository.Update(oldEmployee.Value);
        if (result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok(oldEmployee.Value);
    }
    [HttpDelete("{token:guid}")]
    public IActionResult Delete(Guid token)
    {
        //TODO(RO): token must be based from the authentification token, not from employee id
        var employee = _employeesRepository.Get(token);
        if (employee.IsFailure)
        {
            return BadRequest(employee.Errors);
        }
        var result = _employeesRepository.Delete(employee.Value!);
        if (result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok();
    }
}
