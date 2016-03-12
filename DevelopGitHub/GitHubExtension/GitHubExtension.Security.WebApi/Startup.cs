using GithubExtension.Security.DAL.Context;
using Microsoft.Owin;
using Newtonsoft.Json.Serialization;
using Owin;
using System;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using GitHubExtension.Security.DAL.Entities;
using GitHubExtension.Security.DAL.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;
namespace GithubExtension.Security.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration httpConfig = new HttpConfiguration();
            app.CreatePerOwinContext(SecurityContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<SecurityRoleManager>(SecurityRoleManager.Create);

            //ConfigureOAuthTokenGeneration(app);

            //ConfigureOAuthTokenConsumption(app);

            ConfigureWebApi(httpConfig);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = GetMyCookieAuthenticationProvider(),
            });
            app.UseWebApi(httpConfig);

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

        //private void ConfigureOAuthTokenConsumption(IAppBuilder app)
        //{

        //    var issuer = "http://localhost:59822";
        //    string audienceId = ConfigurationManager.AppSettings["as:AudienceId"];
        //    byte[] audienceSecret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["as:AudienceSecret"]);

        //    // Api controllers with an [Authorize] attribute will be validated with JWT
        //    app.UseJwtBearerAuthentication(
        //        new JwtBearerAuthenticationOptions
        //        {
        //            AuthenticationMode = AuthenticationMode.Active,
        //            AllowedAudiences = new[] {audienceId},
        //            IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
        //            {
        //                new SymmetricKeyIssuerSecurityTokenProvider(issuer, audienceSecret)
        //            }
        //        });
        //}

        //private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        //{
        //    // Configure the db context and user manager to use a single instance per request



        //    OAuthAuthorizationServerOptions oAuthServerOptions = new OAuthAuthorizationServerOptions()
        //    {
        //        //For Dev enviroment only (on production should be AllowInsecureHttp = false)
        //        AllowInsecureHttp = true,
        //        TokenEndpointPath = new PathString("/oauth/token"),
        //        AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
        //        Provider = new CustomOAuthProvider(),
        //        AccessTokenFormat = new CustomJwtFormat("http://localhost:59822")
        //    };

        //    // OAuth 2.0 Bearer Access Token Generation
        //    app.UseOAuthAuthorizationServer(oAuthServerOptions);
        //}

        private void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}