using CloudRestaurant.Models.Repositories;
using CloudRestaurant.Models;
using System.Web.Mvc;
using System.Data.Entity;
using Unity;
using Unity.Mvc5;
using CloudRestaurant.Controllers;
using Unity.Injection;
using Unity.Lifetime;

namespace CloudRestaurant
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<ICloudRestaurantRepository<Country>, CountryRepository>();
            container.RegisterType<ICloudRestaurantRepository<Region>, RegionRepository>();
            container.RegisterType<ICloudRestaurantRepository<DiningType>, DiningTypeRepository>();
            container.RegisterType<ICloudRestaurantRepository<Restaurant>, RestaurantRepository>();
            container.RegisterType<ICloudRestaurantRepository<Category>, CategoryRepository>();
            container.RegisterType<ICloudRestaurantRepository<Item>, ItemRepository>();
            container.RegisterType<ICloudRestaurantRepository<Request>, RequestRepository>();
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());
            container.RegisterType<DbContext, ApplicationDbContext>(new HierarchicalLifetimeManager());

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}