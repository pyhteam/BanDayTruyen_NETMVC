using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jewels.Models
{
    public class LoaiSP
    {
        [Key] 
        public int LoaiSPID {get; set;}
        [Display(Name = "Tên loại sản phẩm")]
        public string TenLoaiSP {get; set;}

        // san pham 
        public ICollection<SanPham> SanPham  {get; set;}
    }
}