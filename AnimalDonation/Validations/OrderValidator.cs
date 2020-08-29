using AnimalDonation.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace AnimalDonation.Validations
{
    public class OrderValidator : AbstractValidator<OrderViewModel>
    {
        public OrderValidator()
        {
            RuleFor(order => order.Amount)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} shouldn't be empty or equal to 0.")
                .GreaterThan(0).WithMessage("{PropertyName} should be more than 0.");


            RuleFor(order => order.Description)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} shouldn't be empty.")
                .MinimumLength(3).WithMessage("{PropertyName} shoul be more than 3 words")
                .MaximumLength(30).WithMessage("{PropertyName} should be less than 30 words.")
                .Must(IsValidDescription).WithMessage("{PropertyName} should be all letters.");
        }


        private bool IsValidDescription(string description)
        {
            return description.All(Char.IsLetter);
        }
    }
}
