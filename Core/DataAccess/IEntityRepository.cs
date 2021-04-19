using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess

{
    public interface IEntityRepository<T> where T:class, IEntity,new()
    {
        //generic constraint  ( <T> t yerine entity dekilerden baska bir sey kabul etmemesi için.
        //class :  referans tip
        //IEntity :  IEntity olabilir veya IEntity implemente eden bir nesne olabilir.
        //new() :  new'lenebilir olmalı (IEntity kendisini yazamayız interfaceler new ' lenemez.

        List<T> GetAll(Expression<Func<T,bool>> filter=null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(int Id);
        
    }
}
