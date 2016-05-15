using System.Threading.Tasks;
using GitHubExtention.Preferences.WebApi.Extensions;
using GitHubExtention.Preferences.WebApi.Models;
using GitHubExtention.Preferences.WebApi.Queries;

namespace GitHubExtention.Preferences.WebApi
{
    public static class AvatarOperations
    {
        public static async Task<string> SaveAvatar(this IAzureContainerQuery container, FileModel file)
        {
             if (file.IsValidImageFormat())
             {
                 return await container.UpdateBlob(file);
             }

            return null;
        }
    }
}
