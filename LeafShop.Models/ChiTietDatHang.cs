namespace LeafShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietDatHang")]
    public partial class ChiTietDatHang
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        [DisplayName("Mã đặt hàng")]
        public string MaDatHang { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        [DisplayName("Mã sản phẩm")]
        public string MaSanPham { get; set; }

        [DisplayName("Số lượng")]
        public int? SoLuong { get; set; }

        [DisplayName("Đơn giá")]
        public int? DonGia { get; set; }

        [DisplayName("Thành tiền")]
        public int? ThanhTien { get; set; }

        [DisplayName("Ngày đặt hàng")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString =
        "{0:yyyy-MM-dd}",
        ApplyFormatInEditMode = true)]
        public DateTime? NgayDatHang { get; set; }

        [DisplayName("Ngày giao hàng")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString =
        "{0:yyyy-MM-dd}",
        ApplyFormatInEditMode = true)]
        public DateTime? NgayGiaoHang { get; set; }

        public virtual DatHang DatHang { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
