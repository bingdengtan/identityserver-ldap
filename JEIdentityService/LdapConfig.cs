using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JEIdentityService
{
    public class LdapConfig
    {
        public LdapConfig() { }

        public string Url { get; set; }
        public int Port { get; set; }
        public bool Ssl { get; set; }
        public string BindDn { get; set; }
        public string BindCredentials { get; set; }
        public string SearchBase { get; set; }
        public string SearchFilter { get; set; }
        public string Redis { get; set; }
        public int RefreshClaimsInSeconds { get; set; }
        public string[] ExtraAttributes { get; set; }
    }
}
