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

namespace CloudRestaurant.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly ICloudRestaurantRepository<Restaurant> restaurantRepository;
        private readonly ICloudRestaurantRepository<Item> itemRepository;
        private readonly ICloudRestaurantRepository<Region> regionRepository;
        private readonly ICloudRestaurantRepository<DiningType> diningTypeRepository;

        public RestaurantsController(ICloudRestaurantRepository<Restaurant> restaurantRepository , ICloudRestaurantRepository<Item> itemRepository
            ,ICloudRestaurantRepository<Region> regionRepository , ICloudRestaurantRepository<DiningType> diningTypeRepository)
        {
            this.restaurantRepository = restaurantRepository;
            this.itemRepository = itemRepository;
            this.regionRepository = regionRepository;
            this.diningTypeRepository = diningTypeRepository;
        }


        public ActionResult RestaurantIndex()
        {
            ViewBag.restaurants = restaurantRepository.List();
            ViewBag.RegionId = new SelectList(regionRepository.List(), "Id", "Name");
            RestaurantDiningTypesVM vm = new RestaurantDiningTypesVM();
            vm.diningTypes = diningTypeRepository.List();   

            return View();
        }

        public PartialViewResult Refreash()
        {
            var restaurants = restaurantRepository.List();
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
        //[HttpPost]
        ////[ValidateAntiForgeryToken] 
        //public JsonResult Create(Restaurant restaurant, HttpPostedFileBase upload, int[] diningTypes)
        //{
        //    var result = false;
        //    if (ModelState.IsValid)
        //    {
        //        string path = Path.Combine(Server.MapPath("~/Uploads/Restaurants/"), upload.FileName);
        //        upload.SaveAs(path);
        //        restaurant.ImgUrl = upload.FileName;
        //        restaurantRepository.Add(restaurant);
        //        foreach (var diningtype in diningTypes)
        //        {
        //            restaurant.DiningTypes.Add(diningTypeRepository.Find(diningtype));
        //        }

        //        result = true;
        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}


        // POST: Restaurants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        //[ValidateAntiForgeryToken] 
        public JsonResult Create(Restaurant restaurant, HttpPostedFileBase upload)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads/Restaurants/"), upload.FileName);
                upload.SaveAs(path);
                restaurant.ImgUrl = upload.FileName;
                restaurantRepository.Add(restaurant);
                result = true;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        // GET: Restaurant/Edit/5
        public ActionResult GetRestaurant(int? id)
        {
            var restaurant = restaurantRepository.Find(id);
            RestaurantVM restaurantVM = new RestaurantVM();
            if (restaurant != null)
            {
                restaurantVM.Name = restaurant.Name;
                restaurantVM.Description = restaurant.Description;
                restaurantVM.Region = restaurant.Region.Name;
                restaurantVM.RegionId = restaurant.RegionId;    
                restaurantVM.ImgUrl = restaurant.ImgUrl;
            }
            return Json(restaurantVM, JsonRequestBehavior.AllowGet);
        }


        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Edit(Restaurant restaurant, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string oldPath = Path.Combine(Server.MapPath("~/Uploads/Restaurants"), restaurant.ImgUrl);

                if (upload != null)
                {
                    System.IO.File.Delete(oldPath);
                    string path = Path.Combine(Server.MapPath("~/Uploads/Restaurants"), upload.FileName);
                    upload.SaveAs(path);
                    restaurant.ImgUrl = upload.FileName;
                }
                restaurantRepository.Update(restaurant);
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
            var restaurant = restaurantRepository.Find(id);
            var items = itemRepository.List().Where(x => x.RestaurantId == restaurant.Id);
            if (items.Count() > 0)
            {
                message = "haveItem";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            if(restaurant == null)
            {
                message = "لايوجد مطعم بالمعرف المرسل الى السرفر";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            string OldPath = Path.Combine(Server.MapPath("~/Uploads/Restaurants/"), restaurant.ImgUrl);
            System.IO.File.Delete(OldPath);
            restaurantRepository.Delete(id);

            return Json(message, JsonRequestBehavior.AllowGet);
        }


        // POST: Restaurants/DeletewithItems/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteRestaurantandItems(int id)
        {
            var restaurant = restaurantRepository.Find(id);
            var items = itemRepository.List().Where(x => x.RestaurantId == restaurant.Id);

            foreach (var item in items)
            {
                string itemOldPath = Path.Combine(Server.MapPath("~/Uploads/Items/"), item.ImgUrl);
                System.IO.File.Delete(itemOldPath);
                itemRepository.Delete(item.Id);
            }

            string OldPath = Path.Combine(Server.MapPath("~/Uploads/Restaurants/"), restaurant.ImgUrl);
            System.IO.File.Delete(OldPath);
            restaurantRepository.Delete(id);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                restaurantRepository.Dispose();
                itemRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
