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
using FOODSTATION.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FOODSTATION.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IFOODSTATIONRepository<Item> itemRepository;
        private readonly IFOODSTATIONRepository<Restaurant> restaurantRepository;
        private readonly IFOODSTATIONRepository<Category> categoryRepository;

        public ItemsController(IFOODSTATIONRepository<Item> itemRepository ,
            IFOODSTATIONRepository<Restaurant> restaurantRepository , IFOODSTATIONRepository<Category> categoryRepository)
        {
            this.itemRepository = itemRepository;
            this.restaurantRepository = restaurantRepository;
            this.categoryRepository = categoryRepository;
        }

        public ItemsController()
        {
            
        }

        // GET: Items
        public ActionResult ItemIndex()
        {
            var items = itemRepository.List();   
            ViewBag.items = items;
            ViewBag.CategoryId = new SelectList(categoryRepository.List(), "Id", "Name");
            ViewBag.RestaurantId = new SelectList(restaurantRepository.List(), "Id", "Name");
            return View();
        }

        public PartialViewResult Refreash()
        {
            var items = itemRepository.List();
            ViewBag.items = items;
            ViewBag.CategoryId = new SelectList(categoryRepository.List(), "Id", "Name");
            ViewBag.RestaurantId = new SelectList(restaurantRepository.List(), "Id", "Name");
            return PartialView("_ItemPartial", items);
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
                itemRepository.Add(item);
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
                itemRepository.Update(item);
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            ViewBag.CategoryId = new SelectList(categoryRepository.List(), "Id", "Name", item.CategoryId);
            ViewBag.RestaurantId = new SelectList(restaurantRepository.List(), "Id", "Name", item.RestaurantId);
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        // GET: Items/Edit/5
        public ActionResult GetItem(int? id)
        {   
           var item = itemRepository.Find(id);
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
            var item = itemRepository.Find(id);
            if(item != null)
            {
                result = true;  
                string OldPath = Path.Combine(Server.MapPath("~/Uploads/Items/"), item.ImgUrl);
                System.IO.File.Delete(OldPath);
                itemRepository.Delete(id);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                restaurantRepository.Dispose();
                itemRepository.Dispose();
                categoryRepository.Dispose();   
            }
            base.Dispose(disposing);
        }
    }
}
