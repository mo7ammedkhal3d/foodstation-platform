using CloudRestaurant.Models;
using CloudRestaurant.Models.Repositories;
using CloudRestaurant.Models.ViewModels;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CloudRestaurant.Controllers
{
    public class HomeController : Controller
    {
        static List<VirtualBill> products = new List<VirtualBill>();
        private readonly ICloudRestaurantRepository<Item> itemRepository;
        private readonly ICloudRestaurantRepository<Restaurant> restaurantRepository;
        private readonly ICloudRestaurantRepository<Category> categoryRepository;
        private readonly ICloudRestaurantRepository<Request> requestRepository;
        private readonly ICloudRestaurantRepository<Region> regionRepository;
        private readonly ICloudRestaurantRepository<Country> countryRepository;

        public HomeController(ICloudRestaurantRepository<Item> itemRepository,
            ICloudRestaurantRepository<Restaurant> restaurantRepository, ICloudRestaurantRepository<Category> categoryRepository,
            ICloudRestaurantRepository<Request> requestRepository , ICloudRestaurantRepository<Region> regionRepository,
            ICloudRestaurantRepository<Country> countryRepository)
        {
            this.itemRepository = itemRepository;
            this.restaurantRepository = restaurantRepository;
            this.categoryRepository = categoryRepository;
            this.requestRepository = requestRepository;
            this.regionRepository = regionRepository;
            this.countryRepository = countryRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetRegions()
        {
            return View(regionRepository.List());
        }


        //public JSON GetSelectedRegion(int id)
        //{
        //    Session["RegionId"] = id;

        //    return Json(Response, JsonRequestBehavior.AllowGet);
        //}


        public ActionResult GetRestaurants()
        {
            return View(restaurantRepository.List());
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
           
        }

        public PartialViewResult Refreash()
        {
            ViewBag.products = products;
            return PartialView("_BillPartial",products);
        }

        public ActionResult GetRestaurantCategories(int? id)
        {
            Session["RestaurantId"] = id;
            var Categories = from item in itemRepository.List()
                       join category in categoryRepository.List() on item.CategoryId equals category.Id
                       where item.RestaurantId == id
                       select category;

            ViewBag.items = itemRepository.List().Where(x => x.RestaurantId == id).ToList();
            var Restaurant = restaurantRepository.List().Where(x => x.Id == id).Single();
            ViewBag.Name = Restaurant.Name;
            ViewBag.restaurantId = Restaurant.Id;
            return View(Categories.Distinct().ToList());
        }

       
       public  PartialViewResult GetCategoryItems(int restaurantId, int categoryId)
        {
            var items = itemRepository.List().Where(x => x.CategoryId == categoryId && x.RestaurantId == restaurantId).ToList();
            ViewBag.items = items;  
            return PartialView("_SelectedCategoryItems", items);
        }

        public PartialViewResult GetAllRestaurantItems(int restaurantId)
        {
            var items = itemRepository.List().Where(x =>x.RestaurantId == restaurantId).ToList();
            ViewBag.items = items;
            return PartialView("_SelectedCategoryItems", items);
        }


        public ActionResult AddToBill(int? id)
        {      
            var item = products.Find(x=> x.ItemId ==id);
            if(item == null)
            {
                var product = itemRepository.Find(id);
                VirtualBill Item = new VirtualBill { ItemId = product.Id,ItemName=product.Name,
                    ItemQuantity = 1,ItemPrice =product.Price};
                products.Add(Item);
            }
            else
            {
                item.ItemQuantity = item.ItemQuantity + 1;
            }
            

            return Json(true, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetBill()
        {
            ViewBag.Restaurant = Session["RestaurantId"];
            ViewBag.products = products;
            return View(products.ToList());
        }

        public ActionResult DeleteItemFromBill(int id)
        {
            var item =products.Find(x=> x.ItemId == id);
            products.Remove(item);
            return RedirectToAction("GetBill");
        }

        public ActionResult IncreasQuantity(int id)
        {
            var item = products.Find(x => x.ItemId == id);
            item.ItemQuantity = item.ItemQuantity + 1;
            return Json(true,JsonRequestBehavior.AllowGet);
        }

        public ActionResult DecreasQuantity(int id)
        {
            var item = products.Find(x => x.ItemId == id);
            item.ItemQuantity = item.ItemQuantity - 1;
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                restaurantRepository.Dispose();
                itemRepository.Dispose();
                categoryRepository.Dispose();
                regionRepository.Dispose();
                requestRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}