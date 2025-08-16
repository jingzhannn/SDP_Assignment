using sdp_Assignment.main.Composite;
using sdp_Assignment.main.Decerator;


namespace sdp_Assignment.main.Decorator
{
    internal class DiscountDecorator : MenuDecorator
    {
        private double discountPercentage;

        public DiscountDecorator(MenuComponent menuComponent, double discountPercentage)
            : base(menuComponent)
        {
            this.discountPercentage = discountPercentage;
        }

        public override double Price => base.Price * (1 - discountPercentage);

        public override void print()
        {
            Console.WriteLine($"{Name} (Discount {discountPercentage * 100}%) | ${Price:N2} | Available: {Availability}");
        }
    }
}