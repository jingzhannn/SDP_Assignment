using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sdp_Assignment.Auxillary_Files
{

    // factored out null or whitespace check
    // idk why i did this
    public static class ValidationUtils
    {
        public static bool NullOrWhiteSpace(string? input)
        {
            return string.IsNullOrWhiteSpace(input);
        }
    }


}
