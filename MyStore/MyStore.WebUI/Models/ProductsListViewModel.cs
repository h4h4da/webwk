using MyStore.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyStore.WebUI.Models
{
    public class ProductsListViewModel
    {

        public IEnumerable<Product> Products { get; set; }

        public PagingInfo paginginfo { get; set; }
        public int CurrentCategoryId { get; set; }
    }
}