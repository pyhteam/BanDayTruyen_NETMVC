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
    public class PhanHoiKHsController : Controller
    {
        private JewelsContext db = new JewelsContext();


        // chi co index voi delete
        // GET: PhanHoiKHs
        public ActionResult Index(int? page)
        {
            /* if (Session["user"] == null) 
            {
                return RedirectToAction("DangNhap", "User");
            }*/

            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 5;
            var IsPhanHoiKHs = db.PhanHoiKHs.AsNoTracking()
                   .OrderBy(x => x.PhanHoiID );
            PagedList<PhanHoiKH> models = new PagedList<PhanHoiKH>(IsPhanHoiKHs, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;


            return View(models);
        }
       
        // GET: PhanHoiKHs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhanHoiKH phanHoiKH = db.PhanHoiKHs.Find(id);
            if (phanHoiKH == null)
            {
                return HttpNotFound();
            }
            return View(phanHoiKH);
        }

        // POST: PhanHoiKHs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhanHoiKH phanHoiKH = db.PhanHoiKHs.Find(id);
            db.PhanHoiKHs.Remove(phanHoiKH);
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

        /*
                public ActionResult Create()
                    {
                        return View();
                    }

                    // POST: PhanHoiKHs/Create
                    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
                    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
                    [HttpPost]
                    [ValidateAntiForgeryToken]
                    public ActionResult Create([Bind(Include = "PhanHoiID,TenKH,MucDoHaiLong,MoTa")] PhanHoiKH phanHoiKH)
                    {
                        if (ModelState.IsValid)
                        {
                            db.PhanHoiKHs.Add(phanHoiKH);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }

                        return View(phanHoiKH);
                    }
                // GET: PhanHoiKHs/Details/5
                public ActionResult Details(int? id)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    PhanHoiKH phanHoiKH = db.PhanHoiKHs.Find(id);
                    if (phanHoiKH == null)
                    {
                        return HttpNotFound();
                    }
                    return View(phanHoiKH);
                }

                // GET: PhanHoiKHs/Create


                // GET: PhanHoiKHs/Edit/5
                public ActionResult Edit(int? id)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    PhanHoiKH phanHoiKH = db.PhanHoiKHs.Find(id);
                    if (phanHoiKH == null)
                    {
                        return HttpNotFound();
                    }
                    return View(phanHoiKH);
                }

                // POST: PhanHoiKHs/Edit/5
                // To protect from overposting attacks, enable the specific properties you want to bind to, for 
                // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
                [HttpPost]
                [ValidateAntiForgeryToken]
                public ActionResult Edit([Bind(Include = "PhanHoiID,TenKH,MucDoHaiLong,MoTa")] PhanHoiKH phanHoiKH)
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(phanHoiKH).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(phanHoiKH);
                }


                */
    }
}