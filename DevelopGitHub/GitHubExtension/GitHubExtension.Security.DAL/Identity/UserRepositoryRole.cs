namespace GitHubExtension.Security.DAL.Identity
{
    public class UserRepositoryRole
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int RepositoryId { get; set; }
        public int SecurityRoleId { get; set; }

        public virtual User User { get; set; }
        public virtual Repository Repository { get; set; }
        public virtual SecurityRole SecurityRole { get; set; }
    }
}
