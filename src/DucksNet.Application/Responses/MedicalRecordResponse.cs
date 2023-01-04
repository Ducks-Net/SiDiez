namespace DucksNet.Application.Responses;
public class MedicalRecordResponse
{
    public Guid Id { get; private set; }
    public Guid IdAppointment { get; private set; }
    public Guid IdClient { get; private set; }
    public MedicalRecordResponse(Guid id, Guid idAppointment, Guid idClient)
    {
        Id = id;
        IdAppointment = idAppointment;
        IdClient = idClient;
    }
}
