using System;
using System.Runtime.Serialization;

namespace GitHubExtension.Activity.External.WebAPI.Exceptions
{
    class LinkHeaderFormatException : ApplicationException
    {
        public LinkHeaderFormatException()
            : base()
        {
        }

        public LinkHeaderFormatException(string message)
            : base(message)
        {
        }

        public LinkHeaderFormatException(string message, Exception inner)
            : base(message, inner)
        {
        }

        // A constructor is needed for serialization when an 
        // exception propagates from a remoting server to the client.  
        protected LinkHeaderFormatException(SerializationInfo info, StreamingContext context)
        {
        }
    }
}