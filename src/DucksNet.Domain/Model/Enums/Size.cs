using DucksNet.SharedKernel.Utils;

namespace DucksNet.Domain.Model.Enums;

public class Size : Enumeration
{
    public static readonly Size Small = new(1, "Small");
    public static readonly Size Medium = new(2, "Medium");
    public static readonly Size Large = new(3, "Large");

    private Size(int id, string name) : base(id, name) { }

    public static Result<Size> CreateFromString(string str)
    {
        var size = GetAll<Size>().FirstOrDefault(x => x.Name == str);
        if (size == null)
            return Result<Size>.Error("Invalid size string. Valid values are: Small, Medium, Large."); // TODO (AL) : Don't hardcode the valid values 

        return Result<Size>.Ok(size);
    }
}
