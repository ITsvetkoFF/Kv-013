using System.Data.Entity;
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

        public async Task DeleteNote(int noteId)
        {
            var note = new Note
            {
                Id = noteId
            };

            _notesContext.Entry(note).State = EntityState.Deleted;
            _notesContext.Notes.Remove(note);
            await _notesContext.SaveChangesAsync();
        }
    }
}