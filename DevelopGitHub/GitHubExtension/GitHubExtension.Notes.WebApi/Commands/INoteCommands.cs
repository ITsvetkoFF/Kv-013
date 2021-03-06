﻿using System.Threading.Tasks;

using GitHubExtension.Notes.DAL.Model;

namespace GitHubExtension.Notes.WebApi.Commands
{
    public interface INoteCommands
    {
        Task AddNote(Note noteEntity);

        Task DeleteNote(int noteId);
    }
}