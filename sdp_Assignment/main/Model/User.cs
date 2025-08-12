using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_Assignment.main.Model
{
    public abstract class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        //public Restaurant Restaurant { get; set; }
    }
}
