using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_Assignment
{
    internal class Restaurant
    {
        public string Name { get; set; }

        private MenuComponent all_menus;

        public Restaurant(string name,MenuComponent mc)
        {
            Name = name;
            all_menus = mc;
        }

        public void addMenu(MenuComponent mc)
        {
            all_menus.add(mc);
        }

        public void printMenu()
        {
            Console.WriteLine($"--- {Name} ---");
            all_menus.print();
        }

        public MenuComponent GetMenuComponentByName(string name)
        {
            // Only search top-level menu items for simplicity
            if (all_menus is RestaurantMenu menu)
            {
                foreach (var component in menu.GetComponents())
                {
                    if (component.Name == name)
                        return component;
                }
            }
            return null;
        }

    }
}
