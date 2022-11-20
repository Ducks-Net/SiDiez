using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DucksNet.SharedKernel.Utils;

namespace DucksNet.Domain.Model.Enums;
public class DrugAdministration : Enumeration
{
    public static readonly DrugAdministration Oral = new("Oral");
    public static readonly DrugAdministration Inhalation = new("Inhalation");
    public static readonly DrugAdministration Intradermal = new("Intradermal");
    public static readonly DrugAdministration Subcutaneous = new("Subcutaneous");
    public static readonly DrugAdministration Intramuscular = new("Intramuscular");
    public static readonly DrugAdministration Ointment = new("Ointment");

    public DrugAdministration(string name) : base(name)
    {
    }
    public static Result<DrugAdministration> createMedicineByString(string str)
    {
        var administrationByString = GetAll<DrugAdministration>().FirstOrDefault(x => x.Name == str);
        if (administrationByString == null)
            return Result<DrugAdministration>.Error("Wrong type of drug administration");
        return Result<DrugAdministration>.Ok(administrationByString);
    }
  
}
