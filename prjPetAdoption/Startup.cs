using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(prjPetAdoption.Startup))]
namespace prjPetAdoption
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
