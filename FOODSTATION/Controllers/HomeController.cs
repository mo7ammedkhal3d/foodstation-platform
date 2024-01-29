using FOODSTATION.Models;
using FOODSTATION.Models.ViewModels;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Web;
using System.Web.Mvc;
using FOODSTATION.algorithms;

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
            ViewBag.adv = db.Advertisements.ToList();
            products.Clear();
              ViewBag.participations = db.Participations.ToList();

            return View();
        }

        public ActionResult GetRegions()
        {
            ViewBag.regions = db.Regions.ToList();

            return View();
        }

        [HttpPost]
        public PartialViewResult GetNearbyRestaurants(double lon, double lat)
        {
            var regions = db.Regions.ToList();
            double[,] pairs = new double[regions.Count, 2];

            for (int i = 0; i < regions.Count; i++)
            {
                pairs[i, 0] = (double)regions[i].Latitude;
                pairs[i, 1] = (double)regions[i].Longitude;
            }

            double[] nearestPair = LocationUtils.Nearest(lat, lon, pairs);
            var region = regions.Where(x => x.Latitude == (decimal)nearestPair[0] && x.Longitude == (decimal)nearestPair[1]).FirstOrDefault();

            var regionRestaurants = db.Restaurants.Where(x => x.RegionId == region.Id);
            ViewBag.regionRestaurants = regionRestaurants;
            ViewBag.regionLng = lon;
            ViewBag.regionLat = lat;

            return PartialView("_PartialGetNearbyresaurants", regionRestaurants);
        }

        public ActionResult GetRegionRestaurants(int? id)
        {
            products.Clear();
            if (id != null)
            {
                Session["RegionId"] = id;
                var region = db.Regions.FirstOrDefault(x => x.Id == id);
                ViewBag.regionLng = region.Longitude;
                ViewBag.regionlat = region.Latitude;
                ViewBag.regionRestaurants = db.Restaurants.Where(x => x.RegionId == id).ToList();
                return View("GetRestaurants");
            }

            else if (Session["RegionId"] != null)
            {
                var regionId = (int)Session["RegionId"];
                var region = db.Regions.FirstOrDefault(x => x.Id == regionId);
                ViewBag.regionLng = region.Longitude;
                ViewBag.regionLat = region.Latitude;
                ViewBag.regionRestaurants = db.Restaurants.Where(x => x.RegionId == regionId).ToList();
                return View("GetRestaurants");
            }

            else
            {
                ViewBag.regionRestaurants = db.Restaurants.ToList();
                return View("GetRestaurants");
            }
        }

        [HttpPost]
        public PartialViewResult ShowBillItems(int id)
        {
            var billDetailes = new BillDetailesVM();
            var bill = db.Bills.Find(id);
            billDetailes.UserName = db.Users.Where(x=>x.Id==bill.UserId).FirstOrDefault().UserName;
            billDetailes.UserPhoneNumber = db.Users.Where(x => x.Id == bill.UserId).FirstOrDefault().PhoneNumber;
            billDetailes.Date = bill.Date;
            billDetailes.DiningType = bill.DiningType.Name;
            billDetailes.Location = bill.Location;  
            if (bill != null)
            {
                var query = from bi in db.BillItems
                            join i in db.Items on bi.ItemId equals i.Id
                            join r in db.Restaurants on i.RestaurantId equals r.Id
                            where bi.BillId == bill.Id
                            select new ItemDetailesVM
                            {
                                ItemName = i.Name,
                                Quantity = bi.Quantity,
                                RestaurantName = r.Name,
                                Price = bi.Quantity*i.Price,                             
                            };
                billDetailes.ItemDetails = query.ToList();

                ViewBag.billDetailes = billDetailes;  

                return PartialView("_PartialBillItem", billDetailes);
            }
            return PartialView("PartialBillItem");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult AddBillAndItems(int diningTypeId , string location)
        {
            var NewBill = new Bill { Date = DateTime.Now, UserId = User.Identity.GetUserId() , DiningTypeId=diningTypeId, Location=location};
            foreach(var item in products)
            {
                var BillItem = new BillItems();
                BillItem.BillId = NewBill.Id;
                BillItem.ItemId = item.ItemId;
                BillItem.Quantity = item.ItemQuantity;
                db.BillItems.Add(BillItem);
            } 
            db.Bills.Add(NewBill);
            db.SaveChanges();
            return RedirectToAction("Index");
          
        }

        [Authorize(Roles = "RestaurantOwner")]
        public ActionResult RestaurantWonerRequests()
        {
            var userId = User.Identity.GetUserId();

            var bills = db.Bills.Where(b => b.Items.Any(i => i.Item.Restaurant.UserId == userId)).ToList();

            var restaurantsList = new SelectList(db.Restaurants.Where(r => r.UserId == userId).ToList(), "Id", "Name");

            var selectall = new SelectListItem
            {
                Text = "عرض الكل",
                Value = "00"
            };

            var updatedRestaurantsList = new List<SelectListItem>(restaurantsList)
            {
                selectall
            };

            ViewBag.Restaurants = new SelectList(updatedRestaurantsList, "Id", "Name");

            return View(bills);
        }  


        [Authorize]
        public ActionResult MyRequsts()
        {
            var id = User.Identity.GetUserId();
            var bills = db.Bills.Where(x=>x.UserId==id).OrderByDescending(x => x.Date).ToList(); 

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
            ViewBag.adv = db.Advertisements.Where(x => x.RestaurantId == id).ToList();
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
            if (User.Identity.IsAuthenticated)
            {
                var item = products.Find(x => x.ItemId == id);
                if (item == null)
                {
                    var product = db.Items.Find(id);
                    VirtualBill Item = new VirtualBill
                    {
                        ItemId = product.Id,
                        ItemName = product.Name,
                        ItemQuantity = 1,
                        ItemPrice = product.Price
                    };
                    products.Add(Item);
                }
                else
                {
                    item.ItemQuantity = item.ItemQuantity + 1;
                }


                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBill()
        {
            ViewBag.Restaurant = Session["RestaurantId"];
            ViewBag.products = products;
            ViewBag.DinnintTypes = db.DiningTypes.ToList();
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