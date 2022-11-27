using DucksNet.API.DTO;
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
    private readonly IRepository<Office> _officeRepository;
    private readonly CageScheduleService _cageScheduleService;
    public CagesController(IRepository<Cage> cages, IRepository<CageTimeBlock> cageTimeBlocks, IRepository<Pet> pets, IRepository<Office> officeRepository)
    {
        _cagesRepository = cages;
        _cageTimeBlocksRepository = cageTimeBlocks;
        _petsRepository = pets;
        _officeRepository = officeRepository;
        _cageScheduleService = new CageScheduleService(_cagesRepository, _cageTimeBlocksRepository, _petsRepository);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var cages = _cagesRepository.GetAll();
        return Ok(cages);
    }

    [HttpGet("byLocation/{locationId}")]
    public IActionResult GetByLocationID(Guid locationId)
    {
        var cage = _cagesRepository.GetAll().Where(c => c.LocationId == locationId).ToList();
        return Ok(cage);
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateCageDTO dto)
    {
        var cage = DucksNet.Domain.Model.Cage.Create(dto.SizeString);
        if(cage.IsFailure || cage.Value == null)
        {
            return BadRequest(cage.Errors);
        }

        var location = _officeRepository.Get(dto.LocationId);
        if(location.IsFailure)
        {
            return BadRequest(location.Errors);
        }
        cage.Value!.AssignToLocation(dto.LocationId);

        var result = _cagesRepository.Add(cage.Value!);
        if(result.IsFailure)
        {
            return BadRequest(result.Errors);
        }

        return Created(nameof(GetAll),cage.Value!);
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
