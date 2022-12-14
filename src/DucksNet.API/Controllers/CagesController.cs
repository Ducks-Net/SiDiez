using AutoMapper;
using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using DucksNet.SharedKernel.Utils;
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
    private readonly IMapper _mapper;
    public CagesController(IRepositoryAsync<Cage> cages, IRepositoryAsync<CageTimeBlock> cageTimeBlocks, IRepositoryAsync<Pet> pets, IRepositoryAsync<Office> officeRepository, IMapper mapper)
    {
        _cagesRepository = cages;
        _cageTimeBlocksRepository = cageTimeBlocks;
        _petsRepository = pets;
        _officeRepository = officeRepository;
        _cageScheduleService = new CageScheduleService(_cagesRepository, _cageTimeBlocksRepository, _petsRepository);
        _mapper = mapper;
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
        // Check if location exists
        var location = await _officeRepository.GetAsync(locationId);

        if (location.IsFailure)
            return NotFound();

        // Get cages for location
        var cages = await _cagesRepository.GetAllAsync();
        var cagesAtLocation = cages.Where(c => c.LocationId == locationId);
        return Ok(cagesAtLocation);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCageDto dto)
    {
        var cage = _mapper.Map<Result<Cage>>(dto);
        if(cage.IsFailure || cage.Value == null)
        {
            return BadRequest(cage.Errors);
        }

        var location = await _officeRepository.GetAsync(dto.LocationId);
        if(location.IsFailure)
        {
            return BadRequest(location.Errors);
        }
        cage.Value.AssignToLocation(dto.LocationId);

        await _cagesRepository.AddAsync(cage.Value!);

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
    public async Task<IActionResult> ScheduleCage([FromBody] ScheduleCageDto dto)
    {
        var result = await _cageScheduleService.ScheduleCage(dto.PetId, dto.LocationId, dto.StartTime, dto.EndTime);
        if(result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Value);
    }

    [HttpGet("schedule/byLocation/{locationId:guid}")]
    public async Task<IActionResult> GetCageSchedule(Guid locationId)
    {
        var result = await _cageScheduleService.GetLocationSchedule(locationId);
        
        return Ok(result);
    }

    [HttpGet("schedule/byPet/{petId:guid}")]
    public async Task<IActionResult> GetPetSchedule(Guid petId)
    {
        var result = await _cageScheduleService.GetPetSchedule(petId);
        
        return Ok(result);
    }
}
