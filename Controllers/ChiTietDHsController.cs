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
    public class ChiTietDHsController : Controller
    {
        private JewelsContext db = new JewelsContext();
        // chỉ có edit  + delete
        // GET: ChiTietDHs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDH chiTietDH = db.ChiTietDHs.Find(id);
            if (chiTietDH == null)
            {
                return HttpNotFound();
            }
            ViewBag.DonHangID = new SelectList(db.DonHangs, "DonHangID", "DonHangID", chiTietDH.DonHangID);
            ViewBag.SanPhamID = new SelectList(db.SanPhams, "SanPhamID", "SanPhamName", chiTietDH.SanPhamID);
            return View(chiTietDH);
        }

        // POST: ChiTietDHs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChiTietDHID,SoLuong,ThanhTien,SanPhamID,DonHangID")] ChiTietDH chiTietDH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietDH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DonHangID = new SelectList(db.DonHangs, "DonHangID", "DonHangID", chiTietDH.DonHangID);
            ViewBag.SanPhamID = new SelectList(db.SanPhams, "SanPhamID", "SanPhamName", chiTietDH.SanPhamID);
            return View(chiTietDH);
        }

        // GET: ChiTietDHs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDH chiTietDH = db.ChiTietDHs.Find(id);

            DonHang donHang = db.DonHangs.FirstOrDefault(x => x.DonHangID == chiTietDH.DonHangID);
            SanPham sanPham = db.SanPhams.FirstOrDefault(x => x.SanPhamID == chiTietDH.SanPhamID);




            if (chiTietDH == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDH);
        }

        // POST: ChiTietDHs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietDH chiTietDH = db.ChiTietDHs.Find(id);
            db.ChiTietDHs.Remove(chiTietDH);
            db.SaveChanges();
            DonHang donHang = db.DonHangs.FirstOrDefault(x => x.DonHangID == chiTietDH.DonHangID);

            return RedirectToAction("Details", "DonHangs", new { id = donHang.DonHangID });
        }


        /*
         
        // GET: ChiTietDHs
        public ActionResult Index()
        {
            var chiTietDHs = db.ChiTietDHs.Include(c => c.DonHang).Include(c => c.SanPham);
            return View(chiTietDHs.ToList());
        }

        // GET: ChiTietDHs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDH chiTietDH = db.ChiTietDHs.Find(id);
            if (chiTietDH == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDH);
        }

        // GET: ChiTietDHs/Create
        public ActionResult Create()
        {
            ViewBag.DonHangID = new SelectList(db.DonHangs, "DonHangID", "DonHangID");
            ViewBag.SanPhamID = new SelectList(db.SanPhams, "SanPhamID", "SanPhamName");
            return View();
        }

        // POST: ChiTietDHs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChiTietDHID,SoLuong,ThanhTien,SanPhamID,DonHangID")] ChiTietDH chiTietDH)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietDHs.Add(chiTietDH);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DonHangID = new SelectList(db.DonHangs, "DonHangID", "DonHangID", chiTietDH.DonHangID);
            ViewBag.SanPhamID = new SelectList(db.SanPhams, "SanPhamID", "SanPhamName", chiTietDH.SanPhamID);
            return View(chiTietDH);
        }

        */

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