using System;
using System.Runtime.Serialization;

namespace GitHubExtension.Security.WebApi.Exceptions
{
    public class TokenNotFoundException : ApplicationException
    {
        public TokenNotFoundException()
            : base()
        {
        }

        public TokenNotFoundException(string message)
            : base(message)
        {
        }

        public TokenNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

        // A constructor is needed for serialization when an 
        // exception propagates from a remoting server to the client.  
        protected TokenNotFoundException(
            SerializationInfo info, 
            StreamingContext context)
        {
        }
    }
}