using System.Collections.Generic;
using GitHubExtension.Activity.External.WebAPI.EventParsers;
using GitHubExtension.Activity.External.WebAPI.Exceptions;
using Newtonsoft.Json.Linq;

namespace GitHubExtension.Activity.External.WebAPI.Services
{
    public static class EventParserHelper
    {
        #region Event type names
        public const string CommitCommentEvent = "CommitCommentEvent";
        public const string CreateEvent = "CreateEvent";
        public const string DeleteEvent = "DeleteEvent";
        public const string ForkEvent = "ForkEvent";
        public const string GolumEvent = "GolumEvent";
        public const string IssueCommentEvent = "IssueCommentEvent";
        public const string IssuesEvent = "IssuesEvent";
        public const string MemeberEvent = "MemeberEvent";
        public const string PullRequestEvent = "PullRequestEvent";
        public const string PullRequestReviewCommentEvent = "PullRequestReviewCommentEvent";
        public const string PushEvent = "PushEvent";
        public const string RealiseEvent = "RealiseEvent";
        public const string WatchEvent = "WatchEvent";
        #endregion

        private static readonly Dictionary<string, BaseEventParser> Parsers = new Dictionary<string, BaseEventParser>()
        {
            //Registering parsers for event types
            { CommitCommentEvent, new CommitCommentEventParser()},
            { CreateEvent, new CreateEventParser()},
            { DeleteEvent, new DeleteEventParser()},
            { ForkEvent, new ForkEventParser()},
            { GolumEvent, new GolumEventParser()},
            { IssueCommentEvent, new IssueCommentEventParser()},
            { IssuesEvent, new IssuesEventParser() },
            { MemeberEvent, new MemberEventParser() },
            { PullRequestEvent, new PullRequestEventParser() },
            { PullRequestReviewCommentEvent, new PullRequestReviewCommentEventParser() },
            { PushEvent, new PushEventParser() },
            { RealiseEvent, new RealeaseEventParser() },
            { WatchEvent, new WatchEventParser() },
        }; 
        
        public static JToken ParseEvent(this JToken eventToken)
        {
            JToken parsedToken;
            string eventType = eventToken.Value<string>("type");
            if (Parsers.ContainsKey(eventType))
                parsedToken = Parsers[eventType].ParseEvent(eventToken);
            else
                throw new ParserNotFoundException();

            return parsedToken;
        }
    }
}