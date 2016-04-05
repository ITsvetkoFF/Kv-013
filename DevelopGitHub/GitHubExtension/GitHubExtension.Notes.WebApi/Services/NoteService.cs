using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using GitHubExtension.Notes.DAL.Model;
using GitHubExtension.Notes.WebApi.ViewModels;
using AutoMapper.QueryableExtensions;

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
            var note = await notesContext.Notes
                .Where(x => x.Id == noteId)
                .ProjectTo<AddNoteModel>()
                .FirstOrDefaultAsync();
            return note;
        }

        public async Task<Note> AddNote(AddNoteModel addNote)
        {
            var note = new Note()
            {
                UserId = addNote.UserId,
                CollaboratorId = addNote.CollaboratorId,
                Body = addNote.Body
            };

            notesContext.Notes.Add(note);
            await notesContext.SaveChangesAsync();

            return note;
        }
    }
}