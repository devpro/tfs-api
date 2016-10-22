using System.Collections.Generic;
using System.Threading.Tasks;
using Devpro.TfsApi.Dto;

namespace Devpro.TfsApi.Lib
{
    public interface IQueryService
    {
        /// <summary>
        /// This sample creates a new work item query for New Bugs, stores it under 'MyQueries', runs the query, and then sends the results.
        /// </summary>
        /// <param name="teamProjectName"></param>
        Task<List<WorkItemDto>> DoRestSample(string teamProjectName);
    }
}
