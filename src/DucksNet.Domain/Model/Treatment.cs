using DucksNet.SharedKernel.Utils;

namespace DucksNet.Domain.Model;
public class Treatment
{
    public Guid ID { get; private set; }
    public Guid? OwnerID { get; private set; }
    public Guid? ClientID { get; private set; }
    public Guid? ClinicID { get; private set; }
    
    public Treatment(Guid ID, Guid? OwnerID, Guid? ClientID, Guid? ClinicID) 
    {
        this.ID = ID;
        this.OwnerID = OwnerID;
        this.ClientID = ClientID;
        this.ClinicID = ClinicID;
    }
    private Treatment(Guid? OwnerID, Guid? ClientID, Guid? ClinicID) 
    {
        ID = Guid.NewGuid();
        this.OwnerID = OwnerID;
        this.ClientID = ClientID;
        this.ClinicID = ClinicID;
    }

    public static Result<Treatment> CreateTreatment(Guid OwnerID, Guid ClientID, Guid ClinicID)
    {
        if (OwnerID == Guid.Empty)
            return Result<Treatment>.Error("Owner ID can not be empty");
        if (ClientID == Guid.Empty)
            return Result<Treatment>.Error("Client ID can not be empty");
        if (ClinicID == Guid.Empty)
            return Result<Treatment>.Error("Clinic ID can not be empty");
        return Result<Treatment>.Ok(new Treatment(OwnerID, ClientID, ClinicID));
    }
}
