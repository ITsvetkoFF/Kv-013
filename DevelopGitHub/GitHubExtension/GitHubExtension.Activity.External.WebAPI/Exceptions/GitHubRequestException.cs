using System;
using System.Runtime.Serialization;

namespace GitHubExtension.Activity.External.WebAPI.Exceptions
{
    public class GitHubRequestException : ApplicationException
    {
        public GitHubRequestException()
            : base()
        {
        }

        public GitHubRequestException(string message)
            : base(message)
        {
        }

        public GitHubRequestException(string message, Exception inner)
            : base(message, inner)
        {
        }

        // A constructor is needed for serialization when an 
        // exception propagates from a remoting server to the client.  
        protected GitHubRequestException(
            SerializationInfo info, 
            StreamingContext context)
        {
        }
    }
}