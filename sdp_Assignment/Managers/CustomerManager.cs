using sdp_Assignment.main.Composite;
using sdp_Assignment.main.Decorator;
using sdp_Assignment.main.Model;
using IteratorClass = sdp_Assignment.main.Iterator.Iterator;

namespace sdp_Assignment.Managers
{
    internal class CustomerManager
    {
        private Restaurant selectedRestaurant;
        private List<MenuComponent> cart = new List<MenuComponent>(); // Cart for selected items

        public CustomerManager(Restaurant restaurant)
        {
            selectedRestaurant = restaurant;
        }

        // Main run loop for the customer
        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\nCustomer Menu:");
                Console.WriteLine("1. List & Select Item to Customize");
                Console.WriteLine("2. View Cart");
                Console.WriteLine("3. Checkout / Submit Order");
                Console.WriteLine("0. Exit");
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        MenuComponent item = SelectMenuItem();
                        Console.WriteLine();
                        if (item != null)
                        {
                            // Placeholder for Decorator pattern
                            var customizedItem = CustomizeItem(item);

                            // Add selected (possibly customized) item to cart
                            AddToCart(customizedItem);
                        }
                        break;
                    case "2":
                        ViewCart();
                        break;
                    case "3":
                        Checkout();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }

        // Select a MenuItem from the restaurant menus
        private MenuItem SelectMenuItem()
        {
            // Cast the root menu to RestaurantMenu to safely call createIterator()
            RestaurantMenu menuRoot = (RestaurantMenu)selectedRestaurant.GetMenuRoot();

            List<MenuItem> itemList = new List<MenuItem>();
            IteratorClass menuIterator = menuRoot.createIterator();

            int index = 1;
            Console.WriteLine("\nAvailable Menu Items:");

            while (menuIterator.hasNext())
            {
                MenuComponent comp = (MenuComponent)menuIterator.next();

                // Only consider leaf nodes (MenuItem)
                if (comp is MenuItem menuItem)
                {
                    Console.WriteLine($"{index}. {menuItem.Name} - ${menuItem.Price:N2} | Available: {menuItem.Availability}");
                    itemList.Add(menuItem);
                    index++;
                }
            }

            if (itemList.Count == 0)
            {
                Console.WriteLine("No items available in this restaurant.");
                return null;
            }

            int selectedIndex = -1;
            while (true)
            {
                Console.Write("Enter the number of the item to select (0 to cancel): ");
                if (int.TryParse(Console.ReadLine(), out selectedIndex))
                {
                    if (selectedIndex == 0) return null;
                    if (selectedIndex >= 1 && selectedIndex <= itemList.Count)
                    {
                        return itemList[selectedIndex - 1];
                    }
                }
                Console.WriteLine("Invalid selection. Try again.");
            }
        }

        // Placeholder for the Decorator pattern
        private MenuComponent CustomizeItem(MenuComponent item)
        {
            Console.WriteLine($"\nCustomizing {item.Name}...");
            Console.WriteLine("1. Add Extra Chicken (+$1.50)");
            Console.WriteLine("2. Upgrade to Large (+50%)");
            Console.WriteLine("0. No Customization");
            Console.Write("Enter choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    item = new ExtraChickenDecerator((MenuItem)item); // Use the correct decorator class
                    break;
                case "2":
                    item = new LargeSizeDecorator((MenuItem)item);
                    break;
                case "0":
                default:
                    break;
            }

            Console.WriteLine("Customization applied:");
            item.print();

            return item;
        }

        // Add item to customer's cart
        private void AddToCart(MenuComponent item)
        {
            cart.Add(item);
            Console.WriteLine($"{item.Name} added to cart.");
        }

        // View all items currently in the cart
        private void ViewCart()
        {
            if (cart.Count == 0)
            {
                Console.WriteLine("Your cart is empty.");
                return;
            }

            Console.WriteLine("\nCart Contents:");
            int idx = 1;
            foreach (var item in cart)
            {
                Console.WriteLine($"{idx}. {item.Name} - ${item.Price:N2}");
                idx++;
            }
        }

        // Submit the order to the restaurant (Command pattern placeholder)
        private void Checkout()
        {
            Console.WriteLine("\nSubmitting your order...");
            ViewCart();
            Console.WriteLine("Order sent to restaurant ");
            Console.WriteLine("Command pattern placeholder"); //TODO://Command Pattern.
            cart.Clear();
            
        }
    }

    // Utility class to select a restaurant
    internal static class RestaurantSelector
    {
        public static Restaurant SelectRestaurant(List<Restaurant> restaurants)
        {
            if (restaurants.Count == 0)
            {
                Console.WriteLine("No restaurants available.");
                return null;
            }

            Console.WriteLine("\nAvailable Restaurants:");
            for (int i = 0; i < restaurants.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {restaurants[i].Name}");
            }

            int selection = -1;
            while (true)
            {
                Console.Write("Select a restaurant by number: ");
                if (int.TryParse(Console.ReadLine(), out selection) && selection >= 1 && selection <= restaurants.Count)
                {
                    break;
                }
                Console.WriteLine("Invalid choice, try again.");
            }

            return restaurants[selection - 1];
        }
    }
}
