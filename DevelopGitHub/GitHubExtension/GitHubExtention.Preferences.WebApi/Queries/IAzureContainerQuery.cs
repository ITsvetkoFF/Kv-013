using System.Threading.Tasks;
using GitHubExtention.Preferences.WebApi.Models;

namespace GitHubExtention.Preferences.WebApi.Queries
{
    public interface IAzureContainerQuery
    {
        Task<string> UpdateBlob(FileModel file);

        void DeleteBlob(string absoluteUri);
    }
}
