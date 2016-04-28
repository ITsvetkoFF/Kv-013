using System.Threading.Tasks;
using System.Web.Http;
using GitHubExtension.Infrastructure.Constants;

using GitHubExtension.Notes.WebApi.Commands;
using GitHubExtension.Notes.WebApi.Constants;
using GitHubExtension.Notes.WebApi.Extensions;
using GitHubExtension.Notes.WebApi.Mappers;
using GitHubExtension.Notes.WebApi.Queries;
using GitHubExtension.Notes.WebApi.ViewModels;

namespace GitHubExtension.Notes.WebApi.Controllers
{
    public class NoteController : ApiController
    { 
        private readonly INoteCommands _commands;

        private readonly INoteQueries _queries;

        public NoteController(INoteCommands commands, INoteQueries queries)
        {
            _commands = commands;
            _queries = queries;
        }

        [Route(RouteConstants.GetNoteForCollaborator)]
        [HttpGet]
        public IHttpActionResult GetNote([FromUri] string collaboratorId)
        {
            var userId = User.GetUserId();
            if (userId == null)
            {
                ModelState.AddModelError(ValidationConstants.UserId, ValidationConstants.UserIdError);
                return BadRequest(ModelState);
            }

            var note = _queries.GetNote(userId, collaboratorId);
            if (note == null)
            {
                return NotFound();
            }

            var noteModel = note.ToNoteViewModel();
            return Ok(noteModel);
        }

        [Route(RouteConstants.CreateNoteForCollaborator)]
        [HttpPost]
        public async Task<IHttpActionResult> CreateNote(AddNoteModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.GetUserId();
            if (userId == null)
            {
                ModelState.AddModelError(ValidationConstants.UserId, ValidationConstants.UserIdError);
                return BadRequest(ModelState);
            }

            var noteModel = model.AddUserIdToModel(userId);

            var noteEntity = noteModel.ToEntity();
            await _commands.AddNote(noteEntity);
            return Ok(noteEntity);
        }
    }
}