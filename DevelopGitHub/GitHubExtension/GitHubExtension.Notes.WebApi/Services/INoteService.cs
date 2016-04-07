using System.Threading.Tasks;
using GitHubExtension.Notes.DAL.Model;
using GitHubExtension.Notes.WebApi.ViewModels;

namespace GitHubExtension.Notes.WebApi.Services
{
    public interface INotesService
    {
       Task<Note> GetNote(int noteId);
       Task<Note> AddNote(AddNoteModel addNote);
    }
}