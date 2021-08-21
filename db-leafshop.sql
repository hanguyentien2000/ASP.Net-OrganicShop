CREATE DATABASE LeafShop 
GO
USE LeafShop

----------------------TẠO BẢNG----------------------

--Tạo bảng Thể loại--
GO
CREATE TABLE DanhMuc
(
	MaDanhMuc INT IDENTITY NOT NULL PRIMARY KEY,
	TenDanhMuc NVARCHAR(100) NOT NULL,
	ParentId int NULL 
)
GO
ALTER TABLE DanhMuc add foreign key (ParentId) references DanhMuc(MaDanhMuc)
GO
--Tạo bảng Nhà sản xuất--
GO
CREATE TABLE ThuongHieu
(
	MaThuongHieu INT IDENTITY NOT NULL  PRIMARY KEY,
	TenThuongHieu NVARCHAR(100) NOT NULL,
	DiaChiThuongHieu NVARCHAR(100) NULL,
	DienThoaiThuongHieu VARCHAR(20) NULL,
	MoTaThuongHieu nvarchar(2000) NULL,
	AnhThuongHieu VARCHAR(1000) NULL
)

--Tạo bảng Nhân viên--
GO
CREATE TABLE NhanVien
(
	MaNhanVien INT NOT NULL IDENTITY PRIMARY KEY,
	TenNhanVien NVARCHAR(100) NOT NULL ,
	GioiTinh BIT NULL,
	Avatar NVARCHAR(1000) NULL,
	Email NVARCHAR(50),
	NgaySinh DATE NULL,
	DienThoai VARCHAR(20) NULL,
	DiaChi NVARCHAR(500) NULL
)


--Tạo bảng Sản phẩm--
GO
CREATE TABLE SanPham
(
	MaSanPham Int identity not null,
	TenSanPham NVARCHAR(500),
	MaDanhMuc INT ,
	MaThuongHieu INT ,
	DonViTinh NVARCHAR(50) NULL,
	SoLuong INT NULL,
	SoLuongBan INT NULL,
	DonGia INT NULL ,
	MoTa nvarchar(2000) NULL,
	NgayKhoiTao Date NULL,
	NgayCapNhat Date NULL,
	HinhMinhHoa VARCHAR(1000) NULL,
	constraint PK_sanpham primary key (MaSanPham),
	constraint PK_sanpham1 foreign key (MaDanhMuc) REFERENCES DanhMuc(MaDanhMuc),
	constraint PK_sanpham3 foreign key (MaThuongHieu) REFERENCES ThuongHieu(MaThuongHieu)
)

GO
CREATE TABLE KhachHang
(
	MaKhachHang INT Identity NOT NULL PRIMARY KEY,
	TenKhachHang NVARCHAR(50) NOT NULL ,
	DiaChiKhachHang NVARCHAR(100)NULL,
	DienThoaiKhachHang VARCHAR(20) NULL,
	TenDangNhap VARCHAR(50) NOT NULL,
	MatKhau VARCHAR(50) NOT NULL,
	NgaySinh date NULL,
	GioiTinh BIT NULL,
	Email VARCHAR(50) NULL,
	TrangThai bit DEFAULT 1
)

GO
CREATE TABLE DanhMucBlog
(
  MaDanhMucBlog INT IDENTITY NOT NULL PRIMARY KEY,
  TenDanhMucBlog NVARCHAR(100) NOT NULL,
)

--Tạo bảng Bài Viết
GO
CREATE TABLE Blog(
	MaBaiViet INT IDENTITY NOT NULL,
	MaNhanVien Int,
	MaDanhMucBlog int,
	TieuDe nvarchar(500) NULL,
	Anh VARCHAR(1000) NULL,
	Tomtat nvarchar(500) null,
	Noidung nvarchar(2000) null,
	NgayKhoiTao Date null,
	constraint PK_Blog primary key (MaBaiViet),
	constraint PK_Blog1 foreign key (MaNhanVien) REFERENCES NhanVien(MaNhanVien),
	constraint PK_Blog2 foreign key (MaDanhMucBlog) REFERENCES DanhMucBlog(MaDanhMucBlog)
)

--Tạo bảng Tài khoản
GO
CREATE TABLE Taikhoan(
	USERNAME nvarchar(50) primary key not null,
	PASSWORD nvarchar(50) ,
	Quantri bit,
	MaNhanVien Int,
	constraint PK_TaiKhoan foreign key (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
)

--Tạo bảng Đặt hàng 
GO
CREATE TABLE DatHang(
	MaDatHang INT NOT NULL IDENTITY,
	MaKhachHang INT, 
	MaNhanVien INT,
	TongTien Int,
	NgayKhoiTao Date not null,
	GhiChu nvarchar(1000) null,
	TrangThai BIT not null,
	constraint PK_DH primary key (MaDatHang),
	constraint PK_DH1 FOREIGN KEY (MaKhachHang)  REFERENCES KhachHang(MaKhachHang),
	constraint PK_DH2 FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
)

--Tạo bảng Chi tiết đặt hàng--
GO
CREATE TABLE ChiTietDatHang
(
	MaDatHang INT not null,
	MaSanPham INT not null,
	SoLuong INT NULL,
	DonGia INT NULL,
	NgayDatHang Date NULL,
	NgayGiaoHang Date NULL,
	constraint PK_CTDH primary key (MaDatHang, MaSanPham),
	constraint PK_CTDH1 foreign key (MaDatHang) references DatHang(MaDatHang),
	constraint PK_CTDH2 foreign key (MaSanPham) references SanPham(MaSanPham)
)


--Thêm dữ liệu vào bảng danh mục--
GO
INSERT INTO DanhMuc VALUES(N'Thực phẩm tươi sống','1')
INSERT INTO DanhMuc VALUES(N'Thực phẩm khô','2')
INSERT INTO DanhMuc VALUES(N'Làm đẹp','3')
INSERT INTO DanhMuc VALUES(N'Chăm sóc cơ thể','4')
INSERT INTO DanhMuc VALUES(N'Mẹ & bé','5')
INSERT INTO DanhMuc VALUES(N'Chăm sóc nhà cửa','6')


--Thêm dữ liệu vào bảng ThuongHieu--
GO
INSERT INTO ThuongHieu VALUES(N'Thương Hiệu A',N'Hà Nội','0123456789', N'Test', NULL)
INSERT INTO ThuongHieu VALUES(N'Thương Hiệu B',N'Lạng Sơn','0123456789', N'Test', NULL)
INSERT INTO ThuongHieu VALUES(N'Thương Hiệu C',N'Đà Nẵng','0123456789',N'Test', NULL)
INSERT INTO ThuongHieu VALUES(N'Thương Hiệu D',N'Hồ Chí Minh','0123456789',N'Test', NULL)

--Thêm dữ liệu vào bảng Khách hàng--
GO
INSERT INTO KhachHang VALUES(N'Đỗ Bá Hoàn',N'Hà Nội','0912345678','allfallsdown','dobahoan','01/01/1990','1','dbh@gmail.com',1)
INSERT INTO KhachHang VALUES(N'Quách Ngọc Hà',N'Đà Nẵng','0912345678','lenovoquach','quachngocha','01/01/1990','1','lenovo@gmail.com',1)
INSERT INTO KhachHang VALUES(N'Nguyễn Tiến Hà',N'Hồ Chí Minh','0912345678','damchem','hanguyen','01/01/1990','1','hanguyentien2000@gmail.com',1)

--------Thêm dữ liệu vào bảng Sản phẩm--
----INSERT INTO SanPham VALUES(N'Hỗn hợp gia vị hữu cơ 24 loại thảo mộc Bragg 42g','MDM001','MTH004','KV001',N'Chiếc','10','02','129000',N'Lá hương thảo','01/01/2021','01/03/2021','MSP001.jpg')
----INSERT INTO SanPham VALUES(N'Dầu tầm xuân hữu cơ Gravier 50ml','MDM002','MTH001','KV002',N'Lọ','20','04','133000',N'Lá hương thảo','01/01/2021','01/03/2021','MSP001.jpg')
----INSERT INTO SanPham VALUES(N'Dầu argan hữu cơ Gravier 50ml','MDM003','MTH002','KV003',N'Chiếc','30','07','119000',N'Lá hương thảo','01/01/2021','01/03/2021','MSP001.jpg')

--Thêm dữ liệu vào bảng Nhân viên--
GO
INSERT INTO NhanVien VALUES(N'Nam',1,NULL,'hanguyentien@gmail.com','01/05/2000','0936890916',N'Hà Nội')
INSERT INTO NhanVien VALUES(N'Hà',1,NULL,'hanguyentien@gmail.com','01/05/2000','0936890916',N'Hà Nội')
INSERT INTO NhanVien VALUES(N'Hoàn',1,NULL,'dobahoan@gmail.com','03/04/2000','0987654321',N'Hà Nội')

----Thêm dữ liệu vào bảng DatHang
--GO
--INSERT INTO DatHang VALUES('MDH001','MKH001','MNV001' ,380000, '12/07/2021')
--INSERT INTO DatHang VALUES('MDH002','MKH002','MNV002' ,480000, '11/08/2021')
--INSERT INTO DatHang VALUES('MDH003','MKH003','MNV003' ,580000, '10/05/2021')


----Thêm dữ liệu vào bảng Chi tiết đặt hàng--
--GO 
--INSERT INTO ChiTietDatHang VALUES('MDH001','MSP001','20','129000','139000','01/02/2021','02/04/2021')
--INSERT INTO ChiTietDatHang VALUES('MDH002','MSP002','03','119000','159000','01/03/2021','02/05/2021')
--INSERT INTO ChiTietDatHang VALUES('MDH003','MSP003','04','139000','199000','01/04/2021','02/06/2021')

--Thêm dữ liệu vào bảng tài khoản
GO
INSERT INTO Taikhoan VALUES('admin','admin',1, 1)
INSERT INTO Taikhoan VALUES('quach','quach',0, 2)

--Thêm dữ liệu vào bảng blog
--GO
--INSERT INTO Blog VALUES('1',N'Thực phẩm đời sống', 'MSP001.jpg',N'Thực phẩm',N'Yếu tó quan trọng của thực phẩm','01/02/2021')
--INSERT INTO Blog VALUES('2',N'Mẹ & Bé', 'MSP001.jpg',N'Dầu nhờn',N'Yếu tó quan trọng của dầu nhờn','01/06/2021')
