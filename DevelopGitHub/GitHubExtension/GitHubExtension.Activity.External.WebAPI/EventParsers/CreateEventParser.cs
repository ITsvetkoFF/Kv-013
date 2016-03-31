using Newtonsoft.Json.Linq;

namespace GitHubExtension.Activity.External.WebAPI.EventParsers
{
    class CreateEventParser : BaseEventParser
    {
        public override JToken ParseEvent(JToken eventToken)
        {
            JToken parsedToken = InitialiseEventData(eventToken);

            parsedToken["payload"]["ref"] = eventToken["payload"]["ref"];
            parsedToken["payload"]["refType"] = eventToken["payload"]["ref_type"];
            parsedToken["payload"]["masterBranch"] = eventToken["payload"]["master_branch"];
            parsedToken["payload"]["description"] = eventToken["payload"]["description"];
            parsedToken["payload"]["pusherType"] = eventToken["payload"]["pusher_type"];

            return parsedToken;
        }
    }
}
