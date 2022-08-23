using FluentValidation;
using StandingOrders.API.Models;
using System;

namespace StandingOrders.API.Validators
{
    public class StandingOrderValidator : AbstractValidator<StandingOrderDetailDto>
    {
        public StandingOrderValidator()
        {
            RuleFor(x => x.AccountNumber)
                .NotEmpty()
                .Must(x => x.Length <= 512)
                .WithMessage("Account number is required and can't have more than 512 characters.");
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.");
            RuleFor(x => x.Amount)
                .NotEmpty()
                .WithMessage("Amount is required.");
            RuleFor(x => x.IntervalId)
                .NotEmpty()
                .WithMessage("Interval is required.");
            RuleFor(x => x.IntervalSpecification)
                .NotNull()
                .WithMessage("Interval specification is required.");
            RuleFor(x => x.ValidFrom)
                .Must(date => date > DateTime.Now)
                .WithMessage("The date value must be in future.");
            RuleFor(x => x.ConstantSymbol)
                .Must(x => x.Length <= 10)
                .When(x => !string.IsNullOrEmpty(x.ConstantSymbol))
                .WithMessage("The constant symbol can't have more than 10 digits.");
            RuleFor(x => x.SpecificSymbol)
               .Must(x => x.Length <= 10)
               .When(x => !string.IsNullOrEmpty(x.SpecificSymbol))
               .WithMessage("The specific symbol can't have more than 10 digits.");
            RuleFor(x => x.VariableSymbol)
               .Must(x => x.Length <= 10)
               .When(x => !string.IsNullOrEmpty(x.VariableSymbol))
               .WithMessage("The variable symbol can't have more than 10 digits.");
            RuleFor(x => x.Note)
                .Must(x => x.Length <= 512)
                .When(x => !string.IsNullOrEmpty(x.Note))
                .WithMessage("Note can't have more than 512 characters.");
            RuleFor(x => x.AccountNumber)
                .Matches(@"^[a-zA-Z]{2}[0-9]{22}$")
                .WithMessage("Account number has to start by 2 letters, followed by 22 digits with no spaces.");
        }
    }
}