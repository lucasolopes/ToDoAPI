using FluentValidation;
using Shared.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators
{
    public class WhiteBoardValidator : AbstractValidator<WhiteBoardRequest>
    {
        public WhiteBoardValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name é Obrigatorio!");
            RuleFor(x => x.Name).MaximumLength(60).WithMessage("Name deve ter no maximo 60 caracteres!");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("Description deve ter no maximo 500 caracteres!");
        }
    }
}
