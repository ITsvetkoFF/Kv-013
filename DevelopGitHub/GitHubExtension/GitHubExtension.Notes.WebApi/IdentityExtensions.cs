using System.Web;
using GitHubExtension.Notes.WebApi.ViewModels;
using Microsoft.AspNet.Identity;

namespace GitHubExtension.Notes.WebApi
{
    public static class IdentityExtensions
    {
        public static bool IsNoteForOwnAccount(this AddNoteModel noteModel)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            return noteModel.UserId == userId;
        }
    }
}