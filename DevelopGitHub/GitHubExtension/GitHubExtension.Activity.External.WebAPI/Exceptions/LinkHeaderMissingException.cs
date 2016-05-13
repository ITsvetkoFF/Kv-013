using System;
using System.Runtime.Serialization;

namespace GitHubExtension.Activity.External.WebAPI.Exceptions
{
    public class LinkHeaderMissingException : ApplicationException
    {
        public LinkHeaderMissingException()
            : base()
        {
        }

        public LinkHeaderMissingException(string message)
            : base(message)
        {
        }

        public LinkHeaderMissingException(string message, Exception inner)
            : base(message, inner)
        {
        }

        // A constructor is needed for serialization when an 
        // exception propagates from a remoting server to the client.  
        protected LinkHeaderMissingException(
            SerializationInfo info, 
            StreamingContext context)
        {
        }
    }
}