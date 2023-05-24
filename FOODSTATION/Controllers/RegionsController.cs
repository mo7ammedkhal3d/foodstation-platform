using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FOODSTATION.Models;
using FOODSTATION.Models.Repositories;
using FOODSTATION.Models.ViewModels;
using System.IO;

namespace FOODSTATION.Controllers
{
    [Authorize(Roles = "Admin")]

    public class RegionsController : Controller
    {
        private readonly ApplicationDbContext db;

        public RegionsController(ApplicationDbContext _db)
        {
            db = _db;
        }

        // GET: Regions
        public ActionResult RegionIndex()
        {
            var Regions= db.Regions.ToList();
            ViewBag.Regions = Regions;
            ViewBag.CountryId = new SelectList(db.Countries.ToList(), "Id", "Name");

            return View();
        }
        public PartialViewResult Refreash()
        {
            var Regions = db.Regions.ToList();
            ViewBag.Regions = Regions;
            return PartialView("_RegionPartial", Regions);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken] 
        public JsonResult Create(Region region)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                db.Regions.Add(region);
                db.SaveChanges();
                result = true;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRegion(int? id)
        {
            var region = db.Regions.Find(id);
            RegionVm regionVm = new RegionVm();
            if (region != null)
            {
                regionVm.Name = region.Name;
                regionVm.CountryId = region.CountryId;  
                return Json(regionVm, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Edit(Region region)
        {
            if (ModelState.IsValid)
            {
                db.Entry(region).State = EntityState.Modified;
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteConfirmed(int id)
        {
            var message = "";
            var region = db.Regions.Find(id);
            if (region == null)
            {
                message = "لايوجد منطقة بالمعرف المرسل الى السرفر";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            var restaurants = db.Restaurants.Where(x => x.RegionId == region.Id);
            if (restaurants.Count() > 0)
            {
                message = "haveRestaurant";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            db.Regions.Remove(region);
            db.SaveChanges();
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteRegionAndRestaurants(int id)
        {
            var region = db.Regions.Find(id);
            var restaurants = db.Restaurants.ToList().Where(x => x.RegionId == region.Id);

            foreach (var restaurant in restaurants)
            {
                var items = db.Items.ToList().Where(x => x.RestaurantId == restaurant.Id);

                if (items.Count() > 0)
                {
                    foreach (var item in items)
                    {
                        string itemOldPath = Path.Combine(Server.MapPath("~/Uploads/Items/"), item.ImgUrl);
                        System.IO.File.Delete(itemOldPath);
                        db.Items.Remove(item);
                    }
                }
                string restaurantOldPath = Path.Combine(Server.MapPath("~/Uploads/Restaurants/"), restaurant.ImgUrl);
                System.IO.File.Delete(restaurantOldPath);
                db.Restaurants.Remove(restaurant);
            }

            db.Regions.Remove(region);
            db.SaveChanges();

            return Json(true, JsonRequestBehavior.AllowGet);
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
