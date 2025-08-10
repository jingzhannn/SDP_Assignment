using sdp_Assignment;

List<User> users = new List<User>();
List<Restaurant> restaurants = new List<Restaurant>();

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
}

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
    Console.WriteLine();
    
    return loggedInUser;
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
        Console.WriteLine();
    }
}

void UpdateRestaurantMenu(Restaurant restaurant)
{
    Console.WriteLine("What do you want to do?");
    Console.WriteLine("1. Add Menu or Item");
    Console.WriteLine("2. Delete Menu or Item");
    Console.WriteLine("3. Update Item");
    Console.WriteLine("0. Return to Owner Menu");
    Console.WriteLine();

    string choice;
    while (true)
    {
        Console.Write("Enter your choice: ");
        choice = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(choice)) break;
        Console.WriteLine("Choice cannot be empty.");
    }
    Console.WriteLine();
    switch (choice)
    {
        case "1":
            restaurant.AddMenuOrItem();
            break;
        case "2":
            restaurant.DeleteMenuOrItem();
            break;
        case "3":
            restaurant.updateItem();
            break;
        case "0":
            Console.WriteLine("Returning to Owner Menu...");
            return;
        default:
            Console.WriteLine("Invalid choice. Please try again.");
            return;
    }
    ReturnToMenu();
}

void RunUserFeatures(User user)
{
    while (true)
    {
        Console.WriteLine($"Welcome {user.Username}!");
        Console.WriteLine();
        if (user is RestaurantOwner owner)
        {
            Console.WriteLine("[1] View Own Restaurant");
            Console.WriteLine("[2] Update Own Restaurant Items");
            Console.WriteLine("[0] Logout");
            Console.WriteLine();

            string ownerChoice;
            while (true)
            {
                Console.Write("Enter your choice: ");
                ownerChoice = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(ownerChoice)) break;
                Console.WriteLine("Choice cannot be empty.");
            }
            Console.WriteLine();
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
                ReturnToMenu();
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
        else if (user is Customer)
        {
            ViewAllRestaurants();
            Console.WriteLine("[L]ogout. Enter L to LOGOUT");
            Console.Write("Enter your choice: ");
            string input = Console.ReadLine();
            Console.WriteLine();
            break;
        }
    }
}

void ReturnToMenu()
{
    while (true)
    {
        Console.WriteLine("[R]eturn to Menu. Enter R to RETURN");
        Console.Write("Enter your choice: ");
        string input = Console.ReadLine();
        if (input?.ToUpper() == "R")
        {
            Console.WriteLine();
            break;
        }
        else
        {
            Console.WriteLine("Invalid choice. Please enter R to return.");
            Console.WriteLine();
        }
    }
}

InitializeSampleData();

////Automatically log in as the first user (e.g., owner1)
//User testUser = users.First(); // Or specify: users.First(u => u.Username == "owner1");
//RunUserFeatures(testUser);

while (true)
{
    MainMenu();
    int choice;
    while (true)
    {
        string input = Console.ReadLine();
        if (int.TryParse(input, out choice) && (choice == 0 || choice == 1 || choice == 2 || choice == 3))
            break;
        Console.WriteLine("Please enter a Valid Menu Option");
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
                {
                    RunUserFeatures(user);
                }               
                break;
            case 2:
                RegisterUser();
                break;
            case 3:
                ViewAllRestaurants();
                
                break;
        }
    }
}
