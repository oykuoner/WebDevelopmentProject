using FluentValidation;
using LSDCS.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSDCS.Service.FluenValidations
{
    public class UserValidator : AbstractValidator<AppUser>
    {

        public UserValidator()
        {
            RuleFor(x => x.FirtsName).NotEmpty().MinimumLength(2).MaximumLength(50).WithName("FirtsName");
            RuleFor(x => x.LastName).NotEmpty().MinimumLength(2).MaximumLength(50).WithName("LastName");
        }



    }
}
