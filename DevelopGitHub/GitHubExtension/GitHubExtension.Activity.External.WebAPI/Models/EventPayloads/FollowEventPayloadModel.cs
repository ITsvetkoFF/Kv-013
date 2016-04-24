namespace GitHubExtension.Activity.External.WebAPI.Models.EventPayloads
{
    // Events of this type are no longer created, but it's possible that they exist in timelines of some users
    public class FollowEventPayloadModel : IPayloadModel
    {
        public UserModel User { get; set; }
    }
}