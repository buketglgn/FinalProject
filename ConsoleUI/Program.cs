using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    class Program
    {
        //SOLID in O harfi
        //OPEN CLOSED PRİNCİPLE yaptıgımız yazılıma yeni bir özellik ekliyorsak mevcuttaki hiçbir kodumuza dokunamayız.


        static void Main(string[] args)
        {
           // ProductTest();
           // CategoryTest();



            Console.ReadLine();
        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll().Data)
            {
                Console.WriteLine((category.CategoryName));
            }

            Console.WriteLine("category ıd si 2 olanlar listelenecek..:");
            Console.WriteLine(categoryManager.GetById(2).Data.CategoryId + " " + categoryManager.GetById(2).Data.CategoryName);
        }

    //    private static void ProductTest()
    //    {
    //        ProductManager productManager = new ProductManager(new EfProductDal());

    //        var result = productManager.GetProductDetails();

    //        if (result.Success)
    //        {
    //         foreach (var product in productManager.GetProductDetails().Data) //join işlemi yapıldı.
    //           {
    //                        Console.WriteLine(product.ProductName+"/"+product.CategoryName);
    //           }
    //        }
    //        else
    //        {
    //            Console.WriteLine(result.Message);
    //        }

           

    //    }
    }
}
