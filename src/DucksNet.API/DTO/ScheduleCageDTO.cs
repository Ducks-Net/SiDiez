namespace DucksNet.API.DTO;

public class ScheduleCageDto
{
    public Guid PetId { get; set; }
    public Guid LocationId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
