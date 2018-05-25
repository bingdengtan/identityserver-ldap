using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Novell.Directory.Ldap;

namespace JEIdentityService
{
    public class LdapService
    {
        private LdapConfig _config;
        private LdapConnection _ldapConnection;

        public LdapService()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfiguration configuration = builder.Build();

            _config = new LdapConfig();
            configuration.GetSection("ldap").Bind(_config);

            _ldapConnection = new LdapConnection
            {
                SecureSocketLayer = _config.Ssl
            };

        }

        public JEUser Login(string username, string password)
        {
            var searchResult = SearchUser(username);

            if (searchResult.hasMore())
            {
                try
                {
                    var user = searchResult.next();
                    if (user != null)
                    {
                        _ldapConnection.Bind(user.DN, password);
                        if (_ldapConnection.Bound)
                        {
                            var appUser = new JEUser();
                            appUser.SetBaseDetails(user, "LDAP"); // Should we change to LDAP.
                            _ldapConnection.Disconnect();

                            return appUser;
                        }
                    }
                }
                catch (Exception e)
                {
                    System.Console.Write(e);
                }
            }

            _ldapConnection.Disconnect();

            return default(JEUser);
        }

        public JEUser FindUser(string username)
        {
            var searchResult = SearchUser(username);

            try
            {
                var user = searchResult.next();
                if (user != null)
                {
                    var appUser = new JEUser();
                    appUser.SetBaseDetails(user, "LDAP");
                    _ldapConnection.Disconnect();
                    return appUser;
                }
            }
            catch (Exception e)
            {
                System.Console.Write(e);
            }

            _ldapConnection.Disconnect();

            return default(JEUser);
        }

        private int LdapPort
        {
            get
            {
                if (_config.Port == 0)
                {
                    return _config.Ssl ? LdapConnection.DEFAULT_SSL_PORT : LdapConnection.DEFAULT_PORT;
                }

                return _config.Port;
            }
        }

        private LdapSearchResults SearchUser(string username)
        {
            _ldapConnection.Connect(_config.Url, LdapPort);
            _ldapConnection.Bind(_config.BindDn, _config.BindCredentials);
            string[] attributes = _config.ExtraAttributes;
            var searchFilter = string.Format(_config.SearchFilter, username);
            var result = _ldapConnection.Search(
                _config.SearchBase,
                LdapConnection.SCOPE_SUB,
                searchFilter,
                attributes,
                false
            );

            return result;
        }
    }
}
