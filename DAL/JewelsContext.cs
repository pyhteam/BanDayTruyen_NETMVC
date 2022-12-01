using Jewels.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Jewels.DAL
{
    public class JewelsContext : DbContext
    {
        public JewelsContext() : base("name=JewelsDbContext") { }

        public DbSet<ChatLieuSP> ChatLieuSPs { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<LoaiSP> LoaiSPs { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<TrangThaiDH> TrangThaiDHs { get; set; }
        public DbSet<ChiTietDH> ChiTietDHs { get; set; }
        public DbSet<KhuyenMai> KhuyenMais { get; set; }
        public DbSet<PhanHoiKH> PhanHoiKHs { get; set; }
        public DbSet<HinhThucTT> HinhThucTTs { get; set; }

    }

}