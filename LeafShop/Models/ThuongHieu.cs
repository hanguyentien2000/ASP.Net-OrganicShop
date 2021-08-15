namespace LeafShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Script.Serialization;

    [Table("ThuongHieu")]
    public partial class ThuongHieu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ThuongHieu()
        {
            SanPhams = new HashSet<SanPham>();
        }

        [Key]
        public int MaThuongHieu { get; set; }

        [Required]
        [StringLength(100)]
        public string TenThuongHieu { get; set; }

        [StringLength(100)]
        public string DiaChiThuongHieu { get; set; }

        [StringLength(20)]
        public string DienThoaiThuongHieu { get; set; }

        [StringLength(2000)]
        public string MoTaThuongHieu { get; set; }

        [StringLength(1000)]
        public string AnhThuongHieu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [ScriptIgnore]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
