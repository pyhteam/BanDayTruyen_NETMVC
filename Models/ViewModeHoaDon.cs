using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jewels.Models
{
    public class ViewModeHoaDon 
    {
       
        public int? DonHangID { get; set; }
        public DateTime NgayDat { get; set; }
        public int TongTien { get; set; }
        public string KhuyenMaiID { get; set; }
        public int TienGiam { get; set; }
        public string DienThoai { get; set; }
        public string DiaChi { get; set; }
        public string TenKH { get; set; }
        public string TenTrangThaiDH { get; set; }
        public int ChiTietDHID { get; set; }
        public string SanPhamName { get; set; }
        public int GiaBan { get; set; }
        public int SoLuong { get; set; }
        public int ThanhTien { get; set; }
        public string HinhThucTTName { get; set; }

    }
}