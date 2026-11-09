using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Exceptions
{
    public class InvalidDateException : Exception
    {
        public InvalidDateException():base("Invalid Date")
        {
            
        }
    }
}
