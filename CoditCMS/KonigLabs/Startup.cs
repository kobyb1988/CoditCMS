using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KonigLabs.Startup))]
namespace KonigLabs
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
