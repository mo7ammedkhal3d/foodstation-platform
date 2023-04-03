using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CloudRestaurant.Models;
using CloudRestaurant.Models.Repositories;

namespace CloudRestaurant.Controllers
{
    public class CountriesController : Controller
    {
        private readonly ICloudRestaurantRepository<Country> countryRepository;
        private readonly ICloudRestaurantRepository<Region> regionRepository;

        public CountriesController(ICloudRestaurantRepository<Country> countryRepository, ICloudRestaurantRepository<Region> regionRepository)
        {
            this.countryRepository = countryRepository;
            this.regionRepository = regionRepository;
        }

        // GET: Countries
        public ActionResult Index()
        {
            ViewBag.Countries = countryRepository.List(); 
            return View();
        }

        public PartialViewResult Refreash()
        {
            var Countries = countryRepository.List();
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
                countryRepository.Add(country);
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
                countryRepository.Update(country);
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
            var country = countryRepository.Find(id);
            var regions = regionRepository.List().Where(x => x.CountryId == country.Id);
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

            countryRepository.Delete(id);

            return Json(message, JsonRequestBehavior.AllowGet);
        }


        // POST: Countries/DeletewithItems/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteCountryAndRegions(int id)
        {
            var country = countryRepository.Find(id);
            var regions = regionRepository.List().Where(x => x.CountryId == country.Id);

            foreach (var item in regions)
            {
                regionRepository.Delete(item.Id);
            }
            countryRepository.Delete(id);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                countryRepository.Dispose();
                regionRepository.Dispose(); 
            }
            base.Dispose(disposing);
        }
    }
}
