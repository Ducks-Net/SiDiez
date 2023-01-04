using DucksNet.SharedKernel.Utils;

namespace DucksNet.Domain.Model;
public class Employee
{
    public Employee(Guid id, string surname, string firstName, string address, string ownerPhone, string ownerEmail) 
    {
        Id = id;
        Surname = surname;
        FirstName = firstName;
        Address = address;
        OwnerPhone = ownerPhone;
        OwnerEmail = ownerEmail;
    }
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
    public Guid IdOffice { get; private set; }
    public string Surname { get; private set; }
    public string FirstName { get; private set; }
    public string Address { get; private set; }
    public string OwnerPhone { get; private set; }
    public string OwnerEmail { get; private set; }

    public static Result<Employee> Create(string? surname, string? firstName, string? address, string? ownerPhone, string? ownerEmail)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            return Result<Employee>.Error("First name can not be empty");
        }
        if (string.IsNullOrWhiteSpace(surname))
        {
            return Result<Employee>.Error("Surname can not be empty");
        }
        if (string.IsNullOrWhiteSpace(address))
        {
            return Result<Employee>.Error("Address can not be empty");
        }
        if (string.IsNullOrWhiteSpace(ownerEmail))
        {
            return Result<Employee>.Error("Email can not be empty");
        }
        if (string.IsNullOrWhiteSpace(ownerPhone))
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
    public void AssignToOffice(Guid idOffice)
    {
        IdOffice = idOffice;
    }
    public Result UpdateFields(string? surname, string? firstName, string? address, string? ownerPhone, string? ownerEmail)
    {
        bool hasNewInfo = !string.IsNullOrWhiteSpace(surname);
        hasNewInfo = hasNewInfo || !string.IsNullOrWhiteSpace(firstName);
        hasNewInfo = hasNewInfo || !string.IsNullOrWhiteSpace(address);
        if (!string.IsNullOrWhiteSpace(ownerPhone) && !Validation.IsTelephoneNumberValid(ownerPhone!))
        {
            return Result.Error("The telephone number is not valid");
        }
        if (!string.IsNullOrWhiteSpace(ownerEmail) && !Validation.IsEmailValid(ownerEmail!))
        {
            return Result.Error("The email is not valid");
        }
        if (!hasNewInfo)
        {
            return Result.Error("You need to add at least one value");
        }
        Surname = !string.IsNullOrWhiteSpace(surname) ? surname : Surname;
        FirstName = !string.IsNullOrWhiteSpace(firstName) ? firstName : FirstName;
        Address = !string.IsNullOrWhiteSpace(address) ? address : Address;
        OwnerPhone = !string.IsNullOrWhiteSpace(ownerPhone) ? ownerPhone : OwnerPhone;
        OwnerEmail = !string.IsNullOrWhiteSpace(ownerEmail) ? ownerEmail : OwnerEmail;
        return Result.Ok();
    }
}
