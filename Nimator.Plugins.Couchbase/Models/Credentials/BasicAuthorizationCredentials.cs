using System;
using System.Text;

namespace Nimator.Plugins.Couchbase.Models.Credentials
{
    public class BasicAuthorizationCredentials : IAuthorizationCredentials
    {
        private readonly string _username;
        private readonly string _password;

        private string _base64String;

        public BasicAuthorizationCredentials(string username, string password)
        {
            _username = username;
            _password = password;
        }

        public string AuthorizationHeaderKey => "Basic";

        public string AuthorizationHeaderValue
        {
            get
            {
                if (string.IsNullOrEmpty(_base64String))
                {
                    var bytes = Encoding.ASCII.GetBytes($"{_username}:{_password}");
                    _base64String = Convert.ToBase64String(bytes);
                }

                return _base64String;
            }
        }

        public bool NotEmpty => !string.IsNullOrEmpty(AuthorizationHeaderValue);
    }
}
