using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jewels.Models
{
    public class DonHang
    {
        [Key]
        public int DonHangID { get; set; }
        [Display(Name = "Ngày đặt ")]
        public DateTime NgayDat { get; set; }
        [Required, Display(Name = "Tổng tiền ")]
        public int TongTien { get; set; }
        //-----
        [Required, Display(Name = "Số điện thoại ")]
        public string DienThoai { get; set; }
        [Required, Display(Name = "Tên người nhận ")]
        public string TenKH { get; set; }
        [Required, Display(Name = "Địa chỉ nhận hàng ")]
        public string DiaChi { get; set; }
        [Required, Display(Name = "Hình thức thanh toán")]
        
        // trạng thái đơn hàng 
        public int TrangThaiDHID { get; set; }
        public TrangThaiDH TrangThaiDH { get; set; }

        //  khuyen mai 
        public string KhuyenMaiID { get; set; }
        public KhuyenMai KhuyenMai { get; set; }
        // hinh thuc thanh toan
        public int HinhThucTTID { get; set; }
        public HinhThucTT HinhThucTT { get; set; }

        // khach hang 
        /*  public int KhachHangID { get; set; }
          public KhachHang KhachHang { get; set; }*/

        // chi tiet don hang 
        public ICollection<ChiTietDH> ChiTietDH { get; set; }
    }
}