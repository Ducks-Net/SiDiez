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

    public Medicine(Guid id, string name, string description, double price, DrugAdministration drugAdministration)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        DrugAdministration = drugAdministration;
    }
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
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result<Medicine>.Error("The name should contain at least one character.");
        }
        if (string.IsNullOrWhiteSpace(description))
        {
            return Result<Medicine>.Error("The description should contain at least one character.");
        }
        if (price <= 0)
        {
            return Result<Medicine>.Error("Invalid price");
        }       
        Result<DrugAdministration> typeOfDrugAdministrationByString = DrugAdministration.createMedicineByString(administration);
        if (typeOfDrugAdministrationByString.Value == null || typeOfDrugAdministrationByString.IsFailure)
        {
            return Result<Medicine>.FromError(typeOfDrugAdministrationByString, "Failed to parse type of medicine administration by string.");
        }
        return Result<Medicine>.Ok(new Medicine(name, description, price, typeOfDrugAdministrationByString.Value));
    }

    public Result UpdateMedicineFields(string name, string description, double price, string drugAdministrationString)
    {
        // TODO(MG): simplify it
        if (!string.IsNullOrWhiteSpace(name))
        {
            Name = name;
        }
        else 
        {
            return Result.Error("Name is empty.");
        }
        if (!string.IsNullOrWhiteSpace(description))
        {
            Description = description;
        }
        else 
        {
            return Result.Error("Description is empty.");
        }
        if (price >= 0)
        {
            Price = price;
        }
        else
        {
            return Result.Error("Invalid price.");
        }
        Result<DrugAdministration> drugAdministrationResult = DrugAdministration.createMedicineByString(drugAdministrationString);
        if (!(drugAdministrationResult.IsFailure || drugAdministrationResult.Value == null))
        {
            DrugAdministration = drugAdministrationResult.Value;
        }
        else 
        {
            return Result.Error("Invalid type of drug administration.");
        }
        return Result.Ok();
    }
}
