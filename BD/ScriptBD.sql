


/****** CREATE TABLES ******/
/****** CREATE TABLES ******/
/****** CREATE TABLES ******/


USE [APIPruebaSmart]
GO

/****** Object:  Table [dbo].[category]    Script Date: 18/04/2024 11:13:06 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[category](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_category] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


USE [APIPruebaSmart]
GO

/****** Object:  Table [dbo].[order]    Script Date: 18/04/2024 11:13:14 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[order](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[OrderCode] [nvarchar](10) NOT NULL,
	[RequiedDate] [date] NOT NULL,
	[ShippedDate] [date] NULL,
	[Comments] [nvarchar](max) NULL,
	[StatusId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_orders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[order]  WITH CHECK ADD  CONSTRAINT [FK_order_status] FOREIGN KEY([StatusId])
REFERENCES [dbo].[orderStatus] ([StatusId])
GO

ALTER TABLE [dbo].[order] CHECK CONSTRAINT [FK_order_status]
GO

ALTER TABLE [dbo].[order]  WITH CHECK ADD  CONSTRAINT [FK_order_user] FOREIGN KEY([UserId])
REFERENCES [dbo].[user] ([UserId])
GO

ALTER TABLE [dbo].[order] CHECK CONSTRAINT [FK_order_user]
GO


USE [APIPruebaSmart]
GO

/****** Object:  Table [dbo].[orderDetail]    Script Date: 18/04/2024 11:13:49 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[orderDetail](
	[OrderDetailId] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [float] NOT NULL,
 CONSTRAINT [PK_orderDetail] PRIMARY KEY CLUSTERED 
(
	[OrderDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[orderDetail]  WITH CHECK ADD  CONSTRAINT [FK_orderDetail_order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[order] ([OrderId])
GO

ALTER TABLE [dbo].[orderDetail] CHECK CONSTRAINT [FK_orderDetail_order]
GO

ALTER TABLE [dbo].[orderDetail]  WITH CHECK ADD  CONSTRAINT [FK_orderDetail_product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[product] ([ProductId])
GO

ALTER TABLE [dbo].[orderDetail] CHECK CONSTRAINT [FK_orderDetail_product]
GO

USE [APIPruebaSmart]
GO

/****** Object:  Table [dbo].[orderStatus]    Script Date: 18/04/2024 11:13:57 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[orderStatus](
	[StatusId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](100) NOT NULL,
 CONSTRAINT [PK_orderStatus] PRIMARY KEY CLUSTERED 
(
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [APIPruebaSmart]
GO

/****** Object:  Table [dbo].[product]    Script Date: 18/04/2024 11:14:07 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[Stock] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Price] [float] NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[product]  WITH CHECK ADD  CONSTRAINT [FK_product_category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[category] ([CategoryId])
GO

ALTER TABLE [dbo].[product] CHECK CONSTRAINT [FK_product_category]
GO

USE [APIPruebaSmart]
GO

/****** Object:  Table [dbo].[user]    Script Date: 18/04/2024 11:14:14 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[user](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](100) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Role] [varchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



/****** CREATE DATA ******/
/****** CREATE DATA ******/
/****** CREATE DATA ******/


USE [APIPruebaSmart]
GO

INSERT INTO [dbo].[category] ([Nombre]) VALUES ('FRUTAS')
INSERT INTO [dbo].[category] ([Nombre]) VALUES ('VERDURAS')
GO

USE [APIPruebaSmart]
GO

INSERT INTO [dbo].[orderStatus] ([Description]) VALUES ('CREATED')
INSERT INTO [dbo].[orderStatus] ([Description]) VALUES ('CONFIRMED')
INSERT INTO [dbo].[orderStatus] ([Description]) VALUES ('COMPLETED')
INSERT INTO [dbo].[orderStatus] ([Description]) VALUES ('CANCELLED')
GO

USE [APIPruebaSmart]
GO

INSERT INTO [dbo].[product]([Nombre],[Stock],[Description],[Price],[CategoryId])VALUES('PAPAYA',100,'PAPAYA',1500,1)
INSERT INTO [dbo].[product]([Nombre],[Stock],[Description],[Price],[CategoryId])VALUES('TOMATE',100,'TOMATE',1500,2)
INSERT INTO [dbo].[product]([Nombre],[Stock],[Description],[Price],[CategoryId])VALUES('CEBOLLA',100,'CEBOLLA',1500,2)
INSERT INTO [dbo].[product]([Nombre],[Stock],[Description],[Price],[CategoryId])VALUES('MANGO',100,'MANGO',1500,1)
INSERT INTO [dbo].[product]([Nombre],[Stock],[Description],[Price],[CategoryId])VALUES('MANZANA',100,'MANZANA',1500,1)
INSERT INTO [dbo].[product]([Nombre],[Stock],[Description],[Price],[CategoryId])VALUES('PERA',100,'PERA',1500,1)
INSERT INTO [dbo].[product]([Nombre],[Stock],[Description],[Price],[CategoryId])VALUES('DURAZNO',100,'DURAZNO',1500,1)
INSERT INTO [dbo].[product]([Nombre],[Stock],[Description],[Price],[CategoryId])VALUES('PIMENTON',100,'PIMENTON',1500,2)
INSERT INTO [dbo].[product]([Nombre],[Stock],[Description],[Price],[CategoryId])VALUES('UVAS',100,'UVAS',1500,1)
GO

USE [APIPruebaSmart]
GO

INSERT INTO [dbo].[user]([UserName],[Password],[Email],[Role],[Active])VALUES('admin','admin','admin@admin','admin',1)
INSERT INTO [dbo].[user]([UserName],[Password],[Email],[Role],[Active])VALUES('user','user','user@user','user',1)
GO
