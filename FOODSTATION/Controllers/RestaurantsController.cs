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
using FOODSTATION.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FOODSTATION.Controllers
{
    [Authorize(Roles = "Admin")]

    public class RestaurantsController : Controller
    {
        private readonly ApplicationDbContext db;

        public RestaurantsController(ApplicationDbContext _db)
        {
            db = _db;
        }

        public ActionResult RestaurantDashboard()
        {
            ViewBag.restaurants = db.Restaurants.ToList();
            ViewBag.RegionId = new SelectList(db.Regions.ToList(), "Id", "Name");
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var roleId = roleManager.Roles.FirstOrDefault(r => r.Name == "RestaurantOwner")?.Id;
            ViewBag.UserId = new SelectList(db.Users.Where(u => u.Roles.Any(r => r.RoleId == roleId)).ToList(), "Id", "UserName");
            ViewBag.ParticipationTypes = new SelectList(db.Participations.ToList(), "Id", "Name");
            ViewBag.AvailableDiningTypes = db.DiningTypes.ToList();

            return View();
        }

        public PartialViewResult Refreash()
        {
            var restaurants = db.Restaurants.ToList();
            ViewBag.restaurants = restaurants;
            return PartialView("_RestaurantPartial", restaurants);
        }

        public JsonResult IsImageExist(string upload)
        {
            var Message = "";

            string path = Path.Combine(Server.MapPath("~/Uploads/Restaurants/"), upload);
            if (System.IO.File.Exists(path))
            {
                Message = "الصورة التي قمت بتحديها موجودة بالفعل لمطعم أخر قم بأختيار صورة مختلفة";

                return Json(Message, JsonRequestBehavior.AllowGet);
            }
            else

                return Json(Message, JsonRequestBehavior.AllowGet);
        }

        // Test
        //// POST: Restaurants/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        ////[ValidateAntiForgeryToken] 
        public JsonResult Create(Restaurant restaurant, HttpPostedFileBase upload, int[] diningTypeIds,int ParticipationTypes)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads/Restaurants/"), upload.FileName);
                upload.SaveAs(path);
                restaurant.ImgUrl = upload.FileName;
                restaurant.Participations.Add(db.Participations.Find(ParticipationTypes));
                if (diningTypeIds != null)
                {
                    foreach (var id in diningTypeIds)
                    {
                        restaurant.DiningTypes.Add(db.DiningTypes.Find(id));
                    }
                }
                else restaurant.DiningTypes.Add(db.DiningTypes.Where(x => x.Name == "محلي").FirstOrDefault());

                db.Restaurants.Add(restaurant);
                db.SaveChanges();

                result = true;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        // GET: Restaurant/Edit/5
        public ActionResult GetRestaurant(int? id)
        {
            var restaurant = db.Restaurants.Find(id);
            RestaurantVM restaurantVM = new RestaurantVM();
            if (restaurant != null)
            {
                restaurantVM.diningTypeIds = new List<int>(); 
                restaurantVM.Name = restaurant.Name;
                restaurantVM.Description = restaurant.Description;
                restaurantVM.Region = restaurant.Region.Name;
                restaurantVM.RegionId = restaurant.RegionId;
                restaurantVM.UserId = restaurant.UserId;
                restaurantVM.UserName = restaurant.User.UserName;
                restaurantVM.Longitude = restaurant.Longitude;  
                restaurantVM.Latitude = restaurant.Latitude;
                if(restaurant.Participations.Count() > 0)
                {
                    restaurantVM.Participation = restaurant.Participations.Last().Id;
                }
                foreach (var item in restaurant.DiningTypes)
                {
                    restaurantVM.diningTypeIds.Add(item.Id);
                }
                ViewBag.ids = restaurantVM.diningTypeIds;
                restaurantVM.ImgUrl = restaurant.ImgUrl;
            }
            return Json(restaurantVM, JsonRequestBehavior.AllowGet);
        }


        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Edit(Restaurant modifiedInfo, HttpPostedFileBase upload, int[] diningTypeIds, int ParticipationTypes)
        {
            if (ModelState.IsValid)
            {
                var restaurant = db.Restaurants.Find(modifiedInfo.Id);
                restaurant.Name = modifiedInfo.Name;
                restaurant.Description = modifiedInfo.Description;
                restaurant.RegionId = modifiedInfo.RegionId;
                restaurant.UserId = modifiedInfo.UserId;
                restaurant.Longitude = modifiedInfo.Longitude;
                restaurant.Latitude = modifiedInfo.Latitude;
                string oldPath = Path.Combine(Server.MapPath("~/Uploads/Restaurants"), restaurant.ImgUrl);

                if (upload != null)
                {
                    System.IO.File.Delete(oldPath);
                    string path = Path.Combine(Server.MapPath("~/Uploads/Restaurants"), upload.FileName);
                    upload.SaveAs(path);
                    restaurant.ImgUrl = upload.FileName;
                }

                if (diningTypeIds != null)
                {
                    restaurant.DiningTypes.Clear();
                    foreach (var id in diningTypeIds)
                    {
                        restaurant.DiningTypes.Add(db.DiningTypes.Find(id));
                    }
                }

                restaurant.Participations.Add(db.Participations.Find(ParticipationTypes));


                db.Entry(restaurant).State = EntityState.Modified;
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
            var restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                message = "لايوجد مطعم بالمعرف المرسل الى السرفر";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            var items = db.Items.ToList().Where(x => x.RestaurantId == restaurant.Id);
            if (items.Count() > 0)
            {
                message = "haveItem";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            string OldPath = Path.Combine(Server.MapPath("~/Uploads/Restaurants/"), restaurant.ImgUrl);
            System.IO.File.Delete(OldPath);
            db.Restaurants.Remove(restaurant);
            db.SaveChanges();

            return Json(message, JsonRequestBehavior.AllowGet);
        }


        // POST: Restaurants/DeletewithItems/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteRestaurantandItems(int id)
        {
            var restaurant = db.Restaurants.Find(id);
            var items = db.Items.ToList().Where(x => x.RestaurantId == restaurant.Id);

            foreach (var item in items)
            {
                string itemOldPath = Path.Combine(Server.MapPath("~/Uploads/Items/"), item.ImgUrl);
                System.IO.File.Delete(itemOldPath);
                db.Items.Remove(item);
            }

            string OldPath = Path.Combine(Server.MapPath("~/Uploads/Restaurants/"), restaurant.ImgUrl);
            System.IO.File.Delete(OldPath);
            db.Restaurants.Remove(restaurant);
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
