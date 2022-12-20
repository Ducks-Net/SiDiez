using DucksNet.Domain.Model;
using System.Net;

namespace DucksNet.API.DTO;

public class PetDto
{
    public string? Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? Species { get; set; }
    public string? Breed { get; set; }
    public Guid OwnerId { get; set; }
    public string Size { get; set; } = string.Empty;
}
