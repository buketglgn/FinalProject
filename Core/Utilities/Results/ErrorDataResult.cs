using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
   public class ErrorDataResult<T>:DataResult<T>

    {
        public ErrorDataResult(T data, string message) : base(data, false, message)
        {

        }

        public ErrorDataResult(T data) : base(data, false)
        {

        }

        //data yı default haliyle yani sadece mesajı göstermek isteyebilir
        public ErrorDataResult(string message) : base(default, false, message)
        {

        }

        //isterse hiç bir sey vermez. sadece dogrulugu tamam
        public ErrorDataResult() : base(default, false)
        {

        }
    }
}
