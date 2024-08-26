using FluentValidation;
using Shared.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators
{
    public class WhiteBoardPutDescriptionValidator : AbstractValidator<WhiteBoardPutDescriptionRequest>
    {
        public WhiteBoardPutDescriptionValidator()
        {
            RuleFor(wb => wb.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(wb => wb.Description).MaximumLength(500).WithMessage("Description must not exceed 500 characters");
        }
    }
}
