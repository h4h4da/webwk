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

        public IQueryable<Customer> Customers
        {
            get{ return db.Customer; }
        }

        public IQueryable<Product> Products
        {
            get { return db.Product; }
        }

        public void SaveProduct(Product product)
        {
            if (product.Id == 0)
            {
                db.Product.Add(product);
            }
            else {
                Product dbEntry = db.Product.Find(product.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.CategoryId = product.CategoryId;
                    dbEntry.Price = product.Price;
                    dbEntry.Description = product.Description;
                    dbEntry.ImageUrl = product.ImageUrl;
                }
            }
            db.SaveChanges();
        }
    }
}
