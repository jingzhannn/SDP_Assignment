using sdp_Assignment.main.Composite;

namespace sdp_Assignment.main.Decerator
{
    internal class ExtraToppingDecorator : MenuDecorator
    {
        private string topping;
        private double toppingPrice;

        public ExtraToppingDecorator(MenuComponent menuComponent, string topping, double toppingPrice)
            : base(menuComponent)
        {
            this.topping = topping;
            this.toppingPrice = toppingPrice;
        }

        public override double Price => base.Price + toppingPrice;

        public override void print()
        {
            Console.WriteLine($"{Name} + {topping} | ${Price:N2} | Available: {Availability}");
        }
    }
}
