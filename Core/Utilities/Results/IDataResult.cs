using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    //mesaj ve islem sonucu(true false) olacak ve ayrıyetten bir data dönecek.
   public interface IDataResult<T>:IResult
    {
        T Data { get; }
    }
}
