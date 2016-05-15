using System.Linq;
using GitHubExtension.Preferences.DAL.Model;

namespace GitHubExtention.Preferences.WebApi.Queries
{
    class PreferencesQuery : IPreferencesQuery
    {
        private readonly PreferencesContext _context;

        public PreferencesQuery(PreferencesContext context)
        {
            _context = context;
        }

        public IOrderedQueryable<User> Users
        {
            get { return _context.Users; }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
