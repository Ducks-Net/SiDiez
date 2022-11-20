using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using DucksNet.Infrastructure.Sqlite;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DucksNet.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IRepository<Employee> repository;

    public EmployeesController(IRepository<Employee> repository)
    {
        //to add the repository for sediu
        this.repository = repository;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        var employees = repository.GetAll();
        return Ok(employees);
    }
    [HttpPost]
    public IActionResult Create([FromBody] EmployeeDTO dto)
    {
        var employee = new Employee(dto.IdSediu, dto.Surname, dto.FirstName, dto.Address, dto.OwnerPhone, dto.OwnerEmail);
        repository.Add(employee);
        return Created(nameof(GetAll), employee);
    }

}
