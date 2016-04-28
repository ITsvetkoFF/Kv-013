using System.Text.RegularExpressions;
using GitHubExtension.Infrastructure.Constants;

namespace GitHubExtension.Notes.Tests.TestsForRoutes.TestRoutesMappers
{
    public static class NoteTestRoutesMappers
    {
        public static string ForGetNoteForCollaborator(this string url)
        {
            return url + Regex.Replace(
                RouteConstants.GetNoteForCollaborator,
                RouteConstants.CollaboratorId,
                "/550e8400-e29b-41d4-a716-446655440000");
        }

        public static string ForCreateNoteForCollaborator(this string url)
        {
            return url + RouteConstants.CreateNoteForCollaborator;
        }
    }
}