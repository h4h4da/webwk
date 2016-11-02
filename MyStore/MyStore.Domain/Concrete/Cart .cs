using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Domain.Concrete
{
    public class Cart
    {

        private List<CartLine> lineCollection = new List<CartLine>();
        public void AddItem(Product p, int quantity)
        {
            CartLine line = lineCollection.Where(e => e.Product.Id == p.Id).FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine { Product = p, Quantity = quantity });
            }
            else line.Quantity += quantity;
        }


        public void Clear()
        {
            lineCollection.Clear();
        }
        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Product.Price * e.Quantity);
        }
        public IEnumerable<CartLine> Line { get { return lineCollection; } }

        public void RemoveLine(Product p)
        {
            lineCollection.RemoveAll(e => e.Product.Id == p.Id);
        }
         
    }   
}
