using sdp_Assignment.main.Iterator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// indiviual item in menu, attributes name, availblity, price
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

        public override IMenuIterator createIterator()
        {
            // MenuItem is a leaf, so no children; return an empty iterator
            return new RestaurantMenuIterator(new List<MenuComponent>());
        }

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
