using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoomir
{
    public class Order
    {
        public int Id { get; set; }
        public string Point {  get; set; }
        public string State { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        

        public Order() { }
        public Order(int id, string point, string state, DateTime date, decimal totalAmount, decimal totalDiscount)
        {
            Id = id;
            Point = point;
            State = state;
            Date = date;
            TotalAmount = totalAmount;
            TotalDiscount = totalDiscount;
        }
    }
}
