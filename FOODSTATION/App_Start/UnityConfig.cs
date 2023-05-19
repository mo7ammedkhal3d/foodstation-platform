using FOODSTATION.Models.Repositories;
using FOODSTATION.Models;
using System.Web.Mvc;
using System.Data.Entity;
using Unity;
using Unity.Mvc5;
using FOODSTATION.Controllers;
using Unity.Injection;
using Unity.Lifetime;

namespace FOODSTATION
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IFOODSTATIONRepository<Country>, CountryRepository>();
            container.RegisterType<IFOODSTATIONRepository<Region>, RegionRepository>();
            container.RegisterType<IFOODSTATIONRepository<DiningType>, DiningTypeRepository>();
            container.RegisterType<IFOODSTATIONRepository<Restaurant>, RestaurantRepository>();
            container.RegisterType<IFOODSTATIONRepository<Category>, CategoryRepository>();
            container.RegisterType<IFOODSTATIONRepository<Item>, ItemRepository>();
            container.RegisterType<IFOODSTATIONRepository<Request>, RequestRepository>();
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