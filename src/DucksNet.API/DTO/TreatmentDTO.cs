using DucksNet.Domain.Model;

namespace DucksNet.API.DTO;

public class TreatmentDTO
{
    public Guid OwnerID { get; set; }
    public Guid ClientID { get; set; }
    public Guid ClinicID { get; set; }
   // public List<Medicine> MedicineList { get; set; } 
    /*public TreatmentDTO(Guid treatmentID, Guid ownerID, Guid clientID, Guid clinicID, List<Medicine> medicineList) 
    {
        TreatmentID = treatmentID;
        OwnerID = ownerID;
        ClientID= clientID;
        ClinicID = clinicID;
        MedicineList = medicineList;
    }*/
    public IEnumerable<Guid> MedicineIDList { get; set; } 
}
