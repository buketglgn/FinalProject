using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Validationrules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.ProductName).MinimumLength(2);
            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0); //unitprice 0 dan büyük olmalı
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);
            //categoryıd si 1 olanların(örn içecekler) birim fiyatı 10 ve 10 dan büyük olmalıdır
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürünler A harfi ile baslamalı");
            //örnek ürünlerimin ismi belli bir pattern ile başlamalı

        }

        private bool StartWithA(string arg)
        {
            //eger true dönerse kurala uygun , false dönerse kurala uymaz
            //arg gönderdigimiz productName

            return arg.StartsWith("A");
        }
    }
}
