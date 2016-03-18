using System;
using System.Data.Entity;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using GitHubExtension.IoCManager;
using GitHubExtension.Security.DAL.Context;
using GitHubExtension.Security.DAL.Infrastructure;
using GitHubExtension.Security.DAL.Migrations;
using GitHubExtension.Security.StorageModels.Identity;
using GitHubExtension.Security.WebApi.Library.Provider;
using GitHubExtension.Security.WebApi.Library.Services;
using GitHubExtension.WebApi.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Owin;
using Owin.Security.Providers.GitHub;

namespace GitHubExtension.Security.WebApi.Library
{
    public class Startup
    {
        private readonly AuthService _authService;

        public Startup(AuthService authService)
        {
            _authService = authService;
        }

        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public static GitHubAuthenticationOptions gitHubAuthOptions { get; private set; }


        public void Configuration(IAppBuilder app)
        {
            //use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {

                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new SimpleAuthorizationServerProvider(_authService),
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);

            gitHubAuthOptions = new GitHubAuthenticationOptions()
            {
                ClientId = "c04e00dfe8db05cf8807",
                ClientSecret = "eed4fa1adf3f8e8633919df736bf51c750471dab",
                Provider = new GitHubAuthProvider()
            };

            gitHubAuthOptions.Scope.Add("user,repo");
            app.UseGitHubAuthentication(gitHubAuthOptions);

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorDependencyResolver(DependencyConfig.BuildContainer());//register container

            GlobalConfiguration.Configure(WebApiConfig.Register);



            HttpConfiguration httpConfig = new HttpConfiguration();
            app.CreatePerOwinContext(SecurityContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<SecurityRoleManager>(SecurityRoleManager.Create);

            ConfigureWebApi(httpConfig);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = GetMyCookieAuthenticationProvider(),
            });
            app.UseWebApi(httpConfig);

            HttpConfiguration config = new HttpConfiguration();

            WebApiConfig.Register(config);
            app.UseWebApi(config);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SecurityContext, Configuration>());

        }

        private static CookieAuthenticationProvider GetMyCookieAuthenticationProvider()
        {
            var cookieAuthenticationProvider = new CookieAuthenticationProvider
            {
                OnValidateIdentity = async context =>
                {
                    // execute default cookie validation function
                    var cookieValidatorFunc = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, User>(
                        TimeSpan.FromMinutes(10),
                        (manager, user) =>
                        {
                            var identity = manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                            return identity;
                        });
                    await cookieValidatorFunc.Invoke(context);

                    // sanity checks
                    if (context.Identity == null || !context.Identity.IsAuthenticated)
                    {
                        return;
                    }

                    var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
                    var claimsToAdd = await userManager.GetClaimsAsync(context.Identity.GetUserId());

                    // get your claim from your DB or other source
                    context.Identity.AddClaims(claimsToAdd);
                }
            };
            return cookieAuthenticationProvider;
        }

        private void ConfigureWebApi(HttpConfiguration config)
        {



            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            config.MapHttpAttributeRoutes();
            
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}