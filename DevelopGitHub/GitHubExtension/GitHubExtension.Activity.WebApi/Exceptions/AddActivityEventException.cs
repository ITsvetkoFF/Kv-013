using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubExtension.Activity.Internal.WebApi.Exceptions
{
    public class AddActivityEventException : ApplicationException
    {
        public AddActivityEventException() : base() { }
        public AddActivityEventException(string message) : base(message) { }
        public AddActivityEventException(string message, Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an 
        // exception propagates from a remoting server to the client.  
        protected AddActivityEventException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
