namespace LeafShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Blog")]
    public partial class Blog
    {
        [Key]
        public int MaBaiViet { get; set; }

        public int? MaNhanVien { get; set; }

        public int? MaDanhMucBlog { get; set; }

        [StringLength(500)]
        public string TieuDe { get; set; }

        [StringLength(1000)]
        public string Anh { get; set; }

        [StringLength(500)]
        public string Tomtat { get; set; }

        [StringLength(2000)]
        public string Noidung { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString =
        "{0:yyyy-MM-dd}",
        ApplyFormatInEditMode = true)]
        public DateTime? NgayKhoiTao { get; set; }

        public virtual NhanVien NhanVien { get; set; }

        public virtual DanhMucBlog DanhMucBlog { get; set; }
    }
}
