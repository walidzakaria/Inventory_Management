using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BethanysPieShop.InventoryManagement.Domain.General
{
    public class Price
    {
        public double ItemPrice { get; set; }

        public Currency Currency { get; set; }

        public Price() { }

        public Price(double itemPrice, Currency currency)
        {
            ItemPrice = itemPrice;
            Currency = currency;
        }

        public override string ToString()
        {
            return $"{ItemPrice} {Currency}";
        }

    }
}
