namespace GitHubExtension.Activity.External.WebAPI.Models.EventPayloads
{
    public class ForkEventPayloadModel : PayloadModel
    {
        public RepositoryModel Forkee { get; set; }
    }

    //Same as repository model
}
