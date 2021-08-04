namespace LeafShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHang")]
    public partial class KhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHang()
        {
            DatHangs = new HashSet<DatHang>();
        }

        [Key]
        [StringLength(10)]
        [DisplayName("Mã khách hàng")]
        public string MaKhachHang { get; set; }

        [Required(ErrorMessage = "Tên khách hàng không được để trống!")]
        [StringLength(50)]
        [DisplayName("Tên khách hàng")]
        public string TenKhachHang { get; set; }

        [StringLength(100)]
        [DisplayName("Địa chỉ khách hàng")]
        public string DiaChiKhachHang { get; set; }

        [StringLength(20)]
        [DisplayName("Điện thoại khách hàng")]
        public string DienThoaiKhachHang { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập không được để trống!")]
        [StringLength(50)]
        [DisplayName("Tên đăng nhập")]
        public string TenDangNhap { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống!")]
        [StringLength(50)]
        [DisplayName("Mật khẩu")]
        public string MatKhau { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Ngày sinh")]
        public DateTime? NgaySinh { get; set; }

        [DisplayName("Giới tính")]
        public bool GioiTinh { get; set; }

        [Required(ErrorMessage = "Email không được để trống!")]
        [StringLength(50)]
        [DisplayName("Email")]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DatHang> DatHangs { get; set; }
    }
}
