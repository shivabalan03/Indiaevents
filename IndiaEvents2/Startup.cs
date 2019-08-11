using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IndiaEvents2.Startup))]
namespace IndiaEvents2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
