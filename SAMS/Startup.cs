using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SAMS.Startup))]
namespace SAMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
