using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GitHubExtension.Activity.External.WebAPI.Models.EventPayloads
{
    public class EventModelConverter: JsonConverter
    {
        private static readonly Dictionary<string, Func<PayloadModel>> PayloadExtractors
            = new Dictionary<string, Func<PayloadModel>>
        {
                { GitHubEventTypeConstants.CommitCommentEvent, () => new CommitCommentEventPayloadModel() },
                { GitHubEventTypeConstants.CreateEvent, () => new CreateEventPayloadModel() },
                { GitHubEventTypeConstants.DeleteEvent, () => new DeleteEventPayloadModel() },
                { GitHubEventTypeConstants.DownloadEvent, () => new DownloadEventPayloadModel() },
                { GitHubEventTypeConstants.FollowEvent, () => new FollowEventPayloadModel() },
                { GitHubEventTypeConstants.ForkEvent, () => new ForkEventPayloadModel() },
                { GitHubEventTypeConstants.ForkApplyEvent, () => new ForkApplyEventPayloadModel() },
                { GitHubEventTypeConstants.GistEvent, () => new GistEventPayloadModel() },
                { GitHubEventTypeConstants.GollumEvent, () => new GollumEventPayloadModel() },
                { GitHubEventTypeConstants.IssueCommentEvent, () => new IssueCommentEventPayloadModel() },
                { GitHubEventTypeConstants.IssuesEvent, () => new IssuesEventPayloadModel() },
                { GitHubEventTypeConstants.MemberEvent, () => new MemberEventPayloadModel() },
                { GitHubEventTypeConstants.PublicEvent, () => new PayloadModel() },
                { GitHubEventTypeConstants.PullRequestEvent, () => new PullRequestEventPayloadModel() },
                { GitHubEventTypeConstants.PullRequestReviewCommentEvent, () => new PullRequestReviewCommentEventPayloadModel() },
                { GitHubEventTypeConstants.PushEvent, () => new PushEventPayloadModel() },
                { GitHubEventTypeConstants.ReleaseEvent, () => new ReleaseEventPayloadModel() },
                { GitHubEventTypeConstants.WatchEvent, () => new WatchEventPayloadModel() }
        };

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        //Use default converter for writing
        public override bool CanWrite
        {
            get { return false; }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jobject = JObject.Load(reader);
            string type = (string)jobject.Property("type");

            Func<PayloadModel> extractor = PayloadExtractors[type];
            if (extractor == null)
                throw new ApplicationException();
           
            PayloadModel payload = extractor();
            var gitHubEvent = new GitHubEventModel() { Payload = payload};
            serializer.Populate(jobject.CreateReader(), gitHubEvent);

            return gitHubEvent;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (GitHubEventModel);
        }
    }
}
