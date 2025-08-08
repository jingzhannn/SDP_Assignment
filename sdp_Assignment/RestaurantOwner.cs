using sdp_Assignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_Assignment
{
    internal class RestaurantOwner : User
    {  
        public Restaurant restaurant { get; set; }

        public void createRestaurant(List<Restaurant> restaurantList)
        {
            string name;
            while (true)
            {
                Console.Write("Enter NAME of Restaurant: ");
                name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name)) break;
                Console.WriteLine("Restaurant name cannot be empty.");
            }
            Console.WriteLine($"Your Restaurant NAME is {name}");
            Console.WriteLine();
            MenuComponent mainMenu = new RestaurantMenu($"{name}'s Menu");
            string createSubmenuInput;
            while (true)
            {
                Console.Write("Do you want to create Submenu? (Y/N): ");
                createSubmenuInput = Console.ReadLine().ToUpper();
                if (createSubmenuInput == "Y" || createSubmenuInput == "N") break;
                Console.WriteLine("Please enter Y or N.");
            }
            if (createSubmenuInput == "Y")
            {
                while (true)
                {
                    string subMenuName;
                    while (true)
                    {
                        Console.Write("Enter Your Submenu Name (or leave blank to finish): ");
                        subMenuName = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(subMenuName)) break;
                        if (!string.IsNullOrWhiteSpace(subMenuName)) break;
                        Console.WriteLine("Submenu name cannot be empty.");
                    }
                    if (string.IsNullOrWhiteSpace(subMenuName)) break;
                    MenuComponent submenu = new RestaurantMenu(subMenuName);
                    while (true)
                    {
                        string menuItemName;
                        while (true)
                        {
                            Console.Write("Enter Menu Item Name: ");
                            menuItemName = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(menuItemName)) break;
                            Console.WriteLine("Menu item name cannot be empty.");
                        }
                        string availabilityInput;
                        while (true)
                        {
                            Console.Write("Is this item available? (Enter Y/N): ");
                            availabilityInput = Console.ReadLine();
                            if (availabilityInput.Equals("Y", StringComparison.OrdinalIgnoreCase) || availabilityInput.Equals("N", StringComparison.OrdinalIgnoreCase)) break;
                            Console.WriteLine("Please enter Y or N.");
                        }
                        bool available = availabilityInput.Equals("Y", StringComparison.OrdinalIgnoreCase);
                        double price;
                        while (true)
                        {
                            Console.Write("Enter Menu Item Price: ");
                            if (double.TryParse(Console.ReadLine(), out price) && price >= 0) break;
                            Console.WriteLine("Please enter a valid positive number.");
                        }
                        submenu.add(new MenuItem(menuItemName, available, price));
                        string addMoreInput;
                        while (true)
                        {
                            Console.Write("Do you want to add another item to this submenu? (Enter Y/N): ");
                            addMoreInput = Console.ReadLine();
                            if (addMoreInput.Equals("Y", StringComparison.OrdinalIgnoreCase) || addMoreInput.Equals("N", StringComparison.OrdinalIgnoreCase)) break;
                            Console.WriteLine("Please enter Y or N.");
                        }
                        if (!addMoreInput.Equals("Y", StringComparison.OrdinalIgnoreCase))
                        {
                            break;
                        }
                    }
                    mainMenu.add(submenu);
                }
            }
            else
            {
                while (true)
                {
                    string menuItemName;
                    while (true)
                    {
                        Console.Write("Enter Menu Item Name (or leave blank to finish): ");
                        menuItemName = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(menuItemName)) break;
                        if (!string.IsNullOrWhiteSpace(menuItemName)) break;
                        Console.WriteLine("Menu item name cannot be empty.");
                    }
                    if (string.IsNullOrWhiteSpace(menuItemName)) break;
                    string availabilityInput;
                    while (true)
                    {
                        Console.Write("Is this item available? (Enter Y/N): ");
                        availabilityInput = Console.ReadLine();
                        if (availabilityInput.Equals("Y", StringComparison.OrdinalIgnoreCase) || availabilityInput.Equals("N", StringComparison.OrdinalIgnoreCase)) break;
                        Console.WriteLine("Please enter Y or N.");
                    }
                    bool available = availabilityInput.Equals("Y", StringComparison.OrdinalIgnoreCase);
                    double price;
                    while (true)
                    {
                        Console.Write("Enter Menu Item Price: ");
                        if (double.TryParse(Console.ReadLine(), out price) && price >= 0) break;
                        Console.WriteLine("Please enter a valid positive number.");
                    }
                    mainMenu.add(new MenuItem(menuItemName, available, price));
                    string addMoreInput;
                    while (true)
                    {
                        Console.Write("Do you want to add another item? (Enter Y/N): ");
                        addMoreInput = Console.ReadLine();
                        if (addMoreInput.Equals("Y", StringComparison.OrdinalIgnoreCase) || addMoreInput.Equals("N", StringComparison.OrdinalIgnoreCase)) break;
                        Console.WriteLine("Please enter Y or N.");
                    }
                    if (!addMoreInput.Equals("Y", StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }
                }
            }

            this.restaurant = new Restaurant(name, mainMenu);
            restaurantList.Add(this.restaurant);
            Console.WriteLine($"Restaurant {name} created and added to list.");
        }
    }
}
