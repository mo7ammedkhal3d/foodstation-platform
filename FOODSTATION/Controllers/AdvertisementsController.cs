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

namespace FOODSTATION.Controllers
{
    [Authorize(Roles = "Admin , RestaurantOwner")]

    public class AdvertisementsController : Controller
    {
        private readonly ApplicationDbContext db;

        public AdvertisementsController(ApplicationDbContext _db)
        {
            db = _db;
        }

        // GET: Advertisements
        public ActionResult AdvertisementDashboard()
        {
            if (User.IsInRole("RestaurantOwner"))
            {
                var userId = User.Identity.GetUserId();

                ViewBag.Advertisements = db.Advertisements.Where(x => x.Restaurant.UserId == userId ).ToList();

                ViewBag.RestaurantId = new SelectList(db.Restaurants.Where(x =>x.UserId==userId).ToList(), "Id", "Name");
            }

            else
            {
                ViewBag.Advertisements = db.Advertisements.ToList();

                ViewBag.RestaurantId = new SelectList(db.Restaurants.ToList() , "Id", "Name");
            }

            return View();
        }

        public PartialViewResult Refreash()
        {
            if (User.IsInRole("RestaurantOwner"))
            {
                var userId = User.Identity.GetUserId();

                var Advertisements = db.Advertisements.Where(x => x.Restaurant.UserId == userId).ToList();

                ViewBag.Advertisements = Advertisements;

                ViewBag.RestaurantId = new SelectList(db.Restaurants.Where(x => x.UserId == userId).ToList(), "Id", "Name");

                return PartialView("_AdvertisementPartial", Advertisements);
            }

            else
            {
                var advertisements = db.Advertisements.ToList();

                ViewBag.Advertisements = advertisements;

                ViewBag.RestaurantId = new SelectList(db.Restaurants.ToList(), "Id", "Name");

                return PartialView("_AdvertisementPartial", advertisements);
            }
        }

        public JsonResult IsImageExist(string upload)
        {
            var Message = "";

            string path = Path.Combine(Server.MapPath("~/Uploads/Advertisements/"), upload);
            if (System.IO.File.Exists(path))
            {
                Message = "الصورة التي قمت بتحديها موجودة بالفعل لعنصر أخر قم بأختيار صورة مختلفة";

                return Json(Message, JsonRequestBehavior.AllowGet);
            }
            else

                return Json(Message, JsonRequestBehavior.AllowGet);
        }


        // POST: Advertisements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken] 
        public JsonResult Create(Advertisement adv, HttpPostedFileBase upload)
        {
            var responce = new AdvertisementResponceViewModel();
            responce.Remaing = ReminingAdeverCounter(adv.RestaurantId);

            if (ModelState.IsValid)
            { 
                if(responce.Remaing <= 0)
                {
                    responce.Message = "تعديت الحد المسموح من الأعلانات في الأشتراك ";
                    responce.Seccess = false;
                    return Json(responce, JsonRequestBehavior.AllowGet);
                }

                string path = Path.Combine(Server.MapPath("~/Uploads/Advertisements/"), upload.FileName);
                upload.SaveAs(path);
                adv.ImgUrl = upload.FileName;
                db.Advertisements.Add(adv);
                db.SaveChanges();

                responce.Remaing = ReminingAdeverCounter(adv.RestaurantId);
                responce.Message = "من الأعلانات ( "+responce.Remaing+" ) تمت الأضافة المتبقي ";
                responce.Seccess = true;

                return Json(responce , JsonRequestBehavior.AllowGet);
            }

            responce.Message =  "حدث خطأما أثناء عملية الأضافة تاكد من أدخال الحقول بالشكل الصحيح وحاول مرة أخرى";
            responce.Seccess = false;

            return Json(responce, JsonRequestBehavior.AllowGet);
        }

        public int ReminingAdeverCounter(int RestaurantId)
        {
            // NumOfAllAdv ---> Number of Allowed Advertisements in Restaurant Participations

            var NumOfAllAdv = db.Restaurants.Where(x => x.Id == RestaurantId).FirstOrDefault().Participations.Last()
                .Features.Where(x => x.Name == "Advertisement").FirstOrDefault().Numb;

            // NumOfAllAdv ---> Number of Advertisements for Restaurant 

            var NumOfAdv = db.Restaurants.Where(x => x.Id == RestaurantId).FirstOrDefault().Advertisements.Count();

            var remaining = NumOfAllAdv - NumOfAdv;

            return remaining;
        }


        public ActionResult GetAdvertisement(int? id)
        {
            var Advertisement = db.Advertisements.Find(id);
            EditAdvertisementViewModel AdvertisementVM = new EditAdvertisementViewModel();
            if (Advertisement != null)
            {
                AdvertisementVM.Description = Advertisement.Description;
                AdvertisementVM.RestaurantId = Advertisement.RestaurantId;
                AdvertisementVM.ImgUrl = Advertisement.ImgUrl;
            }
            return Json(AdvertisementVM, JsonRequestBehavior.AllowGet);
        }

        // POST: Advertisement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Edit(Advertisement advertisement, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string oldPath = Path.Combine(Server.MapPath("~/Uploads/Advertisements"), advertisement.ImgUrl);

                if (upload != null)
                {
                    System.IO.File.Delete(oldPath);
                    string path = Path.Combine(Server.MapPath("~/Uploads/Advertisements"), upload.FileName);
                    upload.SaveAs(path);
                    advertisement.ImgUrl = upload.FileName;
                }
                db.Entry(advertisement).State = EntityState.Modified;
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }


        public JsonResult DeleteConfirmed(int? id)
        {
            var Advertisement = db.Advertisements.Find(id);
            var responce = new AdvertisementResponceViewModel();

            if (Advertisement != null)
            {             
                string OldPath = Path.Combine(Server.MapPath("~/Uploads/Advertisements/"), Advertisement.ImgUrl);
                System.IO.File.Delete(OldPath);
                db.Advertisements.Remove(Advertisement);
                db.SaveChanges();   
                responce.Remaing = ReminingAdeverCounter(Advertisement.RestaurantId);
                responce.Seccess = true;
                responce.Message = "من الإعلانات ( " + responce.Remaing + " ) تمت عملية الحذف المتبقي";
            }
            return Json(responce, JsonRequestBehavior.AllowGet);
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
