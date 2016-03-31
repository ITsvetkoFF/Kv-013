using Newtonsoft.Json.Linq;

namespace GitHubExtension.Activity.External.WebAPI.EventParsers
{
    class GolumEventParser : BaseEventParser
    {
        public override JToken ParseEvent(JToken eventToken)
        {
            JToken parsedToken = InitialiseEventData(eventToken);

            parsedToken["payload"]["pages"] = eventToken["payload"]["pages"];

            return parsedToken;
        }
    }
}
