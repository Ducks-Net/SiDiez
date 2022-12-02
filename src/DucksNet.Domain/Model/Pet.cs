using DucksNet.Domain.Model.Enums;
using DucksNet.SharedKernel.Utils;

namespace DucksNet.Domain.Model;

public class Pet
{
    public Guid ID { get; private set; }
    public Guid OwnerId { get; private set; }
    public Size Size { get; private set; }

    public Pet(Guid ID, Guid ownerId, Size size) // TODO (Al): This constructor only exists to make it json parsable
    {
        this.ID = ID;
        OwnerId = ownerId;
        Size = size;
    }

    private Pet(Size size)
    {
        ID = new Guid();
        Size = size;
    }

    private Pet(int sizeId)
        : this(Size.CreateFromInt(sizeId).Value!) {}
    
    public static Result<Pet> Create(string size)
    {
        Result<Size> sizeResult = Size.CreateFromString(size);
        if (sizeResult.IsFailure || sizeResult.Value == null)
            return Result<Pet>.FromError(sizeResult, "Failed to parse pet size.");

        return Result<Pet>.Ok(new Pet(sizeResult.Value));
    }

    public void AssignToOwner(Guid ownerId)
    {
        OwnerId = ownerId;
    }

    public void UpdateFields(string size, Guid? ownerId)
    {
        Result<Size> sizeResult = Size.CreateFromString(size);
        if (sizeResult.IsSuccess && sizeResult.Value != null)
        {
            Size = sizeResult.Value;
        }
        if (ownerId != null)
        {
            OwnerId = ownerId.Value;
        }
    }
}
