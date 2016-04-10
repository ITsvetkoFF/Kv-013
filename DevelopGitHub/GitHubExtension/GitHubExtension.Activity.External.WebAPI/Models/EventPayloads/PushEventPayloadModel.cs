using Newtonsoft.Json;

namespace GitHubExtension.Activity.External.WebAPI.Models.EventPayloads
{
    public class PushEventPayloadModel : PayloadModel
    {
        /// <summary>
        /// The full Git ref that was pushed. Example: "refs/heads/master".
        /// </summary>
        public string Ref { get; set; }
        /// <summary>
        ///  The SHA of the most recent commit on ref after the push.
        /// </summary>
        public string Head { get; set; }
        /// <summary>
        ///  The SHA of the most recent commit on ref before the push.
        /// </summary>
        public string Before { get; set; }
        /// <summary>
        /// The number of commits in the push.
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// The number of distinct commits in the push.
        /// </summary>
        [JsonProperty(PropertyName = GitHubConstants.DistinctSize)]
        public int DistinctSize { get; set; }
    }
}
