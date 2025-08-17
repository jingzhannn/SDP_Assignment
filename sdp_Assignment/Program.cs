//INIT ALL INSIDE Program.cs 
//supposed to split using with whos doing it 
//auxillary files. 
using sdp_Assignment.Auxillary_Files;
using sdp_Assignment.main.Composite;
using sdp_Assignment.main.Factory;
using sdp_Assignment.main.Model;
using sdp_Assignment.Managers;

//ok so get this
//business-logic is still inside the program.cs
//also single responsiblity
//also open-closed principle
//also the while true loop
//TODO://
// consistency in GUI 
//TODO:// // refactor the code to use a more modular approach
// USING THE diagram, and align that with the diagram. 
// TODO:// Line 204



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
                Console.WriteLine("Invalid choice");
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
                    WaitForUserInput();
                }
                else
                {
                    Console.WriteLine("No restaurant found. Creating a new restaurant...");
                    CreateRestaurant(owner);
                }
                break;

            case "2": // Edit menu

                Restaurant myRestaurant = owner.restaurant; // Assuming owner.restaurant is of type Model.Restaurant
                if (myRestaurant != null)
                {
                    UpdateRestaurantMenu(myRestaurant);
                }
                else
                {
                    Console.WriteLine("No restaurant found. Please create a restaurant first.");
                    CreateRestaurant(owner);
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

void UpdateMenuItem(Restaurant myRestaurant)
{

    while (true)
    {
        Console.WriteLine($"\nEditing Menu for {myRestaurant.Name}");
        ConsoleUI.DisplayEditRestaurantMenu();

        string menuChoice = Console.ReadLine();

        switch (menuChoice)
        {
            case "1":
                myRestaurant.printMenu();
                break;
            case "2":
                myRestaurant.updateItem();
                break;
            case "3":
                myRestaurant.AddMenuOrItem();
                break;
            case "4":
                myRestaurant.DeleteMenuOrItem();
                break;
            case "0":
                Console.WriteLine("not implemented."); // break out of while
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }
}

void RunCustomerFeatures(Customer customer)
{
    bool isRunning = true;
    while (isRunning)
    {

        Console.WriteLine($"Hello: {customer.Username}");
        ConsoleUI.DisplayCustomerMenu();

        string choice = ReadNonEmptyInput("Enter your choice: ");
        switch (choice.ToUpper())
        {
            case "V":
                ViewAllRestaurants();
                WaitForUserInput();
                break;
            case "O":
                if (restaurants.Count == 0)
                {
                    Console.WriteLine("No restaurants available.");
                    break;
                }

                // Step 1: Let the customer pick a restaurant
                customer.SelectedRestaurant = RestaurantSelector.SelectRestaurant(restaurants);

                if (customer.SelectedRestaurant != null)
                {
                    CustomerManager cm = new CustomerManager(customer.SelectedRestaurant);
                    cm.Run();
                }
                else
                {
                    Console.WriteLine("No restaurant selected.");
                }
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

//update
void UpdateItemDetails(Restaurant restaurant)
{
    Console.WriteLine($"Updating menu for {restaurant.Name}");
    Console.WriteLine("Current Menu:");
    restaurant.printMenu();

    while (true)
    {
        string itemName = MenuItemNamePrompt();
        if (string.IsNullOrWhiteSpace(itemName))
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
                WaitForUserInput(); //requires input
                break;
            case "3": // View All Restaurants
                Console.WriteLine("viewing restraunts");
                Console.ReadKey(true); // Wait for user input before exiting
                ViewAllRestaurants();
                WaitForUserInput(); //requires input
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



////debug, ingore
//// 1. Create composite root (Menu)
//RestaurantMenu lunchMenu = new RestaurantMenu("Lunch Menu");

//MenuItem burger = new MenuItem("Burger", true, 5.0);

//// Add burger to menu via Command
//AddItemCommand addBurger = new AddItemCommand(lunchMenu, burger);
//addBurger.Execute();

//// Print before changes
//Console.WriteLine("\n--- BEFORE BURGER CHANGES ---");
//lunchMenu.print();

//// Update price via Command
//UpdatePriceCommand updateBurgerPrice = new UpdatePriceCommand(burger, 7.0);
//updateBurgerPrice.Execute();

//// Change availability via Command
//ChangeAvailabilityCommand updateBurgerAvail = new ChangeAvailabilityCommand(burger, false);
//updateBurgerAvail.Execute();

//// Print after base modifications
//Console.WriteLine("\n--- AFTER BURGER CHANGES ---");
//lunchMenu.print();

//// Apply Decorators (extra topping + discount)
//var cheeseBurger = new ExtraToppingDecorator(burger, "Cheese", 1.50);
//cheeseBurger.print(); // show decorator details

//var promoCheeseBurger = new DiscountDecorator(cheeseBurger, 0.20);
//lunchMenu.add(promoCheeseBurger);

//// Print with decorated burger
//Console.WriteLine("\n--- AFTER BURGER DECORATORS ---");
//lunchMenu.print();


//MenuItem pizza = new MenuItem("Pizza", true, 10.0);

//// Add pizza to menu
//AddItemCommand addPizza = new AddItemCommand(lunchMenu, pizza);
//addPizza.Execute();

//// Update price
//UpdatePriceCommand updatePizzaPrice = new UpdatePriceCommand(pizza, 12.0);
//updatePizzaPrice.Execute();

//// Apply topping decorator only
//var pizzaWithOlives = new ExtraToppingDecorator(pizza, "Olives", 2.00);
//lunchMenu.add(pizzaWithOlives);

//// Print menu with pizza
//Console.WriteLine("\n--- AFTER PIZZA DECORATOR ---");
//lunchMenu.print();


//MenuItem pasta = new MenuItem("Pasta", true, 8.0);

//// Add pasta only
//AddItemCommand addPasta = new AddItemCommand(lunchMenu, pasta);
//addPasta.Execute();

//// Print menu with pasta
//Console.WriteLine("\n--- AFTER ADDING PASTA ---");
//lunchMenu.print();


//Console.WriteLine("\n======= FINAL LUNCH MENU =======");
//lunchMenu.print();
////debug


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
    var menu0 = new RestaurantMenu("Main Menu 1");

    var menu1 = new RestaurantMenu("Main Course");
    menu1.add(new MenuItem("Chicken Rice", true, 3.5));
    menu1.add(new MenuItem("Nasi Lemak", true, 4.0));

    var menu1_2 = new RestaurantMenu("Dessert Menu");
    menu1_2.add(new MenuItem("Ice Cream", true, 2.0));
    menu1_2.add(new MenuItem("Cake", true, 3.0));

    menu0.add(menu1);
    menu0.add(menu1_2);

    var menu2 = new RestaurantMenu("Main Menu");
    menu2.add(new MenuItem("Burger", true, 5.5));
    menu2.add(new MenuItem("Fries", true, 2.5));

    // Sample Restaurants
    var restaurant1 = new Restaurant("Haaker", menu0);
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
}//initalise, sample data should be pointed to subclasses
InitializeSampleData(); // Popluate
LoginHandler(); // Main entry point of the application