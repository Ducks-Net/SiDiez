using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DucksNet.SharedKernel.Utils;

namespace DucksNet.Domain.Model;
public class Treatment
{
    public Guid ID { get; }
    public Guid? OwnerID { get; }
    public Guid? ClientID { get; }
    public Guid? ClinicID { get; }
    List<Medicine> MedicineList { get; set; }

    private Treatment(List<Medicine> medicineList)
    {
        ID = new Guid();
        MedicineList = medicineList;
    }

    public void AddMedicineToTreatment(Medicine medicine)
    {
        if (MedicineList.Contains(medicine))
            return;
        MedicineList.Add(medicine);
    }

    public static Result<Treatment> CreateTreatment(List<Medicine> medicineList)
    {
        if (medicineList.Count == 0)
            return Result<Treatment>.Error("Cannot create treatment as the medicine list is empty.");
        return Result<Treatment>.Ok(new Treatment(medicineList));
    }
}
