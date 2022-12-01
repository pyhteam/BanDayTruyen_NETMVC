using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jewels.Models
{
    public class ChatLieuSP
    {
        [Key]
        public int ChatLieuSPID { get; set; }
        [Display(Name = "Tên chất liệu sản phẩm")]
        public string TenChatLieuSP { get; set; }

        // san pham 
        public ICollection<SanPham> SanPham { get; set; }
    }
}