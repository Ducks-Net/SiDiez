using DucksNet.API.DTO;
using FluentValidation;

namespace DucksNet.API.Validators;

public class UserValidator : AbstractValidator<UserDTO>
{
    public UserValidator()
    {
        RuleSet("CreateUser", () =>
        {
            RuleFor(e => e.FirstName).NotNull().NotEmpty().WithMessage("First name is required.");
            RuleFor(e => e.LastName).NotNull().NotEmpty().WithMessage("Last name is required.");
            RuleFor(e => e.Address).NotNull().NotEmpty().WithMessage("Address is required.");
            RuleFor(e => e.Password).NotEmpty().WithMessage("Password is required.");
            RuleFor(e => e.Email).Matches("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$").WithMessage("The email is not valid.");
            RuleFor(e => e.PhoneNumber).NotNull().Matches("^(\\+4|)?(07[0-8]{1}[0-9]{1}|02[0-9]{2}|03[0-9]{2}){1}?(\\s|\\.|\\-)?([0-9]{3}(\\s|\\.|\\-|)){2}$").WithMessage("The phone number is not valid.");
        });
    }
    

}
