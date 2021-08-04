using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace LeafShop.Models
{
    public partial class LeafShopDb : DbContext
    {
        public LeafShopDb()
            : base("name=LeafShopDb")
        {
        }

        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<ChiTietDatHang> ChiTietDatHangs { get; set; }
        public virtual DbSet<DanhMuc> DanhMucs { get; set; }
        public virtual DbSet<DatHang> DatHangs { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<KhuVuc> KhuVucs { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<Taikhoan> Taikhoans { get; set; }
        public virtual DbSet<ThuongHieu> ThuongHieux { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .Property(e => e.MaBaiViet)
                .IsUnicode(false);

            modelBuilder.Entity<Blog>()
                .Property(e => e.MaNhanVien)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietDatHang>()
                .Property(e => e.MaDatHang)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietDatHang>()
                .Property(e => e.MaSanPham)
                .IsUnicode(false);

            modelBuilder.Entity<DanhMuc>()
                .Property(e => e.MaDanhMuc)
                .IsUnicode(false);

            modelBuilder.Entity<DanhMuc>()
                .Property(e => e.ParentId)
                .IsUnicode(false);

            modelBuilder.Entity<DatHang>()
                .Property(e => e.MaDatHang)
                .IsUnicode(false);

            modelBuilder.Entity<DatHang>()
                .Property(e => e.MaKhachHang)
                .IsUnicode(false);

            modelBuilder.Entity<DatHang>()
                .Property(e => e.MaNhanVien)
                .IsUnicode(false);

            modelBuilder.Entity<DatHang>()
                .HasMany(e => e.ChiTietDatHangs)
                .WithRequired(e => e.DatHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.MaKhachHang)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.DienThoaiKhachHang)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.TenDangNhap)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<KhuVuc>()
                .Property(e => e.MaKhuVuc)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.MaNhanVien)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.DienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.MaSanPham)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.MaDanhMuc)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.MaThuongHieu)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.MaKhuVuc)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.HinhMinhHoa)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.BinhLuan)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.ChiTietDatHangs)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Taikhoan>()
                .Property(e => e.MaNhanVien)
                .IsUnicode(false);

            modelBuilder.Entity<ThuongHieu>()
                .Property(e => e.MaThuongHieu)
                .IsUnicode(false);

            modelBuilder.Entity<ThuongHieu>()
                .Property(e => e.DienThoaiThuongHieu)
                .IsUnicode(false);
        }
    }
}
