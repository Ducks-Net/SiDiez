﻿using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace DucksNet.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PetsController : ControllerBase
{
    private readonly IValidator<PetDTO> _petValidator;
    private readonly IRepository<Pet> _petsRepository;

    public PetsController(IValidator<PetDTO> petValidator, IRepository<Pet> pets)
    {
        _petValidator = petValidator;
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
    public async Task<IActionResult> Register([FromBody] PetDTO dto)
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
        if (dto.OwnerId == Guid.Empty)
        {
            return BadRequest("Owner ID is required.");
        }

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
        pet.Value!.UpdateFields(dto.Name, dto.DateOfBirth, dto.Species!, dto.Breed!, dto.OwnerId, dto.Size);

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
