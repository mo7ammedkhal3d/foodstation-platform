using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FOODSTATION.Models;
using FOODSTATION.Models.Repositories;

namespace FOODSTATION.Controllers
{
    [Authorize(Roles = "Admin")]

    public class DiningTypesController : Controller
    {
        private readonly ApplicationDbContext db;

        public DiningTypesController(ApplicationDbContext _db)
        {
            db = _db;
        }

        // GET: DiningTypes
        public ActionResult Index()
        {
            var diningTypes = db.DiningTypes.ToList();
            ViewBag.DiningTypes = diningTypes;  

            return View();
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
