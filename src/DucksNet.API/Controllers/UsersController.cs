using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;

using Microsoft.AspNetCore.Mvc;

namespace DucksNet.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IRepository<User> _usersRepository;
    private readonly IRepository<Pet> _petsRepository;

    public UsersController(IRepository<User> users, IRepository<Pet> pets)
    {
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
    public IActionResult Register([FromBody] UserDTO dto)
    {
        var user = DucksNet.Domain.Model.User.Create(dto.Name, dto.Email, dto.Password);
        if(user.IsFailure)
        {
            return BadRequest(user.Errors);
        }
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
        user.Value.UpdateFields(dto.Name, dto.Email, dto.Password);

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
