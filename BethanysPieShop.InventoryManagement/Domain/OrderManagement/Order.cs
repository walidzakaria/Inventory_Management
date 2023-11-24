using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BethanysPieShop.InventoryManagement.Domain.OrderManagement
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderFulfilmentDate { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public bool Fulfilled { get; set; }

        public Order() {
            Id = new Random().Next(99999999);
            OrderFulfilmentDate = DateTime.Now.AddSeconds(100);
            OrderItems = new List<OrderItem>();
        }

        public string ShowOrderDetails()
        {
            StringBuilder orderDetails = new();
            orderDetails.AppendLine($"Order ID: {Id}");
            orderDetails.AppendLine($"Order fulfilment date: {OrderFulfilmentDate.ToShortTimeString()}");
            if (OrderItems != null )
            {
                foreach ( var item in OrderItems )
                {
                    orderDetails.AppendLine($"{item.ProductId}. {item.ProductName}: {item.AmountOrdered}");
                }
            }
            return orderDetails.ToString();
        }
    }

}
