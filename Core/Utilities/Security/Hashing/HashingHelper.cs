using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        //ona verilen bir passwordun hashini ve saltını da olusturacak.
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                //saltta ilgili kullandıgımız algroitmanın key degerini kullandık
                //her kullanıcı için bir key olusturur.
                //kendimizde verebilirdik ama değişmeyeck bir sey olmalı. 
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        //password hash ini dogrula demek
        //burada out olmamalı çünkü bu degerleri biz verecegiz.
        //buradaki passwordHash ve salt lar veritabanındakiler
        //bunlar ile password u karsılastıracagız
        //buradaki password sisteme girmeye çalısırken ki parola
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i]!=passwordHash[i])
                    {
                        //demekki iki deger birbiri ile eşleşmiyor
                        return false;
                    }

                }
            }
            return true;
        }
    }
}
