using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Capentry.Startup))]
namespace Capentry
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
