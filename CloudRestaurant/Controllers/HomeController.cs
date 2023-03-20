using CloudRestaurant.Models;
using CloudRestaurant.Models.Repositories;
using CloudRestaurant.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CloudRestaurant.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        static List<VirtualBill> products = new List<VirtualBill>();
        private readonly ICloudRestaurantRepository<Item> itemRepository;
        private readonly ICloudRestaurantRepository<Restaurant> restaurantRepository;
        private readonly ICloudRestaurantRepository<Category> categoryRepository;
        private readonly ICloudRestaurantRepository<Request> requestRepository;

        public HomeController(ICloudRestaurantRepository<Item> itemRepository,
            ICloudRestaurantRepository<Restaurant> restaurantRepository, ICloudRestaurantRepository<Category> categoryRepository,
            ICloudRestaurantRepository<Request> requestRepository)
        {
            this.itemRepository = itemRepository;
            this.restaurantRepository = restaurantRepository;
            this.categoryRepository = categoryRepository;
            this.requestRepository = requestRepository;
        }


        public ActionResult Index()
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
            var Categories = from item in db.Items
                       join categ in db.Categories on item.CategoryId equals categ.Id
                       where item.RestaurantId == id
                       select categ;

            ViewBag.items = db.Items.Where(x => x.RestaurantId == id).ToList();
            var Resturant = db.Restaurants.Where(x => x.Id == id).Single();
            ViewBag.Name = Resturant.Name;
            ViewBag.restaurantId = Resturant.Id;
            return View(Categories.Distinct().ToList());
        }


       public  PartialViewResult GetCategoryItems(int ResturantId,int categoryId)
        {
            var items = db.Items.Where(x => x.CategoryId == categoryId && x.RestaurantId == ResturantId).ToList();
            ViewBag.items = items;  
            return PartialView("_SelectedCategoryItems", items);
        }

        public PartialViewResult GetAllResturantAitems(int ResturantId)
        {
            var items = db.Items.Where(x =>x.RestaurantId == ResturantId).ToList();
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
            ViewBag.Restuarant = Session["RestaurantId"];
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
        public ActionResult decreasQuantity(int id)
        {
            var item = products.Find(x => x.ItemId == id);
            item.ItemQuantity = item.ItemQuantity - 1;
            return RedirectToAction("Index");
        }
    }
}