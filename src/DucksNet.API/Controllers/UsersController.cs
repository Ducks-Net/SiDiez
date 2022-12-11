using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace DucksNet.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IValidator<UserDTO> _userValidator;
    private readonly IRepository<User> _usersRepository;
    private readonly IRepository<Pet> _petsRepository;

    public UsersController(IValidator<UserDTO> userValidator, IRepository<User> users, IRepository<Pet> pets)
    {
        _userValidator = userValidator;
        _usersRepository = users;
        _petsRepository = pets;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _usersRepository.GetAll();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        var user = _usersRepository.Get(id);
        if(user.IsFailure)
        {
            return NotFound(user.Errors);
        }
        return Ok(user.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UserDTO dto)
    {
        ValidationResult resultValidate = await _userValidator.ValidateAsync(dto,
            options => options.IncludeRuleSets("CreateUser"));
        if (!resultValidate.IsValid)
        {
            List<string> errorsList = new List<string>();
            foreach (var error in resultValidate.Errors)
            {
                errorsList.Add(error.ErrorMessage);
            }
            return BadRequest(errorsList);
        }
        var user = Domain.Model.User.Create(dto.FirstName, dto.LastName, dto.Address, dto.PhoneNumber!, dto.Email!, dto.Password);
        var result = _usersRepository.Add(user.Value!);
        if(result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok(user);
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, [FromBody] UserDTO dto)
    {
        // Check if user exists
        var user = _usersRepository.Get(id);
        if(user.IsFailure || user.Value is null)
        {
            return NotFound(user.Errors);
        }

        // Update user
        user.Value.UpdateFields(dto.FirstName, dto.LastName, dto.Address, dto.PhoneNumber!, dto.Email!, dto.Password);

        var result = _usersRepository.Update(user.Value);
        if(result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok(user);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        // Check if user exists
        var user = _usersRepository.Get(id);
        if(user.IsFailure || user.Value is null)
        {
            return NotFound(user.Errors);
        }

        // Delete user
        var result = _usersRepository.Delete(user.Value);
        if(result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok(user);
    }
}
