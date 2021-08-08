CREATE DATABASE LeafShop 
GO
USE LeafShop

----------------------TẠO BẢNG----------------------

--Tạo bảng Thể loại--
GO
CREATE TABLE DanhMuc
(
	MaDanhMuc VARCHAR(10) NOT NULL PRIMARY KEY,
	TenDanhMuc NVARCHAR(100) NOT NULL UNIQUE,
	ParentId VARCHAR(10) NOT NULL
)

--Tạo bảng Nhà sản xuất--
GO
CREATE TABLE ThuongHieu
(
	MaThuongHieu VARCHAR(10) NOT NULL PRIMARY KEY,
	TenThuongHieu NVARCHAR(100) NOT NULL UNIQUE,
	DiaChiThuongHieu NVARCHAR(100) NULL,
	DienThoaiThuongHieu VARCHAR(20) NULL
)

--Tạo bảng Nhân viên--
GO
CREATE TABLE NhanVien
(
	MaNhanVien VARCHAR(10) NOT NULL PRIMARY KEY,
	TenNhanVien NVARCHAR(100) NOT NULL ,
	GioiTinh BIT NOT NULL,
	Avatar NVARCHAR(1000),
	Email NVARCHAR(50),
	NgaySinh SMALLDATETIME NULL,
	DienThoai VARCHAR(20) NULL,
	DiaChi NVARCHAR(500) NULL
)

--Tạo bảng Khu vực--
GO
CREATE TABLE KhuVuc
(
	MaKhuVuc VARCHAR(10) NOT NULL PRIMARY KEY,
	TenKhuVuc NVARCHAR(100) NOT NULL UNIQUE
)

--Tạo bảng Sản phẩm--
GO
CREATE TABLE SanPham
(
	MaSanPham VARCHAR(10) NOT NULL PRIMARY KEY,
	TenSanPham NVARCHAR(500) NOT NULL UNIQUE,
	MaDanhMuc VARCHAR(10) FOREIGN KEY REFERENCES DanhMuc(MaDanhMuc) NULL,
	MaThuongHieu VARCHAR(10) FOREIGN KEY REFERENCES ThuongHieu(MaThuongHieu) NULL,
	MaKhuVuc VARCHAR(10) FOREIGN KEY REFERENCES KhuVuc(MaKhuVuc) NULL,
	DonViTinh NVARCHAR(50) NULL,
	SoLuong INT NULL,
	SoLuongBan INT NULL,
	DonGia INT NULL ,
	MoTa nvarchar(2000) NULL,
	NgayCapNhat SMALLDATETIME NULL,
	HinhMinhHoa VARCHAR(1000) NULL,
	BinhLuan nvarchar(1000) NULL
)

GO
CREATE TABLE KhachHang
(
	MaKhachHang VARCHAR(10) NOT NULL PRIMARY KEY,
	TenKhachHang NVARCHAR(50) NOT NULL ,
	DiaChiKhachHang NVARCHAR(100)NULL,
	DienThoaiKhachHang VARCHAR(20) NULL,
	TenDangNhap VARCHAR(50) NOT NULL,
	MatKhau VARCHAR(50) NOT NULL,
	NgaySinh date NULL,
	GioiTinh BIT NOT NULL,
	Email VARCHAR(50) NOT NULL,
)

--Tạo bảng Bài Viết
GO
CREATE TABLE Blog(
	MaBaiViet VARCHAR(10) PRIMARY KEY NOT NULL,
	MaNhanVien VARCHAR(10) FOREIGN KEY REFERENCES NhanVien(MaNhanVien) NULL,
	TieuDe nvarchar(500) NOT NULL,
	Anh VARCHAR(1000) NULL,
	Tomtat nvarchar(500) not null,
	Noidung nvarchar(2000) not null
)

--Tạo bảng Tài khoản
GO
CREATE TABLE Taikhoan(
	USERNAME nvarchar(50) primary key not null,
	PASSWORD nvarchar(50) ,
	Quantri bit,
	MaNhanVien VARCHAR(10) FOREIGN KEY REFERENCES NhanVien(MaNhanVien) NULL,
)


--Tạo bảng Đặt hàng 
GO
CREATE TABLE DatHang(
	MaDatHang VARCHAR(10) NOT NULL PRIMARY KEY,
	MaKhachHang VARCHAR(10) FOREIGN KEY REFERENCES KhachHang(MaKhachHang) NULL,
	MaNhanVien VARCHAR(10) FOREIGN KEY REFERENCES NhanVien(MaNhanVien) NULL,
	TongTien Int,
	NgayKhoiTao Date
)

--Tạo bảng Chi tiết đặt hàng--
GO
CREATE TABLE ChiTietDatHang
(
	MaDatHang VARCHAR(10) not null,
	MaSanPham VARCHAR(10) not null,
	SoLuong INT NULL,
	DonGia INT NULL,
	ThanhTien INT NULL,
	NgayDatHang Date NULL,
	NgayGiaoHang Date NULL,
	constraint PK_CTDH primary key (MaDatHang, MaSanPham),
	constraint PK_CTDH1 foreign key (MaDatHang) references DatHang(MaDatHang),
	constraint PK_CTDH2 foreign key (MaSanPham) references SanPham(MaSanPham)
)

--Thêm dữ liệu vào bảng Khu vực--
GO
INSERT INTO KhuVuc VALUES('KV001',N'Hà Nội')
INSERT INTO KhuVuc VALUES('KV002',N'Hồ Chí Minh')
INSERT INTO KhuVuc VALUES('KV003',N'Nha Trang')

--Thêm dữ liệu vào bảng danh mục--
GO
INSERT INTO DanhMuc VALUES('MDM001',N'Thực phẩm tươi sống','PID1')
INSERT INTO DanhMuc VALUES('MDM002',N'Thực phẩm khô','PID2')
INSERT INTO DanhMuc VALUES('MDM003',N'Làm đẹp','PID3')
INSERT INTO DanhMuc VALUES('MDM004',N'Chăm sóc cơ thể','PID4')
INSERT INTO DanhMuc VALUES('MDM005',N'Mẹ & bé','PID5')
INSERT INTO DanhMuc VALUES('MDM006',N'Chăm sóc nhà cửa','PID6')


--Thêm dữ liệu vào bảng ThuongHieu--
GO
INSERT INTO ThuongHieu VALUES('MTH001',N'Thương Hiệu A',N'Hà Nội','0123456789')
INSERT INTO ThuongHieu VALUES('MTH002',N'Thương Hiệu B',N'Lạng Sơn','0123456789')
INSERT INTO ThuongHieu VALUES('MTH003',N'Thương Hiệu C',N'Đà Nẵng','0123456789')
INSERT INTO ThuongHieu VALUES('MTH004',N'Thương Hiệu D',N'Hồ Chí Minh','0123456789')

--Thêm dữ liệu vào bảng Khách hàng--
GO
INSERT INTO KhachHang VALUES('MKH001',N'Đỗ Bá Hoàn',N'Hà Nội','0912345678','allfallsdown','dobahoan','01/01/1990','1','dbh@gmail.com')
INSERT INTO KhachHang VALUES('MKH002',N'Quách Ngọc Hà',N'Đà Nẵng','0912345678','lenovoquach','quachngocha','01/01/1990','1','lenovo@gmail.com')
INSERT INTO KhachHang VALUES('MKH003',N'Nguyễn Tiến Hà',N'Hồ Chí Minh','0912345678','damchem','hanguyen','01/01/1990','1','hanguyentien2000@gmail.com')

----Thêm dữ liệu vào bảng Sản phẩm--
INSERT INTO SanPham VALUES('MSP001',N'Hỗn hợp gia vị hữu cơ 24 loại thảo mộc Bragg 42g','MDM001','MTH004','KV001',N'Chiếc','10','02','129000',N'Lá hương thảo','01/01/2021','MSP001.jpg',N'Tôi rất thích sản phẩm này')
INSERT INTO SanPham VALUES('MSP002',N'Dầu tầm xuân hữu cơ Gravier 50ml','MDM002','MTH001','KV002',N'Lọ','20','04','133000',N'Lá hương thảo','01/01/2021','MSP001.jpg',N'Tôi rất thích sản phẩm này')
INSERT INTO SanPham VALUES('MSP003',N'Dầu argan hữu cơ Gravier 50ml','MDM003','MTH002','KV003',N'Chiếc','30','07','119000',N'Lá hương thảo','01/01/2021','MSP001.jpg',N'Tôi rất thích sản phẩm này')

--Thêm dữ liệu vào bảng Nhân viên--
GO
INSERT INTO NhanVien VALUES('MNV001',N'Quách',1,NULL,'Lenovoquach@gmail.com','02/09/2000','0987654321',N'Hà Nội')
INSERT INTO NhanVien VALUES('MNV002',N'Hà',1,NULL,'hanguyentien@gmail.com','01/05/2000','0936890916',N'Hà Nội')
INSERT INTO NhanVien VALUES('MNV003',N'Hoàn',1,NULL,'dobahoan@gmail.com','03/04/2000','0987654321',N'Hà Nội')

--Thêm dữ liệu vào bảng DatHang
GO
INSERT INTO DatHang VALUES('MDH001','MKH001','MNV001' ,380000, '12/07/2021')
INSERT INTO DatHang VALUES('MDH002','MKH002','MNV002' ,480000, '11/08/2021')
INSERT INTO DatHang VALUES('MDH003','MKH003','MNV003' ,580000, '10/05/2021')


--Thêm dữ liệu vào bảng Chi tiết đặt hàng--
GO 
INSERT INTO ChiTietDatHang VALUES('MDH001','MSP001','20','129000','139000','01/02/2021','02/04/2021')
INSERT INTO ChiTietDatHang VALUES('MDH002','MSP002','03','119000','159000','01/03/2021','02/05/2021')
INSERT INTO ChiTietDatHang VALUES('MDH003','MSP003','04','139000','199000','01/04/2021','02/06/2021')

--Thêm dữ liệu vào bảng tài khoản
GO
INSERT INTO Taikhoan VALUES('admin','admin',1, 'MNV001')
INSERT INTO Taikhoan VALUES('quach','quach',0, 'MNV002')

--Thêm dữ liệu vào bảng blog
GO
INSERT INTO Blog VALUES('MBV001','MNV001',N'Thực phẩm đời sống', 'MSP001.jpg',N'Thực phẩm',N'Yếu tó quan trọng của thực phẩm')
INSERT INTO Blog VALUES('MBV002','MNV002',N'Mẹ & Bé', 'MSP001.jpg',N'Dầu nhờn',N'Yếu tó quan trọng của dầu nhờn')
