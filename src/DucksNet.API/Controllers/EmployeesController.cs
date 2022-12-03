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
        employeePost.Value!.AssignToOffice(office.Value!.ID);
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
        return Ok(employeePost.Value);
    }
    [HttpPut("{employeeId:guid}")]
    public IActionResult UpdatePersonalInformationEmployee(Guid employeeId, [FromBody] EmployeeDTO dto)
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
        var result = _employeesRepository.Update(oldEmployee.Value);
        return Ok(resultUpdated.First());
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
        if (oldEmployee.Value!.IdOffice != newOfficeId)
            oldEmployee.Value.AssignToOffice(newOfficeId);
        var result = _employeesRepository.Update(oldEmployee.Value);
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
        var result = _employeesRepository.Delete(employee.Value!);
        return Ok();
    }
}
