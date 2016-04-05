using System.Threading.Tasks;
using GitHubExtension.Notes.DAL.Model;
using GitHubExtension.Notes.WebApi.ViewModels;

namespace GitHubExtension.Notes.WebApi.Services
{
    public interface INotesService
    {
       Task<AddNoteModel> GetNote(int noteId);
       Task<Note> AddNote(AddNoteModel addNote);
    }
}