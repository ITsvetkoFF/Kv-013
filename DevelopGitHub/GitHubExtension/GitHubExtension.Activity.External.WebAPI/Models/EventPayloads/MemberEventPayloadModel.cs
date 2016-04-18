namespace GitHubExtension.Activity.External.WebAPI.Models.EventPayloads
{
    public class MemberEventPayloadModel : IPayloadModel
    {
        // The action that was performed. Currently, can only be "added".
        public string Action { get; set; }
        public UserModel Member { get; set; }
    }
}
