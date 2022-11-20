using DucksNet.SharedKernel.Utils;

namespace DucksNet.Domain.Model;

public class User
{
    public Guid ID { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public virtual List<Pet> Pets { get; private set; } = new List<Pet>();

    private User(string name, string email, string passwordHash)
    {
        ID = new Guid();
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
    }

    public static Result<User> Create(string? name, string? email, string? password)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<User>.Error("Name is required.");
        if (string.IsNullOrWhiteSpace(email))
            return Result<User>.Error("Email is required.");
        if (string.IsNullOrWhiteSpace(password))
            return Result<User>.Error("Password is required.");

        return Result<User>.Ok(new User(name, email, password));
    }

    public void UpdateFields(string? name, string? email, string? password)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            Name = name;
        }
        if (!string.IsNullOrWhiteSpace(email))
        {
            Email = email;
        }
        if (!string.IsNullOrWhiteSpace(password))
        {
            PasswordHash = password;
        }
    }
}
