using Newtonsoft.Json.Linq;

namespace GitHubExtension.Activity.External.WebAPI.EventParsers
{
    class DeleteEventParser : BaseEventParser
    {
        public override JToken ParseEvent(JToken eventToken)
        {
            JToken parsedToken = InitialiseEventData(eventToken);

            parsedToken["payload"]["ref"] = eventToken["payload"]["ref"];
            parsedToken["payload"]["refType"] = eventToken["payload"]["ref_type"];

            return parsedToken;
        }
    }
}
