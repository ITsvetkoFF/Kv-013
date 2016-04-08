using System.Threading.Tasks;
using GitHubExtension.Notes.DAL.Model;

namespace GitHubExtension.Notes.WebApi.Commands
{
    public interface INoteCommands
    {
        Task<Note> AddNote(Note noteEntity);
    }
}