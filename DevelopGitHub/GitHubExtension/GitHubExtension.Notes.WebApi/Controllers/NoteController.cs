using System.Threading.Tasks;
using System.Web.Http;
using GitHubExtension.Notes.WebApi.Commands;
using GitHubExtension.Notes.WebApi.Mappers;
using GitHubExtension.Notes.WebApi.Queries;
using GitHubExtension.Notes.WebApi.ViewModels;

namespace GitHubExtension.Notes.WebApi.Controllers
{
    public class NoteController : ApiController
    {
        private INoteCommands commands;
        private INoteQueries queries;

        public NoteController(INoteCommands commands, INoteQueries queries)
        {
            this.commands = commands;
            this.queries = queries;
        }

        [Route("api/note/{noteId}")]
        public async Task<IHttpActionResult> GetNote([FromUri] int noteId)
        {
            var note = await queries.GetNote(noteId);
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

        [Route("api/note")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateNote(AddNoteModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!model.IsNoteForOwnAccount())
            {
                return BadRequest("You can add notes only in your own account");
            }

            var noteEntity = model.ToEntity();
            var note = await commands.AddNote(noteEntity);
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