using System;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;

namespace Devpro.TfsApi.Lib
{
    public interface ITfsClient
    {
        WorkItemTrackingHttpClient WorkItemTrackingHttpClient { get; }
    }
}
