using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FOODSTATION.Models;
using FOODSTATION.Models.Repositories;

namespace FOODSTATION.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CountriesController : Controller
    {
        private readonly ApplicationDbContext db;

        public CountriesController(ApplicationDbContext _db)
        {
            db = _db;
        }

        // GET: Countries
        public ActionResult CountryIndex()
        {
            ViewBag.Countries = db.Countries.ToList(); 
            return View();
        }

        public PartialViewResult Refreash()
        {
            var Countries = db.Countries.ToList();
            ViewBag.Countries = Countries;
            return PartialView("_CountryPartial", Countries);
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken] 
        public JsonResult Create(Country country)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                db.Countries.Add(country);
                db.SaveChanges();
                result = true;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Edit(Country country)
        {
            if (ModelState.IsValid)
            {
                db.Entry(country).State = EntityState.Modified;
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        // POST: Restaurants/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(int id)
        {
            var message = "";
            var country = db.Countries.Find(id);
            var regions = db.Regions.ToList().Where(x => x.CountryId == country.Id);
            if (regions.Count() > 0)
            {
                message = "haveItem";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            if (country == null)
            {
                message = "لايوجد مطعم بالمعرف المرسل الى السرفر";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            db.Countries.Remove(country);
            db.SaveChanges();

            return Json(message, JsonRequestBehavior.AllowGet);
        }


        // POST: Countries/DeletewithItems/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteCountryAndRegions(int id)
        {
            var country = db.Countries.Find(id);
            var regions = db.Regions.ToList().Where(x => x.CountryId == country.Id);

            foreach (var region in regions)
            {
                var restaurants = db.Restaurants.ToList().Where(x => x.RegionId == region.Id);

                if (restaurants.Count() > 0)
                {
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
                }
                db.Regions.Remove(region);
            }
            db.Countries.Remove(country);
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
