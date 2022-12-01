using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Jewels.DAL;
using Jewels.Models;

namespace Jewels.Controllers
{
    public class HinhThucTTsController : Controller
    {
        private JewelsContext db = new JewelsContext();

        // GET: HinhThucTTs
        public ActionResult Index()
        {
            return View(db.HinhThucTTs.ToList());
        }

        // GET: HinhThucTTs/Details/5
       /* public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HinhThucTT hinhThucTT = db.HinhThucTTs.Find(id);
            if (hinhThucTT == null)
            {
                return HttpNotFound();
            }
            return View(hinhThucTT);
        }*/

        // GET: HinhThucTTs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HinhThucTTs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HinhThucTTID,HinhThucTTName")] HinhThucTT hinhThucTT)
        {
            if (ModelState.IsValid)
            {
                db.HinhThucTTs.Add(hinhThucTT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hinhThucTT);
        }

        // GET: HinhThucTTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HinhThucTT hinhThucTT = db.HinhThucTTs.Find(id);
            if (hinhThucTT == null)
            {
                return HttpNotFound();
            }
            return View(hinhThucTT);
        }

        // POST: HinhThucTTs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HinhThucTTID,HinhThucTTName")] HinhThucTT hinhThucTT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hinhThucTT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hinhThucTT);
        }

        // GET: HinhThucTTs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HinhThucTT hinhThucTT = db.HinhThucTTs.Find(id);
            if (hinhThucTT == null)
            {
                return HttpNotFound();
            }
            return View(hinhThucTT);
        }

        // POST: HinhThucTTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HinhThucTT hinhThucTT = db.HinhThucTTs.Find(id);
            db.HinhThucTTs.Remove(hinhThucTT);
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
