using System;
using System.Text;

namespace Nimator.Plugins.Couchbase.Models
{
    public class BasicAuthorizationCredentials
    {
        private string _base64String;

        public string Username { get; set; }

        public string Password { get; set; }

        public bool NotEmpty => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);

        public string Base64String
        {
            get
            {
                if (string.IsNullOrEmpty(_base64String))
                {
                    var bytes = Encoding.ASCII.GetBytes($"{Username}:{Password}");
                    _base64String = Convert.ToBase64String(bytes);
                }

                return _base64String;
            }
        }
    }
}
