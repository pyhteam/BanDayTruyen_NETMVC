using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jewels.Models;
using Jewels.DAL;

namespace Jewels.Controllers
{
    public class UserController : Controller
    {
        private JewelsContext db = new JewelsContext();
        // GET: KhachHangs/Create
        public ActionResult DangKy()
        {
            return View();
        }

        // POST: KhachHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy([Bind(Include = "KhachHangID,TenKH,TichLuy,DienThoai,Email,DiaChi,MatKhau")] KhachHang khachHang)
        {

            try
            {
                khachHang.TichLuy = 0;
                // Thêm người dùng  mới
                db.KhachHangs.Add(khachHang);
                // Lưu lại vào cơ sở dữ liệu
                db.SaveChanges();
                // Nếu dữ liệu đúng thì trả về trang đăng nhập
                if (ModelState.IsValid)
                {
                    return RedirectToAction("DangNhap");
                }
                return View("Dangky");

            }
            catch
            {
                return View();
            }

        }

        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(string DienThoai, string MatKhau)
        {
            string usr = DienThoai;
            string pwp = MatKhau;

            var islogin = db.KhachHangs.SingleOrDefault(x => x.DienThoai.Equals(usr) && x.MatKhau.Equals(pwp));

            if (islogin != null)
            {
                if (DienThoai == "Ad6412")
                {

                    Session["user"] = islogin;

                    return RedirectToAction("Index", "DonHangs");
                }
                else
                {
                    Session["user"] = islogin;
                    return RedirectToAction("Index", "Home");
                    /*return RedirectToAction("ShowToCart", "ShoppingCart");*/
                }
            }
            else
            {
                ViewBag.Fail = "Đăng nhập thất bại";
                return View("DangNhap");
            }

        }
        public ActionResult DangXuat()
        {

            Session.Clear();
            return RedirectToAction("Index", "Home");

        }

    }
}