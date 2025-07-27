using sdp_Assignment;

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
    Console.WriteLine("[0] Exit");
    Console.WriteLine();
    Console.Write("Enter your choice: ");
}

void RegisterUser()
{
    Console.WriteLine("What are you registering as?");
    Console.WriteLine("[1] Customer");
    Console.WriteLine("[2] Restaurant Owner");
    Console.WriteLine();
    int choice;
    while (true)
    {
        try
        {
            Console.Write("Enter your choice: ");
            choice = Convert.ToInt32(Console.ReadLine());
            if (choice < 0 || choice > 2)
            {
                throw new Exception();
            }
            break;
        }
        catch
        {
            Console.WriteLine("Invalid Option");
        }
    }
    Console.WriteLine();

    if (choice == 2)
    {
        Console.WriteLine("Registering as RESTAURANT OWNER");
        UserGenerator userGenerator = new RestaurantOwnerGenerator();
        User user = userGenerator.createUser();
    }
    else
    {
        Console.WriteLine("Registering as CUSTOMER");
        UserGenerator userGenerator = new CustomerGenerator();
        User user = userGenerator.createUser();
    }
}


RegisterUser();
//while (true)
//{
//    MainMenu();
//    int choice = Convert.ToInt32(Console.ReadLine());
//    switch (choice)
//    {
//        case 1:
//            Console.WriteLine();
//            break;
//        case 2:
//            RegisterUser();
//            break;
//    }
//}
