using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using quickStart;

namespace JEIdentityService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddCors();

            var builder = services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddCustomUserStore();

            //services.AddAuthentication()
            //  .AddOpenIdConnect("demoidsrv", "IdentityServer", options =>
            //  {
            //      options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
            //      options.SignOutScheme = IdentityServerConstants.SignoutScheme;

            //      options.Authority = "https://demo.identityserver.io/";
            //      options.ClientId = "implicit";
            //      options.ResponseType = "id_token";
            //      options.SaveTokens = true;
            //      options.CallbackPath = new PathString("/signin-idsrv");
            //      options.SignedOutCallbackPath = new PathString("/signout-callback-idsrv");
            //      options.RemoteSignOutPath = new PathString("/signout-idsrv");

            //      options.TokenValidationParameters = new TokenValidationParameters
            //      {
            //          NameClaimType = "name",
            //          RoleClaimType = "role"
            //      };
            //  });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
                builder.WithOrigins("*")
                       .AllowAnyHeader()
                );

            app.UseIdentityServer();

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
