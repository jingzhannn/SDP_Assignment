using sdp_Assignment.main.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_Assignment.main.Decerator
{
    //cannot be called from outside the namespace
    internal abstract class MenuDecorator : MenuComponent //calling composite, name, availblity, price
    {
        protected MenuComponent menuComponent; // The wrapped component

        public MenuDecorator(MenuComponent menuComponent)
        {
            this.menuComponent = menuComponent;
        }

        public override string Name => menuComponent.Name;
        public override bool Availability => menuComponent.Availability;
        public override double Price => menuComponent.Price;

        public override void print()
        {
            menuComponent.print();
        }
    }
}
