using System;
using System.Collections.Generic;
using System.Linq;
using GitHubExtension.Preferences.DAL.Model;

namespace GitHubExtention.Preferences.WebApi.Queries
{
    public interface IPreferencesQuery
    {
        IOrderedQueryable<User> Users { get; }

        void SaveChanges();
    }
}
