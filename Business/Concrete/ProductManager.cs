using Business.Abstract;
using Business.Constans;
using Business.BusinessAspects.Autofac;
using Business.Validationrules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;

        }

        [SecuredOperation("product.add")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]//data degistigi için cache in de silinmesi gerek.
        public IResult Add(Product product)
        {

            IResult result = BusinessRules.Run(CheckIfProductNameExist(product.ProductName),
                  CheckIfProductCountOfCategoryCorrect(product.CategoryId), CheckIfCategoryLimit());
            if (result != null) //null ise businessRules ta dönen hiç bir logic (hatalı kural) yok.
            {
                return result;
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);



        }

        [CacheAspect] //key, value
        public IDataResult<List<Product>> GetAll()
        {
            //iş kodları
            //yetkisi var mı?
            //ürünlerin listelenmesi her gün 22 de kapanıyor gibi.
            //if (DateTime.Now.Hour == 23)
            //{
            //    return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            //}


            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
           

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id), Messages.ProductsListed);

        }


        [CacheAspect]
        public IDataResult<Product> GetById(int productId)
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<Product>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId), Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max), Messages.ProductsListed);

        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {

            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails(), Messages.ProductsListed);

        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")] //içerisinde get olan tüm key leri iptal eder.(ürün güncellendiginde)
        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult("güncellendi");
        }

        //bu metod sadece bu class içerisinde kullanılacak. iş kuralı parcacıgı oldugu için private.
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            //select count(*) from products where categoryId=1;
            //products tablosunda gönderilen categoriden kaç tane oldugunu count ile veriyor.
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 15)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();//burada mesaj vermemize gerek yok. müsteriye gösterilecek bir sey degil.
        }

        private IResult CheckIfProductNameExist(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();//Any bu sarta uyan eleman var mı yok mu onu
                                                                                     //true ise                                                          //bool degerinde döndürür.
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimit()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count > 15)  //------------------------result.Data.Count
            {
                return new ErrorResult(Messages.CategoryLimit);
            }
            return new SuccessResult();
        }

        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {
            //bir eklemeyi basarılı yaptı ama altta hata verdi. o zaman yukarıda yaptıgı islemi geri al dersek.
            Add(product);
            if (product.UnitPrice < 10)
            {
                throw new Exception("");
            }
            Add(product);

            return null;
            
        }
    }
}
