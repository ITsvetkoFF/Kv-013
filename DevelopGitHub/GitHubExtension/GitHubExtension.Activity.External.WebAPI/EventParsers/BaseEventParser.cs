using Newtonsoft.Json.Linq;

namespace GitHubExtension.Activity.External.WebAPI.EventParsers
{
    public abstract class BaseEventParser
    {
        public abstract JToken ParseEvent(JToken eventToken);

        protected JToken InitialiseEventData(JToken eventToken)
        {
            JToken parsedToken = new JObject();
            parsedToken["actor"] = eventToken["actor"];
            parsedToken["type"] = eventToken["type"];
            parsedToken["createdAt"] = eventToken["created_at"];
            parsedToken["payload"] = new JObject();

            return parsedToken;
        }

        protected JToken ParseComment(JToken eventToken)
        {
            JToken parsedToken = new JObject();

            parsedToken["url"] = eventToken["payload"]["comment"]["url"];
            parsedToken["html_url"] = eventToken["payload"]["comment"]["html_url"];
            parsedToken["body"] = eventToken["payload"]["comment"]["body"];
            parsedToken["created_at"] = eventToken["payload"]["comment"]["created_at"];
            parsedToken["updated_at"] = eventToken["payload"]["comment"]["updated_at"];

            return parsedToken;
        }
    }
}
