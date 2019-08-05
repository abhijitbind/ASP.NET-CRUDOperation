using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CRUDAssignment.Startup))]
namespace CRUDAssignment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
