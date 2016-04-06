using System;
using System.Data.Entity;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using FluentValidation.WebApi;
using GitHubExtension.Security.DAL.Context;
using GitHubExtension.Security.DAL.Infrastructure;
using GitHubExtension.Security.StorageModels.Identity;
using GitHubExtension.Security.WebApi.Library;
using GitHubExtension.Security.WebApi.Library.ActionFilters;
using GitHubExtension.Security.WebApi.Library.Provider;
using GitHubExtension.Security.WebApi.Library.Validators.ValidatorFactories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Owin;
using Owin.Security.Providers.GitHub;
using SimpleInjector.Integration.WebApi;

[assembly: OwinStartup(typeof(GitHubExtension.Security.WebApi.Startup))]
namespace GitHubExtension.Security.WebApi
{
    public class Startup
    {
        public static GitHubAuthenticationOptions GitHubAuthOptions { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            var container = SimpleInjectorConfiguration.ConfigurationSimpleInjector();
            ConfigureOAuth(app);

            #region config for http
            var config = ConfigureWebApi();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                AuthenticationMode =  AuthenticationMode.Active
            });

            #endregion

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            config.EnsureInitialized();

            app.CreatePerOwinContext(() => container.GetInstance<SecurityContext>());

            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            //FluentValidationModelValidatorProvider
            //    .Configure(config,
            //        provider => provider.ValidatorFactory = new FluentValidatorFactory(container));

            app.UseWebApi(config);

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SecurityContext, DAL.Migrations.Configuration>());
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

        private HttpConfiguration ConfigureWebApi()
        {
            HttpConfiguration config = new HttpConfiguration();
          
            config.MapHttpAttributeRoutes();           

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Filters.Add(new LoggingFilterAttribute());

            // Configure authorization attribute
            config.Filters.Add(new AuthorizeAttribute());

            return config;
        }

        void ConfigureOAuth(IAppBuilder app)
        {
            //use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);

            GitHubAuthOptions = new GitHubAuthenticationOptions()
            {
                ClientId = "eea12517d7846310f98b",
                ClientSecret = "7652a3a398fe09eedaa1a19ae749db7b1bfa85ab",
                Provider = new GitHubAuthProvider()
            };

            GitHubAuthOptions.Scope.Add("user,repo");
            app.UseGitHubAuthentication(GitHubAuthOptions);
        }
    }
}