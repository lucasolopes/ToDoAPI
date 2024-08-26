using FluentValidation;
using Shared.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators
{
    public class CardPutPositionValidator : AbstractValidator<CardPutPositionRequest>
    {
        public CardPutPositionValidator()
        {
            RuleFor(c => c.Position).NotEmpty().WithMessage("Position is required.");
            RuleFor(c => c.Position).GreaterThanOrEqualTo(0).WithMessage("Position must be greater than or equal to 0.");
        }
    }
}
