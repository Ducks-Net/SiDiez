using DucksNet.SharedKernel.Utils;
using DucksNet.Domain.Model.Enums;
using System.Net;

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
        if (price <= 0)
            return Result<Medicine>.Error("Invalid price");
        Result<DrugAdministration> typeOfDrugAdministrationByString = DrugAdministration.createMedicineByString(administration);
        if (typeOfDrugAdministrationByString.Value == null || typeOfDrugAdministrationByString.IsFailure)
            return Result<Medicine>.FromError(typeOfDrugAdministrationByString, "Failed to parse type of medicine administration by string.");
        return Result<Medicine>.Ok(new Medicine(name, description, price, typeOfDrugAdministrationByString.Value));
    }

    public void UpdateMedicineFields(string name, string description, double price)
    {
        if (name != null)
        {
            Name = name;
        }
        if (description!= null)
        {
            Description = description;
        }
        if (price <= 0)
        {
            Price = price;
        }
    }
}
