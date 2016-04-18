namespace GitHubExtension.Activity.External.WebAPI.Exceptions
{
    using System;

    public class GitHubRequestException : ApplicationException
    {
        public GitHubRequestException() : base() { }
        public GitHubRequestException(string message) : base(message) { }
        public GitHubRequestException(string message, Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an 
        // exception propagates from a remoting server to the client.  
        protected GitHubRequestException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
