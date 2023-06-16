using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using FOODSTATION.Models;
using FOODSTATION.Models.ViewModels;

namespace FOODSTATION.Controllers
{
    [Authorize(Roles = "Admin")]

    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext db;

        public CategoriesController(ApplicationDbContext _db)
        {
            db = _db;
        }

        // GET: Categories
        public ActionResult CategoryDashboard()
        {
            ViewBag.categories = db.Categories.ToList();
            return View();
        }

        public PartialViewResult Refreash()
        {
            var categories = db.Categories.ToList();
            ViewBag.categories = categories;
            return PartialView("_CategoryPartial", categories);
        }

        public JsonResult IsImageExist(string upload)
        {
            var Message = "";

            string path = Path.Combine(Server.MapPath("~/Uploads/Categories/"), upload);
            if (System.IO.File.Exists(path))
            {
                Message = "الصورة التي قمت بتحديها موجودة بالفعل لمطعم أخر قم بأختيار صورة مختلفة";

                return Json(Message, JsonRequestBehavior.AllowGet);
            }
            else

                return Json(Message, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        //[ValidateAntiForgeryToken] 
        public JsonResult Create(Category category, HttpPostedFileBase upload)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads/Categories/"), upload.FileName);
                upload.SaveAs(path);
                category.ImgUrl = upload.FileName;
                db.Categories.Add(category);
                db.SaveChanges();
                result = true;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: Category/Edit/5
        public ActionResult GetCategory(int? id)
        {
            var category = db.Categories.Find(id);
            CategoryVM categoryVM = new CategoryVM();
            if (category != null)
            {
                categoryVM.Name = category.Name;
                categoryVM.ImgUrl = category.ImgUrl;
            }
            return Json(categoryVM, JsonRequestBehavior.AllowGet);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Edit(Category category, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string oldPath = Path.Combine(Server.MapPath("~/Uploads/Categories"), category.ImgUrl);

                if (upload != null)
                {
                    System.IO.File.Delete(oldPath);
                    string path = Path.Combine(Server.MapPath("~/Uploads/Categories"), upload.FileName);
                    upload.SaveAs(path);
                    category.ImgUrl = upload.FileName;
                }
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        // POST: Categories/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(int id)
        {
            var message = "";
            var category = db.Categories.Find(id);
            var items = db.Items.ToList().Where(x => x.CategoryId == category.Id);
            if (items.Count() > 0)
            {
                message = "haveItem";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            if (category == null)
            {
                message = "لايوجد مطعم بالمعرف المرسل الى السرفر";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            string OldPath = Path.Combine(Server.MapPath("~/Uploads/Categories/"), category.ImgUrl);
            System.IO.File.Delete(OldPath);
            db.Categories.Remove(category);
            db.SaveChanges();

            return Json(message, JsonRequestBehavior.AllowGet);
        }


        // POST: Categories/DeletewithItems/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteCategoryandItems(int id)
        {
            var category = db.Categories.Find(id);
            var items = db.Items.ToList().Where(x => x.CategoryId == category.Id);

            foreach (var item in items)
            {
                string itemOldPath = Path.Combine(Server.MapPath("~/Uploads/Items/"), item.ImgUrl);
                System.IO.File.Delete(itemOldPath);
                db.Items.Remove(item);
            }

            string OldPath = Path.Combine(Server.MapPath("~/Uploads/Categories/"), category.ImgUrl);
            System.IO.File.Delete(OldPath);
            db.Categories.Remove(category);
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
