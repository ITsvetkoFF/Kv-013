using System.Linq;
using GitHubExtension.Notes.DAL.Model;

namespace GitHubExtension.Notes.WebApi.Queries
{
    public class NoteQueries : INoteQueries
    {
        private readonly NoteContext _notesContext;

        public NoteQueries(NoteContext notesContext)
        {
            _notesContext = notesContext;
        }

        public IOrderedQueryable<Note> Notes
        {
            get { return _notesContext.Notes; }
        }
    }
}