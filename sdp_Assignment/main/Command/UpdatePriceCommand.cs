using sdp_Assignment.main.Composite;

namespace sdp_Assignment.main.Command
{
    // updates price of a MenuItem
    internal class UpdatePriceCommand : ICommand
    {
        private MenuItem item; // Operates on leaf
        private double newPrice;

        //concrete
        public UpdatePriceCommand(MenuItem item, double newPrice)
        {
            this.item = item;
            this.newPrice = newPrice;
        }

        public void Execute()
        {
            // Calls public SetPrice method Menuitem.cs
            item.SetPrice(newPrice);

            // console feedback
            Console.WriteLine($"{item.Name} price updated to ${item.Price:N2}");
        }
    }
}
