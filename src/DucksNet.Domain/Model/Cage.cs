using System.ComponentModel.DataAnnotations.Schema;
using DucksNet.Domain.Model.Enums;
using DucksNet.SharedKernel.Utils;

namespace DucksNet.Domain.Model;

public class Cage
{
    public Guid ID { get; private set; }
    public Size Size { get; private set; }
    public Guid? LocationId { get; private set; }

    private Cage(Size size)
    {
        ID = new Guid();
        Size = size;
        LocationId = null;
    }

    public static Result<Cage> Create(string size)
    {
        Result<Size> sizeResult = Size.CreateFromString(size);
        if (sizeResult.IsFailure || sizeResult.Value == null)
            return Result<Cage>.FromError(sizeResult, "Failed to parse cage size.");

        return Result<Cage>.Ok(new Cage(sizeResult.Value));
    }

    public void AssignToLocation(Guid locationId)
    {
        LocationId = locationId;
    }
}
