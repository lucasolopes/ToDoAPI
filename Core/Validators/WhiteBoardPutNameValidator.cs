using FluentValidation;
using Shared.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators
{
    public class WhiteBoardPutNameValidator : AbstractValidator<WhiteBoardPutNameRequest>
    {
        public WhiteBoardPutNameValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name é Obrigatorio!");
            RuleFor(x => x.Name).MaximumLength(60).WithMessage("Name deve ter no maximo 60 caracteres!");

        }
    }
}
