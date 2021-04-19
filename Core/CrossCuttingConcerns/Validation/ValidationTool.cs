using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Validation
{
   public static class ValidationTool
    {
        //IValidator: kuralların oldugu class..
        public static void Validate(IValidator validator, object entity)
        {
            var context = new ValidationContext<object>(entity);  //
            var result = validator.Validate(context);
            if (!result.IsValid) //sonuc gecerli degilse
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
