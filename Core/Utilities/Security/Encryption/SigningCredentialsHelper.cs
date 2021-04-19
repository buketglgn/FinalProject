using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
   public class SigningCredentialsHelper
    {
        //sistemi kullanabilmek için bir anahtara ihtiyaç var
        //securityKey anahtardır
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            //anahtar olarak bu securityKey i kullan
            //algoritma olarak virgülden sonraki.
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}
