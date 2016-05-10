using System.Data.Entity;
using System.Linq;
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

        public async Task<bool> DeleteNote(string userId, string collaboratorId)
        {
            var note = _notesContext.Notes.FirstOrDefault(n => n.UserId == userId 
                && n.CollaboratorId == collaboratorId);

            if (note == null)
            {
                return false;
            }

            _notesContext.Notes.Remove(note);
            _notesContext.Entry(note).State = EntityState.Deleted;
            await _notesContext.SaveChangesAsync();

            return true;
        }
    }
}