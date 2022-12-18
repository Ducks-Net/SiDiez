using System.Xml.Linq;
using DucksNet.API.DTO;
using DucksNet.Domain.Model;
using DucksNet.SharedKernel.Utils;
using FluentValidation;

namespace DucksNet.API.Validators;

public class PetValidator : AbstractValidator<PetDto>
{
    public PetValidator()
    {
        RuleSet("CreatePet", () =>
        {
            RuleFor(e => e.Name).NotNull().NotEmpty().WithMessage("The name should contain at least one character.");
            RuleFor(e => e.DateOfBirth).LessThan(DateTime.Now).WithMessage("This date is not a valid date of birth.");
            RuleFor(e => e.Species).NotNull().NotEmpty().WithMessage("Address can not be empty");
            RuleFor(e => e.Breed).NotEmpty().WithMessage("The breed field can not be empty.");
        });
    }
}
