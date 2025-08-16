using sdp_Assignment.main.Composite;
using System;

namespace sdp_Assignment.main.Decorator
{
    // Abstract decorator inherits from MenuComponent
    internal abstract class MenuItemDecorator : MenuComponent
    {
        protected MenuItem wrappedItem;

        public MenuItemDecorator(MenuItem item)
        {
            wrappedItem = item;
        }

        // Override Name, Price, Availability by default forwarding to wrapped item
        public override string Name => wrappedItem.Name;
        public override double Price => wrappedItem.Price;
        public override bool Availability => wrappedItem.Availability;

        public override void print()
        {
            wrappedItem.print();
        }
    }
}
