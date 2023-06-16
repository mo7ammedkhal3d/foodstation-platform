using FOODSTATION.Models;
using FOODSTATION.Models.ViewModels;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Web;
using System.Web.Mvc;

namespace FOODSTATION.Controllers
{
    public class HomeController : Controller
    {
       static List<VirtualBill> products = new List<VirtualBill>();
        private readonly ApplicationDbContext db;

        public HomeController(ApplicationDbContext _db)
        {
            db = _db;
        }

        public ActionResult Index()
        {
            products.Clear();
            return View();
        }

        public ActionResult GetRegions()
        {
            ViewBag.regions = db.Regions.ToList();

            return View();
        }

        public ActionResult GetRegionRestaurants(int? id)
        {
            products.Clear();
            if(id != null)
            {
                Session["RegionId"] = id;
                return View("GetRestaurants", db.Restaurants.ToList().Where(x => x.RegionId == id));
            }

            else if(Session["RegionId"] != null)
            {
                return View("GetRestaurants", db.Restaurants.ToList().Where(x => x.RegionId == Convert.ToInt32(Session["RegionId"])));
            }
            else return View("GetRestaurants", db.Restaurants.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult AddBillAndItems()
        {
            var NewBill = new Bill { Date = DateTime.Now, UserId = User.Identity.GetUserId() };
            foreach(var item in products)
            {              
                NewBill.Items.Add(db.Items.Find(item.ItemId));
            }
   
            db.Bills.Add(NewBill);
            db.SaveChanges();
            return RedirectToAction("Index");

           
        }

        [Authorize(Roles = "RestaurantOwner")]
        public ActionResult RestaurantWonerRequests()
        {
            var user =  User.Identity.GetUserId();
            var bills = db.Bills.ToList();
           
            
            return View(bills);
        }
        [Authorize]
        public ActionResult MyRequsts()
        {
            var id = User.Identity.GetUserId();
            var bills = db.Bills.Where(x=>x.UserId==id).ToList();


            return View(bills);
        }

        public PartialViewResult Refreash()
        {
            ViewBag.products = products;
            return PartialView("_BillPartial",products);
        }

        public ActionResult GetRestaurantCategories(int? id)
        {
            Session["RestaurantId"] = id;
            var Categories = from item in db.Items.ToList()
                       join category in db.Categories.ToList() on item.CategoryId equals category.Id
                       where item.RestaurantId == id
                       select category;

            ViewBag.items = db.Items.ToList().Where(x => x.RestaurantId == id).ToList();
            var Restaurant = db.Restaurants.ToList().Where(x => x.Id == id).Single();
            ViewBag.Name = Restaurant.Name;
            ViewBag.restaurantId = Restaurant.Id;
            return View(Categories.Distinct().ToList());
        }

       
       public  PartialViewResult GetCategoryItems(int restaurantId, int categoryId)
        {
            var items = db.Items.ToList().Where(x => x.CategoryId == categoryId && x.RestaurantId == restaurantId).ToList();
            ViewBag.items = items;  
            return PartialView("_SelectedCategoryItems", items);
        }

        public PartialViewResult GetAllRestaurantItems(int restaurantId)
        {
            var items = db.Items.ToList().Where(x =>x.RestaurantId == restaurantId).ToList();
            ViewBag.items = items;
            return PartialView("_SelectedCategoryItems", items);
        }


        public ActionResult AddToBill(int? id)
        {      
            var item = products.Find(x=> x.ItemId ==id);
            if(item == null)
            {
                var product = db.Items.Find(id);
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
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}