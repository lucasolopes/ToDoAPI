using FluentValidation;
using Shared.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators
{
    public class CardPutDescriptionValidator : AbstractValidator<CardPutDescriptionRequest>
    {
        public CardPutDescriptionValidator()
        {
            RuleFor(c => c.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(c => c.Description).MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");
        }
    }
}
