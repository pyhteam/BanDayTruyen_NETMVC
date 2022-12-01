using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jewels.Models
{
    public class KhuyenMai
    {
        [Key]
        public string KhuyenMaiID { get; set; }
        [Display(Name = "Tiêu đề  ")]
        public string Title { get; set; }
        
        [Display(Name = "Số tiền giảm ")]
        public int TienGiam { get; set; }
        [Display(Name = "Url Hình ảnh")]
        public string UrlHinhAnhKM { get; set; }
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }
        [Display(Name = "Ngày bắt đầu ")]
        public DateTime NgayBatDau { get; set; }
        [Display(Name = "Ngày kết thúc ")]
        public DateTime NgayKetThuc { get; set; }

        // đơn hàng 
        public ICollection<DonHang> DonHang {get; set; }
    }
}