using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BethanysPieShop.InventoryManagement.Domain.ProductManagement
{
    public partial class Product
    {
        public static int StockThreshold = 5;

        public static void ChangeStockThreshold(int newStockThreshold)
        {
            if (newStockThreshold > 0)
            {
                StockThreshold = newStockThreshold;
            }
        }
        public void UpdateLowStock()
        {
            IsBelowStockThreshold = AmountInStock < 10; // for now fixed
        }
        private static void Log(string message)
        {
            Console.WriteLine(message);
        }
        private string CreateSimpleProductRepresentation()
        {
            return $"Product {id}";
        }

        public string DisplayDetailsShort()
        {
            return $"{id}. {name}\n{AmountInStock} items in stock";
        }
        public string DisplayDetailsFull()
        {
            return DisplayDetailsFull("");
        }

        private string DisplayDetailsFull(string extraDetails)
        {
            StringBuilder sb = new();
            // @ToDo: add price here too
            sb.Append($"{id} {name} \n{description}\n{Price}\n{AmountInStock} item(s) in stock");
            if (IsBelowStockThreshold)
            {
                sb.Append("\n!!STOCK LOW!!");
            }
            sb.Append(extraDetails);
            return sb.ToString();
        }
    }
}
