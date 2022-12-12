using System.Xml.Linq;
using DucksNet.API.DTO;
using DucksNet.Domain.Model.Enums;
using DucksNet.Domain.Model;
using DucksNet.SharedKernel.Utils;
using FluentValidation;

namespace DucksNet.API.Validators;

public class MedicineValidator : AbstractValidator<MedicineDTO>
{
    public MedicineValidator()
    {
        RuleSet("CreateMedicine", () =>
        {
            RuleFor(e => e.Name).NotNull().NotEmpty().WithMessage("The name should contain at least one character.");
            RuleFor(e => e.Description).NotNull().NotEmpty().WithMessage("The description should contain at least one character.");
            RuleFor(e => e.Price).GreaterThan(0).WithMessage("Invalid price");
            // NOTE (MG) : add a rule for drug administration
        });
    }
}
