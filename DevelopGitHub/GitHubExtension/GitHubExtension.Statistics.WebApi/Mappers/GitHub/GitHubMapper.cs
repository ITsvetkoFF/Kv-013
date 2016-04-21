using System.Collections.Generic;
using System.Linq;
using GitHubExtension.Statistics.WebApi.Queries.Interfaces;

namespace GitHubExtension.Statistics.WebApi.Mappers.GitHub
{
    public static class GitHubMapper
    {
        public static List<int> ToMonths(this IGitHubQuery gitHubQuery, ICollection<int> commits)
        {
            List<int> listOfCommits = new List<int>();
            int countWeeksInMonth = 4;
            for (int i = 0; i < commits.Count; i += countWeeksInMonth)
            {
                listOfCommits.Add(commits.Skip(i).Take(countWeeksInMonth).Sum(x => x));
            }

            return listOfCommits;
        }

        public static List<int> ToGroupCommits(this IGitHubQuery gitHubQuery, ICollection<ICollection<int>> commitsEverRepository)
        {
            List<int> listGroupOfCommits = new List<int>();
            for (int i = 0; i < commitsEverRepository.ToList().Select(a => a.ToList().Count).ToList().FirstOrDefault(); i++)
            {
                listGroupOfCommits.Add(commitsEverRepository.Sum(a => a.ToList()[i]));
            }
            return listGroupOfCommits;
        }
    }
}
