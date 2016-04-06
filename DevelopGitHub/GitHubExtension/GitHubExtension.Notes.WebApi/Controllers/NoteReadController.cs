using System.Threading.Tasks;
using System.Web.Http;
using GitHubExtension.Notes.WebApi.Services;

namespace GitHubExtension.Notes.WebApi.Controllers
{
    public class NoteReadController : ApiController
    {
         private INotesService noteService;

         public NoteReadController(INotesService noteService)
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
    }
}