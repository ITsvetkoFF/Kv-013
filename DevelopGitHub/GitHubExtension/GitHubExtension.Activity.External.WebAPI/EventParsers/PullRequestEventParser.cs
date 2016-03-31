using Newtonsoft.Json.Linq;

namespace GitHubExtension.Activity.External.WebAPI.EventParsers
{
    class PullRequestEventParser : BaseEventParser
    {
        public override JToken ParseEvent(JToken eventToken)
        {
            JToken parsedToken = InitialiseEventData(eventToken);

            parsedToken["payload"]["action"] = eventToken["payload"]["action"];

            parsedToken["payload"]["pullRequest"] = new JObject();
            parsedToken["payload"]["pullRequest"]["number"] = eventToken["payload"]["pull_request"]["number"];
            parsedToken["payload"]["pullRequest"]["url"] = eventToken["payload"]["pull_request"]["url"];
            parsedToken["payload"]["pullRequest"]["htmlUrl"] = eventToken["payload"]["pull_request"]["html_url"];
            parsedToken["payload"]["pullRequest"]["title"] = eventToken["payload"]["pull_request"]["title"];
            parsedToken["payload"]["pullRequest"]["commits"] = eventToken["payload"]["pull_request"]["commits"];
            parsedToken["payload"]["pullRequest"]["additions"] = eventToken["payload"]["pull_request"]["additions"];
            parsedToken["payload"]["pullRequest"]["deletions"] = eventToken["payload"]["pull_request"]["deletions"];
            parsedToken["payload"]["pullRequest"]["changedFiles"] = eventToken["payload"]["pull_request"]["changed_files"];

            return parsedToken;
        }
    }
}
