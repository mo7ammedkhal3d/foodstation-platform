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
    public class DiningTypesController : Controller
    {
        private readonly IFOODSTATIONRepository<DiningType> diningTypeRepository;

        public DiningTypesController(IFOODSTATIONRepository<DiningType> diningTypeRepository)
        {
            this.diningTypeRepository = diningTypeRepository;
        }

        // GET: DiningTypes
        public ActionResult Index()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                diningTypeRepository.Dispose(); 
            }
            base.Dispose(disposing);
        }
    }
}
