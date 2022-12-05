using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jewels.Models
{
    public class SanPham
    {
        [Key] 
        public int SanPhamID { get; set; }
        [Display(Name = "Tên sản phẩm")]
        public string SanPhamName { get; set; }
        [Display(Name = "Giá Bán ")]
        public int GiaBan { get; set; }
        [Display(Name = "Sản phẩm mới ")]
        public string Moi { get; set; }
        [Display(Name = "Sản phẩm bán chạy ")]
        public string BanChay { get; set; }
        [Display(Name = "Sản phẩm hot ")]
        public string Hot { get; set; }
        [Display(Name = "Số lượng ")]
        public int SoLuong { get; set; }
        [Display(Name = "Url hình ảnh ")]
        public string UrlHinhAnhSP { get; set; }
        [Display(Name = "Mô tả  ")]
        public string MoTa { get; set; }

        // loai sp
        public int LoaiSPID { get; set; }
        public LoaiSP LoaiSP  { get; set; }

        // chat lieu sp
        public int ChatLieuSPID { get; set; }
        public ChatLieuSP ChatLieuSP { get; set; }
        // don hang chi tiet 
        public ICollection<ChiTietDH> ChiTietDH { get; set; }
       
    }
}