using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyStore.Domain.Abstract;
namespace MyStore.Domain.Concrete
{
    public class EFProductRepository:IProductsRepository
    {

        private MyStoreEntities db = new MyStoreEntities();

        public IQueryable<Category> Categories
        {
            get{ return db.Category;}
        }

        public IQueryable<Product> Products
        {
            get { return db.Product; }
        }
       
    }
}
