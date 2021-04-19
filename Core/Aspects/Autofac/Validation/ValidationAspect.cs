using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception //aspect
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            //defensive coding yöntemi..savunma odaklı kodlama. sadece ..validator lari kabul etsin.
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);// bu bir reflection
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];//örn product a ulasmak için.
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);//metodun argümanlarını gez.
            //o argümanlardan yukarıdaki entityType a eşit olan yani aynı olan varsa yakala.
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
