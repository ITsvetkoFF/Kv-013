﻿using System.Data.Entity;
using System.Threading.Tasks;
using GitHubExtension.Notes.DAL.Model;

namespace GitHubExtension.Notes.WebApi.Queries
{
    public class NoteQueries : INoteQueries
    {
        private readonly NoteContext notesContext;

        public NoteQueries(NoteContext notesContext)
        {
            this.notesContext = notesContext;
        }

        public async Task<Note> GetNote(string userId, string collaboratorId)
        {
            var note = await notesContext.Notes
                .FirstOrDefaultAsync(x => x.UserId == userId 
                    && x.CollaboratorId == collaboratorId);
            return note;
        }
    }
}