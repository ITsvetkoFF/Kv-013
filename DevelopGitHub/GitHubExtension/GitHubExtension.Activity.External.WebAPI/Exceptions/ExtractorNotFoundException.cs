using System;

namespace GitHubExtension.Activity.External.WebAPI.Exceptions
{
    public class ExtractorNotFoundException : ApplicationException
    {
        public ExtractorNotFoundException() : base() { }
        public ExtractorNotFoundException(string message) : base(message) { }
        public ExtractorNotFoundException(string message, Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an 
        // exception propagates from a remoting server to the client.  
        protected ExtractorNotFoundException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
