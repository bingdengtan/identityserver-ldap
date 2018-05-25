using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Novell.Directory.Ldap;

namespace JEIdentityService
{
    public class JEUser
    {
        public string SubjectId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string ProviderName { get; set; }
        public string Password { get; set; }
        public string Location { get; set; }

        public void SetBaseDetails(LdapEntry ldapEntry, string providerName)
        {
            DisplayName = ldapEntry.getAttribute("displayname").StringValue;
            UserName = ldapEntry.getAttribute("cn").StringValue;
            Email = ldapEntry.getAttribute("mail").StringValue;
            Location = ldapEntry.getAttribute("location").StringValue;
            ProviderName = providerName;
            SubjectId = UserName;
        }
    }
}
