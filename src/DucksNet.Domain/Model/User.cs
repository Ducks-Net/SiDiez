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

    private User(string firstName, string lastName, string address, string phoneNumber, string email, string password) 
    {
        Id = Guid.NewGuid();
        Console.WriteLine($"Create user id: {Id}");
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        PhoneNumber = phoneNumber;
        Email = email;
        Password = password;
    }

    public static Result<User> Create(string? firstName, string? lastName, string? address, string phoneNumber, string email, string? password)
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
        if (string.IsNullOrWhiteSpace(password))
        {
            return Result<User>.Error("Password is required.");
        }
        if (!Validation.IsTelephoneNumberValid(phoneNumber))
        {
            return Result<User>.Error("The phone number is not valid.");
        }
        if (!Validation.IsEmailValid(email))
        {
            return Result<User>.Error("The email is not valid.");
        }
        return Result<User>.Ok(new User(firstName, lastName, address, phoneNumber, email, password));
    }

    public void UpdateFields(string? firstName, string? lastName, string? address, string phoneNumber, string email, string? password)
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
