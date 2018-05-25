using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JEIdentityService
{
    public interface IUserRepository
    {
        bool ValidateCredentials(string username, string password);

        JEUser FindBySubjectId(string subjectId);

        JEUser FindByUsername(string username);
    }
}
