using DucksNet.Domain.Model.Enums;

namespace DucksNet.API.DTO;

public class MedicineDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string DrugAdministrationString { get; set; } = DrugAdministration.Oral.Name;

    public MedicineDTO(string name, string description, double price, string drugAdministrationString) 
    {
        Name= name;
        Description= description;   
        Price= price;   
        DrugAdministrationString= drugAdministrationString;
    }
}
