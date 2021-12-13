using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Prodazba1.Models;

namespace Prodazba1.Controllers
{
    public class ProdazbasController : Controller
    {
        private ProdazbaEntities1 db = new ProdazbaEntities1();

        // GET: Prodazbas
        public ActionResult Index()
        {
            var prodazbas = db.Prodazbas.Include(p => p.Market).Include(p => p.Produkt);
            return View(prodazbas.ToList());
        }

        // GET: Prodazbas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodazba prodazba = db.Prodazbas.Find(id);
            if (prodazba == null)
            {
                return HttpNotFound();
            }
            return View(prodazba);
        }

        // GET: Prodazbas/Create
        public ActionResult Create()
        {
            ViewBag.MarketId = new SelectList(db.Markets, "Id", "Naziv");
            ViewBag.ProduktId = new SelectList(db.Produkts, "Id", "Naziv");
            return View();
        }

        // POST: Prodazbas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MarketId,ProduktId,Cena")] Prodazba prodazba)
        {
            if (ModelState.IsValid)
            {
                db.Prodazbas.Add(prodazba);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MarketId = new SelectList(db.Markets, "Id", "Naziv", prodazba.MarketId);
            ViewBag.ProduktId = new SelectList(db.Produkts, "Id", "Naziv", prodazba.ProduktId);
            return View(prodazba);
        }

        // GET: Prodazbas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodazba prodazba = db.Prodazbas.Find(id);
            if (prodazba == null)
            {
                return HttpNotFound();
            }
            ViewBag.MarketId = new SelectList(db.Markets, "Id", "Naziv", prodazba.MarketId);
            ViewBag.ProduktId = new SelectList(db.Produkts, "Id", "Naziv", prodazba.ProduktId);
            return View(prodazba);
        }

        // POST: Prodazbas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MarketId,ProduktId,Cena")] Prodazba prodazba)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prodazba).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MarketId = new SelectList(db.Markets, "Id", "Naziv", prodazba.MarketId);
            ViewBag.ProduktId = new SelectList(db.Produkts, "Id", "Naziv", prodazba.ProduktId);
            return View(prodazba);
        }

        // GET: Prodazbas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodazba prodazba = db.Prodazbas.Find(id);
            if (prodazba == null)
            {
                return HttpNotFound();
            }
            return View(prodazba);
        }

        // POST: Prodazbas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prodazba prodazba = db.Prodazbas.Find(id);
            db.Prodazbas.Remove(prodazba);
            db.SaveChanges();
            return RedirectToAction("Index");
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
