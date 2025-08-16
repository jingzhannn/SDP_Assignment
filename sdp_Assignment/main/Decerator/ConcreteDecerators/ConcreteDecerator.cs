using sdp_Assignment.main.Composite;

namespace sdp_Assignment.main.Decorator
{
    internal class ExtraChickenDecerator : MenuItemDecorator
    {
        public ExtraChickenDecerator(MenuItem item) : base(item) { }

        public override string Name => wrappedItem.Name + " + Chicken Decerator";
        public override double Price => wrappedItem.Price + 1.50; // add $1.50 for chicken

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
