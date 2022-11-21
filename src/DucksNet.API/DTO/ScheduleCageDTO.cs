namespace DucksNet.API.DTO;

public class ScheduleCageDTO
{
    public Guid PetId { get; set; }
    public Guid LocationId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
