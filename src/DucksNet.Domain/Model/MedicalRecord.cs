using DucksNet.SharedKernel.Utils;

namespace DucksNet.Domain.Model;
public class MedicalRecord
{

    private MedicalRecord(Guid idAppointment, Guid idClient)
    {
        Id = Guid.NewGuid();
        IdAppointment = idAppointment;
        IdClient = idClient;
    }

    public Guid Id { get; private set; }
    public Guid IdAppointment { get; private set; }
    public Guid IdClient { get; private set; }

    //TODO (RO): to add other values (in the future) 
    
    public static Result<MedicalRecord> Create(Guid idAppointment, Guid idClient)
    {
        //TODO (RO): to check if the id's exist in the database 
        var medicalRecord = new MedicalRecord(idAppointment, idClient);
        return Result<MedicalRecord>.Ok(medicalRecord);
    }
    
    public void AssignToAppointment(Guid idAppointment)
    {
        IdAppointment = idAppointment;
    }
    public void AssignToClient(Guid idClient)
    {
        IdClient = idClient;
    }
    
}
