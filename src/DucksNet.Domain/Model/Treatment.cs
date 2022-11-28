using DucksNet.SharedKernel.Utils;

namespace DucksNet.Domain.Model;
public class Treatment
{
    public Guid ID { get; private set; }
    public Guid? OwnerID { get; }
    public Guid? ClientID { get; }
    public Guid? ClinicID { get; }
    List<Medicine> MedicineList { get; set; }

    private Treatment()
    {
        ID = new Guid();
        MedicineList = new List<Medicine>();
    }

    public void AddMedicineToTreatment(Medicine medicine)
    {
        if (MedicineList.Contains(medicine))
            return;
        MedicineList.Add(medicine);
    }

    public static Result<Treatment> CreateTreatment()
    {
        return Result<Treatment>.Ok(new Treatment());
    }
}
