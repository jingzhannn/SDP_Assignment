using sdp_Assignment;
using System.Xml;
//ok so get this
//business-logic is still inside the program.cs
//also single responsiblity
//also open-closed principle
//also the while true loop
//TODO://




List<User> users = new List<User>();
List<Restaurant> restaurants = new List<Restaurant>();


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

User UserLogin()
{
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
}


// Function to get the user's choice from the main menu
int GetMainMenuChoice()
{
    while (true)
    {
        if (int.TryParse(Console.ReadLine()?.Trim(), out int choice) && choice >= 0 && choice <= 3)
        {
            return choice;
        }
        else
        {
            Console.WriteLine("Please enter a valid menu option.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            Console.Clear();
            Loginmenu();
        }
    }
}

void ReturnHandler()
{
    Console.WriteLine("Press 'Y' to return to the main menu, press 'N' to stay");
    var key = Console.ReadKey(true).Key;

    if (key == ConsoleKey.Y)
    {
        Console.WriteLine("returning to main menu");
        Console.ReadKey(true); // Clear the key press
        Console.Clear();
        Loginmenu();
    }
    else if (key == ConsoleKey.N)
    {
        Console.WriteLine("Staying on the current page...");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true); // Clear the key press
    }
    else
    {
        Console.WriteLine("Invalid key pressed. Returning to main menu.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true); // Clear the key press
    }
}


// wait for user input before proceeding
void WaitForUserInput()
{
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
    Console.WriteLine();
}


//Front-end Display
void Loginmenu()
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

// Login Handler Switch Case
void LoginHandler()
{

    Loginmenu();//first display
    
    while (true)
    {
        int choice = GetMainMenuChoice();
        switch (choice)
        {
            case 1: // Login user
                    // TODO: Make it better :)
                var user = UserLogin();
                if (user != null)
                    RunUserFeatures(user);
                break;
            case 2: // Register user
                Console.WriteLine("registering user");
                Console.ReadKey(true); // Wait for user input before exiting
                RegisterUser();
                WaitForUserInput();
                break;
            case 3: // View All Restaurants
                Console.WriteLine("viewing restraunts");
                Console.ReadKey(true); // Wait for user input before exiting
                ViewAllRestaurants();
                WaitForUserInput();
                //was a r to return scafoold code here
                break;
            case 0: // Exit
                Console.WriteLine("Exiting the application...");
                Console.ReadKey(true); // Wait for user input before exiting
                Environment.Exit(0);
                break;
        }
        Loginmenu();
    }
}

//Username = "owner2",
//Password = "password2",
//Email = "owner2@email.com"


//Username = "customer1",
//Password = "password1",
//Email = "customer1@email.com"


InitializeSampleData(); // Popluate
LoginHandler(); // Main entry point of the application