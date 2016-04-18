using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GitHubExtension.Activity.External.WebAPI.Exceptions;
using GitHubExtension.Activity.External.WebAPI.Queries;

namespace GitHubExtension.Activity.External.WebAPI.Extensions
{
    static class GetPagesAmountExtension
    {
        private const string LastLinkRelRegex = "last";
        private const string LinklRelPageRegex = @"page=(\d+)";

        /// <summary>
        /// Gets number of pages from Request link header
        /// </summary>
        /// <returns>number of pages or null if link header is not present</returns>
        public static int? GetNumberOfPages(this IGitHubEventsQuery gitHubEventsQuery, IEnumerable<string> requestLinkHeader)
        {
            string[][] parts = SplitLinkHeader(requestLinkHeader);

            // No link header
            if (parts == null)
                return null;
            
            if (parts.Any(p => p.Length != 2))
                throw new LinkHeaderFormatException();
            
            string unparsedLastPage = GetLastPage(parts);
            
            // lastrel not present in header we are on last page
            if (unparsedLastPage == null)
                return null;

            int lastPage = ParseLastPage(unparsedLastPage);
            return lastPage;
        }

        private static string[][] SplitLinkHeader(IEnumerable<string> linkHeader)
        {
            if (linkHeader == null)
                return null;

            string link = linkHeader.FirstOrDefault();
            if (link == null)
                throw new LinkHeaderMissingException();

            string[][] parts = link.Split(',').Select(l => l.Split(';')).ToArray();
            
            return parts;
        }

        private static string GetLastPage(string[][] partsOfHeader)
        {
            string[] lastRel = partsOfHeader.FirstOrDefault(p => Regex.IsMatch(p[1], LastLinkRelRegex));

            if (lastRel == null)
                return null;

            Match lastPageMatch = Regex.Match(lastRel[0], LinklRelPageRegex);

            if (!lastPageMatch.Success || lastPageMatch.Groups.Count != 2)
                throw new LinkHeaderFormatException();

            return lastPageMatch.Groups[1].Value;
        }

        private static int ParseLastPage(string unparsedLastPage)
        {
            int lastPage;
            if (!Int32.TryParse(unparsedLastPage, out lastPage))
                throw new LinkHeaderFormatException();

            return lastPage;
        }
    }
}
