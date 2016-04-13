using System;

namespace GitHubExtension.Security.WebApi.Library.Exceptions
{
    public class UnsuccessfullGitHubRequestException : ApplicationException
    {
        public UnsuccessfullGitHubRequestException() : base() { }
        public UnsuccessfullGitHubRequestException(string message) : base(message) { }
        public UnsuccessfullGitHubRequestException(string message, Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an 
        // exception propagates from a remoting server to the client.  
        protected UnsuccessfullGitHubRequestException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}