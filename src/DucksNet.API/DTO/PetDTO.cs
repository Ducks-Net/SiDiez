namespace DucksNet.API.DTO;

public class PetDTO
{
    public string Size { get; set; } = string.Empty;
    public Guid? OwnerId { get; set; }
}
