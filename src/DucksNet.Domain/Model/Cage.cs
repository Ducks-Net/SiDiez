using DucksNet.Domain.Model.Enums;
using DucksNet.SharedKernel.Utils;

namespace DucksNet.Domain.Model;

public class Cage
{
    public Guid ID { get; private set; }
    public Size Size { get; private set; }
    public Guid? LocationId { get; private set; }

    public Cage(Guid ID, Size size, Guid? locationId)
    {
        this.ID = ID;
        Size = size;
        LocationId = locationId;
    }

    private Cage(Size size)
    {
        ID = Guid.NewGuid();
        Size = size;
        LocationId = null;
    }

    public static Result<Cage> Create(string size)
    {
        Result<Size> sizeResult = Size.CreateFromString(size);
        if (sizeResult.IsFailure || sizeResult.Value is null)
            return Result<Cage>.FromError(sizeResult, "Failed to parse cage size.");

        return Result<Cage>.Ok(new Cage(sizeResult.Value));
    }

    public void AssignToLocation(Guid locationId)
    {
        LocationId = locationId;
    }
}
