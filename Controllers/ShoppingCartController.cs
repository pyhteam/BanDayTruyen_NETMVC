using Jewels.DAL;
using Jewels.Models;
using System;
using System.Collections.Generic;
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
        
        public ActionResult ShowSale(string KhuyenMaiID)
        {
            string idkm = "";
            if (!String.IsNullOrEmpty(KhuyenMaiID))
            { 
                KhuyenMai khuyenMai = db.KhuyenMais.FirstOrDefault(x => x.KhuyenMaiID == KhuyenMaiID);
               
                    ViewBag.Giam = khuyenMai.TienGiam;
                    idkm = khuyenMai.KhuyenMaiID.ToString();
               
            }
            else
            {
                ViewBag.Giam = 0;
            }

            ViewBag.IDKhuyenMai = idkm;
            ViewBag.KhuyenMaiID = new SelectList(db.KhuyenMais, "KhuyenMaiID", "KhuyenMaiID");
            return View("Payment");
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

       
        public ActionResult CreateOrder()
        {
            ViewBag.KhuyenMaiID = new SelectList(db.KhuyenMais, "KhuyenMaiID", "KhuyenMaiID");
            ViewBag.TrangThaiDHID = new SelectList(db.TrangThaiDHs, "TrangThaiDHID", "TenTrangThaiDH");
            ViewBag.HinhThucTTID = new SelectList(db.HinhThucTTs, "HinhThucTTID", "HinhThucTTName");

            Cart cart = Session["Cart"] as Cart;
            if (Session["user"] == null)
            {
                try
                {
                    DonHang donHang = new DonHang();

                    donHang.TrangThaiDHID = 1;

                   /* donHang.TenKH = donHang.TenKH;
                    hoaDon.DienThoai = dienThoai;
                    hoaDon.DiaChi = diaChi;*/
                    
                    donHang.NgayDat = DateTime.Now;
                    donHang.TongTien = cart.TotalPrice();

                    db.DonHangs.Add(donHang);
                    db.SaveChanges();
                    foreach (var item in cart.Items)
                    {
                        ChiTietDH chiTiet = new ChiTietDH();
                        chiTiet.DonHangID = donHang.DonHangID;

                        chiTiet.SanPhamID = item.shoppingSanpham.SanPhamID;
                        chiTiet.SoLuong = item.shoppingSoluong;
                        chiTiet.ThanhTien = (int)(item.shoppingSanpham.GiaBan * item.shoppingSoluong);

                        db.ChiTietDHs.Add(chiTiet);
                        db.SaveChanges();
                    }

                    db.SaveChanges();
                    cart.RemoveAll();
                    return RedirectToAction("Done", "ShoppingCart");
                }
                catch
                {
                    return RedirectToAction("Payment", "ShoppingCart");
                }
            }
            else
            {
                var u = Session["user"] as Jewels.Models.KhachHang;
                    try
                    {
                            DonHang hoaDon = new DonHang();

                            hoaDon.TenKH = u.TenKH;
                            hoaDon.DienThoai = u.DienThoai;
                            hoaDon.DiaChi = u.DiaChi;
                            hoaDon.NgayDat = DateTime.Now;
                            hoaDon.TongTien = (int)(cart.TotalPrice());

                            db.DonHangs.Add(hoaDon);
                            db.SaveChanges();

                            foreach (var item in cart.Items)
                            {
                                ChiTietDH chiTiet = new ChiTietDH();
                                chiTiet.DonHangID = hoaDon.DonHangID;

                                chiTiet.SanPhamID = item.shoppingSanpham.SanPhamID;
                                chiTiet.SoLuong = item.shoppingSoluong;
                                chiTiet.ThanhTien = (int)(item.shoppingSanpham.GiaBan * item.shoppingSoluong);

                                db.ChiTietDHs.Add(chiTiet);
                                db.SaveChanges();
                            }

                            db.SaveChanges();
                            cart.RemoveAll();
                            return RedirectToAction("Done", "ShoppingCart");
                    }
                    catch
                    {
                            return RedirectToAction("Payment", "ShoppingCart");
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
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
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