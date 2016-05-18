using System.Collections.Generic;
using System.Linq;
using GitHubExtension.Security.DAL.Identity;
using GitHubExtension.Security.WebApi.Mappers;
using GitHubExtension.Security.WebApi.Models;

namespace GitHubExtension.Security.WebApi.Extensions.Collaborators
{
    public static class CollaboratorsExtensions
    {
        public static IEnumerable<CollaboratorWithUserDataModel> AddUserDataToCollaboratorIfExists( 
            this IEnumerable<CollaboratorModel> gitHubCollaboratorsExceptUser,
           IEnumerable<User> users)
        {
            var collaborators = new List<CollaboratorWithUserDataModel>();

            foreach (var collaborator in gitHubCollaboratorsExceptUser)
            {
                var user = users.FirstOrDefault(u => u.ProviderId == collaborator.Id);
                if (user != null)
                {
                    var collaboratorWithUserData = collaborator.ToCollaboratorWithUserData(user.Id);
                    collaboratorWithUserData.Mail = user.IsMailVisible ? user.Email : null;
                    collaborators.Add(collaboratorWithUserData);
                }
                else
                {
                    collaborators.Add(collaborator.ToCollaboratorWithUserData(null));
                }
            }

            return collaborators;
        }
    }
}