using System.Threading.Tasks;
using GitHubExtension.Notes.DAL.Model;

namespace GitHubExtension.Notes.WebApi.Queries
{
    public interface INoteQueries
    {
        Task<Note> GetNote(int noteId);
    }
}