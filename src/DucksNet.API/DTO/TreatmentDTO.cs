using DucksNet.Domain.Model;

namespace DucksNet.API.DTO;

public class TreatmentDto
{
    //NOTE(MG): Redo everyhting with List<Medicine> when it's fixed
    public Guid OwnerID { get; set; }
    public Guid ClientID { get; set; }
    public Guid ClinicID { get; set; }

    public TreatmentDto(Guid ownerID, Guid clientID, Guid clinicID)
    {
        OwnerID = ownerID;
        ClientID = clientID;
        ClinicID = clinicID;
    }
}
