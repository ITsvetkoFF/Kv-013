using System.Collections.Generic;
using System.Linq;
using GitHubExtension.Statistics.WebApi.Queries.Interfaces;

namespace GitHubExtension.Statistics.WebApi.Mappers.GitHub
{
    public static class GitHubMapper
    {
        #region fields
        private static int countWeeksInMonth = 4;
        private static List<int> listGroupOfCommits;
        private static List<int> listOfCommits;
        #endregion

        #region extension methods
        public static List<int> ToMounth(this IGitHubQuery gitHubQuery, ICollection<int> commits)
        {
            listOfCommits = new List<int>();
            for (int i = 0; i < commits.Count; i += countWeeksInMonth)
            {
                listOfCommits.Add(commits.Skip(i).Take(countWeeksInMonth).Sum(x => x));
            }

            return listOfCommits;
        }

        public static List<int> ToGroupCommits(this IGitHubQuery gitHubQuery, ICollection<ICollection<int>> commitsEverRepository)
        {
            listGroupOfCommits = new List<int>();
            for (int i = 0; i < commitsEverRepository.ToList().Select(a => a.ToList().Count).ToList().FirstOrDefault(); i++)
            {
                listGroupOfCommits.Add(commitsEverRepository.Sum(a => a.ToList()[i]));
            }
            return listGroupOfCommits;
        }
        #endregion
    }
}
