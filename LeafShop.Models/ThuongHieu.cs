namespace LeafShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThuongHieu")]
    public partial class ThuongHieu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ThuongHieu()
        {
            SanPhams = new HashSet<SanPham>();
        }

        [Key]
        [StringLength(10)]
        [DisplayName("Mã thương hiệu")]
        public int MaThuongHieu { get; set; }

        [Required(ErrorMessage ="Tên thương hiệu không được để trống!")]
        [StringLength(100)]
        [DisplayName("Tên thương hiệu")]
        public string TenThuongHieu { get; set; }

        [DisplayName("Địa chỉ")]
        [StringLength(100)]
        public string DiaChiThuongHieu { get; set; }

        [DisplayName("Số điện thoại")]
        [StringLength(20)]
        public string DienThoaiThuongHieu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
