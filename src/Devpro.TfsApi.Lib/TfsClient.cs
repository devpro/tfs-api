using System;
using Microsoft.Extensions.Logging;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.Common;

namespace Devpro.TfsApi.Lib
{
    /// <summary>
    /// TFS client.
    /// </summary>
    public class TfsClient : ITfsClient, IDisposable
    {
        private readonly ITfsClientConfiguration _clientConfiguration;
        private readonly ILogger<TfsClient>      _logger;

        private VssConnection _vssConnection;

        public WorkItemTrackingHttpClient WorkItemTrackingHttpClient { get; private set; }

        public TfsClient(ILogger<TfsClient> logger, ITfsClientConfiguration clientConfiguration)
        {
            _clientConfiguration = clientConfiguration;
            _logger              = logger;

            _logger.LogDebug("Constructor called");

            //TODO: should we force it in the constructor?
            OpenConnection();
        }

        private void OpenConnection()
        {
            // We are using default VssCredentials which uses NTLM against a Team Foundation Server.
            // See additional provided examples for creating credentials for other types of authentication.
            VssCredentials vssCredentials;
            if (string.IsNullOrEmpty(_clientConfiguration.TfsPersonalAccessToken))
            {
                vssCredentials = new VssCredentials();
            }
            else
            {
                vssCredentials = new VssBasicCredential(string.Empty, _clientConfiguration.TfsPersonalAccessToken);
            }
            _vssConnection = new VssConnection(new Uri(_clientConfiguration.TfsCollectionUri), vssCredentials);


            WorkItemTrackingHttpClient = _vssConnection.GetClient<WorkItemTrackingHttpClient>();
        }

        private void CloseConnection()
        {
            if (_vssConnection != null && _vssConnection.HasAuthenticated)
                _vssConnection.Disconnect();
        }

        public void Dispose()
        {
            _logger.LogDebug("Dispose called");
            if (WorkItemTrackingHttpClient != null)
                WorkItemTrackingHttpClient.Dispose();
            CloseConnection();
            _vssConnection = null;
        }
    }
}
