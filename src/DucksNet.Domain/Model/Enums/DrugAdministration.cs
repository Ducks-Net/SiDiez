using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DucksNet.SharedKernel.Utils;

namespace DucksNet.Domain.Model.Enums;
public class DrugAdministration : Enumeration
{
    public static readonly DrugAdministration Oral = new(1,"Oral");
    public static readonly DrugAdministration Inhalation = new(2,"Inhalation");
    public static readonly DrugAdministration Intradermal = new(3,"Intradermal");
    public static readonly DrugAdministration Subcutaneous = new(4,"Subcutaneous");
    public static readonly DrugAdministration Intramuscular = new(5,"Intramuscular");
    public static readonly DrugAdministration Ointment = new(6,"Ointment");

    public DrugAdministration(int id, string name) : base(id, name)
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
