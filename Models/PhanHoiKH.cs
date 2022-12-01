using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jewels.Models
{
    public class PhanHoiKH
    {
        [Key]
        public int PhanHoiID { get; set; }
        [Display(Name = "Tên khách hàng ")]
        public string TenKH { get; set; }
        [Display(Name = "Mức độ hài lòng")]
        public string MucDoHaiLong { get; set; }
        [Display(Name = "Ý kiến khách hàng ")]
        public string MoTa { get; set; }
    }
}