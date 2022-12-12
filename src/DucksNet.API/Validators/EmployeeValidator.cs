using System.Net;
using DucksNet.API.DTO;
using FluentValidation;

namespace DucksNet.API.Validators;

public class EmployeeValidator : AbstractValidator<EmployeeDTO>
{
    public EmployeeValidator()
    {
        RuleSet("CreateEmployee", () =>
            {
            RuleFor(e => e.Surname).NotNull().NotEmpty().WithMessage("Surname can not be empty");
            RuleFor(e => e.FirstName).NotNull().NotEmpty().WithMessage("First name can not be empty");
            RuleFor(e => e.Address).NotNull().NotEmpty().WithMessage("Address can not be empty");
            RuleFor(e => e.OwnerEmail).NotEmpty().WithMessage("Email can not be empty");
            RuleFor(e => e.OwnerEmail).Matches("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$").WithMessage("The email is not valid");
            RuleFor(e => e.OwnerPhone).NotNull().NotEmpty().WithMessage("Telephone can not be empty");
            RuleFor(e => e.OwnerPhone).NotNull().Matches("^(\\+4|)?(07[0-8]{1}[0-9]{1}|02[0-9]{2}|03[0-9]{2}){1}?(\\s|\\.|\\-)?([0-9]{3}(\\s|\\.|\\-|)){2}$").WithMessage("The telephone number is not valid");
        });
        RuleSet("UpdateEmployee", () =>
        {
            RuleFor(e => e.OwnerEmail).Matches("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")
            .When(e => !string.IsNullOrWhiteSpace(e.OwnerEmail))
            .WithMessage("The email is not valid");
            RuleFor(e => e.OwnerPhone).Matches("^(\\+4|)?(07[0-8]{1}[0-9]{1}|02[0-9]{2}|03[0-9]{2}){1}?(\\s|\\.|\\-)?([0-9]{3}(\\s|\\.|\\-|)){2}$")
                .When(e => !string.IsNullOrWhiteSpace(e.OwnerPhone))
                .WithMessage("The telephone number is not valid");
            When(e => (string.IsNullOrWhiteSpace(e.FirstName) &&
                string.IsNullOrWhiteSpace(e.Address) && string.IsNullOrWhiteSpace(e.OwnerPhone) &&
                string.IsNullOrWhiteSpace(e.OwnerEmail)), () => {
                    RuleFor(e => e.Surname).NotEmpty().WithMessage("You need to add at least one value");
                });
        });
    }
}
