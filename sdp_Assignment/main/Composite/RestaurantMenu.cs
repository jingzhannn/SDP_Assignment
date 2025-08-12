using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_Assignment.main.Composite
{
    internal class RestaurantMenu : MenuComponent
    {
        private List<MenuComponent> components;
        private string name;

        public string Name { get { return name; } }

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
        //public Iterator createIterator()
        //{
        //    return new RestaurantMenuIterator(components);
        //}

        public override void print()
        {
            Console.WriteLine(Name.ToUpper());
            int itemNumber = 1;
            foreach (var component in components)
            {
                if (component is RestaurantMenu submenu)
                {
                    Console.WriteLine($"- {submenu.Name}");
                    int subItemNumber = 1;
                    foreach (var subComponent in submenu.GetComponents())
                    {
                        Console.Write($"  {subItemNumber}. ");
                        subComponent.print();
                        Console.WriteLine();
                        subItemNumber++;
                    }
                }
                else
                {
                    Console.Write($"{itemNumber}. ");
                    component.print();
                    Console.WriteLine();
                    itemNumber++;
                }
            }
            Console.WriteLine();
        }

        public List<MenuComponent> GetComponents()
        {
            return components;
        }

    }
}
