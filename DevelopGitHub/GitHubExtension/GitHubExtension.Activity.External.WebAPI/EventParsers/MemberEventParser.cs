using Newtonsoft.Json.Linq;

namespace GitHubExtension.Activity.External.WebAPI.EventParsers
{
    public class MemberEventParser : BaseEventParser
    {
        public override JToken ParseEvent(JToken eventToken)
        {
            JToken parsedEvent = InitialiseEventData(eventToken);

            parsedEvent["payload"]["action"] = eventToken["payload"]["action"];
            parsedEvent["payload"]["member"] = eventToken["payload"]["member"];

            return parsedEvent;
        }
    }
}
