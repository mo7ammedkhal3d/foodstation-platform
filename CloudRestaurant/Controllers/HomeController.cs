using CloudRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CloudRestaurant.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.Restaurants.ToList());
        }

        public ActionResult GetRestaurantItems(int id)
        {
            //var items = db.Items.Where(m => m.RestaurantId == id);

            return View();    
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
           
        }
        public ActionResult RestaurantCategories(int? id)
        {
            var cato = from item in db.Items
                       join categ in db.Categories on item.CategoryId equals categ.Id
                       where item.RestaurantId == id
                       select categ;

            var Res= db.Restaurants.Where(x=>x.Id==id).Single();
            ViewBag.Name=Res.Name;
            return View(cato.Distinct().ToList());

        }
    }
}