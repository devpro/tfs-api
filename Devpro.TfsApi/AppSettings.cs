using Devpro.TfsApi.Lib;

namespace Devpro.TfsApi
{
    public class AppSettings : ITfsClientConfiguration
    {
        public string TfsCollectionUri { get; set; }

        public string TfsPersonalAccessToken { get; set; }
    }
}
