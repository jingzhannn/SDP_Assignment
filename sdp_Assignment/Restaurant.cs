using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_Assignment
{
    internal class Restaurant
    {
        public string Name { get; set; }

        private MenuComponent all_menus;

        public Restaurant(string name,MenuComponent mc)
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
                    Iterator menuIterator = all_menus.createIterator();
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
                    Iterator menuIterator = menuToUpdate.createIterator();
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
                    string response = Console.ReadLine().Trim().ToUpper();
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

    }
}
