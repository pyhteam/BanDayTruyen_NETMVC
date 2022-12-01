using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Jewels.Models
{
    public class CartItem
    {
        [Key]
        public SanPham shoppingSanpham { get; set; }
        
       
        public int shoppingSoluong { get; set; }
    }
    public class Cart
    {
        public List<CartItem> items = new List<CartItem>();

        public string KhuyenMaiID { get; set; }
        public KhuyenMai KhuyenMai { get; set; }

        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }
        public void Add(SanPham sanpham, int soluong = 1)
        {
            var item = items.FirstOrDefault(x => x.shoppingSanpham.SanPhamID == sanpham.SanPhamID);
            if (item == null)
            {
                items.Add(new CartItem
                {
                    shoppingSanpham = sanpham,
                    shoppingSoluong = soluong,
                });
            }
            else
            {
                item.shoppingSoluong += soluong;
            }
        }
        public void UpdateSoluongSanphamCart(int id, int soluong)
        {
            var item = items.Find(x => x.shoppingSanpham.SanPhamID == id);
            if (item != null)
            {
                item.shoppingSoluong = soluong;
            }
        }
        public int TotalPrice()
        {
            
            var total = items.Sum(x => x.shoppingSanpham.GiaBan * x.shoppingSoluong);
          
            return (int)total;
        }
        public void RemoveSanpham(int id)
        {
            items.RemoveAll(x => x.shoppingSanpham.SanPhamID == id);
        }

        public int TotalSoluong()
        {
            return items.Sum(x => x.shoppingSoluong);
        }

        public void RemoveAll()
        {
            items.Clear();
        }
    }
}