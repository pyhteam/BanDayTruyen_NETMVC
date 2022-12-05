using Jewels.Models;
using System;

namespace Jewels.ViewModels
{
    public class OrderQuery
    {
        public int DonHangID { get; set; }
        public DateTime NgayDat { get; set; } = DateTime.Now;
        public int TongTien { get; set; }
        public int TienGiamGia { get; set; }
        public string DienThoai { get; set; }
        public string TenKH { get; set; }
        public string DiaChi { get; set; }
        public int TrangThaiDHID { get; set; } = 1;
        public string KhuyenMaiID { get; set; }
        public int HinhThucTTID { get; set; }
    }
}