using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoomir
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Cost { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }

        public Product() { }

        public Product(int id, string title, decimal cost, decimal discount, int quantity)
        {
            Id = id;
            Title = title;
            Cost = cost;
            Discount = discount;
            Quantity = quantity;
        }
    }
}
