using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using AluminiWebApp.Models;
using Microsoft.Owin.Security.Facebook;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Owin.Security;
using Owin.Security.Providers.LinkedIn;

namespace AluminiWebApp
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "891861927596279",
            //   appSecret: "aad6cb03fd28fef2b42d68fbb1d32eb6");

            //app.SetDefaultSignInAsAuthenticationType("ExternalCookie");
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = "ExternalCookie",
            //    AuthenticationMode = AuthenticationMode.Passive,
            //});
            app.UseFacebookAuthentication(new FacebookAuthenticationOptions
            {
                AppId = "881522805296858",
                AppSecret = "2f684709a9941475e25bc3657754241a",
                Scope = { "email" },
                BackchannelHttpHandler = new FacebookBackChannelHandler(),
                UserInformationEndpoint = "https://graph.facebook.com/v2.4/me?fields=id,name,email,first_name,last_name,location",
                Provider = new FacebookAuthenticationProvider
                {
                    OnAuthenticated = context =>
                    {
                        context.Identity.AddClaim(new System.Security.Claims.Claim("FacebookAccessToken", context.AccessToken));
                        return Task.FromResult(true);
                    }
                }

            });
            //app.UseLinkedInAuthentication("75pbqe8ynp8ror", "s75PeQB8PjHIOE05");
            var options = new LinkedInAuthenticationOptions
            {
                ClientId = "75wiw6rws21340",
                ClientSecret = "0xRiBDQmiYrvt1Hp",
            };           
            app.UseLinkedInAuthentication(options);
            //var facebookOptions = new FacebookAuthenticationOptions()
            //{
            //    AppId = "881522805296858",
            //    AppSecret = "2f684709a9941475e25bc3657754241a",
            //    BackchannelHttpHandler = new FacebookBackChannelHandler(),
            //    UserInformationEndpoint = "https://graph.facebook.com/v2.4/me?fields=id,name,email,first_name,last_name,location"
            //};
            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "148671946656-fh3qu8mt23bbjquqm9g6vaobem0ph85d.apps.googleusercontent.com",
            //    ClientSecret = "opA9C1Q7z7ws9q-u1E8QOFK6"
            //});
        }
    }
    public class FacebookBackChannelHandler : HttpClientHandler
    {
        protected override async System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            // Replace the RequestUri so it's not malformed
            if (!request.RequestUri.AbsolutePath.Contains("/oauth"))
            {
                request.RequestUri = new Uri(request.RequestUri.AbsoluteUri.Replace("?access_token", "&access_token"));
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}