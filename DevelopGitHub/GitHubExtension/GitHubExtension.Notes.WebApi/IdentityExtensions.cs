using System.Web;
using GitHubExtension.Notes.WebApi.ViewModels;
using Microsoft.AspNet.Identity;

namespace GitHubExtension.Notes.WebApi
{
    public static class IdentityExtensions
    {
        public static string GetUserId()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            return userId;
        }

        public static NoteModel AddUserIdToModel (this NoteModel noteModel)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            noteModel.UserId = userId;
            return noteModel;
        }
    }
}