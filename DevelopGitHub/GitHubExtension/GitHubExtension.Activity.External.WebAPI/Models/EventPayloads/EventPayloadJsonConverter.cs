using System;
using System.Collections.Generic;

using GitHubExtension.Activity.External.WebAPI.Exceptions;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GitHubExtension.Activity.External.WebAPI.Models.EventPayloads
{
    public class EventModelJsonConverter : JsonConverter
    {
        private const string Type = "type";

        private const string UnknownType = "Unknown event type. Incoming type: {0}";

        private static readonly Dictionary<string, Func<IPayloadModel>> PayloadExtractors =
            new Dictionary<string, Func<IPayloadModel>>
                {
                    {
                        GitHubEventTypeConstants.CommitCommentEvent, 
                        () => new CommitCommentEventPayloadModel()
                    }, 
                    {
                        GitHubEventTypeConstants.CreateEvent, 
                        () => new CreateEventPayloadModel()
                    }, 
                    {
                        GitHubEventTypeConstants.DeleteEvent, 
                        () => new DeleteEventPayloadModel()
                    }, 
                    {
                        GitHubEventTypeConstants.DownloadEvent, 
                        () => new DownloadEventPayloadModel()
                    }, 
                    {
                        GitHubEventTypeConstants.FollowEvent, 
                        () => new FollowEventPayloadModel()
                    }, 
                    {
                        GitHubEventTypeConstants.ForkEvent, 
                        () => new ForkEventPayloadModel()
                    }, 
                    {
                        GitHubEventTypeConstants.ForkApplyEvent, 
                        () => new ForkApplyEventPayloadModel()
                    }, 
                    {
                        GitHubEventTypeConstants.GistEvent, 
                        () => new GistEventPayloadModel()
                    }, 
                    {
                        GitHubEventTypeConstants.GollumEvent, 
                        () => new GollumEventPayloadModel()
                    }, 
                    {
                        GitHubEventTypeConstants.IssueCommentEvent, 
                        () => new IssueCommentEventPayloadModel()
                    }, 
                    {
                        GitHubEventTypeConstants.IssuesEvent, 
                        () => new IssuesEventPayloadModel()
                    }, 
                    {
                        GitHubEventTypeConstants.MemberEvent, 
                        () => new MemberEventPayloadModel()
                    }, 
                    { GitHubEventTypeConstants.PublicEvent, () => null }, 
                    {
                        // public event has empty payload
                        // example can be found here https://gist.github.com/senioroman4uk/f9d7fc52de8d3332dff22d468fd7a57e
                        GitHubEventTypeConstants.PullRequestEvent, 
                        () => new PullRequestEventPayloadModel()
                    }, 
                    {
                        GitHubEventTypeConstants.PullRequestReviewCommentEvent, 
                        () => new PullRequestReviewCommentEventPayloadModel()
                    }, 
                    {
                        GitHubEventTypeConstants.PushEvent, 
                        () => new PushEventPayloadModel()
                    }, 
                    {
                        GitHubEventTypeConstants.ReleaseEvent, 
                        () => new ReleaseEventPayloadModel()
                    }, 
                    {
                        GitHubEventTypeConstants.WatchEvent, 
                        () => new WatchEventPayloadModel()
                    }
                };

        // Use default converter for writing
        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(GitHubEventModel);
        }

        public override object ReadJson(
            JsonReader reader, 
            Type objectType, 
            object existingValue, 
            JsonSerializer serializer)
        {
            JObject jobject = JObject.Load(reader);

            string type = (string)jobject.Property(Type);

            Func<IPayloadModel> extractor = PayloadExtractors[type];
            if (extractor == null)
            {
                throw new ExtractorNotFoundException(string.Format(UnknownType, type));
            }

            IPayloadModel payload = extractor();
            var gitHubEvent = new GitHubEventModel() { Payload = payload };

            serializer.Populate(jobject.CreateReader(), gitHubEvent);

            return gitHubEvent;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}