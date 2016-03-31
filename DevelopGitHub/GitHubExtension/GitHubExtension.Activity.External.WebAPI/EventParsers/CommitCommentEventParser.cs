using Newtonsoft.Json.Linq;

namespace GitHubExtension.Activity.External.WebAPI.EventParsers
{
    class CommitCommentEventParser : BaseEventParser
    {
        public override JToken ParseEvent(JToken eventToken)
        {
            JToken parsedEvent = InitialiseEventData(eventToken);
            parsedEvent["payload"]["comment"] = ParseComment(eventToken);

            return parsedEvent;
        }
    }
}
