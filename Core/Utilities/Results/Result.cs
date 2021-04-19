using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
       

        //this(success) => bu clastaki tek parametreli constructor a successi gönder
        //yani bu const iki parametleri hali gelirse this i kullanır.(hem messaj ı çalıstırır hem de this ile asagıdakini çalıstırır.
        //tek parametreli hali yani sadece true false gösterilsin mesaj gösterilmesin dediginde,
        //asagıdaki tek parametreli hali çalısır.
        public Result(bool success, string message): this(success)
        {
            Message = message;
           // Success = success;
         
        }

        //overloading mesajın olmadıgı bir şekli de gelirse bu kullanılsın.
        //bu şekilde yaparsak DONT REPEAT YOURSELF olmus olur.
        //baska bir sekilde uygula.
        //yukarıdaki success=success i sil.
        public Result(bool success)
        {
            Success = success;
        }

        public bool Success { get; }

        public string Message { get; }
    }
}
