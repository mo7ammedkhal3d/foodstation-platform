using CloudRestaurant.Models;
using CloudRestaurant.Models.ViewModels;
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
       static List<Item> products = new List<Item>();
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

        public ActionResult GetItem(int? id)
        {        
            var item = db.Items.Where(x => x.CategoryId == id).ToList();
                ViewBag.Name = item.First().Category.Name;
                return View(item);

        }

        public ActionResult AddToBill(int? id)
        {
            var element = new virtualBill();
            var item = db.Items.Find(id);
            products.Add(item);
            Session["elements"]=products;
            element.Name = item.Name;
            element.Price=((int)item.Price);

            return Json(element, JsonRequestBehavior.AllowGet);         

        }
        public ActionResult GetBill()
        {
            var list = new List<Item>();
            list = (List<Item>)Session["elements"];
             return View(products.ToList()); ;
        }
    }
}