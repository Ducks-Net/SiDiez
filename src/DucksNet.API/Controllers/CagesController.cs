using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using Microsoft.AspNetCore.Mvc;

namespace DucksNet.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CagesController : ControllerBase
{
    private readonly IRepositoryAsync<Cage> _cagesRepository;
    private readonly IRepositoryAsync<CageTimeBlock> _cageTimeBlocksRepository;
    private readonly IRepositoryAsync<Pet> _petsRepository;
    private readonly IRepositoryAsync<Office> _officeRepository;
    private readonly CageScheduleService _cageScheduleService;
    public CagesController(IRepositoryAsync<Cage> cages, IRepositoryAsync<CageTimeBlock> cageTimeBlocks, IRepositoryAsync<Pet> pets, IRepositoryAsync<Office> officeRepository)
    {
        _cagesRepository = cages;
        _cageTimeBlocksRepository = cageTimeBlocks;
        _petsRepository = pets;
        _officeRepository = officeRepository;
        _cageScheduleService = new CageScheduleService(_cagesRepository, _cageTimeBlocksRepository, _petsRepository);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var cages = await _cagesRepository.GetAllAsync();
        return Ok(cages);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var cage = await _cagesRepository.GetAsync(id);
        if (cage.IsFailure)
        {
            return NotFound(cage.Errors);
        }
        return Ok(cage.Value);
    }

    [HttpGet("byLocation/{locationId}")]
    public async Task<IActionResult> GetByLocationID(Guid locationId)
    {
        var cages = await _cagesRepository.GetAllAsync();
        var cagesAtLocation = cages.Where(c => c.LocationId == locationId);
        if (cagesAtLocation is null || !cagesAtLocation.Any())
        {
            return NotFound();
        }
        return Ok(cagesAtLocation);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCageDTO dto)
    {
        var cage = DucksNet.Domain.Model.Cage.Create(dto.SizeString);
        if(cage.IsFailure || cage.Value == null)
        {
            return BadRequest(cage.Errors);
        }

        var location = await _officeRepository.GetAsync(dto.LocationId);
        if(location.IsFailure)
        {
            return BadRequest(location.Errors);
        }
        cage.Value!.AssignToLocation(dto.LocationId);

        var result = await _cagesRepository.AddAsync(cage.Value!);
        if(result.IsFailure)
        {
            return BadRequest(result.Errors);
        }

        return Created(nameof(GetAll),cage.Value!);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var cage = await _cagesRepository.GetAsync(id);
        if(cage.IsFailure)
        {
            return NotFound(cage.Errors);
        }
        await _cagesRepository.DeleteAsync(cage.Value!);
        return Ok();
    }
    
    [HttpPost("schedule")]
    public IActionResult ScheduleCage([FromBody] ScheduleCageDTO dto)
    {
        var result = _cageScheduleService.ScheduleCage(dto.PetId, dto.LocationId, dto.StartTime, dto.EndTime);
        if(result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Value);
    }

    [HttpGet("schedule/byLocation/{locationId}")]
    public IActionResult GetCageSchedule(Guid locationId)
    {
        var result = _cageScheduleService.GetLocationSchedule(locationId);
        
        return Ok(result);
    }

    [HttpGet("schedule/byPet/{petId}")]
    public IActionResult GetPetSchedule(Guid petId)
    {
        var result = _cageScheduleService.GetPetSchedule(petId);
        
        return Ok(result);
    }
}
