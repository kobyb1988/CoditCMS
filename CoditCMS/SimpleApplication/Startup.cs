using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimpleApplication.Startup))]
namespace SimpleApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
