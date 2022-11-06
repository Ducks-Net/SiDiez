namespace VetAppointment.Model;
using VetAppointment.Util;

class Client
{
    public Guid ID { get; }
    public string Name { get; }
    private DateOnly DateOfBirth { get; }
    public string Species { get; }
    public string Breed { get; }
    public string Colour { get; }
    public Gender Gender { get; }
    public Size Size { get; }

    public Client(Guid id, string name, DateOnly dateOfBirth, string species, string breed, string colour, Gender gender, Size size)
    {
        this.ID = id;
        this.Name = name;
        this.DateOfBirth = dateOfBirth;
        this.Species = species;
        this.Breed = breed;
        this.Colour = colour;
        this.Gender = gender;
        this.Size = size;
    }

    public static Result<Client> Create(string name, DateOnly dateOfBirth, string gender, string species, string breed, string colour, string size)
    {
        Gender g;
        if (!Gender.TryParse(gender, out g))
        {
            return Result<Client>.Failure(g + " is not a valid gender.");
        }
        Size s;
        if (!Size.TryParse(size, out s))
        {
            return Result<Client>.Failure(s + " is not a valid size.");
        }

        name = name.Capitalize();
        species = species.Capitalize();
        breed = breed.Capitalize();
        colour = colour.Capitalize();

        return Result<Client>.Success(new Client
        (
            Guid.NewGuid(),
            name,
            dateOfBirth,
            species,
            breed,
            colour,
            g,
            s
        ));
    }
}