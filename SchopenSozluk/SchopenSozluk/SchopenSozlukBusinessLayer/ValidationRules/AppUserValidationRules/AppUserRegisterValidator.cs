using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SchopenSozlukDtoLayer.Dtos.AppUserDtos;
using System.Data;

namespace SchopenSozlukBusinessLayer.ValidationRules.AppUserValidationRules
{
    public class AppUserRegisterValidator :AbstractValidator<AppUserRegisterDto>
    {
        public AppUserRegisterValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("annaı sikeyim");
        }
    }
}
