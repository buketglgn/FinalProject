using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
   public class SuccessDataResult<T> :DataResult<T>
    {
        public SuccessDataResult(T data, string message):base(data,true,message)
        {

        }

        public SuccessDataResult(T data):base(data, true)
        {

        }

        //data yı default haliyle yani sadece mesajı göstermek isteyebilir
        public SuccessDataResult(string message):base(default,true, message)
        {

        }

        //isterse hiç bir sey vermez. sadece dogrulugu tamam
        //default => datanın
        public SuccessDataResult():base(default,true)
        {

        }
    }
}
