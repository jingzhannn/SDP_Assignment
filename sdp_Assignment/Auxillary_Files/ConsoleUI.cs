using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_Assignment.Auxillary_Files
{
    //all user interfaces should be here
    //this class is used to display the login menu and other user interfaces
    //its static, so can be called without creating an instance
    //pls use ConsoleUI.name() to display the login menu
    public static class ConsoleUI
    {
        // graberoo login menu
        public static void DisplayLoginMenu()
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
        public static void DisplayRestaurantOwnerMenu()
        {
            Console.WriteLine("[1] View Own Restaurant");
            Console.WriteLine("[2] Update Own Restaurant Items");
            Console.WriteLine("[0] Logout");
            Console.WriteLine();
        }


        public static void DisplayCustomerMenu()
        {
            Console.WriteLine("[V] View All Restaurants");
            Console.WriteLine("[L] Logout");
        }

        public static void DisplayRegisterMenu()
        {
            Console.WriteLine("What are you registering as?");
            Console.WriteLine("[1] Customer");
            Console.WriteLine("[2] Restaurant Owner");
            Console.WriteLine();
        }
    }




}
