
CREATE DATABASE LiteCommerceDB



GO

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

CREATE TABLE [dbo].[Categories](

	[CategoryID] [int] IDENTITY(1,1) NOT NULL,

	[CategoryName] [nvarchar](255) NOT NULL,

	[Description] [nvarchar](255) NULL,

 CONSTRAINT [PK__Categories] PRIMARY KEY CLUSTERED 

(

	[CategoryID] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]

GO

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

CREATE TABLE [dbo].[Customers](

	[CustomerID] [int] IDENTITY(1,1) NOT NULL,

	[CustomerName] [nvarchar](255) NOT NULL,

	[ContactName] [nvarchar](255) NOT NULL,

	[Province] [nvarchar](255) NULL,

	[Address] [nvarchar](255) NULL,

	[Phone] [nvarchar](255) NULL,

	[Email] [nvarchar](50) NULL,

	[Password] [nvarchar](50) NULL,

	[IsLocked] [bit] NULL,

 CONSTRAINT [PK__Customers] PRIMARY KEY CLUSTERED 

(

	[CustomerID] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]

GO

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

CREATE TABLE [dbo].[Employees](

	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,

	[FullName] [nvarchar](255) NOT NULL,

	[BirthDate] [date] NULL,

	[Address] [nvarchar](255) NULL,

	[Phone] [nvarchar](255) NULL,

	[Email] [nvarchar](50) NULL,

	[Password] [nvarchar](50) NULL,

	[Photo] [nvarchar](255) NULL,

	[IsWorking] [bit] NULL,

	[RoleNames] [nvarchar](500) NULL,

 CONSTRAINT [PK__Employees] PRIMARY KEY CLUSTERED 

(

	[EmployeeID] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]

GO

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

CREATE TABLE [dbo].[OrderDetails](

	[OrderID] [int] NOT NULL,

	[ProductID] [int] NOT NULL,

	[Quantity] [int] NOT NULL,

	[SalePrice] [money] NOT NULL,

 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 

(

	[OrderID] ASC,

	[ProductID] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]

GO

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

CREATE TABLE [dbo].[Orders](

	[OrderID] [int] IDENTITY(1,1) NOT NULL,

	[CustomerID] [int] NULL,

	[OrderTime] [datetime] NOT NULL,

	[DeliveryProvince] [nvarchar](255) NULL,

	[DeliveryAddress] [nvarchar](255) NULL,

	[EmployeeID] [int] NULL,

	[AcceptTime] [datetime] NULL,

	[ShipperID] [int] NULL,

	[ShippedTime] [datetime] NULL,

	[FinishedTime] [datetime] NULL,

	[Status] [int] NOT NULL,

 CONSTRAINT [PK__Orders] PRIMARY KEY CLUSTERED 

(

	[OrderID] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]

GO

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

CREATE TABLE [dbo].[OrderStatus](

	[Status] [int] NOT NULL,

	[Description] [nvarchar](50) NOT NULL,

 CONSTRAINT [PK_OrderStatus] PRIMARY KEY CLUSTERED 

(

	[Status] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]

GO

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

CREATE TABLE [dbo].[ProductAttributes](

	[AttributeID] [bigint] IDENTITY(1,1) NOT NULL,

	[ProductID] [int] NOT NULL,

	[AttributeName] [nvarchar](255) NOT NULL,

	[AttributeValue] [nvarchar](max) NOT NULL,

	[DisplayOrder] [int] NOT NULL,

 CONSTRAINT [PK_ProductAttributes] PRIMARY KEY CLUSTERED 

(

	[AttributeID] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

CREATE TABLE [dbo].[ProductPhotos](

	[PhotoID] [bigint] IDENTITY(1,1) NOT NULL,

	[ProductID] [int] NOT NULL,

	[Photo] [nvarchar](255) NOT NULL,

	[Description] [nvarchar](255) NOT NULL,

	[DisplayOrder] [int] NOT NULL,

	[IsHidden] [bit] NOT NULL,

 CONSTRAINT [PK_ProductPhotos] PRIMARY KEY CLUSTERED 

(

	[PhotoID] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]

GO

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

CREATE TABLE [dbo].[Products](

	[ProductID] [int] IDENTITY(1,1) NOT NULL,

	[ProductName] [nvarchar](255) NOT NULL,

	[ProductDescription] [nvarchar](2000) NULL,

	[SupplierID] [int] NULL,

	[CategoryID] [int] NULL,

	[Unit] [nvarchar](255) NOT NULL,

	[Price] [money] NOT NULL,

	[Photo] [nvarchar](255) NULL,

	[IsSelling] [bit] NULL,

 CONSTRAINT [PK__Products] PRIMARY KEY CLUSTERED 

(

	[ProductID] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]

GO

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

CREATE TABLE [dbo].[Provinces](

	[ProvinceName] [nvarchar](255) NOT NULL,

 CONSTRAINT [PK_Provinces] PRIMARY KEY CLUSTERED 

(

	[ProvinceName] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]

GO

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

CREATE TABLE [dbo].[Shippers](

	[ShipperID] [int] IDENTITY(1,1) NOT NULL,

	[ShipperName] [nvarchar](255) NOT NULL,

	[Phone] [nvarchar](255) NULL,

 CONSTRAINT [PK__Shippers] PRIMARY KEY CLUSTERED 

(

	[ShipperID] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]

GO

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

CREATE TABLE [dbo].[Suppliers](

	[SupplierID] [int] IDENTITY(1,1) NOT NULL,

	[SupplierName] [nvarchar](255) NOT NULL,

	[ContactName] [nvarchar](255) NOT NULL,

	[Province] [nvarchar](255) NULL,

	[Address] [nvarchar](255) NULL,

	[Phone] [nvarchar](255) NULL,

	[Email] [nvarchar](255) NULL,

 CONSTRAINT [PK__Suppliers] PRIMARY KEY CLUSTERED 

(

	[SupplierID] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]

GO

SET IDENTITY_INSERT [dbo].[Categories] ON 

GO

INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description]) VALUES (1, N'May mặc', N'Quần áo, hàng may mặc, thời trang,...')

GO

INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description]) VALUES (2, N'Mỹ phẩm', N'Mỹ phẩm')

GO

INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description]) VALUES (3, N'Điện tử', N'Tivi, điện thoại, máy tính,...')

GO

INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description]) VALUES (4, N'Hàng gia dụng', N'Trang thiết bị, máy móc gia dụng')

GO

INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description]) VALUES (5, N'Mẹ và Em bé', N'Mẹ và em bé')

GO

INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description]) VALUES (6, N'Xe máy', N'Xe máy và phụ kiện')

GO

INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description]) VALUES (7, N'Oto', N'Oto và phụ kiện')

GO

INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description]) VALUES (9, N'Đồ chơi - phụ kiện', N'Đồ chơi, phụ kiện')

GO

INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description]) VALUES (10, N'Bàn ghế - nội thất', N'Bàn ghế, trang thiết bị nội thất,...')

GO

SET IDENTITY_INSERT [dbo].[Categories] OFF

GO

SET IDENTITY_INSERT [dbo].[Customers] ON 

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4194, N'Hồ Thị Tâm', N'cô Tâm', N'Thừa Thiên Huế', N'', N'09503277635', N'tam03021990@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4195, N'Nguyễn Thị Hà', N'cô Hà', N'Nghệ An', N'', N'02635855659', N'ha20051990@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4196, N'Bảo Nguyên', N'anh Nguyên', N'Thừa Thiên Huế', N'', N'', N'nguyen27111989@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 1)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4197, N'Trần Thanh Tâm Toàn', N'anh Toàn', N'Thừa Thiên Huế', N'', N'06627672674', N'toan25041991@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4198, N'Nguyễn Thị Khoa', N'cô Khoa', N'Nghệ An', N'', N'07034481024', N'khoa20031990@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4199, N'Hồ Thị Uyên Phương', N'cô Phương', N'Thừa Thiên Huế', N'', N'08481442691', N'phuong26101990@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4200, N'Nguyễn Văn Hiếu', N'anh Hiếu', N'Thừa Thiên Huế', N'', N'09144373140', N'hieu09051990@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4201, N'Hồ Thị Liểu', N'cô Liểu', N'Thừa Thiên Huế', N'', N'04553580694', N'lieu25111991@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4202, N'Đoàn Văn Ngọ', N'anh Ngọ', N'Quảng Bình', N'', N'08133835787', N'ngo29121989@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4203, N'Hoàng Thị Thương', N'cô Thương', N'Nghệ An', N'', N'04743628155', N'thuong06021989@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4204, N'Võ Văn Quốc', N'anh Quốc', N'Thừa Thiên Huế', N'', N'04833334572', N'quoc10101989@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4205, N'Nguyễn Thanh Thảo Ly', N'cô Ly', N'Thừa Thiên Huế', N'', N'03934359745', N'ly02081990@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4206, N'Hồ Thị Thu Thảo', N'cô Thảo', N'Thừa Thiên Huế', N'', N'08124532290', N'thao29031992@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4207, N'Trần Đức Trung', N'anh Trung', N'Thừa Thiên Huế', N'', N'05745155750', N'trung29091992@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4208, N'Ngô Thị Sáng', N'cô Sáng', N'Nghệ An', N'', N'04825378941', N'sang23031992@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4209, N'Nguyễn Vương Tiểu Khôi', N'anh Khôi', N'Quảng Ngãi', N'', N'04756993891', N'khoi11011990@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4210, N'Trần Lê Quang Hòa', N'anh Hòa', N'Thừa Thiên Huế', N'', N'07832818082', N'hoa03051991@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4211, N'Trần Thị Lê', N'cô Lê', N'Quảng Trị', N'', N'02988982633', N'le17091992@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4212, N'Nguyễn Thị Tình', N'cô Tình', N'Hà Tĩnh', N'', N'01754538222', N'tinh02091989@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4213, N'Nguyễn Hoàng Sơn', N'anh Sơn', N'Thừa Thiên Huế', N'', N'03879156076', N'son02031991@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4214, N'Phạm Hữu Lỉnh', N'anh Lỉnh', N'Thừa Thiên Huế', N'', N'05777844393', N'linh10111991@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4215, N'Lê Thị Loan', N'cô Loan', N'NGhệ An', N'', N'03623934103', N'loan15081992@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4216, N'Nguyễn Thị Thu Thảo', N'cô Thảo', N'Thừa Thiên Huế', N'', N'09834092288', N'thao30061992@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4217, N'Nguyễn Thị Thương', N'cô Thương', N'Nghệ An', N'', N'09526350256', N'thuong16111991@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4218, N'Nguyễn Thị Hân', N'cô Hân', N'Nghệ An', N'', N'09164574595', N'han20071988@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4219, N'Lê Đăng Vĩnh', N'anh Vĩnh', N'Thừa Thiên Huế', N'', N'02463953493', N'vinh25011991@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4220, N'Nguyễn Thị Thu Hiền', N'cô Hiền', N'Quảng Trị', N'', N'09237196498', N'hien06041988@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4221, N'Trần Xuân Hải', N'anh Hải', N'Quảng Trị', N'', N'09542734433', N'hai16031992@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4222, N'Nguyễn Thị Thuý Nga', N'cô Nga', N'Quảng Trị', N'', N'06213671346', N'nga22011992@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4223, N'Nguyễn Đình Đẩu', N'anh Đẩu', N'Thừa Thiên Huế', N'', N'02655205303', N'dau16081990@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4224, N'Lê Thị Ly Na', N'cô Na', N'Thừa Thiên Huế', N'', N'07015763797', N'na06011991@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4225, N'Nguyễn Thị Hạo', N'cô Hạo', N'Quảng Bình', N'', N'05277403628', N'hao14041989@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4226, N'Lương Duy Tân', N'anh Tân', N'Quảng Trị', N'', N'06408042620', N'tan17071983@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4227, N'Trần Hữu Hiếu', N'anh Hiếu', N'Thừa Thiên Huế', N'', N'01272483121', N'hieu05081992@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4228, N'Ma Thị Phượng', N'cô Phượng', N'Thừa Thiên Huế', N'', N'06503119980', N'phuong01081990@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4229, N'Đặng Tuấn Anh', N'anh Anh', N'Thừa Thiên Huế', N'', N'03833035763', N'anh16011992@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4230, N'Nguyễn Anh Hào', N'anh Hào', N'Quảng Bình', N'', N'04236226473', N'hao22021988@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4231, N'Nguyễn Chánh Tín', N'anh Tín', N'Thừa Thiên Huế', N'', N'03489218396', N'tin13041991@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4232, N'Phạm Thị Thiên Quý', N'cô Quý', N'Quảng Trị', N'', N'08344846567', N'quy09091991@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4233, N'Trần Quang Sơn', N'anh Sơn', N'Thừa Thiên Huế', N'', N'04481442530', N'son27031991@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4234, N'Nguyễn Thị Diệp', N'cô Diệp', N'Nghệ An', N'', N'03425365470', N'diep15081991@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4235, N'Trần Ngọc Anh', N'anh Anh', N'Quảng Bình', N'', N'01201374045', N'anh02101991@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4236, N'Ngô Đình Phú', N'anh Phú', N'Thừa Thiên Huế', N'', N'07625687803', N'phu26031990@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4237, N'Hoàng Trần Như Ngọc', N'cô Ngọc', N'Thừa Thiên Huế', N'', N'04813206132', N'ngoc12021990@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4238, N'Nguyễn Phi Phụng', N'anh Phụng', N'Thừa Thiên Huế', N'', N'04739326580', N'phung24101989@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4239, N'Hồ Thị Trang', N'cô Trang', N'Thừa Thiên Huế', N'', N'08657554107', N'trang12051991@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4240, N'Bùi Văn Huân', N'anh Huân', N'Nam Định', N'', N'05589854942', N'huan25021992@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4241, N'Nguyễn Thị Tuyết', N'cô Tuyết', N'Nghệ An', N'', N'07194221709', N'tuyet25091990@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4242, N'Lê Thị Hải Yến', N'cô Yến', N'Hà Tĩnh', N'', N'09292858310', N'yen10021990@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4243, N'Võ Văn Thảo', N'anh Thảo', N'Thừa Thiên Huế', N'', N'04846219787', N'thao12091990@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4244, N'Nguyễn Tình', N'anh Tình', N'Thừa Thiên Huế', N'', N'08771557001', N'tinh08011988@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4245, N'Lê Hồ Xuân Thịnh', N'anh Thịnh', N'Thừa Thiên Huế', N'', N'05377421890', N'thinh04021990@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4246, N'Nông Lộc Duyên', N'cô Duyên', N'Cao Bằng', N'', N'03077618937', N'duyen20031990@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4247, N'Nguyễn Tú Uyên', N'cô Uyên', N'Thừa Thiên Huế', N'', N'05992073056', N'uyen27081991@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4248, N'Lê Hải Nam', N'anh Nam', N'Thừa Thiên Huế', N'', N'09599163357', N'nam28091992@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4249, N'Trần Văn Thạnh', N'anh Thạnh', N'Thừa Thiên Huế', N'', N'09518749446', N'thanh18021991@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4250, N'Lê Nguyễn Thị Xuân Công', N'cô Công', N'Thừa Thiên Huế', N'', N'05782923458', N'cong09091991@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4251, N'Lê Thị Như', N'cô Như', N'Thừa Thiên Huế', N'', N'08262331117', N'nhu02121990@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4252, N'Đinh Thị Hoa', N'cô Hoa', N'Thừa Thiên Huế', N'', N'07422820545', N'hoa28081990@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4253, N'Lê Văn Công', N'anh Công', N'Quảng Trị', N'', N'08429845173', N'cong09071990@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4254, N'Huỳnh Quang Hải', N'anh Hải', N'Thừa Thiên Huế', N'', N'02079213058', N'hai21081990@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4255, N'Hồ Sỹ Tú', N'anh Tú', N'Quảng Trị', N'', N'07118582304', N'tu16091990@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4256, N'Trần Xuân Vỹ', N'anh Vỹ', N'Thừa Thiên Huế', N'', N'01517037021', N'vy22061987@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4257, N'Nguyễn Thị Kim Anh', N'cô Anh', N'Thừa Thiên Huế', N'', N'08949780792', N'anh06111990@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4258, N'Võ Thị Thu Phong', N'cô Phong', N'Thừa Thiên Huế', N'', N'06155074896', N'phong20071991@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4259, N'Hoàng Như Tín', N'anh Tín', N'Thừa Thiên Huế', N'', N'04446083188', N'tin29111990@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4260, N'Phan Văn Trường', N'anh Trường', N'Thừa Thiên Huế', N'', N'02023542286', N'truong05021990@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4261, N'Đỗ Huyền Trang', N'cô Trang', N'Quảng Bình', N'', N'02507966616', N'trang11011990@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4262, N'Trần Đình Mạnh', N'anh Mạnh', N'Hà Tĩnh', N'', N'08214808282', N'manh11081992@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4263, N'Phan Thế Doanh', N'anh Doanh', N'Thừa Thiên Huế', N'', N'08126750353', N'doanh20101991@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4264, N'Lê Phước Định', N'anh Định', N'Thừa Thiên Huế', N'', N'02572120530', N'dinh15101991@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4265, N'Nguyễn Thị Hoa', N'cô Hoa', N'Đà Nẵng', N'', N'01254835857', N'hoa10111989@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4266, N'Nguyễn Thị Thiên Thanh', N'cô Thanh', N'Thừa Thiên Huế', N'', N'05324508636', N'thanh06111991@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4267, N'Trương Thị Huyền My', N'cô Huyền My', N'Thừa Thiên Huế', N'77 Nguyễn Huệ', N'07876670223', N'my02021992@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4268, N'Hồ Hữu Linh', N'anh Linh', N'Nghệ An', N'', N'09234738393', N'linh02121988@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4269, N'Đào Thị Trang', N'cô Trang', N'Vĩnh Phúc', N'', N'02679697453', N'trang28061991@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4270, N'Trần Nguyễn Minh Giang', N'anh Giang', N'Quảng Bình', N'', N'08877218335', N'giang04091990@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4271, N'Nguyễn Thị Hồng', N'cô Hồng', N'Thừa Thiên Huế', N'', N'08217461386', N'hong01011989@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4272, N'Tôn Nữ Nguyệt Anh', N'cô Anh', N'Thừa Thiên Huế', N'', N'07825551983', N'anh06101991@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4273, N'Nguyễn Thị Thuyền', N'cô Thuyền', N'Thừa Thiên Huế', N'', N'05994746467', N'thuyen24031991@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4274, N'Phạm Thị Bình', N'cô Bình', N'Hà Tĩnh', N'', N'05518169557', N'binh18071989@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4275, N'Nguyễn Thị Kim Tuyến', N'cô Tuyến', N'Thừa Thiên Huế', N'', N'06778323525', N'tuyen02111990@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4276, N'Nguyễn Trần Nhật Hoàng', N'anh Hoàng', N'Thừa Thiên Huế', N'', N'03582583655', N'hoang01101990@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

INSERT [dbo].[Customers] ([CustomerID], [CustomerName], [ContactName], [Province], [Address], [Phone], [Email], [Password], [IsLocked]) VALUES (4277, N'Hồng Hồ Bảo', N'anh Bảo', N'Thừa Thiên Huế', N'', N'06531953196', N'bao28061992@myshop.com', N'e10adc3949ba59abbe56e057f20f883e', 0)

GO

SET IDENTITY_INSERT [dbo].[Customers] OFF

GO