using sdp_Assignment;
using sdp_Assignment.Auxillary_Files;
//ok so get this
//business-logic is still inside the program.cs
//also single responsiblity
//also open-closed principle
//also the while true loop
//TODO://
// consistency in GUI 
//TODO:// // refactor the code to use a more modular approach
// USING THE diagram, and align that with the diagram. 




List<User> users = new List<User>();
List<Restaurant> restaurants = new List<Restaurant>();


void RegisterUser()
{
    User user;
    bool isValidChoice = false;

    while (!isValidChoice)
    {
        ConsoleUI.DisplayRegisterMenu();
        Console.Write("Enter your choice: ");
        string? input = Console.ReadLine()?.Trim();

        switch (input)
        {
            case "1":
                Console.WriteLine("Registering as CUSTOMER");
                user = new CustomerGenerator().createUser(); //create
                isValidChoice = true;
                break;
            case "2":
                Console.WriteLine("Registering as RESTAURANT OWNER");
                user = new RestaurantOwnerGenerator().createUser(); //create
                isValidChoice = true;
                break;
            default:
                Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                continue; // go back to menu
        }

        // Add the user after creation
        users.Add(user);
        Console.WriteLine($"Welcome to Grabberoo {user.Username}");
        Console.WriteLine("Redirecting...");
        Console.ReadKey();
        Console.Clear();
        RunUserFeatures(user);
    }
}

//should be called inside other classes
void RunUserFeatures(User user)
{
    if (user is RestaurantOwner owner)
    {
        RunRestaurantOwnerFeatures(owner);
    }
    else if (user is Customer customer)
    {
        RunCustomerFeatures(customer);
    }
    else
    {
        Console.WriteLine("Unknown user type. Returning to main menu.");
    }
}

string ReadNonEmptyInput(string prompt)
{
    string input;
    do
    {
        Console.Write(prompt);
        input = Console.ReadLine()?.Trim() ?? "";
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Input cannot be empty.");
        }
    } while (string.IsNullOrWhiteSpace(input));
    return input;

}
void RunRestaurantOwnerFeatures(RestaurantOwner owner)
{
    bool isRunning = true;
    while (isRunning)
    {
        ConsoleUI.DisplayRestaurantOwnerMenu();
        
        string choice = ReadNonEmptyInput("Enter your choice: ");
        switch (choice)
        {
            case "1":
                if (owner.restaurant != null)
                {
                    Console.WriteLine($"Restaurant: {owner.restaurant.Name}");
                    owner.restaurant.printMenu();
                }
                else
                {
                    Console.WriteLine("No restaurant found.");
                }
                break;

            case "2":
                if (owner.restaurant != null)
                {
                    UpdateRestaurantMenu(owner.restaurant);
                }
                else
                {
                    Console.WriteLine("No restaurant found.");
                }
                break;

            case "0":
                Console.WriteLine("Logging out...");
                isRunning = false;
                break;

            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
        Console.WriteLine();
    }
}
void RunCustomerFeatures(Customer customer)
{
    bool isRunning = true;
    while (isRunning)
    {

        Console.WriteLine("Hello:", customer.Username);
        ConsoleUI.DisplayCustomerMenu();

        string choice = ReadNonEmptyInput("Enter your choice: ");
        switch (choice.ToUpper())
        {
            case "V":
                ViewAllRestaurants();
                break;

            case "L":
                Console.WriteLine("Logging out...");
                isRunning = false;
                break;

            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
        Console.WriteLine();
    }
}


//classes
void CreateRestaurant(RestaurantOwner owner)
{
    owner.createRestaurant(restaurants);
}//create
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
}//read
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
}//initalise
//update
void UpdateRestaurantMenu(Restaurant restaurant)
{
    Console.WriteLine($"Updating menu for {restaurant.Name}");
    Console.WriteLine("Current Menu:");
    restaurant.printMenu();


    while (true)
    {
        string itemName = MenuItemNamePrompt();
        if (ValidationUtils.NullOrWhiteSpace(itemName))
            return;

        var menuComponent = restaurant.GetMenuComponentByName(itemName);
        if (menuComponent is not MenuItem item)
        {
            Console.WriteLine("Menu item not found. Please enter a valid item name.");
            continue;
        }

        double newPrice = PromptForNewPrice(item);
        item.SetPrice(newPrice);
        Console.WriteLine("Price updated.");

        bool avail = PromptForAvailability(item);
        item.SetAvailability(avail);
        Console.WriteLine("Availability updated.");

        Console.WriteLine("Update another item? (Y/N)");
        var key = Console.ReadKey(true).Key;
        if (key != ConsoleKey.Y)
            break;
    }
}
//classes



//dont need hard code user, just push using RunUserFeatures



string MenuItemNamePrompt()
{
    Console.WriteLine("Enter the name of the item to update (or leave blank to exit): ");
    return Console.ReadLine()?.Trim() ?? "";
}
double PromptForNewPrice(MenuItem item)
{
    while (true)
    {
        Console.WriteLine($"Current price: {item.Price}");
        Console.Write("Enter new price: ");
        string input = Console.ReadLine()?.Trim() ?? "";
        if (double.TryParse(input, out double newPrice) && newPrice >= 0)
            return newPrice;
        Console.WriteLine("Please enter a valid positive number.");
    }
}

bool PromptForAvailability(MenuItem item)
{
    while (true)
    {
        Console.WriteLine($"Current availability: {item.Availability}");
        Console.Write("Is item available? (true/false): ");
        string input = Console.ReadLine()?.Trim() ?? "";
        if (input.Equals("true", StringComparison.OrdinalIgnoreCase)) return true;
        if (input.Equals("false", StringComparison.OrdinalIgnoreCase)) return false;
        Console.WriteLine("Please enter true or false.");
    }
}

User? UserLogin()
{
    Console.Write("Enter your Username: ");
    string? username = Console.ReadLine()?.Trim();

    Console.Write("Enter your Password: ");
    string? password = Console.ReadLine()?.Trim();

    //handle blank input
    if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
    {
        Console.WriteLine("Username and password cannot be empty.");
        return null;
    }

    // Check if user type = RestaurantOwner
    var owner = users.OfType<RestaurantOwner>().FirstOrDefault(o => o.Username == username && o.Password == password); //scope: users.subclass (RestaurantOwner)
    if (owner != null)
        return owner;

    // Check Users (entire), scope
    var customer = users.FirstOrDefault(c => c.Username == username && c.Password == password); //scope: ALL users
    if (customer != null)
        return customer;

    Console.WriteLine("Login failed. Invalid credentials.");
    return null;
}

// handle split between user and owner
void LoginAccessHandler()
{
    User? loggedInUser = UserLogin();

    if (loggedInUser == null)
    {
        Console.WriteLine("Login failed. Returning to main menu.");
        return;
    }

    Console.Clear();
    Console.WriteLine($"Welcome {loggedInUser.Username}!");
    Console.WriteLine();

    if (loggedInUser is RestaurantOwner owner)
    {
        RunRestaurantOwnerFeatures(owner);
    }
    else if (loggedInUser is Customer customer)
    {
        RunCustomerFeatures(customer);
    }
}
//login access handler




// Login Handler Switch Case
void LoginHandler()
{
    ConsoleUI.DisplayLoginMenu();//first display
    while (true)
    {
        string choice = Console.ReadLine()?.Trim();//input

        switch (choice)
        {
            case "1": // Login user
                Console.WriteLine("login user");
                Console.ReadKey(true); // Wait for user input before exiting
                LoginAccessHandler();
                break;
            case "2": // Register user
                Console.WriteLine("registering user");
                Console.ReadKey(true); // Wait for user input before exiting
                RegisterUser();
                WaitForUserInput();
                break;
            case "3": // View All Restaurants
                Console.WriteLine("viewing restraunts");
                Console.ReadKey(true); // Wait for user input before exiting
                ViewAllRestaurants();
                WaitForUserInput();
                //was a r to return scafoold code here
                break;
            case "0": // Exit
                Console.WriteLine("Exiting the application...");
                Console.ReadKey(true); // Wait for user input before exiting
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
        ConsoleUI.DisplayLoginMenu();
    }
}


// wait for user input before proceeding
void WaitForUserInput()
{
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
    Console.WriteLine();
}



//Username = "owner2",
//Password = "password2",
//Email = "owner2@email.com"


//Username = "customer1",
//Password = "password1",
//Email = "customer1@email.com"


InitializeSampleData(); // Popluate
LoginHandler(); // Main entry point of the application