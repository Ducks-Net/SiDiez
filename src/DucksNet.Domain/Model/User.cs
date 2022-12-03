using DucksNet.SharedKernel.Utils;

namespace DucksNet.Domain.Model;

public class User
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Address { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public virtual List<Pet> Pets { get; private set; } = new List<Pet>();

    public User(Guid id, string firstName, string lastName, string phoneNumber, string address, string email, string password) // TODO (Ad): This constructor only exists to make it json parsable
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Address = address;
        Email = email;
        Password = password;
    }

    private User(string firstName, string lastName, string phoneNumber, string address, string email, string password) 
    {
        Id = new Guid();
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        PhoneNumber = phoneNumber;
        Email = email;
        Password = password;
    }

    public static Result<User> Create(string? firstName, string? lastName, string phoneNumber, string? address, string? email, string? password)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            return Result<User>.Error("First name is required.");
        }
        if (string.IsNullOrWhiteSpace(lastName))
        {
            return Result<User>.Error("Last name is required.");
        }
        if (string.IsNullOrWhiteSpace(address))
        {
            return Result<User>.Error("Address is required.");
        }
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            return Result<User>.Error("Phone Number is required.");
        }
        if (string.IsNullOrWhiteSpace(email))
        {
            return Result<User>.Error("Email is required.");
        }
        if (string.IsNullOrWhiteSpace(password))
        {
            return Result<User>.Error("Password is required.");
        }
        //if (!Validation.IsTelephoneNumberValid(phoneNumber))
        //{
        //    return Result<User>.Error("The phone number is not valid.");
        //}
        //if (!Validation.IsEmailValid(email))
        //{
        //    return Result<User>.Error("The email is not valid.");
        //}
        return Result<User>.Ok(new User(firstName, lastName, address, phoneNumber, email, password));
    }

    public void UpdateFields(string? firstName, string? lastName, string? phoneNumber, string? address, string? email, string? password)
    {
        if (!string.IsNullOrWhiteSpace(firstName))
        {
            FirstName = firstName;
        }
        if (!string.IsNullOrWhiteSpace(lastName))
        {
            LastName = lastName;
        }
        if (Validation.IsTelephoneNumberValid(phoneNumber))
        {
            PhoneNumber = phoneNumber;
        }
        if (!string.IsNullOrWhiteSpace(address))
        {
            Address = address;
        }
        if (Validation.IsEmailValid(email))
        {
            Email = email;
        }
        if (!string.IsNullOrWhiteSpace(password))
        {
            Password = password;
        }
    }
}
