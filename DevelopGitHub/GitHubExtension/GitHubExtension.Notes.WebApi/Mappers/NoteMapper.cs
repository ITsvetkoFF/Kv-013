using GitHubExtension.Notes.DAL.Model;
using GitHubExtension.Notes.WebApi.ViewModels;

namespace GitHubExtension.Notes.WebApi.Mappers
{
    public static class NoteMapper
    {
        public static Note ToEntity(this AddNoteModel noteModel)
        {
            var noteEntity = new Note
            {
                UserId = noteModel.UserId,
                CollaboratorId = noteModel.CollaboratorId,
                Body = noteModel.Body
            };
            return noteEntity;
        }

        public static AddNoteModel ToNoteViewModel(this Note noteEntity)
        {
            var noteModel = new AddNoteModel
            {
                UserId = noteEntity.UserId,
                CollaboratorId = noteEntity.CollaboratorId,
                Body = noteEntity.Body
            };
            return noteModel;
        }
    }

}