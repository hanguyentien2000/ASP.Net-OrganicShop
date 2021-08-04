namespace LeafShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DatHang")]
    public partial class DatHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DatHang()
        {
            ChiTietDatHangs = new HashSet<ChiTietDatHang>();
        }

        [Key]
        [StringLength(10)]
        [DisplayName("Mã đặt hàng")]
        public string MaDatHang { get; set; }

        [StringLength(10)]
        [DisplayName("Mã khách hàng")]
        public string MaKhachHang { get; set; }

        [StringLength(10)]
        [DisplayName("Mã nhân viên")]
        public string MaNhanVien { get; set; }

        [DisplayName("Tổng tiền")]
        public int? TongTien { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Ngày khởi tạo")]
        public DateTime? NgayKhoiTao { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDatHang> ChiTietDatHangs { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
