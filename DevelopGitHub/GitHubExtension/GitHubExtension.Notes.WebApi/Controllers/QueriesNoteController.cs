using System.Threading.Tasks;
using System.Web.Http;
using GitHubExtension.Notes.WebApi.Mappers;
using GitHubExtension.Notes.WebApi.Services;

namespace GitHubExtension.Notes.WebApi.Controllers
{
    public class QueriesNoteController : ApiController
    {
        private INotesService noteService;

        public QueriesNoteController(INotesService noteService)
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
            var noteModel = note.ToNoteViewModel();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(noteModel);
        }
    }
}