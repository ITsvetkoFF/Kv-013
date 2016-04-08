using System.Threading.Tasks;
using GitHubExtension.Notes.DAL.Model;

namespace GitHubExtension.Notes.WebApi.Commands
{
    public class NoteCommands : INoteCommands
    {
        private readonly NoteContext notesContext;

        public NoteCommands(NoteContext notesContext)
        {
            this.notesContext = notesContext;
        }

        public async Task<Note> AddNote(Note noteEntity)
        {
            notesContext.Notes.Add(noteEntity);
            await notesContext.SaveChangesAsync();
            return noteEntity;
        } 
    }
}