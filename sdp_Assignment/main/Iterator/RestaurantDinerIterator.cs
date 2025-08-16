using sdp_Assignment.main.Composite;

namespace sdp_Assignment.main.Iterator
{
    internal class RestaurantMenuIterator : Iterator
    {
        private List<MenuComponent> myMenu;
        private int position = 0;

        public RestaurantMenuIterator(List<MenuComponent> menu)
        {
            myMenu = menu;
        }

        public bool hasNext()
        {
            return position < myMenu.Count;
        }

        public object next() { return myMenu[position++]; }

    }
}
