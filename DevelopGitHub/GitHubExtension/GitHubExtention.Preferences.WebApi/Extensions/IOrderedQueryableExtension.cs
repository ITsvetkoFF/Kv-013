using System.Linq;
using GitHubExtension.Preferences.DAL.Model;

namespace GitHubExtention.Preferences.WebApi.Extensions
{
    public static class IOrderedQueryableExtension
    {
        public static User GetUser(this IOrderedQueryable<User> collection, string id)
        {
            return collection.FirstOrDefault(x => x.Id == id);
        }
    }
}
