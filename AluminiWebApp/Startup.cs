using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AluminiWebApp.Startup))]
namespace AluminiWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
