using System;

namespace GitHubExtension.Security.WebApi.Library.Exceptions
{
    public class UnsuccessfullRequestException : ApplicationException
    {
        public UnsuccessfullRequestException() : base() { }
        public UnsuccessfullRequestException(string message) : base(message) { }
        public UnsuccessfullRequestException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an 
        // exception propagates from a remoting server to the client.  
        protected UnsuccessfullRequestException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }

    }
}