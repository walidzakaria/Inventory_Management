using BethanysPieShop.InventoryManagement.Domain.General;
using System.Text;

namespace BethanysPieShop.InventoryManagement.Domain.ProductManagement
{
    public partial class Product
    {
        private int id;
        private string name = string.Empty;
        private string? description;
        private int maxItemsInStock = 0;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value.Length > 50 ? value[..50] : value;
            }
        }

        public string? Description
        {
            get { return description; }
            set
            {
                if (value == null)
                {
                    description = string.Empty;
                }
                else
                {
                    description = value.Length > 250 ? value[..250] : value;
                }
            }
        }

        public UnitType UnitType { get; set; }
        public int AmountInStock { get; private set; }
        public bool IsBelowStockThreshold { get; set; }
        public Price Price { get; set; }

        public Product(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Product(int id) : this(id, string.Empty) { }

        public Product(int id, string name, string? description, Price price, UnitType unitType, int maxAmountInStock)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            UnitType = unitType;
            maxItemsInStock = maxAmountInStock;
            UpdateLowStock();
        }

        public void UseProduct(int items)
        {
            if (items <= AmountInStock)
            {
                // use the items
                AmountInStock -= items;
                UpdateLowStock();
                Log($"Amount in stock updated. Now {AmountInStock} items in stock.");
            }
            else
            {
                Log($"Not enough items on stock for {CreateSimpleProductRepresentation()}. {AmountInStock} available but {items} requested.");
            }
        }

        public void IncreaseStock()
        {
            AmountInStock++;
        }

        public void IncreaseStock(int amount)
        {
            int newStock = AmountInStock + amount;
            if (newStock <= maxItemsInStock)
            {
                AmountInStock += amount;
            }
            else
            {
                AmountInStock = maxItemsInStock; // we only store possible items, overstock isn't stored
                Log($"{CreateSimpleProductRepresentation} stock overflow. {newStock - AmountInStock} items(s) ordered that couldn't be stored.");
            }
            UpdateLowStock();
        }
        public void DecreaseStock(int items, string reason)
        {
            if (items <= AmountInStock)
            {
                // decrease the stock with specified number items
                AmountInStock -= items;
            }
        }

    }
}