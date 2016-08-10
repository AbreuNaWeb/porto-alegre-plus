using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PortoAlegrePlus.Startup))]
namespace PortoAlegrePlus
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
