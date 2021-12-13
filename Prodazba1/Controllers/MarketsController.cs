using Prodazba1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prodazba1.Controllers
{
    public class MarketsController : Controller
    { 

        // GET: Produkts
        public ActionResult Index()
        {
            ProdazbaEntities1 db = new ProdazbaEntities1();
            return View(db.Markets.ToList());
        }
        [HttpGet]
        public PartialViewResult Create()
        {
            return PartialView("Create", new Models.MarketClass());
        }
        [HttpPost]
        public JsonResult Create(Market market)
        {
            ProdazbaEntities1 db = new ProdazbaEntities1();
            db.Markets.Add(market);
            db.SaveChanges();
            return Json(market,JsonRequestBehavior.AllowGet);
        }

    }
}