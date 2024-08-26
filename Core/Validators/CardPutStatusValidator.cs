using FluentValidation;
using OnKanBan.Domain.Entities;
using Shared.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators
{
    public class CardPutStatusValidator : AbstractValidator<CardPutStatusRequest>
    {
        public CardPutStatusValidator()
        {
            RuleFor(x => x.Status).NotNull().WithMessage("Status is required");
            RuleFor(x => x.Status).InclusiveBetween(0, 2).WithMessage("Status must be between 0 and 2");
        }
    }
}
