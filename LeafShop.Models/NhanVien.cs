namespace LeafShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhanVien")]
    public partial class NhanVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhanVien()
        {
            Blogs = new HashSet<Blog>();
            DatHangs = new HashSet<DatHang>();
            Taikhoans = new HashSet<Taikhoan>();
        }

        [Key]
        [StringLength(10)]
        [DisplayName("Mã nhân viên")]
        public string MaNhanVien { get; set; }

        [Required(ErrorMessage = "Tên nhân viên không được để trống!")]
        [StringLength(100)]
        [DisplayName("Tên nhân viên")]
        public string TenNhanVien { get; set; }

        [DisplayName("Giới tính")]
        public bool GioiTinh { get; set; }

        [StringLength(1000)]
        public string Avatar { get; set; }

        [StringLength(50)]
        [DisplayName("Email")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString =
        "{0:yyyy-MM-dd}",
        ApplyFormatInEditMode = true)]
        [DisplayName("Ngày sinh")]
        public DateTime? NgaySinh { get; set; }

        [StringLength(20)]
        [DisplayName("Điện thoại")]
        public string DienThoai { get; set; }

        [StringLength(500)]
        [DisplayName("Địa chỉ")]
        public string DiaChi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Blog> Blogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DatHang> DatHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Taikhoan> Taikhoans { get; set; }
    }
}
