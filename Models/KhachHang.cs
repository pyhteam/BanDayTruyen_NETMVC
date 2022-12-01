using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jewels.Models
{
    public class KhachHang
    {
        public  KhachHang () { }
        public KhachHang(string dienThoai, string matKhau)
        {
            DienThoai = dienThoai;
            MatKhau = matKhau; 
        }
        [Key]
        public int KhachHangID { get; set; }

        [Display(Name = "Tên khách hàng")]
        public string TenKH {get; set; }
        [Display(Name = "Tích lũy")]
        public int TichLuy { get; set; }

        [Display(Name = "Số điện thoại"), ]
        public string DienThoai { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [Display(Name = "Mật Khẩu"), Required(ErrorMessage = "Password Required!")]
        public string MatKhau { get; set; }

        // don hang
       /* public ICollection<DonHang> DonHang { get; set;}*/

      
    }
}