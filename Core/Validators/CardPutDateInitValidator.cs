using FluentValidation;
using Shared.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators
{
    public class CardPutDateInitValidator : AbstractValidator<CardPutDateInitRequest>
    {
        public CardPutDateInitValidator()
        {
            RuleFor(c => c.Date).NotEmpty().WithMessage("Date is required");
            RuleFor(c => c.Date).Must(date => date >= DateTime.MinValue).WithMessage("Date must be greater than or equal to the current date");
        }
    }
}
