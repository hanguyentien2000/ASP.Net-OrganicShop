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
        [StringLength(10)]
        public string MaSanPham { get; set; }

        [Required]
        [StringLength(500)]
        public string TenSanPham { get; set; }

        [StringLength(10)]
        public string MaDanhMuc { get; set; }

        [StringLength(10)]
        public string MaThuongHieu { get; set; }

        [StringLength(10)]
        public string MaKhuVuc { get; set; }

        [StringLength(50)]
        public string DonViTinh { get; set; }

        public int? SoLuong { get; set; }

        public int? SoLuongBan { get; set; }

        public int? DonGia { get; set; }

        [Column(TypeName = "ntext")]
        public string MoTa { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? NgayCapNhat { get; set; }

        [StringLength(1000)]
        public string HinhMinhHoa { get; set; }

        [Column(TypeName = "text")]
        public string BinhLuan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDatHang> ChiTietDatHangs { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }

        public virtual KhuVuc KhuVuc { get; set; }

        public virtual ThuongHieu ThuongHieu { get; set; }
    }
}
