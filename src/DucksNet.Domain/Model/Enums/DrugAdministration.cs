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
        if (administrationByString is null)
            return Result<DrugAdministration>.Error("Wrong type of drug administration");
        return Result<DrugAdministration>.Ok(administrationByString);
    }
    public static Result<DrugAdministration> createMedicineByInt(int id)
    {
        var administrationByInt = GetAll<DrugAdministration>().FirstOrDefault(x => x.Id == id);
        if (administrationByInt is null)
            return Result<DrugAdministration>.Error("Wrong type of drug administration");
        return Result<DrugAdministration>.Ok(administrationByInt);
    }
}
