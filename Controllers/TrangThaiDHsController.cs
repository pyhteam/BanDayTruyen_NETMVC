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
    public class TrangThaiDHsController : Controller
    {
        private JewelsContext db = new JewelsContext();

        // GET: TrangThaiDHs
        public ActionResult Index()
        {
            return View(db.TrangThaiDHs.ToList());
        }

        // GET: TrangThaiDHs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrangThaiDH trangThaiDH = db.TrangThaiDHs.Find(id);
            if (trangThaiDH == null)
            {
                return HttpNotFound();
            }
            return View(trangThaiDH);
        }

        // GET: TrangThaiDHs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TrangThaiDHs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrangThaiDHID,TenTrangThaiDH")] TrangThaiDH trangThaiDH)
        {
            if (ModelState.IsValid)
            {
                db.TrangThaiDHs.Add(trangThaiDH);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trangThaiDH);
        }

        // GET: TrangThaiDHs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrangThaiDH trangThaiDH = db.TrangThaiDHs.Find(id);
            if (trangThaiDH == null)
            {
                return HttpNotFound();
            }
            return View(trangThaiDH);
        }

        // POST: TrangThaiDHs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrangThaiDHID,TenTrangThaiDH")] TrangThaiDH trangThaiDH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trangThaiDH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trangThaiDH);
        }

        // GET: TrangThaiDHs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrangThaiDH trangThaiDH = db.TrangThaiDHs.Find(id);
            if (trangThaiDH == null)
            {
                return HttpNotFound();
            }
            return View(trangThaiDH);
        }

        // POST: TrangThaiDHs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrangThaiDH trangThaiDH = db.TrangThaiDHs.Find(id);
            db.TrangThaiDHs.Remove(trangThaiDH);
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
