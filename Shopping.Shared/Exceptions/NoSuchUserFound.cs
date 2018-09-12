using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.Exceptions
{
    public class NoSuchUserFound :Exception
    {
        public NoSuchUserFound () : base(String.Format("No Such User Found!"))
        {

        }
    }
}
