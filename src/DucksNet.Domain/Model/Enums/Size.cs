using DucksNet.SharedKernel.Utils;

namespace DucksNet.Domain.Model.Enums;

public class Size : Enumeration
{
    public static readonly Size Small = new(1, "Small");
    public static readonly Size Medium = new(2, "Medium");
    public static readonly Size Large = new(3, "Large");

    public Size(int id, string name) : base(id, name) { } // NOTE (Ad): Make this private. Public as a workaround for json serialization/deserialization.

    public static Result<Size> CreateFromString(string str)
    {
        var size = GetAll<Size>().FirstOrDefault(x => x.Name == str);
        if (size == null)
        {
            return Result<Size>.Error("Invalid size string. Valid values are: Small, Medium, Large."); // NOTE (Ad): Hardcoded
        }
        return Result<Size>.Ok(size);
    }

    public static Result<Size> CreateFromInt(int id)
    {
        var size = GetAll<Size>().FirstOrDefault(x => x.Id == id);
        if (size == null)
        {
            return Result<Size>.Error("Invalid size id. Valid values are: 1, 2, 3."); // NOTE (Ad): Hardcoded
        }
        return Result<Size>.Ok(size);
    }
}
