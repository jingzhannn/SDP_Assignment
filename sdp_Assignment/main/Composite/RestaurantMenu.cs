using sdp_Assignment.main.Iterator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//TODO:
//Line 13 : CS0118
//Line 49 : CS0103
//cant run without commenting entire class

namespace sdp_Assignment.main.Composite
{
    internal class RestaurantMenu : MenuComponent
    {
        private List<MenuComponent> components;
        private string name;
        private RestaurantMenuIterator iter = null;

        public override string Name { get { return name; } }

        public RestaurantMenu(string name)
        {
            this.name = name;
            components = new List<MenuComponent>();
        }

        public override void add(MenuComponent mc)
        {
            components.Add(mc);
        }

        public override void remove(MenuComponent mc)
        {
            components.Remove(mc);
        }

        public override MenuComponent getChild(int index)
        {
            return components[index];
        }
        public RestaurantMenuIterator createIterator()
        {
            return new RestaurantMenuIterator(components);
        }

        public override void print()
        {
            Console.WriteLine(Name.ToUpper());
            this.iter = createIterator();
            int index = 0;
            while (iter.hasNext())
            {
                MenuComponent menuComponent = (MenuComponent)iter.next();
                if (menuComponent is not MenuItem)
                {
                    Console.Write($"({index + 1}) ");
                }
                else
                {
                    Console.Write($"    {index + 1}. ");
                }
                
                menuComponent.print();
                index++;
            }
            this.iter = null;

            Console.WriteLine();
        }
    }
}
