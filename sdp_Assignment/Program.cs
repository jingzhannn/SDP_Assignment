using sdp_Assignment;
//ok so get this
//business-logic is still inside the program.cs
//also single responsiblity
//also open-closed principle
//also the while true loop
//TODO://




List<User> users = new List<User>();
List<Restaurant> restaurants = new List<Restaurant>();

void MainMenu()
{
    Console.WriteLine("======================================");
    Console.WriteLine("Welcome to Grabberoo");
    Console.WriteLine("======================================");
    Console.WriteLine();
    Console.WriteLine("Please select an option to proceed:");
    Console.WriteLine();
    Console.WriteLine("[1] Login");
    Console.WriteLine("[2] Register");
    Console.WriteLine("[3] View All Restaurants");
    Console.WriteLine("[0] Exit");
    Console.WriteLine();
    Console.Write("Enter your choice: ");
}

void RegisterUser()
{
    User user;

    Console.WriteLine("What are you registering as?");
    Console.WriteLine("[1] Customer");
    Console.WriteLine("[2] Restaurant Owner");
    Console.WriteLine();
    int choice;
    while (true)
    {
        Console.Write("Enter your choice: ");
        if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= 2)
            break;
        Console.WriteLine("Please enter 1 or 2.");
    }
    Console.WriteLine();

    if (choice == 2)
    {
        Console.WriteLine("Registering as RESTAURANT OWNER");
        UserGenerator userGenerator = new RestaurantOwnerGenerator();
        user = userGenerator.createUser();
    }
    else
    {
        Console.WriteLine("Registering as CUSTOMER");
        UserGenerator userGenerator = new CustomerGenerator();
        user = userGenerator.createUser();
    }

    users.Add(user);
    Console.WriteLine($"Welcome to Grabberoo {user.Username}");
    Console.WriteLine("Redirecting you back to MainMenu...");
    Console.WriteLine();
}

User LoginUser()
{
    string username;
    while (true)
    {
        Console.Write("Enter your USERNAME: ");
        username = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(username)) break;
        Console.WriteLine("Username cannot be empty.");
    }
    string password;
    while (true)
    {
        Console.Write("Enter your PASSWORD: ");
        password = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(password)) break;
        Console.WriteLine("Password cannot be empty.");
    }
    User loggedInUser = users.FirstOrDefault(u => u.Username == username && u.Password == password);
    if (loggedInUser == null)
    {
        Console.WriteLine("Login failed. Invalid credentials.");
        return null;
    }
    Console.WriteLine($"Login Successful! Welcome Back {username}!");
    Console.WriteLine();
    return loggedInUser;
}

void RunUserFeatures(User user)
{
    if (user is RestaurantOwner owner)
    {
        while (true)
        {
            Console.WriteLine("[1] View Own Restaurant");
            Console.WriteLine("[2] Update Own Restaurant Items");
            Console.WriteLine("[0] Logout");
            string ownerChoice;
            while (true)
            {
                Console.Write("Enter your choice: ");
                ownerChoice = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(ownerChoice)) break;
                Console.WriteLine("Choice cannot be empty.");
            }
            if (ownerChoice == "1")
            {
                if (owner.restaurant != null)
                {
                    Console.WriteLine($"Restaurant: {owner.restaurant.Name}");
                    owner.restaurant.printMenu();
                }
                else
                {
                    Console.WriteLine("No restaurant found.");
                }
            }
            else if (ownerChoice == "2")
            {
                if (owner.restaurant != null)
                {
                    UpdateRestaurantMenu(owner.restaurant);
                }
                else
                {
                    Console.WriteLine("No restaurant found.");
                }
            }
            else if (ownerChoice == "0")
            {
                Console.WriteLine();
                break;
            }
        }
    }
    else if (user is Customer)
    {
        ViewAllRestaurants();
        Console.WriteLine("[L]ogout. Enter L to LOGOUT");
        Console.Write("Enter your choice: ");
        string input = Console.ReadLine();
        Console.WriteLine();
    }
}

void CreateRestaurant(RestaurantOwner owner)
{
    owner.createRestaurant(restaurants);
}

void ViewAllRestaurants()
{
    if (restaurants.Count == 0)
    {
        Console.WriteLine("No restaurants available.");
        return;
    }
    foreach (var r in restaurants)
    {
        Console.WriteLine($"Restaurant: {r.Name}");
        r.printMenu();
    }
}

void InitializeSampleData()
{
    // Sample RestaurantOwner
    var owner1 = new RestaurantOwner
    {
        Username = "owner1",
        Password = "password1",
        Email = "owner1@email.com"
    };
    var owner2 = new RestaurantOwner
    {
        Username = "owner2",
        Password = "password2",
        Email = "owner2@email.com"
    };

    // Sample Customers
    var customer1 = new Customer
    {
        Username = "customer1",
        Password = "password1",
        Email = "customer1@email.com"
    };
    var customer2 = new Customer
    {
        Username = "customer2",
        Password = "password2",
        Email = "customer2@email.com"
    };

    // Sample Menus
    var menu1 = new RestaurantMenu("Main Menu");
    menu1.add(new MenuItem("Chicken Rice", true, 3.5));
    menu1.add(new MenuItem("Nasi Lemak", true, 4.0));
    var menu2 = new RestaurantMenu("Main Menu");
    menu2.add(new MenuItem("Burger", true, 5.5));
    menu2.add(new MenuItem("Fries", true, 2.5));

    // Sample Restaurants
    var restaurant1 = new Restaurant("Haaker", menu1);
    var restaurant2 = new Restaurant("Mcdooonal", menu2);
    owner1.restaurant = restaurant1;
    owner2.restaurant = restaurant2;

    // Add to lists
    users.Add(owner1);
    users.Add(owner2);
    users.Add(customer1);
    users.Add(customer2);
    restaurants.Add(restaurant1);
    restaurants.Add(restaurant2);
}

void UpdateRestaurantMenu(Restaurant restaurant)
{
    Console.WriteLine($"Updating menu for {restaurant.Name}");
    // Only allow update for top-level menu items (no submenu support for simplicity)
    Console.WriteLine("Current Menu:");
    restaurant.printMenu();
    string itemName;
    while (true)
    {
        Console.WriteLine("Enter the name of the item to update (or leave blank to exit): ");
        itemName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(itemName)) return;
        var menuComponent = restaurant.GetMenuComponentByName(itemName);
        if (menuComponent is MenuItem item)
        {
            double newPrice;
            while (true)
            {
                Console.WriteLine($"Current price: {item.Price}");
                Console.Write("Enter new price: ");
                if (double.TryParse(Console.ReadLine(), out newPrice) && newPrice >= 0) break;
                Console.WriteLine("Please enter a valid positive number.");
            }
            item.SetPrice(newPrice);
            Console.WriteLine("Price updated.");
            bool avail;
            while (true)
            {
                Console.WriteLine($"Current availability: {item.Availability}");
                Console.Write("Is item available? (true/false): ");
                string availInput = Console.ReadLine();
                if (availInput.Equals("true", StringComparison.OrdinalIgnoreCase)) { avail = true; break; }
                if (availInput.Equals("false", StringComparison.OrdinalIgnoreCase)) { avail = false; break; }
                Console.WriteLine("Please enter true or false.");
            }
            item.SetAvailability(avail);
            Console.WriteLine("Availability updated.");
            break;
        }
        else
        {
            Console.WriteLine("Menu item not found. Please enter a valid item name.");
        }
    }
}

InitializeSampleData();

while (true)
{
    MainMenu();
    int choice;
    while (true) 
    {
        string input = Console.ReadLine();
        if (int.TryParse(input, out choice) && (choice == 0 || choice == 1 || choice == 2 || choice == 3)) //can we not tryparse, switch case, easier to understand //TODO:
            break;
        Console.WriteLine("Please enter a valid menu option (0-3).");
    }
    Console.WriteLine();
    if (choice == 0)
    {
        Console.WriteLine("Goodbye!");
        break;
    }
    else
    {
        switch (choice)
        {
            case 1:
                var user = LoginUser();
                if (user != null)
                    RunUserFeatures(user);
                break;
            case 2:
                RegisterUser();
                break;
            case 3:
                ViewAllRestaurants();
                Console.WriteLine("[R]eturn. Enter R to RETURN to MainMenu");
                Console.Write("Enter you choice: ");
                string input = Console.ReadLine();
                Console.WriteLine();
                break;
        }
    }
}
