using sdp_Assignment.main.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_Assignment.main.Factory
{
    internal abstract class UserGenerator
    {
        public abstract User createUser();
    }
}
