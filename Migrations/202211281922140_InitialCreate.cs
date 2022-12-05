namespace Jewels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatLieuSPs",
                c => new
                    {
                        ChatLieuSPID = c.Int(nullable: false, identity: true),
                        TenChatLieuSP = c.String(),
                    })
                .PrimaryKey(t => t.ChatLieuSPID);
            
            CreateTable(
                "dbo.SanPhams",
                c => new
                    {
                        SanPhamID = c.Int(nullable: false, identity: true),
                        SanPhamName = c.String(),
                        GiaBan = c.Int(nullable: false),
                        Moi = c.String(),
                        BanChay = c.String(),
                        Hot = c.String(),
                        SoLuong = c.Int(),
                        UrlHinhAnhSP = c.String(),
                        MoTa = c.String(),
                        LoaiSPID = c.Int(nullable: false),
                        ChatLieuSPID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SanPhamID)
                .ForeignKey("dbo.ChatLieuSPs", t => t.ChatLieuSPID, cascadeDelete: true)
                .ForeignKey("dbo.LoaiSPs", t => t.LoaiSPID, cascadeDelete: true)
                .Index(t => t.LoaiSPID)
                .Index(t => t.ChatLieuSPID);
            
            CreateTable(
                "dbo.ChiTietDHs",
                c => new
                    {
                        ChiTietDHID = c.Int(nullable: false, identity: true),
                        SoLuong = c.Int(nullable: false),
                        ThanhTien = c.Int(nullable: false),
                        SanPhamID = c.Int(nullable: false),
                        DonHangID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ChiTietDHID)
                .ForeignKey("dbo.DonHangs", t => t.DonHangID, cascadeDelete: true)
                .ForeignKey("dbo.SanPhams", t => t.SanPhamID, cascadeDelete: true)
                .Index(t => t.SanPhamID)
                .Index(t => t.DonHangID);
            
            CreateTable(
                "dbo.DonHangs",
                c => new
                    {
                        DonHangID = c.Int(nullable: false, identity: true),
                        NgayDat = c.DateTime(nullable: false),
                        TongTien = c.Int(nullable: false),
                        DienThoai = c.String(nullable: false),
                        TenKH = c.String(nullable: false),
                        DiaChi = c.String(nullable: false),
                        TrangThaiDHID = c.Int(nullable: false),
                        KhuyenMaiID = c.String(maxLength: 128),
                        HinhThucTTID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DonHangID)
                .ForeignKey("dbo.HinhThucTTs", t => t.HinhThucTTID, cascadeDelete: true)
                .ForeignKey("dbo.KhuyenMais", t => t.KhuyenMaiID)
                .ForeignKey("dbo.TrangThaiDHs", t => t.TrangThaiDHID, cascadeDelete: true)
                .Index(t => t.TrangThaiDHID)
                .Index(t => t.KhuyenMaiID)
                .Index(t => t.HinhThucTTID);
            
            CreateTable(
                "dbo.HinhThucTTs",
                c => new
                    {
                        HinhThucTTID = c.Int(nullable: false, identity: true),
                        HinhThucTTName = c.String(),
                    })
                .PrimaryKey(t => t.HinhThucTTID);
            
            CreateTable(
                "dbo.KhuyenMais",
                c => new
                    {
                        KhuyenMaiID = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        TienGiam = c.Int(nullable: false),
                        UrlHinhAnhKM = c.String(),
                        MoTa = c.String(),
                        NgayBatDau = c.DateTime(nullable: false),
                        NgayKetThuc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.KhuyenMaiID);
            
            CreateTable(
                "dbo.TrangThaiDHs",
                c => new
                    {
                        TrangThaiDHID = c.Int(nullable: false, identity: true),
                        TenTrangThaiDH = c.String(),
                    })
                .PrimaryKey(t => t.TrangThaiDHID);
            
            CreateTable(
                "dbo.LoaiSPs",
                c => new
                    {
                        LoaiSPID = c.Int(nullable: false, identity: true),
                        TenLoaiSP = c.String(),
                    })
                .PrimaryKey(t => t.LoaiSPID);
            
            CreateTable(
                "dbo.KhachHangs",
                c => new
                    {
                        KhachHangID = c.Int(nullable: false, identity: true),
                        TenKH = c.String(),
                        TichLuy = c.Int(nullable: false),
                        DienThoai = c.String(),
                        Email = c.String(),
                        DiaChi = c.String(),
                        MatKhau = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.KhachHangID);
            
            CreateTable(
                "dbo.PhanHoiKHs",
                c => new
                    {
                        PhanHoiID = c.Int(nullable: false, identity: true),
                        TenKH = c.String(),
                        MucDoHaiLong = c.String(),
                        MoTa = c.String(),
                    })
                .PrimaryKey(t => t.PhanHoiID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SanPhams", "LoaiSPID", "dbo.LoaiSPs");
            DropForeignKey("dbo.ChiTietDHs", "SanPhamID", "dbo.SanPhams");
            DropForeignKey("dbo.DonHangs", "TrangThaiDHID", "dbo.TrangThaiDHs");
            DropForeignKey("dbo.DonHangs", "KhuyenMaiID", "dbo.KhuyenMais");
            DropForeignKey("dbo.DonHangs", "HinhThucTTID", "dbo.HinhThucTTs");
            DropForeignKey("dbo.ChiTietDHs", "DonHangID", "dbo.DonHangs");
            DropForeignKey("dbo.SanPhams", "ChatLieuSPID", "dbo.ChatLieuSPs");
            DropIndex("dbo.DonHangs", new[] { "HinhThucTTID" });
            DropIndex("dbo.DonHangs", new[] { "KhuyenMaiID" });
            DropIndex("dbo.DonHangs", new[] { "TrangThaiDHID" });
            DropIndex("dbo.ChiTietDHs", new[] { "DonHangID" });
            DropIndex("dbo.ChiTietDHs", new[] { "SanPhamID" });
            DropIndex("dbo.SanPhams", new[] { "ChatLieuSPID" });
            DropIndex("dbo.SanPhams", new[] { "LoaiSPID" });
            DropTable("dbo.PhanHoiKHs");
            DropTable("dbo.KhachHangs");
            DropTable("dbo.LoaiSPs");
            DropTable("dbo.TrangThaiDHs");
            DropTable("dbo.KhuyenMais");
            DropTable("dbo.HinhThucTTs");
            DropTable("dbo.DonHangs");
            DropTable("dbo.ChiTietDHs");
            DropTable("dbo.SanPhams");
            DropTable("dbo.ChatLieuSPs");
        }
    }
}
