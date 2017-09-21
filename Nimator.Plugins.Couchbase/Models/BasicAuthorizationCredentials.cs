using System;
using System.Text;

namespace Nimator.Plugins.Couchbase.Models
{
    public class BasicAuthorizationCredentials
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public bool NotEmpty => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);

        public string Base64String
        {
            get
            {
                var bytes = Encoding.ASCII.GetBytes($"{Username}:{Password}");
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
