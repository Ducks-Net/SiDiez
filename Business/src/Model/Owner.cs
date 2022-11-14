namespace VetAppointment.Model;
using VetAppointment.Util;
public class Owner
{
    private Guid ID;
    public string CNP { get; }
    public string Name { get; }
    public string Surname { get; }
    public Gender Gender { get; }
    public DateOnly Birthday { get; }
    public string Address { get; }
    public string EmailAddress { get; }
    public string Phone { get; set; }

    // TODO(AL + OR + NA): vedem cum facem cu persoanele juridice

    private Owner(Guid id, string CNP, string Name, string Surname, Gender geneder, DateOnly birthday,
                    string address, string emailAddress, string phone) {
        

        this.ID = id;
        this.CNP = CNP;
        this.Name = Name;
        this.Surname = Surname;
        this.Gender = Gender;
        this.Birthday = birthday;
        this.Address = address;
        this.EmailAddress = emailAddress;
        this.Phone = phone;

    }

    public static Result<Owner> Create(string cnp, string name, string surname, string gender, DateOnly birthday, string address, string emailAddress, string phone)
    {
        //TODO(MG):
        // if (cnp.Length != 13 || cnp.Substring(0, 1) > 6)
        //     if (birthday.get("YY") == cnp.Substring(2, 4) || birthday.get("MM") == cnp.Substring(4, 6) || birthday.get("DD") == cnp.Substring(6, 8))
        //         return Result<Owner>.Failure(cnp + " is not a valid CNP.");
        
        Result<Gender> g = new Gender().Create(gender);

        if(g.IsFailure)
            return Result<Owner>.Failure(g.Error);

        // TODO (AL): take birthday as string and parse here

        return Result<Owner>.Success(new Owner
        (
            Guid.NewGuid(),
            cnp,
            name,
            surname,
            g.Entity,
            birthday,
            address,
            emailAddress,
            phone
        ));
    }


}