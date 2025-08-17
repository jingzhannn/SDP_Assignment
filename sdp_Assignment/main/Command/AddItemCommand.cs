using sdp_Assignment.main.Composite;

namespace sdp_Assignment.main.Command
{
    // adds a MenuComponent to a RestaurantMenu
    internal class AddItemCommand : ICommand
    {
        private RestaurantMenu menu;  // Operates on composite
        private MenuComponent item;

        //concrete
        public AddItemCommand(RestaurantMenu menu, MenuComponent item)
        {
            this.menu = menu;
            this.item = item;
        }

        public void Execute()
        {
            // Calls composite add() method
            menu.add(item);
            Console.WriteLine($"Added '{item.Name}' to '{menu.Name}'");
        }
    }
}
