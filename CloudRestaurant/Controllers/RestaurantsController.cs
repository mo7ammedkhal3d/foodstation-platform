using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CloudRestaurant.Models;
using CloudRestaurant.Models.Repositories;

namespace CloudRestaurant.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly ICloudRestaurantRepository<Restaurant> restaurantRepository;

        public RestaurantsController(ICloudRestaurantRepository<Restaurant> restaurantRepository)
        {
            this.restaurantRepository = restaurantRepository;
        }


        public ActionResult Index()
        {
            return View(restaurantRepository.List());
        }

        // GET: Restaurants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = restaurantRepository.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // GET: Restaurants/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Restaurant restaurant, HttpPostedFileBase upload)
        {
            if(ModelState.IsValid)
            { 
                string path = Path.Combine(Server.MapPath("~/Uploads/Restaurants/"), upload.FileName);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.Path = "the file is oready exists";
                    return View("Create");
                }
                else
                {
                    upload.SaveAs(path);
                    restaurant.ImgUrl = upload.FileName;
                    restaurantRepository.Add(restaurant);
                    return RedirectToAction("Index");
                }
            }
            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = restaurantRepository.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Restaurant restaurant, HttpPostedFileBase upload)
        {
            string OldPath = Path.Combine(Server.MapPath("~/Uploads/Restaurants"), restaurant.ImgUrl);
            if (ModelState.IsValid)
            {
                if (upload != null)
                {
                    string oldPath = Path.Combine(Server.MapPath("~/Uploads/Restaurants"), restaurant.ImgUrl);
                    System.IO.File.Delete(oldPath);
                    string path = Path.Combine(Server.MapPath("~/Uploads/Restaurants"), upload.FileName);
                    upload.SaveAs(path);
                    restaurant.ImgUrl = upload.FileName;
                }
                restaurantRepository.Update(restaurant);
                return RedirectToAction("Index");
            }          
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = restaurantRepository.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Restaurant restaurant = restaurantRepository.Find(id);
            string OldPath = Path.Combine(Server.MapPath("~/Uploads/Restaurants"), restaurant.ImgUrl);
            System.IO.File.Delete(OldPath);
            restaurantRepository.Delete(id);
            return RedirectToAction("Index");
        }


        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)                    MK187  ???! 
        //    {
        //        restaurantRepository.db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
