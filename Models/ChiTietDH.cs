using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jewels.Models
{
    public class ChiTietDH
    {
        [Key]
        public int ChiTietDHID {get; set;}
        [Display(Name = "Số lượng")]
        public int SoLuong {get; set;}
        [Display(Name = "Thành tiền")]
        public int ThanhTien { get; set; }

        // san pham
        public int SanPhamID { get; set; }
        public SanPham SanPham { get; set; }
     
        // don hang 
        public int DonHangID { get; set; }  
        public DonHang DonHang { get; set; }

    }
}