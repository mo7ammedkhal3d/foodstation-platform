using Microsoft.Extensions.DependencyInjection;
using CloudRestaurant.Models;
using CloudRestaurant.Models.Repositories;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CloudRestaurant.Startup))]
namespace CloudRestaurant
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
