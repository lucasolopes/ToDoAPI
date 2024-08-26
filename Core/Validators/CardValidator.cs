using FluentValidation;
using Shared.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators
{
    public class CardValidator : AbstractValidator<CardRequest>
    {
        public CardValidator() 
        { 
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title é Obrigatorio!");
            RuleFor(x => x.Title).MaximumLength(60).WithMessage("Title deve ter no maximo 60 caracteres!");
            RuleFor(x => x.Description).MaximumLength(1000).WithMessage("Description deve ter no maximo 1000 caracteres!");
            RuleFor(x=> x.Position).NotEmpty().WithMessage("Position é Obrigatorio!");
            RuleFor(x=> x.Position).GreaterThan(0).WithMessage("Position deve ser maior que 0!");
            RuleFor(x=> x.ListaId).NotEmpty().WithMessage("ListId é Obrigatorio!");
        }
    }
}
