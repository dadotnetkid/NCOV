using Microsoft.Owin;
using Models.Startups;
using Owin;
using Web;

[assembly: OwinStartup(typeof(Startup))]
namespace Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Authentication.ConfigureAuth(app);
        }
    }
}
