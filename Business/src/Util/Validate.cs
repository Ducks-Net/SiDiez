namespace VetAppointment.Util;

class Validate{

    static public bool PhoneNumber(string phoneNumber){
        bool result = true;
        result = result && (phoneNumber.Length == 10);
        result = result && (phoneNumber[0] == '0');
        result = result && (phoneNumber.Any(x => !char.IsLetter(x)));
        return result;
    }
    static public bool ValidateEmail(string email){
        var trimmedEmail = email.Trim();
        if (trimmedEmail.EndsWith(".")) {
            return false; 
        }
        try {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch {
            return false;
        }
    }
}