using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Devpro.TfsApi.Dto;
using Microsoft.Extensions.Logging;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace Devpro.TfsApi.Lib
{
    /// <summary>
    /// TFS query service.
    /// </summary>
    public class QueryService : IQueryService
    {
        private readonly ILogger<QueryService> _logger;
        private readonly ITfsClient            _tfsClient;

        public QueryService(ILogger<QueryService> logger, ITfsClient tfsClient)
        {
            _logger    = logger;
            _tfsClient = tfsClient;
        }

        /// <summary>
        /// Do a REST sample.
        /// </summary>
        /// <see cref="https://www.visualstudio.com/en-us/docs/integrate/get-started/client-libraries/samples"/>
        /// <param name="teamProjectName">Team project name.</param>
        /// <returns>List of work item DTO.</returns>
        public async Task<List<WorkItemDto>> DoRestSample(string teamProjectName)
        {
            var witClient = _tfsClient.WorkItemTrackingHttpClient;
            
            // Get 2 levels of query hierarchy items
            var queryHierarchyItems = await witClient.GetQueriesAsync(teamProjectName, depth: 2);

            // Search for 'My Queries' folder
            var myQueriesFolder = queryHierarchyItems.FirstOrDefault(qhi => qhi.Name == "My Queries");
            if (myQueriesFolder == null)
                return null;

            var queryName = "REST Sample";

            // See if our 'REST Sample' query already exists under 'My Queries' folder.
            QueryHierarchyItem newBugsQuery = null;
            if (myQueriesFolder.Children != null)
            {
                newBugsQuery = myQueriesFolder.Children.FirstOrDefault(qhi => qhi.Name == queryName);
            }
            if (newBugsQuery == null)
            {
                // if the 'REST Sample' query does not exist, create it.
                newBugsQuery = new QueryHierarchyItem
                {
                    Name     = queryName,
                    IsFolder = false,
                    Wiql     = "SELECT [System.Id],[System.WorkItemType],[System.Title],[System.AssignedTo],[System.State],[System.Tags] "
                        + "FROM WorkItems "
                        + "WHERE [System.TeamProject] = @project "
                        + "AND [System.WorkItemType] = 'Bug' "
                        //+ "AND [System.State] = '01-New'"
                };
                newBugsQuery = await witClient.CreateQueryAsync(newBugsQuery, teamProjectName, myQueriesFolder.Name);
            }

            var result = await witClient.QueryByIdAsync(newBugsQuery.Id);

            var workItems = new List<WorkItemDto>();
            if (!result.WorkItems.Any())
                _logger.LogWarning("No work items were returned from query.");

            //TODO: loop code to be rewritten, not easy to read and possible multiple enumerations issue
            var skip = 0;
            const int batchSize = 100;
            IEnumerable<WorkItemReference> workItemRefs;
            do
            {
                workItemRefs = result.WorkItems.Skip(skip).Take(batchSize);
                if (workItemRefs.Any())
                {
                    // get details for each work item in the batch
                    var subWorkItems = await witClient.GetWorkItemsAsync(workItemRefs.Select(wir => wir.Id));
                    foreach (var workItem in subWorkItems)
                    {
                        //TODO: use AutoMapper!
                        workItems.Add(new WorkItemDto
                        {
                            Id           = workItem.Id ?? 0,
                            Title        = (string)workItem.Fields["System.Title"],
                            WorkItemType = (string)workItem.Fields["System.WorkItemType"],
                            State        = (string)workItem.Fields["System.State"]
                        });
                    }
                }
                skip += batchSize;
            }
            while (workItemRefs.Count() == batchSize);

            return workItems;
        }
    }
}
