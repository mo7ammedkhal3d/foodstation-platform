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

namespace CloudRestaurant.Controllers
{
    public class RegionsController : Controller
    {
        private readonly ICloudRestaurantRepository<Region> regionRepository;
        ApplicationDbContext db = new ApplicationDbContext();

        public RegionsController(ICloudRestaurantRepository<Region> regionRepository)
        {
            this.regionRepository = regionRepository;
        }

        // GET: Regions
        public ActionResult Index()
        {
            var Regions= regionRepository.List();
            ViewBag.Regions = Regions;
            ViewBag.Countries = regionRepository.List();

            return View(db.Regions.ToList());
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

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
