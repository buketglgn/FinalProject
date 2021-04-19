using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
   public class AccessToken
    {
        //JWT nin ta kendisi
        
        public string Token { get; set; }

        //expiration bitiş zamanı
        public DateTime Expiration { get; set; }
    }
}
