using Newtonsoft.Json.Linq;

namespace GitHubExtension.Activity.External.WebAPI.EventParsers
{
    class ForkEventParser : BaseEventParser
    {
        public override JToken ParseEvent(JToken eventToken)
        {
            JToken parsedEvent = InitialiseEventData(eventToken);
            parsedEvent["payload"]["forkie"] = eventToken["payload"]["forkie"];

            return parsedEvent;
        }
    }
}
