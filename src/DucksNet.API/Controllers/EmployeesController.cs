using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using DucksNet.Infrastructure.Sqlite;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DucksNet.API.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IRepository<Employee> _employeesRepository;

    public EmployeesController(IRepository<Employee> repository)
    {
        this._employeesRepository = repository;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        var employees = _employeesRepository.GetAll();
        return Ok(employees);
    }
    [HttpGet("{employeeId}")]
    public IActionResult GetByLocationID(Guid employeeId)
    {
        var employee = _employeesRepository.GetAll().Where(c => c.Id == employeeId).ToList();
        return Ok(employee);
    }
    [HttpPost]
    public IActionResult Create([FromBody] EmployeeDTO dto)
    {
        var employee = Employee.Create(dto.IdSediu, dto.Surname, dto.FirstName, dto.Address, dto.OwnerPhone, dto.OwnerEmail);
        if (employee.IsSuccess && employee.Value is not null)
        {
           _employeesRepository.Add(employee.Value);
            return Created(nameof(GetAll), employee.Value);
        }
        return BadRequest();
    }
}
