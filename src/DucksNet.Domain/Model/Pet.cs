using System;
using System.Globalization;
using System.Net;
using DucksNet.Domain.Model.Enums;
using DucksNet.SharedKernel.Utils;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

    public Pet(Guid id, string name, DateTime dateOfBirth, string species, string breed, Guid ownerId, Size size)
    {
        Id = id;
        Name = name;
        DateOfBirth = dateOfBirth;
        Species = species;
        Breed = breed;
        OwnerId = ownerId;
        Size = size;
    }

    private Pet(string name, DateTime dateOfBirth, string species, string breed, Guid ownerId, Size size)
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
        if (dateOfBirth.CompareTo(DateTime.Now) > 0)
        {
            return Result<Pet>.Error("This date is not a valid date of birth.");
        }
        if (string.IsNullOrWhiteSpace(species))
        {
            return Result<Pet>.Error("The species field can not be empty.");
        }
        if (string.IsNullOrWhiteSpace(breed))
        {
            return Result<Pet>.Error("The breed field can not be empty.");
        }
        if (ownerId == Guid.Empty)
        {
            return Result<Pet>.Error("The owner id can not be null.");
        }

        Result<Size> sizeResult = Size.CreateFromString(size);
        if (sizeResult.IsFailure || sizeResult.Value is null)
        {
            return Result<Pet>.FromError(sizeResult, "Failed to parse pet size.");
        }

        dateOfBirth = new DateTime(dateOfBirth.Year, dateOfBirth.Month, dateOfBirth.Day);
        return Result<Pet>.Ok(new Pet(name, dateOfBirth, species, breed, ownerId, sizeResult.Value));
    }

    public void UpdateFields(string? name, DateTime dateOfBirth, string species, string breed, Guid ownerId, string size)
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
        if (dateOfBirth.CompareTo(DateTime.Now) < 0)
        {
            DateOfBirth = new DateTime(dateOfBirth.Year, dateOfBirth.Month, dateOfBirth.Day);
        }
        if (ownerId != Guid.Empty)
        {
            OwnerId = ownerId;
        }

        Result<Size> sizeResult = Size.CreateFromString(size);
        if (!(sizeResult.IsFailure || sizeResult.Value is null))
        {
            Size = sizeResult.Value;
        }
    }
}
