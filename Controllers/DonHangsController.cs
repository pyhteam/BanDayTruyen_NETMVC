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
    public class DonHangsController : Controller
    {
        private JewelsContext db = new JewelsContext();

        // GET: DonHangs
        /* public ActionResult Index()
         {
             var donHangs = db.DonHangs.Include(d => d.KhachHang).Include(d => d.KhuyenMai).Include(d => d.TrangThaiDH);
             return View(donHangs.ToList());
         }*/

        public ActionResult Index(int? page)
        {

            /* if (Session["user"] == null)
             {
                 return RedirectToAction("DangNhap", "User");
             }*/

            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 8;
            var IsDonHangs = db.DonHangs.AsNoTracking()
                .Include(d => d.KhuyenMai)
                .Include(d => d.TrangThaiDH)
                .Include(d => d.HinhThucTT)
                .OrderByDescending(x => x.DonHangID); // sắp xếp
            PagedList<DonHang> models = new PagedList<DonHang>(IsDonHangs, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;


            return View(models);
        }
        //dang xu ly 
        public ActionResult DangXuLy(int? page)
        {
            /* if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "User");
            }*/

            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 8;
            var IsDonHangs = db.DonHangs.AsNoTracking()
                .Include(d => d.KhuyenMai)
                .Include(d => d.TrangThaiDH)
                .Include(d => d.HinhThucTT)
                .OrderByDescending(x => x.DonHangID)// sắp xếp
                .Where(x => x.TrangThaiDHID == 1).ToList(); // dieu kien 
            PagedList<DonHang> models = new PagedList<DonHang>(IsDonHangs, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;


            return View(models);
        }
        //dang giao hang 
        public ActionResult DangGiaoHang(int? page)
        {
            /* if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "User");
            }*/

            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 8;
            var IsDonHangs = db.DonHangs.AsNoTracking()
                .Include(d => d.KhuyenMai)
                .Include(d => d.TrangThaiDH)
                .Include(d => d.HinhThucTT)
                .OrderByDescending(x => x.DonHangID)// sắp xếp
                .Where(x => x.TrangThaiDHID == 2).ToList(); // dieu kien 
            PagedList<DonHang> models = new PagedList<DonHang>(IsDonHangs, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;


            return View(models);
        }
        //da hoan thanh
        public ActionResult DaHoanThanh(int? page)
        {
            /* if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "User");
            }*/

            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 8;
            var IsDonHangs = db.DonHangs.AsNoTracking()
                .Include(d => d.KhuyenMai)
                .Include(d => d.TrangThaiDH)
                .Include(d => d.HinhThucTT)
                .OrderByDescending(x => x.DonHangID)// sắp xếp
                .Where(x => x.TrangThaiDHID == 3).ToList(); // dieu kien 
            PagedList<DonHang> models = new PagedList<DonHang>(IsDonHangs, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;


            return View(models);
        }
        //da hoan thanh
        public ActionResult DaHuy(int? page)
        {
            /* if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "User");
            }*/

            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 8;
            var IsDonHangs = db.DonHangs.AsNoTracking()
                .Include(d => d.KhuyenMai)
                .Include(d => d.TrangThaiDH)
                .Include(d => d.HinhThucTT)
                .OrderByDescending(x => x.DonHangID)// sắp xếp
                .Where(x => x.TrangThaiDHID == 4).ToList(); // dieu kien 
            PagedList<DonHang> models = new PagedList<DonHang>(IsDonHangs, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;


            return View(models);
        }

        // GET: DonHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DonHang donHang = db.DonHangs.Find(id);


            List<DonHang> donHangs = db.DonHangs.ToList();
            List<ChiTietDH> chiTietDHs = db.ChiTietDHs.Where(x => x.DonHangID == id).ToList();
            List<KhachHang> khachHangs = db.KhachHangs.ToList();
            List<SanPham> sanPhams = db.SanPhams.ToList();
            List<KhuyenMai> khuyenMais = db.KhuyenMais.ToList();
            List<TrangThaiDH> trangThaiDHs = db.TrangThaiDHs.ToList();
            List<HinhThucTT> hinhThucTTs = db.HinhThucTTs.ToList();

            var countCTID = db.ChiTietDHs.Where(x => x.DonHangID == id).Count();




            var IsCTHDs = (from d in db.DonHangs
                           join ct in db.ChiTietDHs on d.DonHangID equals ct.DonHangID
                           join km in db.KhuyenMais on d.KhuyenMaiID equals km.KhuyenMaiID
                           join sp in db.SanPhams on ct.SanPhamID equals sp.SanPhamID
                           join tt in db.TrangThaiDHs on d.TrangThaiDHID equals tt.TrangThaiDHID
                           join ht in db.HinhThucTTs on d.HinhThucTTID equals ht.HinhThucTTID
                           where d.DonHangID == id
                           select new ViewModeHoaDon
                           {
                               DonHangID = id,
                               NgayDat = d.NgayDat,
                               TongTien = d.TongTien,
                               DienThoai = d.DienThoai,
                               TenKH = d.TenKH,
                               DiaChi = d.DiaChi,
                               KhuyenMaiID = d.KhuyenMaiID,
                               TienGiam = km.TienGiam,
                               TenTrangThaiDH = tt.TenTrangThaiDH,
                               HinhThucTTName = ht.HinhThucTTName,

                               ChiTietDHID = ct.ChiTietDHID,
                               SanPhamName = sp.SanPhamName,
                               GiaBan = sp.GiaBan,
                               SoLuong = ct.SoLuong,
                               ThanhTien = ct.ThanhTien

                           }).ToList();

            ViewBag.CountID = countCTID;
            if (donHang == null)
            {
                return HttpNotFound();
            }

            return View(IsCTHDs);



        }

        // GET: DonHangs/Create
        public ActionResult Create()
        {

            ViewBag.KhuyenMaiID = new SelectList(db.KhuyenMais, "KhuyenMaiID", "KhuyenMaiID");
            ViewBag.TrangThaiDHID = new SelectList(db.TrangThaiDHs, "TrangThaiDHID", "TenTrangThaiDH");
            ViewBag.HinhThucTTID = new SelectList(db.HinhThucTTs, "HinhThucTTID", "HinhThucTTName");
            return View();
        }

        // POST: DonHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DonHangID,NgayDat,TongTien,DienThoai,TenKH,DiaChi,TrangThaiDHID,HinhThucTTID,KhuyenMaiID")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                db.DonHangs.Add(donHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KhuyenMaiID = new SelectList(db.KhuyenMais, "KhuyenMaiID", "KhuyenMaiID", donHang.KhuyenMaiID);
            ViewBag.TrangThaiDHID = new SelectList(db.TrangThaiDHs, "TrangThaiDHID", "TenTrangThaiDH", donHang.TrangThaiDHID);
            ViewBag.HinhThucTTID = new SelectList(db.HinhThucTTs, "HinhThucTTID", "HinhThucTTName", donHang.HinhThucTTID);
            return View(donHang);
        }

        // GET: DonHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.KhuyenMaiID = new SelectList(db.KhuyenMais, "KhuyenMaiID", "KhuyenMaiID", donHang.KhuyenMaiID);
            ViewBag.TrangThaiDHID = new SelectList(db.TrangThaiDHs, "TrangThaiDHID", "TenTrangThaiDH", donHang.TrangThaiDHID);
            ViewBag.HinhThucTTID = new SelectList(db.HinhThucTTs, "HinhThucTTID", "HinhThucTTName", donHang.HinhThucTTID);
            return View(donHang);
        }

        // POST: DonHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DonHangID,NgayDat,TongTien,DienThoai,TenKH,DiaChi,TrangThaiDHID,HinhThucTTID,KhuyenMaiID")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donHang).State = EntityState.Modified;

                // update soluong
                if (donHang.TrangThaiDHID == 4)
                {
                    var chiTietDHs = db.ChiTietDHs.Where(x => x.DonHangID == donHang.DonHangID).ToList();
                    if (chiTietDHs != null)
                    {
                        foreach (var item in chiTietDHs)
                        {
                            var sanPham = db.SanPhams.FirstOrDefault(x => x.SanPhamID == item.SanPhamID);
                            if (sanPham == null)
                            {
                                continue;
                            }
                            sanPham.SoLuong += 1;
                            db.Entry(sanPham).State = EntityState.Modified;
                        }


                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KhuyenMaiID = new SelectList(db.KhuyenMais, "KhuyenMaiID", "KhuyenMaiID", donHang.KhuyenMaiID);
            ViewBag.TrangThaiDHID = new SelectList(db.TrangThaiDHs, "TrangThaiDHID", "TenTrangThaiDH", donHang.TrangThaiDHID);
            ViewBag.HinhThucTTID = new SelectList(db.HinhThucTTs, "HinhThucTTID", "HinhThucTTName", donHang.HinhThucTTID);
            return View(donHang);
        }

        // GET: DonHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHangs.Find(id);

            KhuyenMai khuyenMai = db.KhuyenMais.FirstOrDefault(x => x.KhuyenMaiID == donHang.KhuyenMaiID);
            TrangThaiDH trangThaiDH = db.TrangThaiDHs.FirstOrDefault(x => x.TrangThaiDHID == donHang.TrangThaiDHID);
            HinhThucTT hinhThucTT = db.HinhThucTTs.FirstOrDefault(x => x.HinhThucTTID == donHang.HinhThucTTID);



            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }

        // POST: DonHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonHang donHang = db.DonHangs.Find(id);
            db.DonHangs.Remove(donHang);
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