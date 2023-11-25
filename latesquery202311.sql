
USE [medco]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 11/25/2023 5:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 11/25/2023 5:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Discriminator] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 11/25/2023 5:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 11/25/2023 5:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 11/25/2023 5:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 11/25/2023 5:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[City] [nvarchar](max) NULL,
	[State] [nvarchar](max) NULL,
	[Zip] [nvarchar](max) NULL,
	[Country] [nvarchar](max) NULL,
	[Address1] [nvarchar](max) NULL,
	[Address2] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[ShopName] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BankAccounts]    Script Date: 11/25/2023 5:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BankAccounts](
	[Id] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](max) NULL,
	[BankName] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[AccountName] [nvarchar](max) NULL,
	[Branch] [nvarchar](max) NULL,
	[AccountNumber] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.BankAccounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Banners]    Script Date: 11/25/2023 5:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Banners](
	[Id] [nvarchar](128) NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
	[Link] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Banners] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 11/25/2023 5:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[ParentId] [nvarchar](128) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cities]    Script Date: 11/25/2023 5:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cities](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Zone] [nvarchar](max) NULL,
	[Amount] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_dbo.Cities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 11/25/2023 5:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Subject] [nvarchar](max) NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[IsRead] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Contacts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Images]    Script Date: 11/25/2023 5:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Images](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Path] [nvarchar](max) NOT NULL,
	[ProductId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.Images] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 11/25/2023 5:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [nvarchar](128) NOT NULL,
	[ProductId] [nvarchar](128) NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Sku] [nvarchar](max) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
	[SubTotal] [decimal](18, 2) NOT NULL,
	[DetuctionPersontage] [int] NOT NULL,
	[Detuction] [decimal](18, 2) NOT NULL,
	[Tex] [int] NOT NULL,
	[ReturnTotal] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_dbo.OrderDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 11/25/2023 5:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [nvarchar](128) NOT NULL,
	[SubTotal] [decimal](18, 2) NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[ShipDate] [datetime] NULL,
	[OrderNotes] [nvarchar](max) NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NOT NULL,
	[Country] [nvarchar](max) NOT NULL,
	[City] [nvarchar](max) NOT NULL,
	[State] [nvarchar](max) NOT NULL,
	[Zip] [nvarchar](max) NOT NULL,
	[Address1] [nvarchar](max) NOT NULL,
	[Address2] [nvarchar](max) NULL,
	[IsShipToDifferentAddress] [bit] NOT NULL,
	[DiffCountry] [nvarchar](max) NULL,
	[DiffCity] [nvarchar](max) NULL,
	[DiffState] [nvarchar](max) NULL,
	[DiffZip] [nvarchar](max) NULL,
	[DiffAddress1] [nvarchar](max) NULL,
	[DiffAddress2] [nvarchar](max) NULL,
	[UserId] [nvarchar](128) NULL,
	[OrderStatusId] [nvarchar](128) NULL,
	[PaymentMethodId] [nvarchar](128) NULL,
	[ShippingPrice] [decimal](18, 2) NOT NULL,
	[Total] [decimal](18, 2) NOT NULL,
	[OrderWaybillid] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderStatuses]    Script Date: 11/25/2023 5:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderStatuses](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.OrderStatuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentMethods]    Script Date: 11/25/2023 5:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentMethods](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.PaymentMethods] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 11/25/2023 5:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [nvarchar](128) NOT NULL,
	[Sku] [nvarchar](450) NOT NULL,
	[MetaTitle] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[DiscountPrice] [decimal](18, 2) NULL,
	[Quantity] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[DateAdded] [datetime] NOT NULL,
	[DateModified] [datetime] NULL,
	[CategoryId] [nvarchar](128) NULL,
	[ShortDesciption] [nvarchar](max) NULL,
	[LongDescription] [nvarchar](max) NULL,
	[StatusId] [nvarchar](128) NULL,
	[IsFeatured] [bit] NOT NULL,
	[EndDiscountDate] [datetime] NULL,
	[UserId] [nvarchar](max) NOT NULL,
	[Weight] [int] NULL,
	[Width] [int] NULL,
	[Hight] [int] NULL,
 CONSTRAINT [PK_dbo.Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductSpecifications]    Script Date: 11/25/2023 5:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductSpecifications](
	[ProductId] [nvarchar](128) NOT NULL,
	[ScreenSize] [nvarchar](max) NULL,
	[DisplayTechnology] [nvarchar](max) NULL,
	[MaxScreenResolution] [nvarchar](max) NULL,
	[Processor] [nvarchar](max) NULL,
	[RearFrontCamera] [nvarchar](max) NULL,
	[ExpandableMemory] [nvarchar](max) NULL,
	[USBTechnology] [nvarchar](max) NULL,
	[Fingerprint] [bit] NULL,
	[NFC] [bit] NULL,
	[RAM] [nvarchar](max) NULL,
	[HardDrive] [nvarchar](max) NULL,
	[GraphicsCoprocessor] [nvarchar](max) NULL,
	[ChipsetBrand] [nvarchar](max) NULL,
	[CardDescription] [nvarchar](max) NULL,
	[GraphicsCardRamSize] [nvarchar](max) NULL,
	[WirelessType] [nvarchar](max) NULL,
	[NumberOfUSB2Dot0Ports] [int] NULL,
	[NumberOfUSB3Dot0Ports] [int] NULL,
	[BrandName] [nvarchar](max) NULL,
	[Series] [nvarchar](max) NULL,
	[ItemModelNumber] [nvarchar](max) NULL,
	[HardwarePlatform] [nvarchar](max) NULL,
	[OperatingSystem] [nvarchar](max) NULL,
	[ItemWeight] [nvarchar](max) NULL,
	[ProductDimensions] [nvarchar](max) NULL,
	[ItemDimensionsLxWxH] [nvarchar](max) NULL,
	[ProcessorBrand] [nvarchar](max) NULL,
	[ProcessorCount] [int] NULL,
	[ComputerMemoryType] [nvarchar](max) NULL,
	[FlashMemorySize] [nvarchar](max) NULL,
	[HardDriveInterface] [nvarchar](max) NULL,
	[HardDriveRotationalSpeed] [nvarchar](max) NULL,
	[OpticalDriveType] [nvarchar](max) NULL,
	[Batteries] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.ProductSpecifications] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductTags]    Script Date: 11/25/2023 5:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductTags](
	[ProductId] [nvarchar](128) NOT NULL,
	[TagId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.ProductTags] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[TagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReturnCashes]    Script Date: 11/25/2023 5:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReturnCashes](
	[Id] [nvarchar](128) NOT NULL,
	[OrderId] [nvarchar](max) NOT NULL,
	[UserId] [nvarchar](max) NULL,
	[ProductId] [nvarchar](128) NULL,
	[SubTotal] [decimal](18, 2) NOT NULL,
	[Total] [decimal](18, 2) NOT NULL,
	[Status] [nvarchar](max) NULL,
	[ReturnDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[Reason] [nvarchar](max) NULL,
	[OtherReason] [nvarchar](max) NULL,
	[Image1] [nvarchar](max) NULL,
	[Image2] [nvarchar](max) NULL,
	[Image3] [nvarchar](max) NULL,
	[OrderWaybillid] [int] NOT NULL,
 CONSTRAINT [PK_dbo.ReturnCashes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reviews]    Script Date: 11/25/2023 5:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reviews](
	[UserId] [nvarchar](128) NOT NULL,
	[ProductId] [nvarchar](128) NOT NULL,
	[Rating] [int] NOT NULL,
	[Body] [nvarchar](max) NOT NULL,
	[IsApproved] [bit] NOT NULL,
	[DateAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Reviews] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpecialFeatureds]    Script Date: 11/25/2023 5:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpecialFeatureds](
	[Id] [nvarchar](128) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Link] [nvarchar](max) NOT NULL,
	[BackgroundImage] [nvarchar](max) NOT NULL,
	[ProductImage] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.SpecialFeatureds] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Statuses]    Script Date: 11/25/2023 5:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Statuses](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Statuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tags]    Script Date: 11/25/2023 5:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tags](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Tags] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Wishlists]    Script Date: 11/25/2023 5:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wishlists](
	[UserId] [nvarchar](128) NOT NULL,
	[ProductId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.Wishlists] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [Discriminator]) VALUES (N'267bc00a-89c3-453a-9958-7f476371bc23', N'root', N'ApplicationRole')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [Discriminator]) VALUES (N'42833d40-a4c7-41ba-ab34-9a026a2222b9', N'customer', N'ApplicationRole')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [Discriminator]) VALUES (N'43e989f2-6d7f-44c6-97dc-504dc6229055', N'administrator', N'ApplicationRole')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [Discriminator]) VALUES (N'5feee5b2-5c24-4fe1-9b2c-5a20c3abcc2e', N'Courier', N'ApplicationRole')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [Discriminator]) VALUES (N'caf78bb6-a0d7-4a59-b526-6510f32d9dd6', N'modifier', N'ApplicationRole')
GO  
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a38cb145-28a7-4fb1-b824-eef4a58824a4', N'267bc00a-89c3-453a-9958-7f476371bc23')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a69c45bd-bf73-457f-b5fa-0c199a4cc2b9', N'42833d40-a4c7-41ba-ab34-9a026a2222b9')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b5f8cc89-5b45-414b-bacc-b2d7a3aad240', N'267bc00a-89c3-453a-9958-7f476371bc23') 
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e54a9f85-6620-447f-86d0-2b4bca713917', N'caf78bb6-a0d7-4a59-b526-6510f32d9dd6') 
GO 
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Image], [City], [State], [Zip], [Country], [Address1], [Address2], [CreatedDate], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [ShopName]) VALUES (N'a38cb145-28a7-4fb1-b824-eef4a58824a4', N'Medco', N'Ecommerce', N'/Assets/admin/images/logo-sm.png', N'09020000-5d9d-0015-5b40-08d978048c75', N'Western ', N'94', N'Sri Lankas', N'Division No 06', N'Udappu', CAST(N'2021-06-06T10:10:00.553' AS DateTime), N'admin@admin.com', 1, N'AFopE13glpBq79CWLzGO9p0QjD0dmX2w45Jj4t3pAf1ARzZYT7d4o/wzIxaZxVXg8g==', N'08ac334a-e64e-4495-ae60-580fe2dba893', N'0778998490', 0, 0, NULL, 0, 0, N'admin@admin.com', NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Image], [City], [State], [Zip], [Country], [Address1], [Address2], [CreatedDate], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [ShopName]) VALUES (N'a69c45bd-bf73-457f-b5fa-0c199a4cc2b9', NULL, NULL, N'/Assets/admin/images/logo-sm.png', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2021-09-28T12:14:38.583' AS DateTime), N'aux71030@gmail.com', 1, N'ABHcj3WLiJuW9Dh+OY7IHE2NIxXrEBHzGtjR5GohiO3rRL42WELtwFeGogP+D9lXHw==', N'e968fb2f-ebc8-4359-8388-1d5a9834e943', NULL, 0, 0, NULL, 0, 0, N'aux71030@gmail.com', NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Image], [City], [State], [Zip], [Country], [Address1], [Address2], [CreatedDate], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [ShopName]) VALUES (N'b5f8cc89-5b45-414b-bacc-b2d7a3aad240', N'Hau', N'Nguyen Dinh', N'/Assets/admin/images/logo-sm.png', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2021-06-05T16:30:11.440' AS DateTime), N'admin@localshop.com', 1, N'AG5dKof5FY8LMlLkiQXgGw6kMF+qHleZgSHJE3+5wTeXH80iJEPLFmOsQtJjsSeDeA==', N'ba5bef60-0f30-4ca2-86a0-1c6791ff19bc', NULL, 0, 0, NULL, 0, 0, N'admin@localshop.com', NULL)
GO 
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Image], [City], [State], [Zip], [Country], [Address1], [Address2], [CreatedDate], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [ShopName]) VALUES (N'e54a9f85-6620-447f-86d0-2b4bca713917', NULL, NULL, N'/Assets/admin/images/logo-sm.png', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2021-09-23T11:32:20.837' AS DateTime), N'groupproj2nd5@gmail.com', 0, N'AMDZCBl5BZUP1QPKORwqbXhEy8qtjUL+gOAiVYkvpL7tkC/RJpvLuJ7Ufk2IjGMvYg==', N'f7d9621c-14e0-4218-b054-01383038c27c', NULL, 0, 0, NULL, 0, 0, N'groupproj2nd5@gmail.com', NULL)
GO
 
INSERT [dbo].[Banners] ([Id], [Image], [Link]) VALUES (N'09020000-5d9d-0015-0d51-08d97756af62', N'/ckfinder/connector?command=Proxy&lang=en&type=Images&currentFolder=%2F&hash=ea8d60299e82343398f8ef2451c2b22b3c8cc8c6&fileName=grocery-tech-carts_1586189341.jpg', N'http://expts.lk/shop?category=Fashion')
GO
INSERT [dbo].[Banners] ([Id], [Image], [Link]) VALUES (N'09020000-5d9d-0015-5dc6-08d97756f5b2', N'/ckfinder/connector?command=Proxy&lang=en&type=Images&currentFolder=%2F&hash=ea8d60299e82343398f8ef2451c2b22b3c8cc8c6&fileName=converting-sites.jpg', N'http://expts.lk/shop?category=Other')
GO
INSERT [dbo].[Categories] ([Id], [Name], [ParentId], [IsActive]) VALUES (N'09020000-5d9d-0015-6b2c-08d9b0e16531', N'Cosmatics', NULL, 1)
GO
INSERT [dbo].[Categories] ([Id], [Name], [ParentId], [IsActive]) VALUES (N'09020000-5d9d-0015-cc81-08d92be08bcf', N'Other', NULL, 0)
GO
INSERT [dbo].[Categories] ([Id], [Name], [ParentId], [IsActive]) VALUES (N'9d6b0000-0a65-5c26-0fbb-08d928a88988', N'Ayurveda', NULL, 1)
GO
INSERT [dbo].[Categories] ([Id], [Name], [ParentId], [IsActive]) VALUES (N'9d6b0000-0a65-5c26-a403-08d928a898e4', N'Medicine', NULL, 1)
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-00db-08d978082205', N'Galgamuwa', N'E', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-0343-08d978065aa7', N'Ehaliyagoda', N'D', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-0721-08d97804488a', N'Colombo-09', N'A', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-088d-08d978077cc4', N'Anuradhapura', N'E', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-0a93-08d9780801ff', N'Mahiyanganay', N'E', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-0c24-08d978085e87', N'Neluwa', N'E', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-0ee9-08d97807b938', N'Pollannaruwa', N'E', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-0f20-08d978062aea', N'Ambalangoda', N'D', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-0f46-08d9780668f2', N'Wennappuwa', N'D', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-10d6-08d97803b61a', N'Colombo-05', N'A', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-1117-08d97806004b', N'Kaluthara', N'D', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-167a-08d978063483', N'Chilaw', N'D', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-17e5-08d978042bd5', N'Colombo-07', N'A', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-1c2b-08d9780505a8', N'Kaduwela', N'C', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-1eca-08d978075878', N'Mullativu', N'E', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-2b68-08d978064240', N'Negambo', N'D', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-2e88-08d978046cb4', N'Colombo-12', N'A', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-301b-08d978047817', N'Colombo-13', N'A', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-311f-08d97807eb82', N'Rathnapura', N'E', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-35d3-08d978085587', N'Nuwaraeliya', N'E', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-3619-08d978080c0d', N'Matale', N'E', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-39c0-08d977fe7455', N'Colombo ', N'A', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-476f-08d978046189', N'Colombo-11', N'A', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-4985-08d977fe9b1e', N'Colombo 03', N'A', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-4afe-08d978083024', N'Gampola', N'E', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-4bfb-08d97808437f', N'Hatton', N'E', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-4d5b-08d97805e078', N'Kurunagala', N'D', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-4de5-08d9780685b1', N'Panadura', N'D', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-4fa3-08d978051b94', N'Homagama', N'C', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-51d9-08d9780749de', N'Kilinochchi', N'E', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-5ac1-08d97807d4b8', N'Matara', N'E', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-5b40-08d978048c75', N'Colombo-15', N'A', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-624b-08d97807ca51', N'Puttalam', N'E', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-64b1-08d97805d5ae', N'Gampaha', N'D', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-650f-08d97746e54b', N'Colombo-02', N'A', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-667a-08d978073c6d', N'Jafna', N'E', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-677f-08d97803204f', N'Colombo 04', N'A', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-690c-08d97807df87', N'Hambanthota', N'E', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-6af1-08d97804572d', N'Colombo-10', N'A', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-6b2c-08d978077210', N'Vavuniya', N'E', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-6e40-08d97730f3be', N'Colombo-01', N'A', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-75f6-08d97807872d', N'Trincomalee', N'E', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-7a0e-08d978061f45', N'Awissawella', N'D', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-7d1d-08d97804398f', N'Colombo-08', N'A', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-82d3-08d978076158', N'Mannar', N'E', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-8e2e-08d978079196', N'Batticoloa', N'E', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-8eba-08d97807c20a', N'Badulla', N'E', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-93c8-08d97806135e', N'Galle', N'D', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-965e-08d97807afba', N'Dambulla', N'E', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-a06e-08d97808161a', N'Bandarawala', N'E', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-b6bf-08d97807f538', N'Embilipitiya', N'E', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-b83f-08d9780671fb', N'Wattala', N'D', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-ba65-08d97805ebb3', N'Kegalle', N'D', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-c96f-08d97807a670', N'Monaragala', N'E', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-d857-08d978079945', N'Ampara', N'E', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-db8a-08d97805f383', N'Kandy', N'D', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-df28-08d9780483ae', N'Colombo-14', N'A', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-e0b9-08d97808681b', N'Balangoda', N'E', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-e871-08d978067a78', N'Mirigama', N'D', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-e9e1-08d97803d063', N'Colombo-06', N'A', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-f5e1-08d97804eb9c', N'suburbs', N'B', CAST(275.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cities] ([Id], [Name], [Zone], [Amount]) VALUES (N'09020000-5d9d-0015-f962-08d9780510f3', N'Pilindala', N'C', CAST(275.00 AS Decimal(18, 2)))
GO
SET IDENTITY_INSERT [dbo].[Contacts] ON 
GO
INSERT [dbo].[Contacts] ([Id], [Name], [Email], [Subject], [Message], [IsRead]) VALUES (1, N'Hairul Mirshad', N'hndit2016@gmail.com', N'Salam', N'yes', 1)
GO 
SET IDENTITY_INSERT [dbo].[Contacts] OFF
GO 
INSERT [dbo].[OrderStatuses] ([Id], [Name]) VALUES (N'f9d10000-d769-34e6-4e67-08d7b48f1d56', N'Pending')
GO
INSERT [dbo].[OrderStatuses] ([Id], [Name]) VALUES (N'f9d10000-d769-34e6-786e-08d7b48f1d56', N'ReadyToShip')
GO
INSERT [dbo].[OrderStatuses] ([Id], [Name]) VALUES (N'f9d10000-d769-34e6-a60e-08d7b48f1d56', N'Delivered')
GO
INSERT [dbo].[OrderStatuses] ([Id], [Name]) VALUES (N'f9d10000-d769-34e6-a9d0-08d7b48f1d56', N'Cancelled')
GO
INSERT [dbo].[OrderStatuses] ([Id], [Name]) VALUES (N'f9d10000-d769-34e6-d603-08d7b94c7194', N'Approved')
GO
INSERT [dbo].[OrderStatuses] ([Id], [Name]) VALUES (N'f9d10000-d769-34e6-d603-08d7b94c7786', N'Picked')
GO
INSERT [dbo].[PaymentMethods] ([Id], [Name]) VALUES (N'f9d10000-d769-34e6-0077-08d7b492a03f', N'Direct bank transfer')
GO
INSERT [dbo].[PaymentMethods] ([Id], [Name]) VALUES (N'f9d10000-d769-34e6-aadb-08d7b492a03e', N'Cash on delivery')
GO   
INSERT [dbo].[Statuses] ([Id], [Name]) VALUES (N'222', N'Active')
GO
INSERT [dbo].[Statuses] ([Id], [Name]) VALUES (N'333', N'InActive')
GO
INSERT [dbo].[Statuses] ([Id], [Name]) VALUES (N'444', N'Pending')
GO 
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ProductId]    Script Date: 11/25/2023 5:18:12 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProductId] ON [dbo].[ReturnCashes]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Categories] ADD  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[OrderDetails] ADD  DEFAULT ((0)) FOR [DetuctionPersontage]
GO
ALTER TABLE [dbo].[OrderDetails] ADD  DEFAULT ((0)) FOR [Detuction]
GO
ALTER TABLE [dbo].[OrderDetails] ADD  DEFAULT ((0)) FOR [Tex]
GO
ALTER TABLE [dbo].[OrderDetails] ADD  DEFAULT ((0)) FOR [ReturnTotal]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT ((0)) FOR [ShippingPrice]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT ((0)) FOR [Total]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT ((0)) FOR [OrderWaybillid]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT ((0)) FOR [IsFeatured]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT ('') FOR [UserId]
GO
ALTER TABLE [dbo].[ReturnCashes] ADD  DEFAULT ((0)) FOR [OrderWaybillid]
GO
ALTER TABLE [dbo].[Reviews] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [DateAdded]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Categories]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Categories_dbo.Categories_ParentId] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[Categories] CHECK CONSTRAINT [FK_dbo.Categories_dbo.Categories_ParentId]
GO
ALTER TABLE [dbo].[Images]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Images_dbo.Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Images] CHECK CONSTRAINT [FK_dbo.Images_dbo.Products_ProductId]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_dbo.OrderDetails_dbo.Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_dbo.OrderDetails_dbo.Orders_OrderId]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_dbo.OrderDetails_dbo.Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_dbo.OrderDetails_dbo.Products_ProductId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Orders_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_dbo.Orders_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Orders_dbo.OrderStatuses_OrderStatusId] FOREIGN KEY([OrderStatusId])
REFERENCES [dbo].[OrderStatuses] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_dbo.Orders_dbo.OrderStatuses_OrderStatusId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Orders_dbo.PaymentMethods_PaymentMethodId] FOREIGN KEY([PaymentMethodId])
REFERENCES [dbo].[PaymentMethods] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_dbo.Orders_dbo.PaymentMethods_PaymentMethodId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Products_dbo.Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_dbo.Products_dbo.Categories_CategoryId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Products_dbo.Statuses_StatusId] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Statuses] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_dbo.Products_dbo.Statuses_StatusId]
GO
ALTER TABLE [dbo].[ProductSpecifications]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProductSpecifications_dbo.Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductSpecifications] CHECK CONSTRAINT [FK_dbo.ProductSpecifications_dbo.Products_ProductId]
GO
ALTER TABLE [dbo].[ProductTags]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProductTags_dbo.Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductTags] CHECK CONSTRAINT [FK_dbo.ProductTags_dbo.Products_ProductId]
GO
ALTER TABLE [dbo].[ProductTags]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProductTags_dbo.Tags_TagId] FOREIGN KEY([TagId])
REFERENCES [dbo].[Tags] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductTags] CHECK CONSTRAINT [FK_dbo.ProductTags_dbo.Tags_TagId]
GO
ALTER TABLE [dbo].[ReturnCashes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ReturnCashes_dbo.Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[ReturnCashes] CHECK CONSTRAINT [FK_dbo.ReturnCashes_dbo.Products_ProductId]
GO
ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Reviews_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Reviews] CHECK CONSTRAINT [FK_dbo.Reviews_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Reviews_dbo.Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Reviews] CHECK CONSTRAINT [FK_dbo.Reviews_dbo.Products_ProductId]
GO
ALTER TABLE [dbo].[Wishlists]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Whishlists_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Wishlists] CHECK CONSTRAINT [FK_dbo.Whishlists_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Wishlists]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Whishlists_dbo.Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Wishlists] CHECK CONSTRAINT [FK_dbo.Whishlists_dbo.Products_ProductId]
GO
USE [master]
GO
ALTER DATABASE [medco] SET  READ_WRITE 
GO
