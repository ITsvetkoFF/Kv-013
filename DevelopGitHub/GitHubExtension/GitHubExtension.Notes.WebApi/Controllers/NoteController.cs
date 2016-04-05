using System.Threading.Tasks;
using System.Web.Http;
using GitHubExtension.Notes.WebApi.Services;
using GitHubExtension.Notes.WebApi.ViewModels;

namespace GitHubExtension.Notes.WebApi.Controllers
{
    public class NoteController : ApiController
    {
        private INotesService noteService;

        public NoteController(INotesService noteService)
        {
            this.noteService = noteService;
        }

        [Route("api/note/{noteId}")]
        public async Task<IHttpActionResult> GetNote([FromUri] int noteId)
        {
            var note = await noteService.GetNote(noteId);
            if (note == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(note);
            }
        }

        [Route("api/note")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateNote(AddNoteModel model)
        {
            var note = await noteService.AddNote(model);

            if (note == null)
            {
                return BadRequest("Note not added");
            }
            else
            {
                return Ok(note);
            }
        }
    }
}