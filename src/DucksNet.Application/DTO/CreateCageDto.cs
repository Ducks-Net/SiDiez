namespace DucksNet.API.DTO;
public class CreateCageDto
{
    public string SizeString { get; set; } = string.Empty;
    public Guid LocationId { get; set; }
}
