using VetAppointment.Util;

public enum Size
{
    Small,
    Medium,
    Big
};


public static class SizeExtensions
{

    //Note(AL): kinda sad that we can't add this method to the `class` proper...
    public static Result<Size> FromString(string size)
    {
        Size parsedSize;
        bool parsedSuccefully = Size.TryParse(size, out parsedSize);

        if (!parsedSuccefully)
        {
            // Error case:
            Array possibleSizeValues = Enum.GetValues(typeof(Size));
            string errorMessage = $"Failed to parse '{size}' as Size. Possible values for Size are: {possibleSizeValues}";
            return Result<Size>.Failure(errorMessage);
        }

        return Result<Size>.Success(parsedSize);
    }
};