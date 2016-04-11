using GitHubExtension.Notes.DAL.Model;
using GitHubExtension.Notes.WebApi.ViewModels;

namespace GitHubExtension.Notes.WebApi.Mappers
{
    public static class NoteMapper
    {
        public static Note ToEntity(this NoteModel noteModel)
        {
            var noteEntity = new Note
            {
                UserId = noteModel.UserId,
                CollaboratorId = noteModel.CollaboratorId,
                Body = noteModel.Body
            };
            return noteEntity;
        }

        public static NoteModel ToNoteViewModel(this Note noteEntity)
        {
            var noteModel = new NoteModel
            {
                UserId = noteEntity.UserId,
                CollaboratorId = noteEntity.CollaboratorId,
                Body = noteEntity.Body
            };
            return noteModel;
        }
    }

}