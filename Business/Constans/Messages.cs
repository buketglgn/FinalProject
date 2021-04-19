using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constans
{
    //new lenmeyen yapılar static yapılır
   public static class Messages
    {
        public static string ProductAdded = "Ürün Eklendi";
        public static string ProductNameInvalid = "Ürün İsmi geçersiz";
        public static string ProductsListed = "Ürünler Listelendi";
        public static string MaintenanceTime = "Bakımda, Ürünler listelenemedi";
        public static string ProductCountOfCategoryError = "Bir Categoryde en fazla 10 ürün olabilir.";
        public static string ProductNameAlreadyExists = "Bu isimde zaten başka bir ürün var";
        public static string CategoryLimit = "category limiti 15 i gectiği için yeni ürün eklenemez";
        public static string AuthorizationDenied = "Yetkiniz yok.";
    }
}
