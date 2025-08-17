using sdp_Assignment.main.Composite;
using sdp_Assignment.main.Iterator;
//TODO: read Program.cs Line 204
namespace sdp_Assignment.main.Model
{
    internal class Restaurant
    {
        public string Name { get; set; }

        private MenuComponent all_menus;

        public Restaurant(string name, MenuComponent mc)
        {
            Name = name;
            all_menus = mc;
        }

        public void addMenu(MenuComponent mc)
        {
            all_menus.add(mc);
        }

        public void printMenu()
        {
            all_menus.print();
        }

        public MenuComponent? GetMenuComponentByName(string itemName)
        {
            return FindMenuComponentByName(all_menus, itemName);
        }

        private MenuComponent? FindMenuComponentByName(MenuComponent component, string itemName)
        {
            if (component.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase))
            {
                return component;
            }

            try
            {
                IMenuIterator iterator = component.createIterator();
                while (iterator.hasNext())
                {
                    MenuComponent child = (MenuComponent)iterator.next();
                    MenuComponent? found = FindMenuComponentByName(child, itemName);
                    if (found != null)
                    {
                        return found;
                    }
                }
            }
            catch (NotSupportedException)
            {
                // This is a leaf node (MenuItem), no children to search
            }

            return null;
        }

        public void updateItem()
        {
            int input = 0;

            Console.WriteLine($"Updating menu for {Name}");
            Console.WriteLine();
            printMenu();

            while (true)
            {
                Console.Write("Which Menu do you want to update? ");
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                    IMenuIterator menuIterator = all_menus.createIterator();
                    int menuCount = 0;
                    while (menuIterator.hasNext())
                    {
                        menuIterator.next();
                        menuCount++;
                    }
                    if (input < 1 || input > menuCount)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    Console.WriteLine();
                    break;
                }
                catch
                {
                    Console.WriteLine("Please Enter a Valid Option");
                    Console.WriteLine();
                }

            }

            MenuComponent menuToUpdate = all_menus.getChild(input - 1);
            Console.WriteLine($"Updating {menuToUpdate.Name.ToUpper()}");
            Console.WriteLine();
            menuToUpdate.print();

            while (true)
            {
                Console.Write("Select Item to Update: ");
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                    IMenuIterator menuIterator = menuToUpdate.createIterator();
                    int menuCount = 0;
                    while (menuIterator.hasNext())
                    {
                        menuIterator.next();
                        menuCount++;
                    }
                    if (input < 1 || input > menuCount)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    Console.WriteLine();
                    break;
                }
                catch
                {
                    Console.WriteLine("Please Enter a Valid Option");
                    Console.WriteLine();
                }

            }

            MenuItem itemToUpdate = (MenuItem)menuToUpdate.getChild(input - 1);
            itemToUpdate.print();
            Console.WriteLine();

            while (true)
            {
                try
                {
                    Console.WriteLine("What Do You Want To Update? ");
                    Console.WriteLine("1. Price");
                    Console.WriteLine("2. Availability");
                    Console.WriteLine();
                    Console.Write("Enter Option: ");

                    input = Convert.ToInt32(Console.ReadLine());
                    if (input < 1 || input > 2)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    Console.WriteLine();
                    break;
                }
                catch
                {
                    Console.WriteLine("Please Enter a Valid Option");
                    Console.WriteLine();
                }

            }

            if (input == 1)
            {
                double newPrice = 0;
                while (true)
                {
                    Console.Write($"Enter {itemToUpdate.Name} New Price: ");
                    try
                    {
                        newPrice = Convert.ToDouble(Console.ReadLine());
                        if (newPrice < 0)
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Please Enter a Valid Price");
                    }
                }
                itemToUpdate.SetPrice(newPrice);
                Console.WriteLine($"{itemToUpdate.Name} price updated to ${itemToUpdate.Price:N2} successfully.");
            }
            else if (input == 2)
            {
                bool newAvailability = false;
                while (true)
                {
                    Console.Write($"Is {itemToUpdate.Name} Available? (Y/N): ");
                    string response = Console.ReadLine()?.Trim().ToUpper() ?? "";
                    if (response == "Y")
                    {
                        newAvailability = true;
                        break;
                    }
                    else if (response == "N")
                    {
                        newAvailability = false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please Enter Y or N");
                    }
                }
                itemToUpdate.SetAvailability(newAvailability);
                Console.WriteLine($"{itemToUpdate.Name} is {(newAvailability ? "Available" : "Not Available")} successfully.");
            }
        }

        public void AddMenuOrItem()
        {
            int input = 0;
            Console.WriteLine("What do you want to add?");
            Console.WriteLine("1. Submenu");
            Console.WriteLine("2. Menu Item");
            Console.WriteLine();
            while (true)
            {
                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out input) && (input == 1 || input == 2))
                    break;
                Console.WriteLine("Invalid choice. Please enter 1 or 2.");
            }

            Console.WriteLine();

            if (input == 1)
            {
                Console.Write("Enter Submenu Name: ");
                string submenuName = Console.ReadLine() ?? "";
                MenuComponent submenu = new RestaurantMenu(submenuName);
                all_menus.add(submenu);
                Console.WriteLine();
                Console.WriteLine($"Submenu '{submenuName}' added successfully.");
                Console.WriteLine();
                printMenu();
            }
            else
            {
                Console.Write("Enter Item Name: ");
                string itemName = Console.ReadLine() ?? "";
                bool available = false;
                while (true)
                {
                    Console.Write("Is it Available? (Y/N): ");
                    string availInput = Console.ReadLine()?.Trim().ToUpper() ?? "";
                    if (availInput == "Y")
                    {
                        available = true;
                        break;
                    }
                    else if (availInput == "N")
                    {
                        available = false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter Y or N.");
                    }
                }
                double price = 0;
                while (true)
                {
                    Console.Write("Enter Price: ");
                    try
                    {
                        price = Convert.ToDouble(Console.ReadLine());
                        if (price < 0)
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Please Enter a Valid Price");
                    }
                }

                Console.WriteLine();
                MenuItem item = new MenuItem(itemName, available, price);
                printMenu();

                IMenuIterator submenuIterator = all_menus.createIterator();
                int submenuCount = 0;
                while (submenuIterator.hasNext())
                {
                    submenuIterator.next();
                    submenuCount++;
                }

                MenuComponent submenu = null;
                int submenuIndex = -1;
                while (true)
                {
                    Console.Write($"Which Submenu to add '{itemName}' to? ");
                    string submenuInput = Console.ReadLine() ?? "";
                    if (int.TryParse(submenuInput, out submenuIndex) && submenuIndex >= 1 && submenuIndex <= submenuCount)
                    {
                        submenu = all_menus.getChild(submenuIndex - 1);
                        break;
                    }
                    Console.WriteLine($"Invalid submenu index. Please enter a number between 1 and {submenuCount}");
                }
                if (submenu != null)
                {
                    submenu.add(item);
                    Console.WriteLine($"Menu item '{itemName}' added successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to add menu item. Submenu not found.");
                }
                printMenu();
            }
        }

        public void DeleteMenuOrItem()
        {
            int input = 0;
            Console.WriteLine("What do you want to delete?");
            Console.WriteLine("1. Submenu");
            Console.WriteLine("2. Menu Item");
            Console.WriteLine();
            int deleteChoice = 0;
            while (true)
            {
                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out deleteChoice) && (deleteChoice == 1 || deleteChoice == 2))
                    break;
                Console.WriteLine("Invalid choice. Please enter 1 or 2.");
            }
            Console.WriteLine();

            if (deleteChoice == 1)
            {
                // Delete Submenu
                Console.WriteLine("Deleting SUBMENU");
                Console.WriteLine();
                printMenu();
                IMenuIterator menuIterator = all_menus.createIterator();
                int menuCount = 0;
                while (menuIterator.hasNext())
                {
                    menuIterator.next();
                    menuCount++;
                }
                if (menuCount == 0)
                {
                    Console.WriteLine("No submenus to delete.");
                    return;
                }
                while (true)
                {
                    Console.Write("Select Submenu to Delete: ");
                    try
                    {
                        input = Convert.ToInt32(Console.ReadLine());
                        if (input < 1 || input > menuCount)
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                        break;
                    }
                    catch
                    {
                        Console.WriteLine($"Please enter a valid submenu index (1 to {menuCount})");
                    }
                }
                Console.WriteLine();
                MenuComponent submenuToDelete = all_menus.getChild(input - 1);
                all_menus.remove(submenuToDelete);
                Console.WriteLine();
                Console.WriteLine($"Submenu '{submenuToDelete.Name}' deleted successfully.");
                Console.WriteLine();
            }
            else if (deleteChoice == 2)
            {
                // Delete Menu Item
                Console.WriteLine("Deleting MENU ITEM");
                Console.WriteLine();
                IMenuIterator menuIterator = all_menus.createIterator();
                int menuCount = 0;
                while (menuIterator.hasNext())
                {
                    menuIterator.next();
                    menuCount++;
                }
                if (menuCount == 0)
                {
                    Console.WriteLine("No submenus available to delete items from.");
                    return;
                }
                printMenu();
                while (true)
                {
                    Console.Write("Delete item from which Submenu? ");
                    try
                    {
                        input = Convert.ToInt32(Console.ReadLine());
                        if (input < 1 || input > menuCount)
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                        break;
                    }
                    catch
                    {
                        Console.WriteLine($"Please enter a valid submenu index (1 to {menuCount})");
                    }
                }
                MenuComponent submenu = all_menus.getChild(input - 1);
                submenu.print();
                Console.WriteLine();
                IMenuIterator itemIterator = submenu.createIterator();
                int itemCount = 0;
                while (itemIterator.hasNext())
                {
                    itemIterator.next();
                    itemCount++;
                }
                if (itemCount == 0)
                {
                    Console.WriteLine("No items to delete in this submenu.");
                    return;
                }
                int itemIndex = -1;
                while (true)
                {
                    Console.Write("Select Item to Delete: ");
                    try
                    {
                        itemIndex = Convert.ToInt32(Console.ReadLine());
                        if (itemIndex < 1 || itemIndex > itemCount)
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                        break;
                    }
                    catch
                    {
                        Console.WriteLine($"Please enter a valid item index (1 to {itemCount})");
                    }
                }
                Console.WriteLine();
                MenuComponent itemToDelete = submenu.getChild(itemIndex - 1);
                submenu.remove(itemToDelete);
                Console.WriteLine($"Menu item '{itemToDelete.Name}' deleted successfully.");
                Console.WriteLine();
            }
            printMenu();
        }

        // Add this method to expose the root menu component
        public MenuComponent GetMenuRoot()
        {
            return all_menus;
        }
    }
}
