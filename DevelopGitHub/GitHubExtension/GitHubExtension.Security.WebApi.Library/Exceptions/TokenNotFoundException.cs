using System;

namespace GitHubExtension.Security.WebApi.Library.Exceptions
{
    public class TokenNotFoundException : ApplicationException
    {
        public TokenNotFoundException() : base() { }
        public TokenNotFoundException(string message) : base(message) { }
        public TokenNotFoundException(string message, Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an 
        // exception propagates from a remoting server to the client.  
        protected TokenNotFoundException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
