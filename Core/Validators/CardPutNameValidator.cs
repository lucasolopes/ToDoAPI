﻿using FluentValidation;
using Shared.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators
{
    public class CardPutNameValidator : AbstractValidator<CardPutNameRequest>
    {
        public CardPutNameValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Name is required!");
            RuleFor(x => x.Title).MaximumLength(60).WithMessage("Name must have a maximum of 60 characters!");


        }
    }
}
