using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] logics)
        {
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    return logic; //başarız olanı business a haber veriyoruz., kurala uymayanı döndürüyoruz
                    //logic dediğimiz kuralın kendisi
                }

            }
            return null; //başarılı ise hiç bir sey göndermesine gerek yok
        }
    }
}
