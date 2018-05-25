using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JEIdentityService
{
    public class UserRepository : IUserRepository
    {
        private LdapService ldapService;

        public UserRepository()
        {
            ldapService = new LdapService();
        }

        // some dummy data. Replce this with your user persistence. 
        private readonly List<JEUser> _users = new List<JEUser>
        {
            new JEUser{
                SubjectId = "123",
                UserName = "damienbod",
                Password = "damienbod",
                Email = "damienbod@email.ch"
            },
            new JEUser{
                SubjectId = "124",
                UserName = "raphael",
                Password = "raphael",
                Email = "raphael@email.ch"
            },
        };

        public JEUser FindBySubjectId(string subjectId)
        {
            //return _users.FirstOrDefault(x => x.SubjectId == subjectId);
            return ldapService.FindUser(subjectId);
        }

        public JEUser FindByUsername(string username)
        {
            //return _users.FirstOrDefault(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
            return ldapService.FindUser(username);
        }

        public bool ValidateCredentials(string username, string password)
        {
            //var user = FindByUsername(username);
            var user = ldapService.Login(username, password);
            return user != null;
        }
    }
}
