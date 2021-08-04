namespace LeafShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Taikhoan")]
    public partial class Taikhoan
    {
        [Key]
        [StringLength(50)]
        [DisplayName("Tên tài khoản")]

        public string USERNAME { get; set; }

        [StringLength(50)]
        [DisplayName("Mật khẩu")]
        public string PASSWORD { get; set; }

        [DisplayName("Quản trị")]
        public bool Quantri { get; set; }

        [StringLength(10)]
        [DisplayName("Mã nhân viên")]
        public string MaNhanVien { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
