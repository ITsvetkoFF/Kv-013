using Newtonsoft.Json.Linq;

namespace GitHubExtension.Activity.External.WebAPI.EventParsers
{
    class WatchEventParser : BaseEventParser
    {
        public override JToken ParseEvent(JToken eventToken)
        {
            JToken parsedEvent = InitialiseEventData(eventToken);

            parsedEvent["payload"]["action"] = eventToken["payload"]["action"];

            return parsedEvent;
        }
    }
}
