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
using CloudRestaurant.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CloudRestaurant.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ICloudRestaurantRepository<Item> itemRepository;
        private readonly ICloudRestaurantRepository<Restaurant> restaurantRepository;
        private readonly ICloudRestaurantRepository<Category> categoryRepository;

        public ItemsController(ICloudRestaurantRepository<Item> itemRepository ,
            ICloudRestaurantRepository<Restaurant> restaurantRepository , ICloudRestaurantRepository<Category> categoryRepository)
        {
            this.itemRepository = itemRepository;
            this.restaurantRepository = restaurantRepository;
            this.categoryRepository = categoryRepository;
        }

        // GET: Items
        public ActionResult Index()
        {
            var items = itemRepository.List();   
            ViewBag.items = items;
            ViewBag.CategoryId = new SelectList(categoryRepository.List(), "Id", "Name");
            ViewBag.RestaurantId = new SelectList(restaurantRepository.List(), "Id", "Name");
            return View();
        }

        //public PartialViewResult ProductionModelDetail(int id)
        //{ 
        //    ProductBAL obj = new ProductBAL(); 
        //    List<ProductionStockDetailModel> productModellist = new List<ProductionStockDetailModel>(); 
        //    List<ProductionStockDetailEntity> paintentity = obj.ProductionModelDetail(id); 
        //    ProductionStockDetailModel productModel; 
        //    if (paintentity != null)
        //    { 
        //        foreach (ProductionStockDetailEntity item in paintentity) 
        //        { 
        //            productModel = new ProductionStockDetailModel();
        //            productModel.MakeId = item.MakeId; 
        //            productModel.ModelId = item.ModelId;
        //            productModel.PartId = item.PartId; 
        //            productModellist.Add(productModel); 
        //        }
        //    } 
        //    return PartialView(productModellist); 
        //}

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
        public ActionResult Create(Item item, HttpPostedFileBase upload)
        {
            var result = "";
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads/Items/"), upload.FileName);
                    upload.SaveAs(path);
                    item.ImgUrl = upload.FileName;
                    itemRepository.Add(item);
                    return Json("تمت الأضافة قم بالرجوع الى الخلف وعمل رفرش للصفحة");
            }

             return Json("لم تتم الاضافة");
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = itemRepository.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(categoryRepository.List(), "Id", "Name", item.CategoryId);
            ViewBag.RestaurantId = new SelectList(restaurantRepository.List(), "Id", "Name", item.RestaurantId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Item item, HttpPostedFileBase upload)
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
                return RedirectToAction("");
            }

            ViewBag.CategoryId = new SelectList(categoryRepository.List(), "Id", "Name", item.CategoryId);
            ViewBag.RestaurantId = new SelectList(restaurantRepository.List(), "Id", "Name", item.RestaurantId);
            return View(item);
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
            //List<string> itemcontent = new List<string>();
            //itemcontent.Add(item.Name);
            //itemcontent.Add(item.Price.ToString());
            //itemcontent.Add(item.TimeOfDone.ToString());
            //itemcontent.Add(item.Category.Name);
            //itemcontent.Add(item.Restaurant.Name);
            //itemcontent.Add(item.ImgUrl); 
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

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        ApplicationDbContext db = new ApplicationDbContext();
        //         db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
