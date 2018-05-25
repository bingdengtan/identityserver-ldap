using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace JEIdentityService
{
    public static class JEIdentityServerBuilderExtensions
    {
        public static IIdentityServerBuilder AddCustomUserStore(this IIdentityServerBuilder builder)
        {
            builder.Services.AddSingleton<IUserRepository, UserRepository>();
            builder.AddProfileService<JEProfileService>();
            builder.AddResourceOwnerValidator<JEResourceOwnerPasswordValidator>();

            return builder;
        }
    }
}
