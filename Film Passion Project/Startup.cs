using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Film_Passion_Project.Startup))]
namespace Film_Passion_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
