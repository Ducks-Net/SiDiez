using DucksNet.SharedKernel.Utils;
using DucksNet.Domain.Model.Enums;

namespace DucksNet.Domain.Model;
public class Medicine
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public double Price { get; private set; }
    public DrugAdministration DrugAdministration { get; private set; }
    
    private Medicine(string name, string description, double price, int drugAdministrationId) 
        :this(name, description, price, DrugAdministration.createMedicineByInt(drugAdministrationId).Value!)
    { }
    private Medicine(string name, string description, double price, DrugAdministration drugAdministration)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Price = price;
        DrugAdministration = drugAdministration;
    }

    public static Result<Medicine> Create(string name, string description, double price, string administration)
    {
        if (price <= 0)
            return Result<Medicine>.Error("Invalid price");
        Result<DrugAdministration> typeOfDrugAdministration = DrugAdministration.createMedicineByString(administration);
        if (typeOfDrugAdministration.Value == null || typeOfDrugAdministration.IsFailure)
            return Result<Medicine>.FromError(typeOfDrugAdministration, "Failed to parse type of medicine administration.");
        return Result<Medicine>.Ok(new Medicine(name, description, price, typeOfDrugAdministration.Value));
    }
}
