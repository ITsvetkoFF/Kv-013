using System;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Owin.Security.Providers.GitHub;
using GitHubExtension.Security.DAL.Context;
using GitHubExtension.IoCManager;
using GitHubExtension.Security.DAL.Migrations;
using GitHubExtension.Security.WebApi.Library.Services;
using GitHubExtension.WebApi.Providers;


[assembly: OwinStartup(typeof(GitHubExtension.WebApi.Startup))]

namespace GitHubExtension.WebApi
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
            HttpConfiguration config = new HttpConfiguration();

            ConfigureOAuth(app);

            WebApiConfig.Register(config);
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SecurityContext, Configuration>());
        }

        public void ConfigureOAuth(IAppBuilder app)
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
        }
    }

}