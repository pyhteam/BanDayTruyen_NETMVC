using Jewels.DAL;
using Jewels.Models;
using Jewels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jewels.Controllers
{
    public class ShoppingCartController : Controller
    {
        public JewelsContext db = new JewelsContext();
        // GET: ShoppingCart
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult AddtoCart(int id)
        {
            var sanpham = db.SanPhams.SingleOrDefault(x => x.SanPhamID == id);
            if (sanpham != null)
            {
                GetCart().Add(sanpham);
            }
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }

        public ActionResult ShowToCart()
        {
            if (Session["Cart"] == null)
                return RedirectToAction("NullCart", "ShoppingCart");
            Cart cart = Session["Cart"] as Cart;

            return View(cart);
        }
        [HttpPost]
        public JsonResult ShowSale(string KhuyenMaiID)
        {
            string idkm = "";
            int tienGiamGia = 0;
            if (!String.IsNullOrEmpty(KhuyenMaiID))
            {
                KhuyenMai khuyenMai = db.KhuyenMais.FirstOrDefault(x => x.KhuyenMaiID == KhuyenMaiID);

                if (khuyenMai == null)
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "Mã giảm giá không tồn tại"
                    });
                }
                tienGiamGia = khuyenMai.TienGiam;
                idkm = khuyenMai.KhuyenMaiID;
            }
            return Json(new
            {
                Success = true,
                IDKhuyenMai = idkm,
                TienGiamGia = tienGiamGia
            });
        }

        public ActionResult NullCart()
        {
            return View();
        }

        public ActionResult UpdateQuantityInCart(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int IDSanPham = int.Parse(form["ID_SanPham"]);
            int soluongSP = int.Parse(form["SoLuong_SP"]);
            cart.UpdateSoluongSanphamCart(IDSanPham, soluongSP);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }

        public ActionResult RemoveProductInCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.RemoveSanpham(id);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }

        [HttpPost]
        public ActionResult CreateOrder(OrderQuery orderQuery)
        {
            if (Session["Cart"] == null)
            {
                return Json(new
                {
                    Success = false,
                    Message = "Giỏ hàng trống"
                });
            }
            Cart cart = Session["Cart"] as Cart;
            // Do khách hàng không đăng nhập nên điền tay 
            if (Session["userId"] == null)
            {
                try
                {
                    DonHang donHang = new DonHang();

                    donHang.TrangThaiDHID = 1;
                    donHang.NgayDat = DateTime.Now;
                    donHang.TongTien = orderQuery.TongTien;
                    donHang.DiaChi = orderQuery.DiaChi;
                    donHang.DienThoai = orderQuery.DienThoai;
                    donHang.TenKH = orderQuery.TenKH;
                    donHang.HinhThucTTID = orderQuery.HinhThucTTID;
                    donHang.KhuyenMaiID = orderQuery.KhuyenMaiID;
                    db.DonHangs.Add(donHang);

                    foreach (var item in cart.Items)
                    {
                        ChiTietDH chiTiet = new ChiTietDH();
                        chiTiet.DonHangID = donHang.DonHangID;
                        chiTiet.SanPhamID = item.shoppingSanpham.SanPhamID;
                        chiTiet.SoLuong = item.shoppingSoluong;
                        chiTiet.ThanhTien = item.shoppingSanpham.GiaBan * item.shoppingSoluong;
                        db.ChiTietDHs.Add(chiTiet);
                    }

                   ;
                    cart.RemoveAll();

                    return Json(new
                    {
                        Success = db.SaveChanges() > 0,
                        Message = "Order Success"
                    });
                }
                catch
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "Order false"
                    });
                }
            }
            else // neu da dang nhap thi lay luôn thong tin 
            {
                int userId = int.Parse(Session["userId"].ToString());
                var userLogin = db.KhachHangs.FirstOrDefault(x => x.KhachHangID.Equals(userId));
                if (userLogin == null)
                {
                    return HttpNotFound();
                }
                try
                {
                    DonHang hoaDon = new DonHang();

                    hoaDon.TenKH = userLogin.TenKH;
                    hoaDon.DienThoai = userLogin.DienThoai;
                    hoaDon.DiaChi = userLogin.DiaChi;
                    hoaDon.NgayDat = DateTime.Now;
                    hoaDon.TongTien = orderQuery.TongTien;
                    hoaDon.KhuyenMaiID = orderQuery.KhuyenMaiID;
                    hoaDon.HinhThucTTID = orderQuery.HinhThucTTID;
                    hoaDon.TrangThaiDHID = 1;
                    db.DonHangs.Add(hoaDon);

                    foreach (var item in cart.Items)
                    {
                        ChiTietDH chiTiet = new ChiTietDH();
                        chiTiet.DonHangID = hoaDon.DonHangID;
                        chiTiet.SanPhamID = item.shoppingSanpham.SanPhamID;
                        chiTiet.SoLuong = item.shoppingSoluong;
                        chiTiet.ThanhTien = (int)(item.shoppingSanpham.GiaBan * item.shoppingSoluong);
                        db.ChiTietDHs.Add(chiTiet);
                        // update soluong
                        var sanPham = db.SanPhams.FirstOrDefault(x => x.SanPhamID == item.shoppingSanpham.SanPhamID);
                        if (sanPham == null)
                        {
                            continue;
                        }
                        sanPham.SoLuong = sanPham.SoLuong - 1;
                        db.Entry(sanPham).State = EntityState.Modified;

                        // db.SaveChanges();
                    }

                    cart.RemoveAll();
                    userLogin.TichLuy += orderQuery.TongTien;
                    db.Entry(userLogin).State = EntityState.Modified;
                    return Json(new
                    {
                        Success = db.SaveChanges() > 0,
                        Message = "Order Success"
                    });
                }
                catch
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "Order fail"
                    });
                }
            }




        }
        public ActionResult Done()
        {
            return View();
        }


        //[Authorize]
        public ActionResult Payment()
        {
            ViewBag.Giam = 0;
            ViewBag.KhuyenMaiID = new SelectList(db.KhuyenMais, "KhuyenMaiID", "KhuyenMaiID");
            ViewBag.TrangThaiDHID = new SelectList(db.TrangThaiDHs, "TrangThaiDHID", "TenTrangThaiDH");
            ViewBag.HinhThucTTID = new SelectList(db.HinhThucTTs, "HinhThucTTID", "HinhThucTTName");

            Cart cart = Session["Cart"] as Cart;
            // Check login
            KhachHang khachHang = new KhachHang();
            if (Session["userId"] != null)
            {
                int id = int.Parse(Session["userId"].ToString());
                khachHang = db.KhachHangs.FirstOrDefault(x => x.KhachHangID.Equals(id));
            }

            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }

            ViewBag.UserLogin = khachHang;
            return View(cart);
        }

        public PartialViewResult BagCart()
        {
            int tongSP = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
            {
                tongSP = cart.TotalSoluong();
            }
            ViewBag.infoCart = tongSP;
            return PartialView("BagCart");
        }
    }
}