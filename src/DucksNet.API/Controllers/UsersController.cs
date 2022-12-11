using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using DucksNet.Infrastructure.Prelude;

using Microsoft.AspNetCore.Mvc;

namespace DucksNet.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IRepositoryAsync<User> _usersRepository;
    private readonly IRepositoryAsync<Pet> _petsRepository;

    public UsersController(IRepositoryAsync<User> users, IRepositoryAsync<Pet> pets)
    {
        _usersRepository = users;
        _petsRepository = pets;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _usersRepository.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var user = await _usersRepository.GetAsync(id);
        if(user.IsFailure)
        {
            return NotFound(user.Errors);
        }
        return Ok(user.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UserDTO dto)
    {
        var user = DucksNet.Domain.Model.User.Create(dto.FirstName, dto.LastName, dto.Address, dto.PhoneNumber!, dto.Email!, dto.Password);
        if(user.IsFailure)
        {
            return BadRequest(user.Errors);
        }
        var result = await _usersRepository.AddAsync(user.Value!);
        if(result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok(user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UserDTO dto)
    {
        // Check if user exists
        var user = await _usersRepository.GetAsync(id);
        if(user.IsFailure || user.Value is null)
        {
            return NotFound(user.Errors);
        }

        // Update user
        user.Value.UpdateFields(dto.FirstName, dto.LastName, dto.Address, dto.PhoneNumber!, dto.Email!, dto.Password);

        var result = await _usersRepository.UpdateAsync(user.Value);
        if(result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok(user);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        // Check if user exists
        var user = await _usersRepository.GetAsync(id);
        if(user.IsFailure || user.Value is null)
        {
            return NotFound(user.Errors);
        }

        // Delete user
        var result = await _usersRepository.DeleteAsync(user.Value);
        if(result.IsFailure)
        {
            return BadRequest(result.Errors);
        }
        return Ok(user);
    }
}
