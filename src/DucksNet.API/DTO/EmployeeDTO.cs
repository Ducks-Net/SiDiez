namespace DucksNet.API.DTO;

public class EmployeeDTO
{
    public Guid IdSediu { get; set; } 
    public string Surname { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string OwnerPhone { get; set; } = string.Empty;
    public string OwnerEmail { get; set; } = string.Empty;
}
