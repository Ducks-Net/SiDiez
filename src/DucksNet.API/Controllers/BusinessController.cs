using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using Microsoft.AspNetCore.Mvc;
using DucksNet.API.DTO;
namespace DucksNet.API.Controllers;


[ApiController]
[Route("api/v1/[controller]")]
public class BusinessController : ControllerBase
{
    private readonly IRepository<Business> repository;

    public BusinessController(IRepository<Business> repository)
    {
        this.repository = repository;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        var businesses = repository.GetAll();
        return Ok(businesses);
    }
    [HttpPost]
    public IActionResult Create([FromBody] BusinessDTO dto)
    {
        var business = DucksNet.Domain.Model.Business.Create(dto.BusinessName, dto.Surname, dto.FirstName, dto.Address, dto.OwnerPhone, dto.OwnerEmail);
        if(business.IsFailure)
        {
            return BadRequest(business.Errors);
        }
        var result = repository.Add(business.Value!);
        if(result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok(business);
    }
    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        var business = repository.Get(id);
        if(business.IsFailure)
        {
            return NotFound(business.Errors);
        }
        return Ok(business.Value);
    }
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var business = repository.Get(id);
        if(business.IsFailure)
        {
            return NotFound(business.Errors);
        }
        var result = repository.Delete(business.Value!);
        if(result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok(business);
    }
}
