namespace Nimator.Plugins.Couchbase.Models.Credentials
{
    public interface IAuthorizationCredentials
    {
        string AuthorizationHeaderKey { get; }

        string AuthorizationHeaderValue { get; }

        bool NotEmpty { get; }
    }
}
