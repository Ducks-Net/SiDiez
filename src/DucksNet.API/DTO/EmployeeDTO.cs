namespace DucksNet.API.DTO;

public class EmployeeDTO
{
    public EmployeeDTO(Guid idSediu, string surname, string firstName, string address, string ownerPhone, string ownerEmail)
    {
        IdSediu = idSediu;
        Surname = surname;
        FirstName = firstName;
        Address = address;
        OwnerPhone = ownerPhone;
        OwnerEmail = ownerEmail;
    }
    public Guid IdSediu { get; set; }
    public string Surname { get; set; }
    public string FirstName { get; set; }
    public string Address { get; set; }
    public string OwnerPhone { get; set; }
    public string OwnerEmail { get; set; }
}
