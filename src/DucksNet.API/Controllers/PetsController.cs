using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DucksNet.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PetsController : ControllerBase
{
    private readonly IRepositoryAsync<Pet> _petsRepository;
    private readonly IValidator<PetDto> _petValidator;

    public PetsController(IValidator<PetDto> petValidator, IRepositoryAsync<Pet> pets)
    {
        _petValidator = petValidator;
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

    [HttpGet("byOwner/{id}")]
    public async Task<IActionResult> GetByOwner(Guid id)
    {
        var pets = await _petsRepository.GetAllAsync();
        pets = pets.Where(p => p.OwnerId == id);
        return Ok(pets);
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] PetDto dto)
    {
        ValidationResult resultValidate = await _petValidator.ValidateAsync(dto,
            options => options.IncludeRuleSets("CreatePet"));
        if (!resultValidate.IsValid)
        {
            List<string> errorsList = new List<string>();
            foreach (var error in resultValidate.Errors)
            {
                errorsList.Add(error.ErrorMessage);
            }
            return BadRequest(errorsList);
        }
        var pet = Pet.Create(dto.Name!, dto.DateOfBirth, dto.Species!, dto.Breed!, dto.OwnerId, dto.Size);
        if (pet.IsFailure)
        {
            return BadRequest(pet.Errors);
        }

        var result = await _petsRepository.AddAsync(pet.Value!);
        if (result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok(pet.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] PetDto dto)
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
        return Ok("The information has been updated.");
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
