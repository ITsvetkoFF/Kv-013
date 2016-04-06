using System.Data.Entity;
using System.Threading.Tasks;
using GitHubExtension.Notes.DAL.Model;
using GitHubExtension.Notes.WebApi.ViewModels;
using GitHubExtension.Notes.WebApi.Mappers;

namespace GitHubExtension.Notes.WebApi.Services
{
    public class NoteService : INotesService
    {
        private readonly NoteContext notesContext;

        public NoteService(NoteContext notesContext)
        {
            this.notesContext = notesContext;
        }

        public async Task<AddNoteModel> GetNote(int noteId)
        {
            var query = await notesContext.Notes
                .FirstOrDefaultAsync(x => x.Id == noteId);
            var noteModel = query.ToNoteViewModel();
            return noteModel;
        }

        public async Task<Note> AddNote(AddNoteModel addNote)
        {
            var noteEntity = addNote.ToEntity();
            notesContext.Notes.Add(noteEntity);
            await notesContext.SaveChangesAsync();
            return noteEntity;
        }
    }
}