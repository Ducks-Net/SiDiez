

namespace DucksNet.API.DTO;

public class MedicineDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string DrugAdministrationString { get; set; }

    public MedicineDto(string name, string description, double price, string drugAdministrationString) 
    {
        Name= name;
        Description= description;   
        Price= price;   
        DrugAdministrationString= drugAdministrationString;
    }
}
