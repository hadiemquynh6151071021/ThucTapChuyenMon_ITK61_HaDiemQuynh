CREATE DATABASE CERAMICS_STORE
GO
USE CERAMICS_STORE
GO

CREATE TABLE [dbo].[AspNetUsers] (
    [Id]                   NVARCHAR (128) NOT NULL,
	[UserName]             NVARCHAR (256) NOT NULL,
    [Email]                NVARCHAR (256) NULL,
    [EmailConfirmed]       BIT            NOT NULL,
    [PasswordHash]         NVARCHAR (MAX) NULL,
    [SecurityStamp]        NVARCHAR (MAX) NULL,
    [PhoneNumber]          NVARCHAR (MAX) NULL,
    [PhoneNumberConfirmed] BIT            NOT NULL,
    [TwoFactorEnabled]     BIT            NOT NULL,
    [LockoutEndDateUtc]    DATETIME       NULL,
    [LockoutEnabled]       BIT            NOT NULL,
    [AccessFailedCount]    INT            NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE TABLE [dbo].[__MigrationHistory] (
    [MigrationId]    NVARCHAR (150)  NOT NULL,
    [ContextKey]     NVARCHAR (300)  NOT NULL,
    [Model]          VARBINARY (MAX) NOT NULL,
    [ProductVersion] NVARCHAR (32)   NOT NULL,
    CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED ([MigrationId] ASC, [ContextKey] ASC)
);

CREATE TABLE [dbo].[AspNetRoles] (
    [Id]   NVARCHAR (128) NOT NULL,
    [Name] NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]
    ON [dbo].[AspNetRoles]([Name] ASC);

CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [UserId]     NVARCHAR (128) NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[AspNetUserClaims]([UserId] ASC);

CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] NVARCHAR (128) NOT NULL,
    [ProviderKey]   NVARCHAR (128) NOT NULL,
    [UserId]        NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC, [UserId] ASC),
    CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[AspNetUserLogins]([UserId] ASC);

CREATE TABLE [dbo].[AspNetUserRoles] (
    [UserId] NVARCHAR (128) NOT NULL,
    [RoleId] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[AspNetUserRoles]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_RoleId]
    ON [dbo].[AspNetUserRoles]([RoleId] ASC);


CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
    ON [dbo].[AspNetUsers]([UserName] ASC);

CREATE TABLE THONGTINCUAHANG(
	MATT INT IDENTITY(1,1) PRIMARY KEY,
	TENCH NVARCHAR(225),
	DIACHI NVARCHAR(500),
	EMAIL VARCHAR(225),
	SDT VARCHAR(10),
	FLUGINPAGE NTEXT,
	URLMAP NTEXT
)

CREATE TABLE KHUYENMAI(
	MAKM INT IDENTITY(1,1) PRIMARY KEY,
	TENKM NVARCHAR(225),
	HINHANH VARCHAR(225),
	CHITIET NTEXT,
	NGAYBATDAU DATE,
	NGAYKETHUC DATE,
	TILE DECIMAL
)

CREATE TABLE DANHGIASP(
	MADG INT IDENTITY(1,1) PRIMARY KEY,
	USERNAME NVARCHAR (256),
	NOIDUNG NVARCHAR (500),
	SAO INT,

	CONSTRAINT RBTV_PF_DANHGIASP FOREIGN KEY (USERNAME) REFERENCES AspNetUsers(UserName)
)

CREATE TABLE PHANQUYEN(
	STT INT IDENTITY(1,1) PRIMARY KEY,
	QUYENTRUYCAP NVARCHAR(128)
)


CREATE TABLE CHATLIEU(
	MACL INT PRIMARY KEY IDENTITY(1,1),
	TENCL NVARCHAR(255)
)

CREATE TABLE LOAISANPHAM(
	MALSP INT PRIMARY KEY IDENTITY(1,1),
	TENSP NVARCHAR(255)
)

CREATE TABLE SANPHAM(
	MASP INT PRIMARY KEY IDENTITY(1,1),
	MACL INT,
	MALSP INT,
	TENSP NVARCHAR(255),
	MOTA NTEXT,
	SOLUONGTONKHO INT,
	SOLUONGDABAN INT,
	GIA DECIMAL,

	CONSTRAINT RBTV_PF1_SANPHAM FOREIGN KEY (MACL) REFERENCES CHATLIEU(MACL),
	CONSTRAINT RBTV_PF2_SANPHAM FOREIGN KEY (MALSP) REFERENCES LOAISANPHAM(MALSP)
)

INSERT INTO SANPHAM VALUES
(3,3,N'Set 2 khay vuông sọc dọc',N'Set khay gia vị vuông bằng sành là một vật dụng
 hữu ích trong nhà bếp để giữ gìn các loại gia vị và đồ dùng nhỏ khác tại nơi trang trí bàn 
 ăn hay kệ tủ bếp. Được thiết kế sang trọng,đẹp mắt và tiện lợi giúp người sử dụng có thể dễ
  dàng phân chia và sắp xếp các loại gia vị.',22,8,85000),
(3,3,N'Set 2 khay vuông',N'Set khay gia vị vuông bằng sành màu vàng là một vật dụng
 hữu ích trong nhà bếp để giữ gìn các loại gia vị và đồ dùng nhỏ khác tại nơi trang trí bàn 
 ăn hay kệ tủ bếp. Được thiết kế sang trọng,đẹp mắt và tiện lợi giúp người sử dụng có thể dễ
  dàng phân chia và sắp xếp các loại gia vị.',17,3,80000),
(3,4,N'Set 4 cốc coffee lòng trứng gà',N'Set 4 cốc coffee lòng trứng gà là một sản phẩm
 độc đáo và đầy phong cách cho các tín đồ yêu thích cà phê. Các cốc được làm từ chất liệu sành 
 cao cấp, với thiết kế lòng trứng gà độc đáo và đẹp mắt, tạo cảm giác ấm áp và gần gũi. Set 4 
 cốc coffee chỉ là sản phẩm để uống cà phê mà còn trở thành một tác phẩm nghệ thuật trang trí 
 cho không gian phòng khách hoặc phòng ăn của bạn',30,10,200000),
(1,3,N'Khay chấm ba ngăn',N'Khay gốm vuông ba ngăn để gia vị là một sản phẩm tiện dụng trong 
nhà bếp để giữ các loại gia vị và đồ dùng nhỏ khác gọn gàng và ngăn nắp. Khay được làm từ chất
 liệu gốm cao cấp, với thiết kế vuông vắn và đơn giản, kích thước phù hợp và các ngăn chứa tiện
  lợi giúp bạn dễ dàng phân chia và sắp xếp các loại gia vị.',22,8,30000),
(3,4,N'Ly ô vuông caro màu xanh lá',N'Ly kẻ ô vuông màu xanh lá là một sản phẩm thú vị để thưởng
 thức thức uống trong nhà hàng hoặc tại gia. Ly được làm từ chất liệu cao cấp, với thiết kế giúp
  tôn lên vẻ đẹp độc đáo của từng loại thức uống. Màu sắc xanh lá thiên nhiên trên ly càng làm tăng
   thêm sự tươi mới và gần gũi với tự nhiên.',27,3,55000),
(3,4,N'Ly hồng pastel',N'Ly hồng pastel bằng sành là một sản phẩm đẹp mắt và sang trọng cho bàn uống 
của bạn. Ly được làm từ chất liệu sành cao cấp, với màu sắc hồng pastel nhẹ nhàng và tinh tế, giúp tôn
 lên vẻ đẹp cổ điển và thời thượng của tổng thể sản phẩm. Thiết kế ly đơn giản, trơn và đẹp mắt cùng 
 với kích thước vừa phải, khiến sản phẩm rất dễ sử dụng và phù hợp với nhiều loại đồ uống.',13,17,45000),
(3,4,N'Set 4 ly vàng ombre',N'Set 4 ly vàng ombre là một bộ sản phẩm sang trọng giúp trang trí quầy bar hoặc
 bàn ăn của bạn. Bộ sản phẩm bao gồm 4 ly được làm từ chất liệu cao cấp, có màu vàng ombre độc đáo. Thiết kế
  ly đơn giản, trơn và đẹp mắt cùng với kích thước vừa phải, khiến sản phẩm rất dễ sử dụng và phù hợp với nhiều
   loại đồ uống.',16,4,180000),
(3,2,N'Set 2 bát trắng đơn giản',N'Bát trắng đơn giản lòng sâu là một sản phẩm tuyệt vời để phục vụ các món ăn 
trong không gian nhà bếp của bạn. Bát được làm từ chất liệu sành cao cấp, với màu sắc tinh khiết đem lại vẻ đẹp
 đơn giản và thanh lịch cho sản phẩm. Thiết kế bát cao và lòng sâu giúp bạn đựng được lượng thực phẩm nhiều hơn.',12,18,180000),
(3,1,N'Đĩa hạt đậu',N'Đĩa hạt đậu là sản phẩm hoàn hảo để trang trí và phục vụ các loại đồ ăn nhẹ như hạt khô, trái cây tươi, hay
 những món ăn nhỏ khác. Được làm bằng chất liệu sành cao cấp, đĩa có kích thước nhỏ nhắn, tạo cảm giác dễ thương và đáng yêu.',25,5,30000),
(3,4,N'Set tách và đĩa vàng',N'Set tách đĩa vàng là một bộ sản phẩm hoàn hảo để trang trí bàn ăn hoặc quầy bar của bạn. Bộ sản
 phẩm bao gồm tách và đĩa được làm từ chất liệu cao cấp, có màu vàng sang trọng và đẹp mắt. Thiết kế sản phẩm đơn giản nhưng 
 tinh tế, với các đường cong mềm mại và mặt lưng bắt mắt, tạo nên dấu ấn riêng cho sản phẩm. ',13,7,85000),
(1,1,N'Đĩa gốm dày',N'Đĩa gốm dày rộng 20cm là sản phẩm chất lượng cao để phục vụ bữa ăn trong
 gia đình hoặc kinh doanh trong ngành nhà hàng khách sạn. Được làm từ chất liệu gốm cao cấp,
  đĩa có đường kính rộng 20cm, là kích thước lý tưởng để phục vụ các món ăn như thịt, cá, rau củ,..
   với lượng đồ ăn phù hợp.',24,6,80000),
(3,4,N'Ly century in hoa nổi',N'Ly sành in hoa nổi là sản phẩm trang trí hữu ích giúp tăng thêm vẻ đẹp sang trọng
 và phong cách cho không gian sống của bạn. Được làm từ sành cao cấp và in hoa nổi bằng công nghệ tiên tiến, ly sành
  mang đến cho bạn cảm giác mềm mại, tinh tế và đầy chất lượng. Thiết kế của ly sành in hoa nổi rất tinh tế và đẹp mắt,
   với hoa văn được in lên mặt ly theo phong cách cổ điển và trang nhã, tạo nên một đường nét mềm mại và thu hút mọi ánh nhìn.',12,8,65000),
(3,1,N'Đĩa mộc hạt tiêu',N'Đĩa mộc hạt tiêu là một sản phẩm cao cấp được làm từ sành chất lượng tốt và được thiết kế độc đáo.
 Chiếc đĩa được thiết kế với họa tiết trang trí mềm mại và tinh tế giúp bạn tạo ra một không gian bàn ăn trang nhã, hiện đại và đầy phong cách.',21,9,75000),
(3,2,N'Bát men mịn',N'Bát men mịn là sản phẩm tuyệt vời để trang trí bàn ăn của bạn. Sản phẩm được làm bằng sành chất lượng cao và cung cấp cảm giác mịn màng,
 trơn tru và tinh tế khi sử dụng. Với màu sắc ngọt ngào và thiết kế đơn giản nhưng sang trọng, bát men mịn sẽ tạo ra một không gian bàn ăn trang nhã và ấm cúng',33,7,60000),
(3,1,N'Đĩa tròn mịn',N'Đĩa tròn mịn là sản phẩm trang trí bàn ăn đẹp mắt và đầy kiêu hãnh. Được làm từ sành cao cấp, đĩa có đường kính 30cm, kích thước lớn giúp bạn phục
 vụ nhiều món ăn hơn một lần. Thiết kế đơn giản nhưng sang trọng với hình dáng tròn và bề mặt êm ái, trơn tru, đĩa sành tròn mịn mang đến cảm giác tinh tế và thanh lịch,
  thực sự làm nổi bật bữa ăn của bạn.',27,3,90000),
(1,1,N'Set 3 đĩa gốm nâu',N'Set 3 đĩa gốm nâu là một bộ sản phẩm gồm ba đĩa được làm từ chất liệu gốm nâu. Mỗi đĩa có kích thước khác nhau và được thiết kế với hình dạng 
đơn giản, trang nhã. Chất liệu gốm nâu mang lại cho sản phẩm sự cứng cáp, độ bền cao và khả năng chống trầy xước tốt. Set 3 đĩa gốm nâu thường được sử dụng trong việc trang
 trí bàn ăn hoặc dùng trong các buổi tiệc tại gia.',28,2,210000),
(2,4,N'Cặp ly thủy tinh 30cm',N'Cặp ly thủy tinh 30cm là bộ sản phẩm gồm hai ly được làm từ chất liệu thủy tinh cao cấp và có chiều cao khoảng 30cm. Thiết kế của 
sản phẩm là hình trụ, có độ trong suốt và mỏng, tạo nên vẻ đẹp sang trọng, tinh tế và hiện đại. Cặp ly thủy tinh 30cm thường được sử dụng để đựng nước uống như nước trái cây,
 nước ép, nước lọc... và được dùng để trang trí bàn tiệc hoặc bàn ăn tại gia.',17,3,120000),
(2,2,N'Set 4 bát thủy tinh vàng trơn',N'Set 4 bát thủy tinh vàng trơn là bộ sản phẩm gồm bốn bát được làm từ chất liệu thủy tinh cao cấp và được phủ lớp mạ vàng trơn bên ngoài.
 Bề mặt của sản phẩm trơn nhẵn, đẹp mắt và sang trọng. Set 4 bát thủy tinh vàng trơn thường được sử dụng để đựng các món ăn như súp, trứng cút, salad... hoặc dùng để trang trí 
 bàn ăn trong các bữa tiệc tại gia.',15,15,240000),
(2,4,N'Cặp ly cocktail thủy tinh',N'Cặp ly cocktail thủy tinh là một bộ sản phẩm gồm hai ly được làm từ chất liệu thủy tinh cao cấp. Thiết kế của sản phẩm nhỏ gọn, tinh tế và 
thanh lịch, phù hợp với việc phục vụ những loại cocktail, rượu và đồ uống khác. Cặp ly cocktail thủy tinh thường được sử dụng trong các buổi tiệc tại gia hoặc quán bar để tạo 
điểm nhấn cho không gian trang trí.',24,6,120000),
(2,1,N'Cặp đĩa thủy tinh vàng xanh',N'Cặp đĩa thủy tinh vàng xanh đường kính 20cm là một bộ sản phẩm gồm hai đĩa được làm từ chất liệu thủy tinh cao cấp với màu vàng xanh 
trang nhã. Đĩa có đường kính khoảng 20cm, độ sâu vừa phải, phù hợp để đựng các món ăn như sushi, trái cây, bánh quy... hoặc trang trí bàn ăn trong các bữa tiệc tại gia. ',22,8,130000),
(3,3,N'Set 2 khay chấm 3 ngăn hình chữ nhật',N'Set 2 khay chấm 3 ngăn hình chữ nhật là một bộ sản phẩm gồm hai khay được làm từ chất liệu cao cấp với thiết kế hình chữ nhật và được 
chia làm ba ngăn để đựng các loại sốt chấm khác nhau khi ăn nhậu. Đây là sản phẩm tiện dụng và đa năng, giúp bạn sắp xếp các loại sốt chấm một cách ngăn nắp và tiết kiệm không gian.',17,3,80000),
(3,2,N'Bát sành ba chân',N'Bát sành ba chân là một sản phẩm bát sành được thiết kế độc đáo với ba chân để đứng, trông thanh lịch và sang trọng hơn so với các loại bát thông thường. Bát sành ba chân có kích thước
 vừa phải, thường được sử dụng để đựng các món canh, súp hoặc các món ăn khác đi kèm với cơm. sản phẩm này dễ dàng vệ sinh và có thể sử dụng được trong thời gian dài.',11,9,60000),
(2,1,N'Đĩa thủy tinh viền lượn sóng',N'Đĩa thủy tinh viền lượn sóng đường kính 25cm là một sản phẩm đựng thực phẩm được làm từ chất liệu thủy tinh cao cấp, có thiết kế lượn sóng độc đáo quanh viền đĩa, giúp tăng tính thẩm mỹ của sản phẩm. Đường kính của đĩa là 25cm, độ sâu vừa phải, đủ rộng để đựng nhiều loại thực phẩm',12,18,80000);

CREATE TABLE HINHANHSP(
	MAANH INT PRIMARY KEY IDENTITY(1,1),
	MASP INT,
	TENANH VARCHAR(128),

	CONSTRAINT RBTV_PF_HINHANHSP FOREIGN KEY (MASP) REFERENCES SANPHAM(MASP)
)

INSERT INTO HINHANHSP VALUES
(24,'Set_khay_vuong_soc_doc_1'),
(24,'Set_khay_vuong_soc_doc_2'),
(24,'Set_khay_vuong_soc_doc_3'),
(25,'Set_khay_vuong_gia_vi_1'),
(25,'Set_khay_vuong_gia_vi_2'),
(25,'Set_khay_vuong_gia_vi_3'),
(26,'Set_ly_coffee_long_trung_ga_1'),
(26,'Set_ly_coffee_long_trung_ga_2'),
(26,'Set_ly_coffee_long_trung_ga_3'),
(27,'Khay_cham_3_ngan_1'),
(27,'Khay_cham_3_ngan_2'),
(27,'Khay_cham_3_ngan_3'),
(28,'Ly_o_xanh_la_1'),
(28,'Ly_o_xanh_la_2'),
(28,'Ly_o_xanh_la_3'),
(29,'Ly_hong_pastel_1'),
(29,'Ly_hong_pastel_2'),
(29,'Ly_hong_pastel_3'),
(30,'Set_ly_vang_ombre_1'),
(30,'Set_ly_vang_ombre_2'),
(30,'Set_ly_vang_ombre_2'),
(31,'Bat_trang_don_gian_1'),
(31,'Bat_trang_don_gian_2'),
(31,'Bat_trang_don_gian_3'),
(32,'Dia_hat_dau_1'),
(32,'Dia_hat_dau_2'),
(32,'Dia_hat_dau_3'),
(33,'Set_tach_dia_vang_kem_1'),
(33,'Set_tach_dia_vang_kem_2'),
(33,'Set_tach_dia_vang_kem_3'),
(34,'Dia-gom_day_1'),
(34,'Dia-gom_day_2'),
(34,'Dia-gom_day_3'),
(35,'Ly_century_hoa_1'),
(35,'Ly_century_hoa_2'),
(35,'Ly_century_hoa_3'),
(36,'Dia_moc_hat_tieu_1'),
(36,'Dia_moc_hat_tieu_2'),
(36,'Dia_moc_hat_tieu_3'),
(37,'Bat_men_min_1'),
(37,'Bat_men_min_2'),
(37,'Bat_men_min_3'),
(38,'Dia_ik3a_1'),
(38,'Dia_ik3a_2'),
(38,'Dia_ik3a_3'),
(39,'Set_3_dia_tron_gom_nau_1'),
(39,'Set_3_dia_tron_gom_nau_2'),
(39,'Set_3_dia_tron_gom_nau_3'),
(40,'Ly_trong_30cm_1'),
(40,'Ly_trong_30cm_2'),
(40,'Ly_trong_30cm_3'),
(41,'Set_3_bat_vang_1'),
(41,'Set_3_bat_vang_2'),
(41,'Set_3_bat_vang_3'),
(42,'Ly_trong_cocktail_1'),
(42,'Ly_trong_cocktail_2'),
(42,'Ly_trong_cocktail_3'),
(43,'Set_dia_vang_xanh_1'),
(43,'Set_dia_vang_xanh_2'),
(43,'Set_dia_vang_xanh_3'),
(44,'Set_khay_cham_3_ngan_hcn_1'),
(44,'Set_khay_cham_3_ngan_hcn_2'),
(44,'Set_khay_cham_3_ngan_hcn_3'),
(45,'Bat_moc_3_chan_1'),
(45,'Bat_moc_3_chan_2'),
(45,'Bat_moc_3_chan_3'),
(46,'Dia_thuy_tinh_chiu_nhiet_1'),
(46,'Dia_thuy_tinh_chiu_nhiet_2'),
(46,'Dia_thuy_tinh_chiu_nhiet_3');

CREATE TABLE KHACHHANG(
	MAKH INT IDENTITY(1,1) PRIMARY KEY,
	ID_USER NVARCHAR(128),
	TENKH TEXT,

	CONSTRAINT RBTV_PF_KHACHHANG FOREIGN KEY (ID_USER) REFERENCES AspNetUsers(Id)
)

CREATE TABLE DONHANG(
	MAVANDON INT IDENTITY(1,1) PRIMARY KEY,
	MAKH NVARCHAR(128),
	SDT CHAR(10),
	DIACHI NTEXT,
	TINHTRANG NVARCHAR(128),
	NGAYDAT DATE,
	TONGTIEN DECIMAL,
	PHUONGTHUCTHANHTOAN NVARCHAR(128),
	TIENTHU DECIMAL

	CONSTRAINT RBTV_PF_DONHANG FOREIGN KEY (MAKH) REFERENCES AspNetUsers(Id)
)

INSERT INTO DONHANG VALUES
(N'0bf88122-2118-430d-ae8f-7835daff12b6','0926654783','Long An','','2023-05-05');

CREATE TABLE CHITIETDONHANG(
	STT INT IDENTITY(1,1) PRIMARY KEY,
	MAVANDON INT,
	MASP INT,
	SOLUONG INT,
	NGAYDAT DATETIME,

	CONSTRAINT RBTV_PF1_CHITIETDONHANG FOREIGN KEY (MAVANDON) REFERENCES DONHANG(MAVANDON),
	CONSTRAINT RBTV_PF2_CHITIETDONHANG FOREIGN KEY (MASP) REFERENCES SANPHAM(MASP)
)
INSERT INTO CHITIETDONHANG VALUES
(1,24,1,'','2023-05-05'),
(1,29,1,'','2023-05-05');
INSERT INTO CHATLIEU VALUES
(N'Gốm'),
(N'Thủy tinh'),
(N'Sành,sứ');

INSERT INTO LOAISANPHAM VALUES
(N'Đĩa'),
(N'Bát'),
(N'Khay'),
(N'Set ly, tách');


