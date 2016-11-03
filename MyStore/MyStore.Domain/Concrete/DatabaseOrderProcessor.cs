using MyStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Domain.Concrete
{
    public class DatabaseOrderProcessor : IOrderProcessor
    {
        public void ProcessOrder(Cart cart, ShippingAddress shippingAddress, Customer customer)
        {
            using (var db = new MyStoreEntities())
            {
                using (var dbContextTransaction=db.Database.BeginTransaction())
                {

                    try
                    {
                        Order order = new Order();
                        order.CustomerId = customer.Id;
                        order.OrderDate = DateTime.Now;
                        order.Name = shippingAddress.Name;
                        db.Order.Add(order);
                        foreach (var cartLine in cart.Line)
                        {
                            OrderDetail orderDetail = new OrderDetail();
                            orderDetail.OrderId = order.Id;
                            orderDetail.ProductId = cartLine.Product.Id;
                            orderDetail.Price = cartLine.Product.Price;
                            db.OrderDetail.Add(orderDetail);
                        }
                        Customer customerEntry = db.Customer.Find(customer.Id);
                        customerEntry.Balance = customerEntry.Balance - cart.ComputeTotalValue();
                        db.SaveChanges();
                        dbContextTransaction.Commit();

                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
                    
            }
        }
    }
}
