using DucksNet.Domain.Model.Enums;
using DucksNet.SharedKernel.Utils;

namespace DucksNet.Domain.Model;

public class Pet
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public string Species { get; private set; }
    public string Breed { get; private set; }
    public Guid OwnerId { get; private set; }
    public Size Size { get; private set; }

    public Pet(Guid id, string name, DateTime dateOfBirth, string species, string breed, Guid ownerId, Size size) // TODO (Ad): This constructor only exists to make it json parsable
    {
        Id = id;
        Name = name;
        DateOfBirth = dateOfBirth;
        Species = species;
        Breed = breed;
        OwnerId = ownerId;
        Size = size;
    }

    private Pet(string name, DateTime dateOfBirth, string species, string breed, Guid ownerId, Size size) // TODO (Ad): This constructor only exists to make it json parsable
    {
        Id = Guid.NewGuid();
        Name = name;
        DateOfBirth = dateOfBirth;
        Species = species;
        Breed = breed;
        OwnerId = ownerId;
        Size = size;
    }

    public static Result<Pet> Create(string name, DateTime dateOfBirth, string species, string breed, Guid ownerId, string size)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result<Pet>.Error("The name should contain at least one character.");
        }
        if (DateTime.Compare(dateOfBirth, DateTime.Now) > 0)
        {
            return Result<Pet>.Error("This date is not a real date of birth.");
        }
        if (string.IsNullOrWhiteSpace(species))
        {
            return Result<Pet>.Error("The species field can not be empty.");
        }
        if (string.IsNullOrWhiteSpace(breed))
        {
            return Result<Pet>.Error("The breed field can not be empty.");
        }

        Result<Size> sizeResult = Size.CreateFromString(size);
        if (sizeResult.IsFailure || sizeResult.Value == null)
        {
            return Result<Pet>.FromError(sizeResult, "Failed to parse pet size.");
        }
        return Result<Pet>.Ok(new Pet(name, dateOfBirth, species, breed, ownerId, sizeResult.Value));
    }

    public void AssignToOwner(Guid ownerId)
    {
        OwnerId = ownerId;
    }

    public void UpdateFields(string? name, DateTime dateOfBirth, string species, string breed, Guid? ownerId, string size)
    {
        
        if (!string.IsNullOrWhiteSpace(name))
        {
            Name = name;
        }
        if (!string.IsNullOrWhiteSpace(species))
        {
            Species = species;
        }
        if (!string.IsNullOrWhiteSpace(breed))
        {
            Breed = breed;
        }
        if (ownerId != null)
        {
            OwnerId = ownerId.Value;
        }

        Result<Size> sizeResult = Size.CreateFromString(size);
        if (sizeResult.IsSuccess && sizeResult.Value != null)
        {
            Size = sizeResult.Value;
        }
    }
}
