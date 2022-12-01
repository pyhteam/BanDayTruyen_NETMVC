using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jewels.Models
{
    public class TrangThaiDH
    {
        [Key]
        public int TrangThaiDHID { get; set; }
        [Display(Name = "Trạng thái đơn hàng ")]
        public string TenTrangThaiDH { get; set; }
        // don hang 
        public ICollection<DonHang> DonHang { get; set; }

    }
}