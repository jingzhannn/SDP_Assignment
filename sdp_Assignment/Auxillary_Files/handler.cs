using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_Assignment.Auxillary_Files
{
    public static class Handler
    {
        // returnhandler
        /// This method handles the return functionality in the console application.
        /// basic y or n got it
        /// for MENUS only, cw only has reutrn to main menu, and staing on current page.
        public static bool MenuReturnHandler()
        {
            Console.WriteLine("Press 'Y' to return to the main menu, press 'N' to stay");
            while (true)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Y)
                {
                    Console.WriteLine("Returning to main menu...");
                    Console.ReadKey(true);
                    Console.Clear();
                    return true;
                }
                else if (key == ConsoleKey.N)
                {

                    Console.WriteLine("Staying on the current page...");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                    Console.Clear();
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid key pressed. Please press 'Y' or 'N'.");
                }
            }
        }



    }

}
