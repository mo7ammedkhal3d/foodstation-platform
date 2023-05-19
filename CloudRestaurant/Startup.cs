using Microsoft.Extensions.DependencyInjection;
using FOODSTATION.Models;
using FOODSTATION.Models.Repositories;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FOODSTATION.Startup))]
namespace FOODSTATION
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
