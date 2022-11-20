using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;

using Microsoft.AspNetCore.Mvc;

namespace DucksNet.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CagesController : ControllerBase
{
    private readonly IRepository<Cage> _cagesRepository;
    public CagesController(IRepository<Cage> repository)
    {
        _cagesRepository = repository;
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
}
