using DucksNet.API.DTOs;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;

using Microsoft.AspNetCore.Mvc;

namespace DucksNet.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CagesController : ControllerBase
{
    private readonly IRepository<Cage> _cagesRepository;
    private readonly IRepository<CageTimeBlock> _cageTimeBlocksRepository;
    private readonly IRepository<Pet> _petsRepository;
    private readonly CageScheduleService _cageScheduleService;
    public CagesController(IRepository<Cage> cages, IRepository<CageTimeBlock> cageTimeBlocks, IRepository<Pet> pets)
    {
        _cagesRepository = cages;
        _cageTimeBlocksRepository = cageTimeBlocks;
        _petsRepository = pets;
        _cageScheduleService = new CageScheduleService(cages, cageTimeBlocks, pets);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var cages = _cagesRepository.GetAll();
        return Ok(cages);
    }

    [HttpGet("{locationId}")]
    public IActionResult GetByLocationID(Guid locationId)
    {
        var cage = _cagesRepository.GetAll().Where(c => c.LocationId == locationId).ToList();
        return Ok(cage);
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

    [HttpGet("schedule/{locationId}")]
    public IActionResult GetCageSchedule(Guid locationId)
    {
        var result = _cageScheduleService.GetLocationSchedule(locationId);
        if(result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Value);
    }

    [HttpGet("schedule/{petId}")]
    public IActionResult GetPetSchedule(Guid petId)
    {
        var result = _cageScheduleService.GetPetSchedule(petId);
        if(result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Value);
    }
}
