using System.Linq;
using GitHubExtension.Notes.DAL.Model;

namespace GitHubExtension.Notes.WebApi.Queries
{
    public interface INoteQueries
    {
        IOrderedQueryable<Note> Notes { get; }
    }
}