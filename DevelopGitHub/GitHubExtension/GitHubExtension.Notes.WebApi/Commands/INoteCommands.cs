using System.Threading.Tasks;

using GitHubExtension.Notes.DAL.Model;

namespace GitHubExtension.Notes.WebApi.Commands
{
    public interface INoteCommands
    {
        Task AddOrUpdateNote(Note noteEntity);
    }
}