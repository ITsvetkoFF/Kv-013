using System;

namespace GitHubExtension.Activity.External.WebAPI.Exceptions
{
    public class ParserNotFoundException : ApplicationException
    {
        public ParserNotFoundException() : base() { }
        public ParserNotFoundException(string message) : base(message) { }
        public ParserNotFoundException(string message, Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an 
        // exception propagates from a remoting server to the client.  
        protected ParserNotFoundException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
