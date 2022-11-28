namespace DucksNet.API.DTO;

public class MedicalRecordDTO
{
    public MedicalRecordDTO(Guid idAppointment, Guid idClient)
    {
        IdAppointment = idAppointment;
        IdClient = idClient;
    }

    public Guid IdAppointment { get; set; }
    public Guid IdClient { get; set; }
}
