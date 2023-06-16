using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FOODSTATION.Models;
using FOODSTATION.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FOODSTATION.Controllers
{
    [Authorize(Roles = "Admin , RestaurantOwner")]

    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext db;

        public ItemsController(ApplicationDbContext _db)
        {
            db = _db;
        }

        // GET: Items
        public ActionResult ItemDashboard()
        {
            var userId=User.Identity.GetUserId();
            if (User.IsInRole("RestaurantOwner"))
            {
                var items = db.Items.ToList().Where(x=>x.Restaurant.UserId== userId);   
                ViewBag.items = items;
                ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "Name");
                ViewBag.RestaurantId = new SelectList(db.Restaurants.ToList().Where(x => x.UserId == userId), "Id", "Name");
                return View();
            }
            else
            {
                var items = db.Items.ToList();
                ViewBag.items = items;
                ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "Name");
                ViewBag.RestaurantId = new SelectList(db.Restaurants.ToList(), "Id", "Name");
                return View();
            }
            
        }

        public PartialViewResult Refreash()
        {
            if (User.IsInRole("RestaurantOwner"))
            {
                var items = db.Items.ToList().Where(x => x.Restaurant.UserId == User.Identity.GetUserId());
                ViewBag.items = items;
                ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "Name");
                ViewBag.RestaurantId = new SelectList(db.Restaurants.ToList().Where(x => x.UserId == User.Identity.GetUserId()), "Id", "Name");
                return PartialView("_ItemPartial", items);
            }
            else
            {
                var items = db.Items.ToList();
                ViewBag.items = items;
                ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "Name");
                ViewBag.RestaurantId = new SelectList(db.Restaurants.ToList(), "Id", "Name");
                return PartialView("_ItemPartial", items);
            }
        }

        public JsonResult IsImageExist(string upload)
        {
            var Message = "";
             
                string path = Path.Combine(Server.MapPath("~/Uploads/Items/"), upload);
                if (System.IO.File.Exists(path))
                {
                    Message = "الصورة التي قمت بتحديها موجودة بالفعل لعنصر أخر قم بأختيار صورة مختلفة";

                return Json(Message, JsonRequestBehavior.AllowGet); 
                }
                else

                return Json(Message, JsonRequestBehavior.AllowGet);         
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken] 
        public JsonResult Create(Item item, HttpPostedFileBase upload)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads/Items/"), upload.FileName);
                upload.SaveAs(path);
                item.ImgUrl = upload.FileName;
                db.Items.Add(item);
                db.SaveChanges();
                result = true;    
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Edit(Item item, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string oldPath = Path.Combine(Server.MapPath("~/Uploads/Items"), item.ImgUrl);

                if (upload != null)
                {
                    System.IO.File.Delete(oldPath);
                    string path = Path.Combine(Server.MapPath("~/Uploads/Items"), upload.FileName);
                    upload.SaveAs(path);
                    item.ImgUrl = upload.FileName;
                }
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "Name", item.CategoryId);
            ViewBag.RestaurantId = new SelectList(db.Restaurants.ToList(), "Id", "Name", item.RestaurantId);
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        // GET: Items/Edit/5
        public ActionResult GetItem(int? id)
        {   
           var item = db.Items.Find(id);
            ItemVM itemVM = new ItemVM();
            if (item != null)
            {                  
                itemVM.Name = item.Name;
                itemVM.Price = item.Price;
                itemVM.TimeOfDone = item.TimeOfDone.ToString();
                itemVM.Restaurant = item.Restaurant.Name;
                itemVM.ImgUrl = item.ImgUrl;
                itemVM.Category = item.Category.Name;
                itemVM.CategoryId = item.CategoryId;
                itemVM.RestaurantId = item.RestaurantId;
            }
            return Json(itemVM ,JsonRequestBehavior.AllowGet);
        }

        // POST: Items/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(int id)
        {
            bool result = false;
            var item = db.Items.Find(id);
            if(item != null)
            {
                result = true;  
                string OldPath = Path.Combine(Server.MapPath("~/Uploads/Items/"), item.ImgUrl);
                System.IO.File.Delete(OldPath);
                db.Items.Remove(item);
                db.SaveChanges();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
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
