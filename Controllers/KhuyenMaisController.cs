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
using PagedList;

namespace Jewels.Controllers
{
    public class KhuyenMaisController : Controller
    {
        private JewelsContext db = new JewelsContext();

        /*public ActionResult Index()
        {
            return View(db.KhuyenMais.ToList());
        }
*/
        public ActionResult Index(int? page)
        {

            /* if (Session["user"] == null)
             {
                 return RedirectToAction("DangNhap", "User");
             }*/

            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 6;
            var IsKhuyenMais = db.KhuyenMais.AsNoTracking()
                   .OrderByDescending(x => x.KhuyenMaiID);
            PagedList<KhuyenMai> models = new PagedList<KhuyenMai>(IsKhuyenMais, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;


            return View(models);
        }
       
        // GET: KhuyenMais/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KhuyenMais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KhuyenMaiID,Title,TienGiam,MoTa,UrlHinhAnhKM,NgayBatDau,NgayKetThuc")] KhuyenMai khuyenMai)
        {
            
            if (ModelState.IsValid)
            {
                if (khuyenMai.NgayKetThuc.Date > DateTime.Now.Date)
                {
                        db.KhuyenMais.Add(khuyenMai);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                }
              
            }

            return View(khuyenMai);
        }

        // GET: KhuyenMais/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhuyenMai khuyenMai = db.KhuyenMais.Find(id);
            if (khuyenMai == null)
            {
                return HttpNotFound();
            }
            return View(khuyenMai);
        }

        // POST: KhuyenMais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KhuyenMaiID,Title,TienGiam,MoTa,UrlHinhAnhKM,NgayBatDau,NgayKetThuc")] KhuyenMai khuyenMai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khuyenMai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(khuyenMai);
        }

        // GET: KhuyenMais/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhuyenMai khuyenMai = db.KhuyenMais.Find(id);
            if (khuyenMai == null)
            {
                return HttpNotFound();
            }
            return View(khuyenMai);
        }

        // POST: KhuyenMais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            KhuyenMai khuyenMai = db.KhuyenMais.Find(id);
            db.KhuyenMais.Remove(khuyenMai);
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
