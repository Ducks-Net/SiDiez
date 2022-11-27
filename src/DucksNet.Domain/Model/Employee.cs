using System.Net.Sockets;
using DucksNet.SharedKernel.Utils;

namespace DucksNet.Domain.Model;
public class Employee
{
    private Employee(string surname, string firstName, string address, string ownerPhone, string ownerEmail)
    {
        Id = Guid.NewGuid();
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

    public static Result<Employee> Create(string surname, string firstName, string address, string ownerPhone, string ownerEmail)
    {
        if (firstName == null)
        {
            return Result<Employee>.Error("First name can not be empty");
        }
        if (surname == null)
        {
            return Result<Employee>.Error("Surname can not be empty");
        }
        if (address == null)
        {
            return Result<Employee>.Error("Address can not be empty");
        }
        if (ownerEmail == null)
        {
            return Result<Employee>.Error("Email can not be empty");
        }
        if (ownerPhone == null)
        {
            return Result<Employee>.Error("Telephone can not be empty");
        }
        if (!Validation.IsTelephoneNumberValid(ownerPhone))
        {
            return Result<Employee>.Error("The telephone number is not valid");
        }
        if (!Validation.IsEmailValid(ownerEmail))
        {
            return Result<Employee>.Error("The email is not valid");
        }
        var employee = new Employee(surname, firstName, address, ownerPhone, ownerEmail);
        return Result<Employee>.Ok(employee);
    }
    public void AssignToSediu(Guid idSediu)
    {
        IdSediu = idSediu;
    }
    public void UpdateFields(string surname, string firstName, string address, string ownerPhone, string ownerEmail)
    {
        if (surname != null)
        {
            Surname = surname;
        }
        if (firstName != null)
        {
            FirstName = firstName;
        }
        if (address != null)
        {
            Address = address;
        }
        if (ownerPhone != null)
        {
            OwnerPhone = ownerPhone;
        }
        if (OwnerEmail != null)
        {
            OwnerEmail = ownerEmail;
        }
    }
}
