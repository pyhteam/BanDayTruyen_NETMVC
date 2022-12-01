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
    public class ProductDetailController : Controller
    {
        private JewelsContext db = new JewelsContext();
        // GET: ProductDetail
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);

            ChatLieuSP chatLieu = db.ChatLieuSPs.FirstOrDefault(x => x.ChatLieuSPID == sanPham.ChatLieuSPID);
            LoaiSP loaiSP = db.LoaiSPs.FirstOrDefault(x => x.LoaiSPID == sanPham.LoaiSPID);



            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);

        }
    }
}
