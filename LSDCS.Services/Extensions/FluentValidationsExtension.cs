using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using ValidationResult = FluentValidation.Results.ValidationResult;
using FluentValidation;
using LSDCS.Entity.DTOs.MailLog;
using LSDCS.Service.FluenValidations;
using Microsoft.AspNetCore.Identity;



namespace LSDCS.Service.Extensions
{
    public static class FluentValidationsExtension
    {

        public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
        }
        public static void AddToIdentityModelState(this IdentityResult result, ModelStateDictionary modelState)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError("", error.Description);
            }
        }
    }
}
