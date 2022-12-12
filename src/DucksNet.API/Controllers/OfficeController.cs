using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using Microsoft.AspNetCore.Mvc;
using DucksNet.API.DTO;
namespace DucksNet.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class OfficeController : ControllerBase
{
    private readonly IRepositoryAsync<Office> _repository;

    public OfficeController(IRepositoryAsync<Office> repository)
    {
        this._repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var offices = await _repository.GetAllAsync();
        return Ok(offices);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OfficeDTO dto)
    {
        var office = DucksNet.Domain.Model.Office.Create(dto.BusinessId, dto.Address, dto.AnimalCapacity);
        if(office.IsFailure)
        {
            return BadRequest(office.Errors);
        }
        var result = await _repository.AddAsync(office.Value!);
        if(result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok(office.Value!);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var office = await _repository.GetAsync(id);
        if(office.IsFailure)
        {
            return NotFound(office.Errors);
        }
        return Ok(office.Value);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var office = await _repository.GetAsync(id);
        if(office.IsFailure)
        {
            return NotFound(office.Errors);
        }
        var result = await _repository.DeleteAsync(office.Value!);
        if(result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok(office);
    }
}
