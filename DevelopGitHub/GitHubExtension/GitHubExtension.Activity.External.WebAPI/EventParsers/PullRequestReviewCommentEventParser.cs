using Newtonsoft.Json.Linq;

namespace GitHubExtension.Activity.External.WebAPI.EventParsers
{
    public class PullRequestReviewCommentEventParser : BaseEventParser
    {
        public override JToken ParseEvent(JToken eventToken)
        {
            JToken parsedToken = InitialiseEventData(eventToken);

            parsedToken["payload"]["action"] = eventToken["payload"]["action"];
            parsedToken["payload"]["comment"] = ParseComment(eventToken);

            parsedToken["payload"]["pullRequest"] = new JObject();
            parsedToken["payload"]["pullRequest"]["number"] = eventToken["payload"]["pull_request"]["number"];
            parsedToken["payload"]["pullRequest"]["url"] = eventToken["payload"]["pull_request"]["url"];
            parsedToken["payload"]["pullRequest"]["htmlUrl"] = eventToken["payload"]["pull_request"]["html_url"];
            parsedToken["payload"]["pullRequest"]["title"] = eventToken["payload"]["pull_request"]["title"];
            
            return parsedToken;
        }
    }
}
