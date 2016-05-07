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

        public async Task AddOrUpdateNote(Note noteEntity)
        {
            var note = await _notesContext.Notes.SingleOrDefaultAsync(n => n.UserId == noteEntity.UserId 
                && n.CollaboratorId == noteEntity.CollaboratorId);
            if (note != null)
            {
                note.Body = noteEntity.Body;
                _notesContext.Entry(note).State = EntityState.Modified;
            }
            else
            {
                _notesContext.Notes.Add(noteEntity);
            }

            await _notesContext.SaveChangesAsync();
        }
    }
}