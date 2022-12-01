using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jewels.Models
{
    public class HinhThucTT
    {
        [Key]
        public int HinhThucTTID { get; set; }
        [Display(Name ="Hình thức thanh toán ")]
        public string HinhThucTTName { get; set; }
        // don hang 
        public ICollection<DonHang> DonHang { get; set; }
    }
}