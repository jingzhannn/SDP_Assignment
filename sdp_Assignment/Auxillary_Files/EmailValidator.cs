using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_Assignment.Auxillary_Files
{

    //TODO: MORE ROBUST VERFICIATON
    internal class EmailValidator
    {
        public static bool IsValidEmail(string email)
        {
            return !string.IsNullOrWhiteSpace(email) &&
                   email.Contains("@") &&
                   email.Contains(".") &&
                   email.Length >= 6;
        }
    }
}
