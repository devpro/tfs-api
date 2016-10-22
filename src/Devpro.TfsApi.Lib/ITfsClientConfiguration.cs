namespace Devpro.TfsApi.Lib
{
    public interface ITfsClientConfiguration
    {
        /// <summary>
        /// Full collection URI. For example: "http://myserver:8080/tfs/defaultcollection".
        /// </summary>
        string TfsCollectionUri { get; }

        /// <summary>
        /// TFS personal access token.
        /// </summary>
        /// <see cref="https://www.visualstudio.com/en-us/docs/setup-admin/team-services/use-personal-access-tokens-to-authenticate"/>
        string TfsPersonalAccessToken { get; }
    }
}
