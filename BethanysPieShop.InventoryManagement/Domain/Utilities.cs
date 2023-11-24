using BethanysPieShop.InventoryManagement.Domain.General;
using BethanysPieShop.InventoryManagement.Domain.OrderManagement;
using BethanysPieShop.InventoryManagement.Domain.ProductManagement;

namespace BethanysPieShop.InventoryManagement.Domain
{
    internal class Utilities
    {
        private static List<Product> inventory = new();
        private static List<Order> orders = new();

        internal static void InitializeStock()
        {
            Product p1 = new(1, "Sugar", "Lorem ipsum", new Price(10, Currency.Euro), UnitType.PerKg, 100);
            Product p2 = new(2, "Cake decorations", "Lorem ipsum", new Price(8, Currency.Euro), UnitType.PerItem, 20);
            Product p3 = new(3, "Strawberry", "Lorem ipsm", new Price(3, Currency.Euro), UnitType.PerBox, 10);
            inventory.Add(p1);
            inventory.Add(p2);
            inventory.Add(p3);

            // check to handle fulfilled orders
            ShowFulfilledOrders();
            if (orders.Count > 0)
            {
                Console.WriteLine("Open orders:");
                foreach (var order in orders)
                {
                    Console.WriteLine(order.ShowOrderDetails());
                }
            } else
            {
                Console.WriteLine("There're no open orders");
            }
            Console.ReadLine();
        }

        internal static void ShowMainMenu()
        {
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine("********************");
            Console.WriteLine("* Select an action *");
            Console.WriteLine("********************");
            Console.WriteLine("1: Inventory management");
            Console.WriteLine("2: Order management");
            Console.WriteLine("3: Save all data");
            Console.WriteLine("0: Close application");

            Console.Write("Your selection: ");
            string? userSelection = Console.ReadLine();
            switch (userSelection)
            {
                case "1":
                    ShowInventoryManagementMenu(); break;
                case "2": ShowOrderManagementMenu(); break;
                case "3": ShowSettingsMenu(); break;
                case "4":
                    // SaveAllData();
                    break;
                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }


        private static void ShowInventoryManagementMenu()
        {
            string? userSelection;
            do
            {
                Console.ResetColor();
                Console.Clear();
                Console.WriteLine("************************");
                Console.WriteLine("* Inventory management *");
                Console.WriteLine("************************");

                ShowAllProductsOverview();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("What do you want to do?");
                Console.ResetColor();

                Console.WriteLine("1: View details of product");
                Console.WriteLine("2: Add new product");
                Console.WriteLine("3: Clone product");
                Console.WriteLine("4: View products with low stock");
                Console.WriteLine("0: Back to main menu");

                Console.Write("Your selection: ");
                userSelection = Console.ReadLine();

                switch (userSelection)
                {
                    case "1":
                        ShowDetailsAndUseProduct(); break;
                    case "2":
                        // ShowCreateNewProduct();
                        break;
                    case "3":
                        // ShowCloneExistingProduct();
                        break;
                    case "4":
                        ShowProductsLowOnStock();
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }

            }
            while (userSelection != "0");
            ShowMainMenu();
        }

        private static void ShowDetailsAndUseProduct()
        {
            string? userSelection = string.Empty;
            Console.WriteLine("Enter the ID of product: ");
            string? selectedProductId = Console.ReadLine();
            if (selectedProductId != null)
            {
                Product? selectedProduct = inventory.Where((p) => p.Id == int.Parse(selectedProductId)).FirstOrDefault();
                if (selectedProduct != null) {
                    Console.WriteLine(selectedProduct.DisplayDetailsFull());
                    Console.WriteLine("\nWhat do you want to do?");
                    Console.WriteLine("1: Use product");
                    Console.WriteLine("0: Back to inventory overview");

                    Console.Write("Your selection: ");
                    userSelection = Console.ReadLine();

                    if (userSelection == "1")
                    {
                        Console.WriteLine("How many products do you want to use?");
                        int amount = int.Parse(Console.ReadLine() ?? "0");
                        selectedProduct.UseProduct(amount);
                        Console.ReadLine();
                    }
                }
            }
            else
            {
                Console.WriteLine("Non-existing product selected. Plesae try again.");
            }
        }

        private static void ShowProductsLowOnStock()
        {
            throw new NotImplementedException();
        }

        private static void ShowAllProductsOverview()
        {
            foreach (var product in inventory)
            {
                Console.WriteLine(product.DisplayDetailsShort());
                Console.WriteLine();
            }
        }

        private static void ShowSettingsMenu()
        {
            string? userSelection;
            do
            {
                Console.ResetColor();
                Console.Clear();
                Console.WriteLine("************");
                Console.WriteLine("* Settings *");
                Console.WriteLine("************");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("What do you want to do?");
                Console.ResetColor();

                Console.WriteLine("1: Change Stock threshold");
                Console.WriteLine("0: Back to main menu");

                Console.Write("Your selection: ");
                userSelection = Console.ReadLine();

                switch (userSelection)
                {
                    case "1":
                        ShowChangStockThreshold(); break;
                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }

            }
            while (userSelection != "0");
            ShowMainMenu();
        }

        private static void ShowOrderManagementMenu()
        {
            string? userSelection;
            do
            {
                Console.ResetColor();
                Console.Clear();
                Console.WriteLine("********************");
                Console.WriteLine("* Select an action *");
                Console.WriteLine("********************");


                Console.WriteLine("1: Open order overview");
                Console.WriteLine("2: Add new order");
                Console.WriteLine("0: Back to main menu");

                Console.Write("Your selection: ");
                userSelection = Console.ReadLine();

                switch (userSelection)
                {
                    case "1":
                        ShowOpenOrderOvewview(); break;
                    case "2":
                        ShowAddNewOrder();
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }

            }
            while (userSelection != "0");
            ShowMainMenu();
        }

        private static void ShowAddNewOrder()
        {
            Order newOrder = new();
            string? selectedProductId = string.Empty;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Creating new order");
            Console.ResetColor();

            do
            {
                ShowAllProductsOverview();
                Console.WriteLine("Which product do you want to order? (enter 0 to stop adding new products to the order");
                Console.Write("Enter the ID of product: ");
                selectedProductId = Console.ReadLine();

                if (selectedProductId != "0")
                {
                    Product? selectedProduct = inventory.Where(p => p.Id == int.Parse(selectedProductId ?? "0")).FirstOrDefault();
                    if (selectedProduct != null)
                    {
                        Console.WriteLine("How many do you want to order? ");
                        int amountOrdered = int.Parse(Console.ReadLine() ?? "0");
                        OrderItem orderItem = new() { ProductId = selectedProduct.Id, ProductName = selectedProduct.Name, AmountOrdered = amountOrdered };

                    }
                }
            } while (selectedProductId != "0");
        }

        private static void ShowOpenOrderOvewview()
        {
            // check to handle fulfilled orders
            ShowFulfilledOrders();
            if (orders.Count > 0)
            {
                Console.WriteLine("Open orders:");
                foreach (var order in orders)
                {
                    Console.WriteLine(order.ShowOrderDetails());
                    Console.WriteLine();
                }
            } else
            {
                Console.WriteLine("There are no open orders");
            }
            Console.ReadLine();
        }

        private static void ShowFulfilledOrders()
        {
            Console.WriteLine("Checking fulfilled orders.");
            foreach (var order in orders)
            {
                if (!order.Fulfilled && order.OrderFulfilmentDate < DateTime.Now)
                {
                    foreach (var orderItem in order.OrderItems)
                    {
                        Product? selectedProduct = inventory.Where((p) => p.Id == orderItem.ProductId).FirstOrDefault();
                        if (selectedProduct != null)
                        {
                            selectedProduct.IncreaseStock(orderItem.AmountOrdered);
                        }
                    }
                    order.Fulfilled = true;
                }
            }
            orders.RemoveAll(o => o.Fulfilled);
            Console.WriteLine("Fulfiled orders checked'");
        }

        private static void ShowChangStockThreshold()
        {
            Console.WriteLine($"Enter the new stock threshold (current value: {Product.StockThreshold}). This applies to all products!");
            Console.Write("New value: ");
            int newValue = int.Parse(Console.ReadLine() ?? "0");
            Product.StockThreshold = newValue;
            foreach (var product in inventory)
            {
                product.UpdateLowStock();
            }
            Console.ReadLine();
        }
    }
}
