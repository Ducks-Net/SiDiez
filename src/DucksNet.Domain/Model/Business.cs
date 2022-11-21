using DucksNet.Domain.Model.Enums;
using DucksNet.SharedKernel.Utils;

namespace DucksNet.Domain.Model;
public class Business
{
    public Guid ID { get; private set; }
    public string BusinessName { get; private set; }
    public string Surname {get; private set;}
    public string FirstName {get; private set;}
    public string Address {get; private set;}
    public string OwnerPhone {get; private set;}
    public string OwnerEmail {get; private set;}

    private Business(string businessName, string surname, string firstName, string address, string ownerPhone, string ownerEmail) 
    {
        ID = new Guid();
        BusinessName = businessName;
        Surname = surname;
        FirstName = firstName;
        Address = address;
        OwnerPhone = ownerPhone;
        OwnerEmail = ownerEmail;
    }
    public static Result<Business> Create(string businessName, string surname, string firstName, string address, string ownerPhone, string ownerEmail)
    {
        if(businessName == null || businessName == string.Empty)
            return Result<Business>.Error("Business name is required");
        if(surname == null || surname == string.Empty)
            return Result<Business>.Error("Surname is required");
        if(firstName == null || firstName == string.Empty)
            return Result<Business>.Error("First name is required");
        if(address == null || address == string.Empty)
            return Result<Business>.Error("Address is required");
        if(ownerPhone == null || ownerPhone == string.Empty)  
            return Result<Business>.Error("Owner phone is required");
        if(ownerEmail == null || ownerEmail == string.Empty)  
            return Result<Business>.Error("Owner email is required");
        return Result<Business>.Ok(new Business(businessName, surname, firstName, address, ownerPhone, ownerEmail));
    }
}

