using Newtonsoft.Json.Linq;

namespace GitHubExtension.Activity.External.WebAPI.EventParsers
{
    class PushEventParser : BaseEventParser
    {
        public override JToken ParseEvent(JToken eventToken)
        {
            JToken parsedToken = InitialiseEventData(eventToken);

            parsedToken["payload"]["size"] = parsedToken["payload"]["size"];
            parsedToken["payload"]["distinctSize"] = parsedToken["payload"]["distinct_size"];
            parsedToken["payload"]["ref"] = parsedToken["payload"]["ref"];

            return parsedToken;
        }
    }
}
