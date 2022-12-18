using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using Microsoft.AspNetCore.Mvc;
using DucksNet.API.DTO;
namespace DucksNet.API.Controllers;


[ApiController]
[Route("api/v1/[controller]")]
public class BusinessController : ControllerBase
{
    private readonly IRepositoryAsync<Business> _repository;

    public BusinessController(IRepositoryAsync<Business> repository)
    {
        this._repository = repository;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var businesses = await _repository.GetAllAsync();
        return Ok(businesses);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BusinessDto dto)
    {
        var business = Business.Create(dto.BusinessName, dto.Surname, dto.FirstName, dto.Address, dto.OwnerPhone, dto.OwnerEmail);
        if(business.IsFailure)
        {
            return BadRequest(business.Errors);
        }
        await _repository.AddAsync(business.Value!);
        return Ok(business.Value!);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var business = await _repository.GetAsync(id);
        if(business.IsFailure)
        {
            return NotFound(business.Errors);
        }
        return Ok(business.Value);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var business = await _repository.GetAsync(id);
        if(business.IsFailure)
        {
            return NotFound(business.Errors);
        }
        await _repository.DeleteAsync(business.Value!);
        return Ok(business);
    }
}
