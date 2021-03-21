using Microsoft.Owin;
using Owin;
using University.Models;

[assembly: OwinStartupAttribute(typeof(University.Startup))]
namespace University
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
