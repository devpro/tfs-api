using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Moq;
using Xunit;

namespace Devpro.TfsApi.Lib.Tests
{
    public class QueryServiceTest
    {
        [Fact]
        public async Task TestRestSample()
        {
            // create mocks
            var logger        = new Mock<ILogger<QueryService>>();
            var tfsClient     = new Mock<ITfsClient>();
            var witHttpClient = new Mock<WorkItemTrackingHttpClient>(new Uri("http://dummy.url"), new VssCredentials());

            // inject behaviors in mocks
            witHttpClient.Setup(x => x.GetQueriesAsync(It.IsAny<string>(), null, It.IsAny<int?>(), null, It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(GetNominalTfsQueryHierarchy());
            witHttpClient.Setup(x => x.QueryByIdAsync(It.IsAny<Guid>(), null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(GetNominalTfsQueryResult());
            witHttpClient.Setup(x => x.GetWorkItemsAsync(It.IsAny<IEnumerable<int>>(), null, It.IsAny<DateTime?>(), It.IsAny<WorkItemExpand?>(), null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(GetNominalWorkItemList());
            tfsClient.Setup(x => x.WorkItemTrackingHttpClient)
                .Returns(() => witHttpClient.Object);

            // do the actual test
            var service = new QueryService(logger.Object, tfsClient.Object);
            var output  = await service.DoRestSample("dummyProject");

            // verify the test execution
            Assert.True(output != null);
            Assert.Equal(5, output.Count);
        }

        private List<QueryHierarchyItem> GetNominalTfsQueryHierarchy()
        {
            return new List<QueryHierarchyItem>
            {
                new QueryHierarchyItem
                {
                    Name     = "My Queries",
                    Children = new List<QueryHierarchyItem>
                    {
                        new QueryHierarchyItem
                        {
                            Id   = new Guid("E2776C35-3A86-4FC7-86CB-1991C88BDE31"),
                            Name = "REST Sample"
                        }
                    }
                }
            };
        }

        private WorkItemQueryResult GetNominalTfsQueryResult()
        {
            return new WorkItemQueryResult
            {
                WorkItems = new List<WorkItemReference>
                {
                    new WorkItemReference { Id = 1 },
                    new WorkItemReference { Id = 2 },
                    new WorkItemReference { Id = 3 },
                    new WorkItemReference { Id = 4 },
                    new WorkItemReference { Id = 5 }
                }
            };
        }

        private static List<WorkItem> GetNominalWorkItemList()
        {
            return new List<WorkItem>
            {
                CreateWorkItem(1, "bug 1", "Bug", "New"),
                CreateWorkItem(2, "bug 2", "Bug", "New"),
                CreateWorkItem(3, "bug 3", "Bug", "New"),
                CreateWorkItem(4, "bug 4", "Bug", "New"),
                CreateWorkItem(5, "bug 5", "Bug", "New")
            };
        }

        private static WorkItem CreateWorkItem(int id, string title, string workItemType, string state)
        {
            return new WorkItem
            {
                Id     = id,
                Fields = new Dictionary<string, object>
                {
                    { "System.Title"       , title },
                    { "System.WorkItemType", workItemType },
                    { "System.State"       , state }
                }
            };
        }
    }
}
