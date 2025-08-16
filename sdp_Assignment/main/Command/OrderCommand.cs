using sdp_Assignment.main.Composite;
using sdp_Assignment.main.Model;
using System;

namespace sdp_Assignment.main.Command
{
    internal class OrderCommand
    {
        private Restaurant restaurant;

        public OrderCommand(Restaurant r)
        {
            restaurant = r;
        }

        public void Execute()
        {
            // Placeholder for real order logic
            Console.WriteLine($"Order is being processed for {restaurant.Name}...");
        }
    }
}
