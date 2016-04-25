using System.Net.Http;
using GitHubExtension.Infrastructure.Constants;
using GitHubExtension.Notes.Tests.TestsForRoutes.TestRoutesMappers;
using GitHubExtension.Notes.WebApi.Controllers;
using GitHubExtension.Notes.WebApi.ViewModels;
using MvcRouteTester;
using Xunit;

namespace GitHubExtension.Notes.Tests.TestsForRoutes
{
    public class NoteRouteTests : TestNoteRoutesConfig
    {
        public NoteRouteTests() : base(RouteConstants.ApiNote)
        {
        }

        [Fact]
        public void GetNoteForCollaboratorRoute()
        {
            url = url.ForGetNoteForCollaborator();
            var routes = GetRoutes();
            routes.ShouldMap(url).To<NoteController>(HttpMethod.Get, x => x.GetNote("550e8400-e29b-41d4-a716-446655440000"));
        }

        [Fact]
        public void CreateNoteForCollaboratorRoute()
        {
            url = url.ForCreateNoteForCollaborator();
            var routes = GetRoutes();
            routes.ShouldMap(url).WithFormUrlBody("CollaboratorId=550e8400-e29b-41d4-a716-446655440000&Body=TestBody").To<NoteController>(HttpMethod.Post, x=>x.CreateNote(new AddNoteModel()));
        }
    }
}