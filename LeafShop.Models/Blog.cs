namespace LeafShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Blog")]
    public partial class Blog
    {
        [Key]
        [StringLength(10)]
        [DisplayName("Mã bài viết")]
        public string MaBaiViet { get; set; }

        [StringLength(10)]
        [DisplayName("Mã nhân viên")]
        public string MaNhanVien { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống!")]
        [StringLength(500)]
        [DisplayName("Tiêu đề")]
        public string TieuDe { get; set; }

        [StringLength(1000)]
        [DisplayName("Ảnh")]
        public string Anh { get; set; }

        [Required(ErrorMessage = "Tóm tắt không được để trống!")]
        [StringLength(500)]
        [DisplayName("Tóm tắt")]
        public string Tomtat { get; set; }

        [Required(ErrorMessage = "Nội dung không được để trống!")]
        [StringLength(2000)]
        [DisplayName("Nội dung")]
        public string Noidung { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
