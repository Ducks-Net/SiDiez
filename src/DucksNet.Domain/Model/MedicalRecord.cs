using DucksNet.SharedKernel.Utils;

namespace DucksNet.Domain.Model;
public class MedicalRecord
{
    public MedicalRecord(Guid id, Guid idAppointment, Guid idClient)
    {
        Id = id;
        IdClient = idClient;
        IdAppointment = idAppointment;
    }

    private MedicalRecord(Guid idAppointment, Guid idClient)
    {
        Id = Guid.NewGuid();
        IdAppointment = idAppointment;
        IdClient = idClient;
    }

    public Guid Id { get; private set; }
    public Guid IdAppointment { get; private set; }
    public Guid IdClient { get; private set; }

    //NOTE (RO): to add other values (in the future) 
    
    public static Result<MedicalRecord> Create(Guid idAppointment, Guid idClient)
    {
        if (idAppointment == Guid.Empty)
        {
            return Result<MedicalRecord>.Error("Id appointment can not be empty");
        }
        if (idClient == Guid.Empty)
        {
            return Result<MedicalRecord>.Error("Id client can not be empty");
        }
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
