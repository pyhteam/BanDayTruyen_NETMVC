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
    public class SanPhamsController : Controller
    {
        private JewelsContext db = new JewelsContext();

        // GET: SanPhams
        /*public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.ChatLieuSP).Include(s => s.LoaiSP);
            return View(sanPhams.ToList());
        }*/
        public ActionResult Index(int? page)
        {

            /* if (Session["user"] == null)
             {
                 return RedirectToAction("DangNhap", "User");
             }*/

            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 8;
            var IsSanPhams = db.SanPhams.AsNoTracking()
                .Include(s => s.ChatLieuSP)
                .Include(s => s.LoaiSP)
                   .OrderByDescending(x => x.SanPhamID); // sắp xếp
            PagedList<SanPham> models = new PagedList<SanPham>(IsSanPhams, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;


            return View(models);
        }

        // GET: SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);

            ChatLieuSP chatLieu = db.ChatLieuSPs.FirstOrDefault(x => x.ChatLieuSPID == sanPham.ChatLieuSPID);
            LoaiSP loaiSP = db.LoaiSPs.FirstOrDefault(x => x.LoaiSPID == sanPham.LoaiSPID);

            ViewBag.tenCL = chatLieu.TenChatLieuSP;

            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.ChatLieuSPID = new SelectList(db.ChatLieuSPs, "ChatLieuSPID", "TenChatLieuSP");
            ViewBag.LoaiSPID = new SelectList(db.LoaiSPs, "LoaiSPID", "TenLoaiSP");
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SanPhamID,SanPhamName,GiaBan,Moi,BanChay,Hot,SoLuong,UrlHinhAnhSP,MoTa,LoaiSPID,ChatLieuSPID")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ChatLieuSPID = new SelectList(db.ChatLieuSPs, "ChatLieuSPID", "TenChatLieuSP", sanPham.ChatLieuSPID);
            ViewBag.LoaiSPID = new SelectList(db.LoaiSPs, "LoaiSPID", "TenLoaiSP", sanPham.LoaiSPID);
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChatLieuSPID = new SelectList(db.ChatLieuSPs, "ChatLieuSPID", "TenChatLieuSP", sanPham.ChatLieuSPID);
            ViewBag.LoaiSPID = new SelectList(db.LoaiSPs, "LoaiSPID", "TenLoaiSP", sanPham.LoaiSPID);
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SanPhamID,SanPhamName,GiaBan,Moi,BanChay,Hot,SoLuong,UrlHinhAnhSP,MoTa,LoaiSPID,ChatLieuSPID")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ChatLieuSPID = new SelectList(db.ChatLieuSPs, "ChatLieuSPID", "TenChatLieuSP", sanPham.ChatLieuSPID);
            ViewBag.LoaiSPID = new SelectList(db.LoaiSPs, "LoaiSPID", "TenLoaiSP", sanPham.LoaiSPID);

            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
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