public class ScheduleAppointmentDTO
{
    public Guid LocationID { get; set; }
    public Guid PetID { get; set; }
    public string TypeString { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
