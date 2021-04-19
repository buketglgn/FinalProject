using Core.Entities;

using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    //çıplak class kalmasın (herhangi bir class inheritance ve interface implementasyonu almıyorsa sıkıntı yasarsın

   public class Category :IEntity
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

    }
}
