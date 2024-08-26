using FluentValidation;
using Shared.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators
{
    public class ListaPutPositionValidator : AbstractValidator<ListaPutPositionRequest>
    {
        public ListaPutPositionValidator()
        {
            RuleFor(x => x.Position).NotEmpty().WithMessage("Position is required");
            RuleFor(x => x.Position).Must(x => x >= 0).WithMessage("Position must be greater than or equal to 0");
        }
    }
}
