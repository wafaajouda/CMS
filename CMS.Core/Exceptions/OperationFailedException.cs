using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Exceptions
{
    public class OperationFailedException : Exception
    {
        public OperationFailedException():base("Operation Failed")
        {
            
        }
    }
}
