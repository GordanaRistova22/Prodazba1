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
    public class ProduktsController : Controller
    {
        private ProdazbaEntities1 db = new ProdazbaEntities1();

        // GET: Produkts
        public ActionResult Index(String SelectedNaziv,String SelectedKategorija)
        {
            var ramData = (from s in db.Produkts select s).ToList();
            var produkts = from s in ramData
                           select s;
            if (!String.IsNullOrEmpty(SelectedNaziv))
            {
                produkts = produkts.Where(s => s.Naziv.Trim().Equals(SelectedNaziv.Trim()));
            }
            if (!String.IsNullOrEmpty(SelectedKategorija))
            {
                produkts = produkts.Where(s => s.Kategorija.Trim().Equals(SelectedKategorija.Trim()));
            }
            var UniqueNaziv = from s in produkts
                              group s by s.Naziv into newGroup
                              where newGroup.Key != null
                              orderby newGroup.Key
                              select new { Naziv.newGroup.Key };
            ViewBag.UniqueNaziv = UniqueNaziv.select(m => new SelectListItem { Value = m.Naziv, Text = m.Naziv }).toList();
            var UniqueKategorija = from s in produkts
                              group s by s.Kategorija into newGroup
                              where newGroup.Key != null
                              orderby newGroup.Key
                              select new { Kategorija.newGroup.Key };
            ViewBag.UniqueKategorija = UniqueKategorija.select(m => new SelectListItem { Value = m.Kategorija, Text = m.Kategorija }).toList();
            return View(db.Produkts.ToList());
        }

        // GET: Produkts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produkt produkt = db.Produkts.Find(id);
            if (produkt == null)
            {
                return HttpNotFound();
            }
            return View(produkt);
        }

        // GET: Produkts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Produkts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naziv,Kategorija")] Produkt produkt)
        {
            if (ModelState.IsValid)
            {
                db.Produkts.Add(produkt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(produkt);
        }

        // GET: Produkts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produkt produkt = db.Produkts.Find(id);
            if (produkt == null)
            {
                return HttpNotFound();
            }
            return View(produkt);
        }

        // POST: Produkts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naziv,Kategorija")] Produkt produkt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produkt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(produkt);
        }

        // GET: Produkts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produkt produkt = db.Produkts.Find(id);
            if (produkt == null)
            {
                return HttpNotFound();
            }
            return View(produkt);
        }

        // POST: Produkts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produkt produkt = db.Produkts.Find(id);
            db.Produkts.Remove(produkt);
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
