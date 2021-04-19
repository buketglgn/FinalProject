using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
   public class SuccessResult : Result
    {
        //base dedigimiz yukarıdaki Result
        //base in çift parametreli olanı
        public SuccessResult(string message) : base(true, message)
        {

        }

        //base in tek parametreli olanı
        //true yi default verdik , zaten success clasında ekleme basarılıdır.
        //sadece string message gösterir.
        public SuccessResult() : base(true)
        {

        }
    }
}
