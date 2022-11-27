using System.Text.RegularExpressions;

namespace DucksNet.SharedKernel.Utils;
public class Validation
{
    public static bool IsTelephoneNumberValid(string phone)
    {
        Regex validatePhoneNumber = new Regex("^(\\+4|)?(07[0-8]{1}[0-9]{1}|02[0-9]{2}|03[0-9]{2}){1}?(\\s|\\.|\\-)?([0-9]{3}(\\s|\\.|\\-|)){2}$");
        return validatePhoneNumber.IsMatch(phone);
    }
    public static bool IsEmailValid(string email)
    {
        Regex validateEmail = new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
        return validateEmail.IsMatch(email);
    }
}
