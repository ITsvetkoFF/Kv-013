using System;
using System.Linq;
using System.Text.RegularExpressions;
using GitHubExtension.Activity.External.WebAPI.Exceptions;
using GitHubExtension.Activity.External.WebAPI.Queries;

namespace GitHubExtension.Activity.External.WebAPI.Extensions
{
    static class GetPagesAmoutExtension
    {
        private const string LastLinkRelRegex = "last";
        private const string LinklRelPageRegex = @"page=(\d+)";

        public static int? GetNumberOfPages(this IGitHubEventsQuery gitHubEventsQuery)
        {
            if (gitHubEventsQuery.RequestLinkHeader == null)
                return null;

            string link = gitHubEventsQuery.RequestLinkHeader.FirstOrDefault();
            if (link == null)
                throw new LinkHeaderMissingException();

            string[][] parts = link.Split(',').Select(l => l.Split(';')).ToArray();
            if (parts.Any(p => p.Length != 2))
                throw new LinkHeaderFormatException();
            string[] lastRel = parts.FirstOrDefault(p => Regex.IsMatch(p[1], LastLinkRelRegex));

            // last rel not found in header this means we are on the last page
            if (lastRel == null)
                return null;

            Match lastPageMatch = Regex.Match(lastRel[0], LinklRelPageRegex);
            if (!lastPageMatch.Success || lastPageMatch.Groups.Count != 2)
                throw new LinkHeaderFormatException();

            int lastPage;
            if (!Int32.TryParse(lastPageMatch.Groups[1].Value, out lastPage))
                throw new LinkHeaderFormatException();

            return lastPage;
        }
    }
}
