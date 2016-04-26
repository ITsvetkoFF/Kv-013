using System.Threading.Tasks;

using GitHubExtension.Notes.DAL.Model;

namespace GitHubExtension.Notes.WebApi.Commands
{
    public class NoteCommands : INoteCommands
    {
        private readonly NoteContext _notesContext;

        public NoteCommands(NoteContext notesContext)
        {
            _notesContext = notesContext;
        }

        public async Task AddNote(Note noteEntity)
        {
            _notesContext.Notes.Add(noteEntity);
            await _notesContext.SaveChangesAsync();
        }
    }
}