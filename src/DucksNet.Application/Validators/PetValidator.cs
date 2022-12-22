using DucksNet.API.DTO;
using FluentValidation;
using DucksNet.Domain.Model.Enums;

namespace DucksNet.API.Validators;

public class PetValidator : AbstractValidator<PetDto>
{
    public PetValidator()
    {
        RuleSet("CreatePet", () =>
        {
            RuleFor(e => e.Name).NotNull().NotEmpty().WithMessage("The name should contain at least one character.");
            RuleFor(e => e.DateOfBirth).LessThan(DateTime.Now).WithMessage("This date is not a valid date of birth.");
            RuleFor(e => e.Species).NotNull().NotEmpty().WithMessage("The species field can not be empty.");
            RuleFor(e => e.Breed).NotEmpty().WithMessage("The breed field can not be empty.");
            RuleFor(e => e.OwnerId).NotNull().WithMessage("The onwer id can not be null.");
        });
    }
}
