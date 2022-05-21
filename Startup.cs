using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AthonEventos.Startup))]
namespace AthonEventos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
