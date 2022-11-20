using DucksNet.SharedKernel.Utils;

namespace DucksNet.Domain.Model;
public class MedicalRecord
{

    public MedicalRecord(Guid idAppointment, Guid idClient)
    {
        Id = Guid.NewGuid();
        IdAppointment = idAppointment;
        IdClient = idClient;
    }

    public Guid Id { get; private set; }
    public Guid IdAppointment { get; private set; }
    public Guid IdClient { get; private set; }

    //TODO: to add other values (in the future) (RO)
    /*
    public static Result<MedicalRecord> Create(Guid idAppointment, Guid idClient)
    {
        //TODO: to check if the id's exist in the database (RO)
        var medicalRecors = new MedicalRecord
        {
            Id = Guid.NewGuid(),
            IdAppointment = idAppointment,
            IdClient = idClient
        };
        return Result<MedicalRecord>.Ok(medicalRecors);
    }
    */
    public void AssignToAppointment(Guid idAppointment)
    {
        IdAppointment = idAppointment;
    }
    public void AssignToClient(Guid idClient)
    {
        IdClient = idClient;
    }
    
}
