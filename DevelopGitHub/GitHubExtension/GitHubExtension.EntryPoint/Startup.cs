using System;
using System.Data.Entity;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

using FluentValidation.WebApi;

using GitHubExtension.EntryPoint;
using GitHubExtension.Infrastructure.Extensions;
using GitHubExtension.Security.DAL.Context;
using GitHubExtension.Security.DAL.Infrastructure;
using GitHubExtension.Security.DAL.Migrations;
using GitHubExtension.Security.WebApi.ActionFilters;
using GitHubExtension.Security.WebApi.Provider;
using GitHubExtension.Security.WebApi.Validators.ValidatorFactories;

using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;

using Newtonsoft.Json.Serialization;

using Owin;
using Owin.Security.Providers.GitHub;

using SimpleInjector.Integration.WebApi;

[assembly: OwinStartup(typeof(Startup))]

namespace GitHubExtension.EntryPoint
{
    public class Startup
    {
        public static GitHubAuthenticationOptions GitHubAuthOptions { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            var container = SimpleInjectorConfiguration.ConfigurationSimpleInjector();

            app.UseSimpleInjectorContext(container);
            app.CreatePerOwinContext(container.GetInstance<SecurityContext>);
            app.CreatePerOwinContext(container.GetInstance<ApplicationUserManager>);

            ConfigureOauth(app);
            ConfigureCookies(app);

            var config = ConfigureWebApi();
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            config.EnsureInitialized();

            FluentValidationModelValidatorProvider.Configure(
                config, 
                provider => provider.ValidatorFactory = new FluentValidatorFactory(container));

            app.UseWebApi(config);

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SecurityContext, Configuration>());
        }

        void ConfigureCookies(IAppBuilder app)
        {
            app.UseCookieAuthentication(
                new CookieAuthenticationOptions
                {
                    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                    AuthenticationMode = AuthenticationMode.Active,
                    CookieHttpOnly = false
                });
        }

        void ConfigureOauth(IAppBuilder app)
        {
            // use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            OAuthAuthorizationServerOptions oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);

            GitHubAuthOptions = new GitHubAuthenticationOptions()
            {
                ClientId = "eea12517d7846310f98b",
                ClientSecret = "7652a3a398fe09eedaa1a19ae749db7b1bfa85ab",
                Provider = new GitHubAuthProvider()
            };

            GitHubAuthOptions.Scope.Add("user,repo");
            app.UseGitHubAuthentication(GitHubAuthOptions);
        }

        public HttpConfiguration ConfigureWebApi()
        {
            HttpConfiguration config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonFormatter.SerializerSettings.PreserveReferencesHandling =
                Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Filters.Add(new LoggingFilterAttribute());
            config.Filters.Add(new ModelIsNullFilterAttribute());

            return config;
        }
    }
}