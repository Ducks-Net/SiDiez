namespace DucksNet.Domain.Model;
public class Employee
{
    public Employee(Guid idSediu, string surname, string firstName, string address, string ownerPhone, string ownerEmail)
    {
        Id = Guid.NewGuid();
        IdSediu = idSediu;
        Surname = surname;
        FirstName = firstName;
        Address = address;
        OwnerPhone = ownerPhone;
        OwnerEmail = ownerEmail;
    }

    public Guid Id { get; private set; }
    public Guid IdSediu { get; private set; }
    public string Surname { get; private set; }
    public string FirstName { get; private set; }
    public string Address { get; private set; }
    public string OwnerPhone { get; private set; }
    public string OwnerEmail { get; private set; }

    public void AssignToSediu(Guid idSediu)
    {
        IdSediu = idSediu;
    }
}
