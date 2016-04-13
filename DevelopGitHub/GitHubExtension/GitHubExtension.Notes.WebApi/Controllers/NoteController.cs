﻿using System.Threading.Tasks;
using System.Web.Http;
using GitHubExtension.Notes.WebApi.Commands;
using GitHubExtension.Notes.WebApi.Constants;
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

        [Route(NotesRouteConstants.GetNoteRoute)]
        [HttpGet]
        public async Task<IHttpActionResult> GetNote([FromUri] string collaboratorId)
        {
            var userId = IdentityExtensions.GetUserId();
            var note = await queries.GetNote(userId, collaboratorId);
            if (note == null)
            {
                return NotFound();
            }
            var noteModel = note.ToNoteViewModel();
            return Ok(noteModel);
        }

        [Route(NotesRouteConstants.CreateNoteRoute)]
        [HttpPost]
        public async Task<IHttpActionResult> CreateNote(NoteModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var noteEntity = model.AddUserIdToModel().ToEntity();
            if (noteEntity == null)
            {
                return BadRequest();
            }
            else
            {
                await commands.AddNote(noteEntity);
                return Ok(noteEntity);
            }
        }
    } 
}