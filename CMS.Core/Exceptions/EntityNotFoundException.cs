using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException():base("Item Not Found")
        {
            
        }
    }
}
