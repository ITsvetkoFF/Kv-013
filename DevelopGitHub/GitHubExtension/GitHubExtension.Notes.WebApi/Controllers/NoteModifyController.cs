using System.Threading.Tasks;
using System.Web.Http;
using GitHubExtension.Notes.WebApi.Services;
using GitHubExtension.Notes.WebApi.ViewModels;
using Microsoft.AspNet.Identity;

namespace GitHubExtension.Notes.WebApi.Controllers
{
    public class NoteModifyController : ApiController
    {
        private INotesService noteService;

        public NoteModifyController(INotesService noteService)
        {
            this.noteService = noteService;
        }

        [Route("api/note")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateNote(AddNoteModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.Identity.GetUserId();

            if (model.UserId != userId)
            {
                return BadRequest("You can add notes only in your own account");
            }

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