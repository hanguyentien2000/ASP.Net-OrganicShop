namespace LeafShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            ChiTietDatHangs = new HashSet<ChiTietDatHang>();
        }

        [Key]
        public int MaSanPham { get; set; }

        [StringLength(500)]
        public string TenSanPham { get; set; }

        public int? MaDanhMuc { get; set; }

        public int? MaThuongHieu { get; set; }

        public int? MaKhuVuc { get; set; }

        [StringLength(50)]
        public string DonViTinh { get; set; }

        public int? SoLuong { get; set; }

        public int? SoLuongBan { get; set; }

        public int? DonGia { get; set; }

        [StringLength(2000)]
        public string MoTa { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString =
        "{0:yyyy-MM-dd}",
        ApplyFormatInEditMode = true)]
        public DateTime? NgayKhoiTao { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString =
        "{0:yyyy-MM-dd}",
        ApplyFormatInEditMode = true)]
        public DateTime? NgayCapNhat { get; set; }

        [StringLength(1000)]
        public string HinhMinhHoa { get; set; }
        public bool TrangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDatHang> ChiTietDatHangs { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }

        public virtual KhuVuc KhuVuc { get; set; }

        public virtual ThuongHieu ThuongHieu { get; set; }
    }
}
