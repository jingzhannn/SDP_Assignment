using sdp_Assignment.main.Composite;

namespace sdp_Assignment.main.Decorator
{
    internal class ExtraChickenDecorator : MenuItemDecorator
    {
        public ExtraChickenDecorator(MenuItem item) : base(item) { }

        public override string Name => wrappedItem.Name + " + Extra Chicken";
        public override double Price => wrappedItem.Price + 2.50; // add $2.50 for extra chicken

        public override void print()
        {
            Console.WriteLine($"{Name} - ${Price:N2} | Available: {Availability}");
        }
    }

    internal class LargeSizeDecorator : MenuItemDecorator
    {
        public LargeSizeDecorator(MenuItem item) : base(item) { }

        public override string Name => "Large " + wrappedItem.Name;
        public override double Price => wrappedItem.Price * 1.5; // 50% more for large
        public override void print()
        {
            Console.WriteLine($"{Name} - ${Price:N2} | Available: {Availability}");
        }
    }
}
