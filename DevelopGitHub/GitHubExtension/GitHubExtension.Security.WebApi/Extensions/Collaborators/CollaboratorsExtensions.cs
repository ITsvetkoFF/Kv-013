using System.Collections.Generic;
using System.Linq;
using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.WebApi.Mappers;
using GitHubExtension.Security.WebApi.Models;

namespace GitHubExtension.Security.WebApi.Extensions.Collaborators
{
    public static class CollaboratorsExtensions
    {
        public static IEnumerable<CollaboratorWithUserIdModel> AddUserIdToCollaboratorIfExists( 
            this IEnumerable<CollaboratorModel> gitHubCollaboratorsExceptUser,
           IEnumerable<User> users)
        {
            var collaborators = new List<CollaboratorWithUserIdModel>();

            foreach (var collaborator in gitHubCollaboratorsExceptUser)
            {
                var user = users.FirstOrDefault(u => u.ProviderId == collaborator.Id);
                if (user != null)
                {
                    var collaboratorWithUserId = collaborator.ToCollaboratorWithUserId(user.Id);
                    collaborators.Add(collaboratorWithUserId);
                }
                else
                {
                    collaborators.Add(collaborator.ToCollaboratorWithUserId(null));
                }
            }

            return collaborators;
        }
    }
}