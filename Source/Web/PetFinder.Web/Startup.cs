using Microsoft.Owin;

using Owin;

[assembly: OwinStartupAttribute(typeof(PetFinder.Web.Startup))]

namespace PetFinder.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
