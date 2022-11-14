namespace VetAppointment.Util;


static public class StringExtensions {
    static public string Capitalize(this string input) {    
        input = input[0].ToString().ToUpper() + input.Substring(1).ToLower();
        return input;
    }
}