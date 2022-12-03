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

    public static Result<Employee> Create(string surname, string firstName, string address, string ownerPhone, string ownerEmail)
    {
        if (firstName == null || firstName!.Length == 0)
        {
            return Result<Employee>.Error("First name can not be empty");
        }
        if (surname == null || surname!.Length == 0)
        {
            return Result<Employee>.Error("Surname can not be empty");
        }
        if (address == null || address!.Length == 0)
        {
            return Result<Employee>.Error("Address can not be empty");
        }
        if (ownerEmail == null || ownerEmail!.Length == 0)
        {
            return Result<Employee>.Error("Email can not be empty");
        }
        if (ownerPhone == null || ownerPhone!.Length == 0)
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
    public List<string> UpdateFields(string? surname, string? firstName, string? address, string? ownerPhone, string? ownerEmail)
    {
        List<string> output= new List<string>();
        bool hasNewInfo = false;
        hasNewInfo = hasNewInfo || !string.IsNullOrWhiteSpace(surname);
        hasNewInfo = hasNewInfo || !string.IsNullOrWhiteSpace(firstName);
        hasNewInfo = hasNewInfo || !string.IsNullOrWhiteSpace(address);
        hasNewInfo = hasNewInfo || !string.IsNullOrWhiteSpace(ownerPhone);
        hasNewInfo = hasNewInfo || !string.IsNullOrWhiteSpace(ownerEmail);
        if (!string.IsNullOrWhiteSpace(ownerPhone) && !Validation.IsTelephoneNumberValid(ownerPhone!))
        { 
            output.Add("The telephone number is not valid");
        }
        if (!string.IsNullOrWhiteSpace(ownerEmail) && !Validation.IsEmailValid(ownerEmail!))
        {
            output.Add("The email is not valid");
        }
        if (!hasNewInfo)
        {
            output.Add("You need to add at least one value");
        }
        if (output.Count > 0)
        {
            return output;
        }
        Surname = !string.IsNullOrWhiteSpace(surname) ? surname : Surname;
        FirstName = !string.IsNullOrWhiteSpace(firstName) ? firstName : FirstName;
        Address = !string.IsNullOrWhiteSpace(address) ? address : Address;
        OwnerPhone = !string.IsNullOrWhiteSpace(ownerPhone) ? ownerPhone : OwnerPhone;
        OwnerEmail = !string.IsNullOrWhiteSpace(ownerEmail) ? ownerEmail : OwnerEmail;
        output.Add("The information has been updated");
        return output;
    }
}
