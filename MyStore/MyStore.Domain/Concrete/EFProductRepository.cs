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

        public Category DeleteCategory(int id)
        {
            Category dbEntry = db.Category.Find(id);
            if (dbEntry != null)
            {
                db.Category.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public Product DeleteProduct(int id)
        {
            Product dbEntry = db.Product.Find(id);
            if (dbEntry != null)
            {
                db.Product.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveCategory(Category category)
        {
            if (category.Id == 0)
            {
                db.Category.Add(category);
            }
            else {
                Category dbEntry = db.Category.Find(category.Id);
                if (dbEntry != null)
                {
                  
                    dbEntry.Name = category.Name;
                   
                }
            }
            db.SaveChanges();
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
