using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.Exceptions
{
    public class EmailAlreadyExists : Exception
    {
        public EmailAlreadyExists()
        : base(String.Format("User Email Already Exists"))
        {

        }
    }
}
