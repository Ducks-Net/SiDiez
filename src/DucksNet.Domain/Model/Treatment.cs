using DucksNet.SharedKernel.Utils;

namespace DucksNet.Domain.Model;
public class Treatment
{
    public Guid ID { get; private set; }
    public Guid? OwnerID { get; }
    public Guid? ClientID { get; }
    public Guid? ClinicID { get; }
    
    public Treatment(Guid ID, Guid? OwnerID, Guid? ClientID, Guid? ClinicID) 
    {
        this.ID = ID;
        this.OwnerID = OwnerID;
        this.ClientID = ClientID;
        this.ClinicID = ClinicID;
    }
    private Treatment(Guid? OwnerID, Guid? ClientID, Guid? ClinicID) 
    {
        ID = new Guid();
        this.OwnerID = OwnerID;
        this.ClientID = ClientID;
        this.ClinicID = ClinicID;
        
    }

    public static Result<Treatment> CreateTreatment(Guid? OwnerID, Guid? ClientID, Guid? ClinicID)
    {
        return Result<Treatment>.Ok(new Treatment(OwnerID, ClientID, ClinicID));
    }
}
