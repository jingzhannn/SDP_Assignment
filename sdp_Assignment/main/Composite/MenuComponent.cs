using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_Assignment.main.Composite
{
    internal abstract class MenuComponent
    {
        public virtual string Name
        {
            get { throw new NotSupportedException(); }
        }
        public virtual bool Availability
        {
            get { throw new NotSupportedException(); }
        }
        public virtual double Price
        {
            get { throw new NotSupportedException(); }
        }

        public virtual void add(MenuComponent mc)
        {
            throw new NotSupportedException();
        }
        public virtual void remove(MenuComponent mc)
        {
            throw new NotSupportedException();
        }
        public virtual MenuComponent getChild(int index)
        {
            throw new NotSupportedException();
        }
        public virtual void print()
        {
            throw new NotSupportedException();
        }
        public virtual Iterator.Iterator createIterator()
        {
            throw new NotSupportedException();
        }
    }
}
