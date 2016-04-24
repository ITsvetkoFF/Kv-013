namespace GitHubExtension.Activity.External.WebAPI.Models.EventPayloads
{
    // Triggered when a patch is applied in the Fork Queue.
    // Events of this type are no longer created, but it's possible that they exist in timelines of some users.
    public class ForkApplyEventPayloadModel : IPayloadModel
    {
        public string After { get; set; }

        public string Before { get; set; }

        public string Head { get; set; }
    }
}