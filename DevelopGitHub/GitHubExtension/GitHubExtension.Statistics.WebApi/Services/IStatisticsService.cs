using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubExtension.Statistics.WebApi.CommunicationModels;

namespace GitHubExtension.Statistics.WebApi.Services
{
    public interface IStatisticsService
    {
        Task<Graph> GraphCreation(string userName, string token);
    }
}
