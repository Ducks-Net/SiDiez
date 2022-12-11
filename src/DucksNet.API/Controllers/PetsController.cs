using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using Microsoft.AspNetCore.Mvc;

namespace DucksNet.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PetsController : ControllerBase
{
    private readonly IRepositoryAsync<Pet> _petsRepository;

    public PetsController(IRepositoryAsync<Pet> pets)
    {
        _petsRepository = pets;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var pets = await _petsRepository.GetAllAsync();
        return Ok(pets);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var pet = await _petsRepository.GetAsync(id);
        if (pet.IsFailure)
        {
            return NotFound(pet.Errors);
        }
        return Ok(pet.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] PetDTO dto)
    {
        var pet = DucksNet.Domain.Model.Pet.Create(dto.Name!, dto.DateOfBirth, dto.Species!, dto.Breed!, dto.OwnerId, dto.Size);
        if (pet.IsFailure)
        {
            return BadRequest(pet.Errors);
        }
        if (dto.OwnerId == Guid.Empty)
        {
            return BadRequest("Owner ID is required.");
        }

        var result = await _petsRepository.AddAsync(pet.Value!);
        if (result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok(pet.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] PetDTO dto)
    {
        // Check if pet exists
        var pet = await _petsRepository.GetAsync(id);
        if (pet.IsFailure)
        {
            return NotFound(pet.Errors);
        }
        // Update pet
        pet.Value!.UpdateFields(dto.Name, dto.DateOfBirth, dto.Species!, dto.Breed!, dto.OwnerId, dto.Size);

        var result = await _petsRepository.UpdateAsync(pet.Value!);
        if (result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok(pet.Value);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var pet = await _petsRepository.GetAsync(id);
        if (pet.IsFailure)
        {
            return NotFound(pet.Errors);
        }
        var result = await _petsRepository.DeleteAsync(pet.Value!);
        if (result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok();
    }
}
