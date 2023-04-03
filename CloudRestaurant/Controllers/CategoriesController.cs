using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using CloudRestaurant.Models;
using CloudRestaurant.Models.Repositories;

namespace CloudRestaurant.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICloudRestaurantRepository<Category> categoryRepository;

        public CategoriesController(ICloudRestaurantRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        // GET: Categories
        public ActionResult Index()
        {
            return View(categoryRepository.List());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryRepository.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category , HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads/Categories/"),upload.FileName);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.Path = "the file is oready exists";
                    return View("Create");
                }
                else
                {
                    upload.SaveAs(path);
                    category.ImgUrl=upload.FileName;    
                    categoryRepository.Add(category);
                    return RedirectToAction("Index");
                }
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryRepository.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {

                if (upload != null)
                {                   
                    if (category.ImgUrl !=null)
                    {
                        string oldPath = Path.Combine(Server.MapPath("~/Uploads/Categories/"), category.ImgUrl);
                        System.IO.File.Delete(oldPath);
                    }
                   
                    string path = Path.Combine(Server.MapPath("~/Uploads/Categories/"), upload.FileName);
                    upload.SaveAs(path);
                    category.ImgUrl = upload.FileName;
                }
                categoryRepository.Update(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryRepository.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            categoryRepository.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                categoryRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
