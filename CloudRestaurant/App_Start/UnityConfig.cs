using CloudRestaurant.Models.Repositories;
using CloudRestaurant.Models;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using CloudRestaurant.Controllers;
using Unity.Injection;

namespace CloudRestaurant
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<ICloudRestaurantRepository<Restaurant>, RestaurantRepository>();
            container.RegisterType<ICloudRestaurantRepository<Category>, CategoryRepository>();
            container.RegisterType<ICloudRestaurantRepository<Item>, ItemRepository>();
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}