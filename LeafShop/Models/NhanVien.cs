namespace LeafShop.Models
{
    using System;
    using System.Collections.Generic;
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
        public int MaNhanVien { get; set; }

        [Required(ErrorMessage = "Tên nhân viên không được để trống")]
        [StringLength(100)]
        public string TenNhanVien { get; set; }

        public bool GioiTinh { get; set; }

        [StringLength(1000)]
        public string Avatar { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString =
        "{0:yyyy-MM-dd}",
        ApplyFormatInEditMode = true)]
        public DateTime? NgaySinh { get; set; }

        [StringLength(20)]
        public string DienThoai { get; set; }

        [StringLength(500)]
        public string DiaChi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Blog> Blogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DatHang> DatHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Taikhoan> Taikhoans { get; set; }
    }
}
