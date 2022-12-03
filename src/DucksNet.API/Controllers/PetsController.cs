using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using Microsoft.AspNetCore.Mvc;

namespace DucksNet.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PetsController : ControllerBase
{
    private readonly IRepository<Pet> _petsRepository;

    public PetsController(IRepository<Pet> pets)
    {
        _petsRepository = pets;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var pets = _petsRepository.GetAll();
        return Ok(pets);
    }

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        var pet = _petsRepository.Get(id);
        if (pet.IsFailure)
        {
            return NotFound(pet.Errors);
        }
        return Ok(pet.Value);
    }

    [HttpPost]
    public IActionResult Register([FromBody] PetDTO dto)
    {
        var pet = DucksNet.Domain.Model.Pet.Create(dto.Name, dto.DateOfBirth, dto.Species, dto.Breed, (Guid)dto.OwnerId, dto.Size);
        if (pet.IsFailure)
        {
            return BadRequest(pet.Errors);
        }
        if (dto.OwnerId == null)
        {
            return BadRequest("Owner ID is required.");
        }
        pet.Value!.AssignToOwner(dto.OwnerId.Value);

        var result = _petsRepository.Add(pet.Value!);
        if (result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok(pet.Value);
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, [FromBody] PetDTO dto)
    {
        // Check if pet exists
        var pet = _petsRepository.Get(id);
        if (pet.IsFailure)
        {
            return NotFound(pet.Errors);
        }
        // Update pet
        pet.Value!.UpdateFields(dto.Name, dto.DateOfBirth, dto.Species, dto.Breed, dto.OwnerId, dto.Size);

        var result = _petsRepository.Update(pet.Value!);
        if (result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok(pet.Value);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var pet = _petsRepository.Get(id);
        if (pet.IsFailure)
        {
            return NotFound(pet.Errors);
        }
        var result = _petsRepository.Delete(pet.Value!);
        if (result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok();
    }
}
