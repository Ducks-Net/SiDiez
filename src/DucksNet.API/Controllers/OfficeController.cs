using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using Microsoft.AspNetCore.Mvc;
using DucksNet.API.DTO;
namespace DucksNet.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class OfficeController : ControllerBase
{
    private readonly IRepository<Office> repository;

    public OfficeController(IRepository<Office> repository)
    {
        this.repository = repository;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        var offices = repository.GetAll();
        return Ok(offices);
    }
    [HttpPost]
    public IActionResult Create([FromBody] OfficeDTO dto)
    {
        var office = DucksNet.Domain.Model.Office.Create(dto.BusinessId, dto.Address, dto.AnimalCapacity);
        if(office.IsFailure)
        {
            return BadRequest(office.Errors);
        }
        var result = repository.Add(office.Value!);
        if(result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok(office);
    }
    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        var office = repository.Get(id);
        if(office.IsFailure)
        {
            return NotFound(office.Errors);
        }
        return Ok(office.Value);
    }
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var office = repository.Get(id);
        if(office.IsFailure)
        {
            return NotFound(office.Errors);
        }
        var result = repository.Delete(office.Value!);
        if(result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok(office);
    }
}
