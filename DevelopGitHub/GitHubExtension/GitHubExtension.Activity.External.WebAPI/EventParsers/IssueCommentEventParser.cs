using Newtonsoft.Json.Linq;

namespace GitHubExtension.Activity.External.WebAPI.EventParsers
{
    class IssueCommentEventParser : BaseEventParser
    {
        public override JToken ParseEvent(JToken eventToken)
        {
            JToken parsedEvent = InitialiseEventData(eventToken);
            parsedEvent["payload"]["action"] = eventToken["payload"]["action"];
            parsedEvent["payload"]["comment"] = ParseComment(eventToken);

            parsedEvent["payload"]["issue"] = new JObject();
            parsedEvent["payload"]["issue"]["htmlUrl"] = eventToken["payload"]["issue"]["html_url"];
            parsedEvent["payload"]["issue"]["url"] = eventToken["payload"]["issue"]["url"];
            parsedEvent["payload"]["issue"]["number"] = eventToken["payload"]["issue"]["number"];
            parsedEvent["payload"]["issue"]["title"] = eventToken["payload"]["issue"]["title"];
            parsedEvent["payload"]["issue"]["createdAt"] = eventToken["payload"]["issue"]["created_at"];
            parsedEvent["payload"]["issue"]["updatedAt"] = eventToken["payload"]["issue"]["updated_at"];
            parsedEvent["payload"]["issue"]["closedAt"] = eventToken["payload"]["issue"]["closed_at"];
            parsedEvent["payload"]["issue"]["comments"] = eventToken["payload"]["issue"]["comments"];

            return parsedEvent;
        }
    }
}
