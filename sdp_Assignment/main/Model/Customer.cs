using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_Assignment.main.Model
{
    internal class Customer : User
    {
        // Add this property to allow selection of a restaurant
        public Restaurant? SelectedRestaurant { get; set; }
    }
}
