using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyStore.Domain.Concrete;
namespace MyStore.Domain.Abstract
{
    public interface IProductsRepository
    {

        IQueryable<Product> Products { get; }
        IQueryable<Category> Categories { get; }
        IQueryable<Customer> Customers { get; }
    }
}
