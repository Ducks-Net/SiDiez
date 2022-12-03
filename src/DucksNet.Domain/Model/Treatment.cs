using DucksNet.SharedKernel.Utils;

namespace DucksNet.Domain.Model;
public class Treatment
{
    public Guid ID { get; private set; }
    public Guid? OwnerID { get; }
    public Guid? ClientID { get; }
    public Guid? ClinicID { get; }
    //List<Guid> MedicineIDList { get; set; }
    public Treatment(Guid ID, Guid? OwnerID, Guid? ClientID, Guid? ClinicID) // + List<Guid> MedicineIDList
    {
        this.ID = ID;
        this.OwnerID = OwnerID;
        this.ClientID = ClientID;
        this.ClinicID = ClinicID;
        //this.MedicineIDList = MedicineIDList;
    }
    private Treatment() // +  List<Guid> MedicineIDList
    {
        ID = new Guid();
        //MedicineIDList = medicineIDList;
    }

    //public void AddMedicineToTreatment(Medicine medicine)
    //{
    //    if (!MedicineIDList.Contains(medicine.Id))
    //        MedicineIDList.Add(medicine.Id);
    //}

    //public static Result<Treatment> CreateTreatment(List<Guid> medicineIDList)
    //{
    //    if (medicineIDList.Count == 0)
    //        return Result<Treatment>.Error("Can't create treatment with no medicine.");
    //    return Result<Treatment>.Ok(new Treatment(medicineIDList));
    //}

    public static Result<Treatment> CreateTreatment()
    {
        return Result<Treatment>.Ok(new Treatment());
    }
}
