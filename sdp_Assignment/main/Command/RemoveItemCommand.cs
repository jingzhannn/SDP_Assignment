using sdp_Assignment.main.Composite;

namespace sdp_Assignment.main.Command
{
    // removes a MenuComponent from a RestaurantMenu
    internal class RemoveItemCommand : ICommand
    {
        private RestaurantMenu menu;
        private MenuComponent item;

        //concrete
        public RemoveItemCommand(RestaurantMenu menu, MenuComponent item)
        {
            this.menu = menu;
            this.item = item;
        }

        public void Execute()
        {
            // Calls composite remove() method
            menu.remove(item);
            // print
            Console.WriteLine($"Removed '{item.Name}' from '{menu.Name}'");
        }
    }
}