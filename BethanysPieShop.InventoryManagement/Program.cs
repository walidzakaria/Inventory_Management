using BethanysPieShop.InventoryManagement.Domain;
using BethanysPieShop.InventoryManagement.Domain.General;
using BethanysPieShop.InventoryManagement.Domain.ProductManagement;


PrintWelcome();
Utilities.InitializeStock();
Utilities.ShowMainMenu();
Console.WriteLine("Application shutting down...");
Console.ReadLine();

#region Layout
static void PrintWelcome()
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(@"
    ()()()()()()()()()()()()()()()()
    -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    Welcome To Bethany's Pie Shop
    Inventory Management
    ()()()()()()()()()()()()()()()()
    -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    ");
    Console.ResetColor();
    Console.WriteLine("Press enter key to start loggin in!");

    // accepting enter here;
    Console.ReadLine();
    Console.Clear();

}
#endregion