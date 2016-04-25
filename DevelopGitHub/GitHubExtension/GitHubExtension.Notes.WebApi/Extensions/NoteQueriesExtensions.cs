using System.Data.Entity;
using System.Linq;
using GitHubExtension.Notes.DAL.Model;
using GitHubExtension.Notes.WebApi.Queries;

namespace GitHubExtension.Notes.WebApi.Extensions
{
    public static class NoteQueriesExtensions
    {
        public static Note GetNote(this INoteQueries noteQuery, string userId, string collaboratorId)
        {
            var note = noteQuery.Notes.AsNoTracking()
                .FirstOrDefault(n => n.UserId == userId && n.CollaboratorId == collaboratorId);
            return note;
        } 
    }
}