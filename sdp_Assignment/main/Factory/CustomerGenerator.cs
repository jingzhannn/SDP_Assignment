using sdp_Assignment.Auxillary_Files;
using sdp_Assignment.main.Model;

namespace sdp_Assignment.main.Factory
{
    //TODO: PLEASE REGEX VERFIFICATION NAME AND PASSWORD THANKS, and check if the NAME already exists
    internal class CustomerGenerator : UserGenerator
    {
        public override User createUser()
        {
            User user = new Customer();
            while (true)
            {
                Console.Write("Enter your EMAIL: ");
                string email = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(email))
                {
                    Console.WriteLine("Email cannot be empty");
                    Console.WriteLine();
                    continue;
                }

                if (!EmailValidator.IsValidEmail(email))
                {
                    Console.WriteLine("Invalid email format. Try again.");
                    Console.WriteLine();
                    continue;
                }

                user.Email = email;
                break;
            }
            while (true)
            {
                Console.Write("Enter your USERNAME: ");
                string username = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(username))
                {
                    Console.WriteLine("Username cannot be empty");
                    Console.WriteLine();
                    continue;
                }

                user.Username = username;
                break;
            }

            while (true)
            {
                Console.Write("Enter your PASSWORD: ");
                string password = Console.ReadLine().Trim();

                if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
                {
                    Console.WriteLine("Password must be at least 6 characters long.");
                    Console.WriteLine();
                    continue;
                }

                Console.Write("CONFIRM Password: ");
                string confirmPassword = Console.ReadLine().Trim();

                if (password != confirmPassword)
                {
                    Console.WriteLine("Passwords do not match. Try Again.");
                    Console.WriteLine();
                    continue;
                }

                user.Password = password;
                break;
            }
            return user;
        }
    }
}
