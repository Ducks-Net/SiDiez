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

    public Business(Guid id, string businessName, string surname, string firstName, string address, string ownerPhone, string ownerEmail) 
    {
        ID = id;
        BusinessName = businessName;
        Surname = surname;
        FirstName = firstName;
        Address = address;
        OwnerPhone = ownerPhone;
        OwnerEmail = ownerEmail;
    }

    private Business(string businessName, string surname, string firstName, string address, string ownerPhone, string ownerEmail) 
    {
        ID = Guid.NewGuid();
        BusinessName = businessName;
        Surname = surname;
        FirstName = firstName;
        Address = address;
        OwnerPhone = ownerPhone;
        OwnerEmail = ownerEmail;
    }
    public static Result<Business> Create(string businessName, string surname, string firstName, string address, string ownerPhone, string ownerEmail)
    {
        if(string.IsNullOrWhiteSpace(businessName))
            return Result<Business>.Error("Business name is required");
        if(string.IsNullOrWhiteSpace(surname))
            return Result<Business>.Error("Surname is required");
        if(string.IsNullOrWhiteSpace(firstName))
            return Result<Business>.Error("First name is required");
        if(string.IsNullOrWhiteSpace(address))
            return Result<Business>.Error("Address is required");
        if(string.IsNullOrWhiteSpace(ownerPhone))  
            return Result<Business>.Error("Owner phone is required");

        for(int i = 0; i < ownerPhone.Length; i++)
            if(!char.IsDigit(ownerPhone[i]))
                return Result<Business>.Error("Phone number must be numeric");
        if(ownerPhone.Length != 10)
            return Result<Business>.Error("Phone number must be 10 digits");

        if(string.IsNullOrEmpty(ownerEmail))  
            return Result<Business>.Error("Owner email is required");
        if(!Validation.IsEmailValid(ownerEmail))
            return Result<Business>.Error("Owner email is invalid");
            
        return Result<Business>.Ok(new Business(businessName, surname, firstName, address, ownerPhone, ownerEmail));
    }
}

