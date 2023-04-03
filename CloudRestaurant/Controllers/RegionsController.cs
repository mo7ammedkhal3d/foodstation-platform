using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CloudRestaurant.Models;
using CloudRestaurant.Models.Repositories;
using CloudRestaurant.Models.ViewModels;

namespace CloudRestaurant.Controllers
{
    public class RegionsController : Controller
    {
        private readonly ICloudRestaurantRepository<Region> regionRepository;
        private readonly ICloudRestaurantRepository<Country> countryRepository;
        ApplicationDbContext db = new ApplicationDbContext();

        public RegionsController(ICloudRestaurantRepository<Region> regionRepository, ICloudRestaurantRepository<Country> countryRepository)
        {
            this.regionRepository = regionRepository;
            this.countryRepository = countryRepository;
        }

        // GET: Regions
        public ActionResult Index()
        {
            var Regions= regionRepository.List();
            ViewBag.Regions = Regions;
            ViewBag.Countries = regionRepository.List();
            ViewBag.CountryId = new SelectList(countryRepository.List(), "Id", "Name");

            return View();
        }
        public PartialViewResult Refreash()
        {
            var Regions = regionRepository.List();
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
                regionRepository.Add(region);
                result = true;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteConfirmed(int id)
        {
            bool result = false;
            var item = regionRepository.Find(id);
            if (item != null)
            {
                result = true;
                regionRepository.Delete(id);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRegion(int? id)
        {
            var region = regionRepository.Find(id);
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
                regionRepository.Update(region);
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        //// GET: Regions/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Regions/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "Id,Name")] Region region)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Regions.Add(region);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    return View(region);
        //}

        //// GET: Regions/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Region region = await db.Regions.FindAsync(id);
        //    if (region == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(region);
        //}

        //// POST: Regions/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] Region region)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(region).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(region);
        //}

        //// GET: Regions/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Region region = await db.Regions.FindAsync(id);
        //    if (region == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(region);
        //}

        //// POST: Regions/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    Region region = await db.Regions.FindAsync(id);
        //    db.Regions.Remove(region);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                regionRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
