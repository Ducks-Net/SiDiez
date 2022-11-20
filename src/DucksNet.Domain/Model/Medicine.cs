using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DucksNet.SharedKernel.Utils;
using DucksNet.Domain.Model.Enums;

namespace DucksNet.Domain.Model;
public class Medicine
{
    public Guid Id { get; }
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public double? Price { get; private set; }
    public DrugAdministration? DrugAdministration { get; private set; }
    
    public Medicine(DrugAdministration drugAdministration)
    {
        Id = Guid.NewGuid();
        DrugAdministration = drugAdministration;
    }

    public static Result<Medicine> Create(string administration)
    {
        DrugAdministration typeOfAdministration = new DrugAdministration(administration);
        Result<DrugAdministration> typeOfDrugAdministration = DrugAdministration.createMedicineByString(administration);
        if (typeOfDrugAdministration.Value == null || typeOfDrugAdministration.IsFailure)
            return Result<Medicine>.FromError(typeOfDrugAdministration, "Failed to parse type of medicine administration.");
        return Result<Medicine>.Ok(new Medicine(typeOfAdministration));
    }
}
