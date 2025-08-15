using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_Assignment.main.Composite
{
    internal class MenuItem : MenuComponent
    {
        private string name;
        private bool availability;
        private double price;

        public MenuItem(string name, bool availability, double price)
        {
            this.name = name;
            this.availability = availability;
            this.price = price;
        }


        public override string Name { get { return name; } }
        public override bool Availability { get { return availability; } }
        public override double Price { get { return price; } }
        public override void print()
        {
            Console.WriteLine($"{name} | ${price:N2} | Available: {availability}");
        }

        public void SetPrice(double newPrice)
        {
            price = newPrice;
        }
        public void SetAvailability(bool avail)
        {
            availability = avail;
        }

    }
}
