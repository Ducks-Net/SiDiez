namespace VetAppointment.Model;
using VetAppointment.Util;
public enum Gender {
    Male,
    Female,
    Other,
}

static public class GenderExtensions {
    public static Result<Gender> Create(this Gender b, string input)
    {
        Gender g;
        if(!Gender.TryParse(input, out g)) {
            return Result<Gender>.Failure(g + " is not a valid gender.");
        }

        return Result<Gender>.Success(g);
    }
}