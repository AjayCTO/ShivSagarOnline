
USE [SHIVAMEcommerce]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 05/26/2018 12:07:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AllProductImages]    Script Date: 05/26/2018 12:07:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AllProductImages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ImageName] [nvarchar](max) NULL,
	[ImagePath] [nvarchar](max) NULL,
	[UserID] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[Sort] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AllProductImages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 05/26/2018 12:07:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerAddresses]    Script Date: 05/26/2018 12:07:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerAddresses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Address1] [nvarchar](max) NULL,
	[Address2] [nvarchar](max) NULL,
	[City] [nvarchar](max) NULL,
	[State] [nvarchar](max) NULL,
	[Country] [nvarchar](max) NULL,
	[Region] [nvarchar](max) NULL,
	[AddressType] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[Sort] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.CustomerAddresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Claims]    Script Date: 05/26/2018 12:07:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Claims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[Sort] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Claims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 05/26/2018 12:07:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
	[Sort] [int] NULL,
	[Discriminator] [nvarchar](128) NOT NULL,
	[resetPasswordToken] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Plans]    Script Date: 05/26/2018 12:07:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Plans](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PlanName] [nvarchar](max) NOT NULL,
	[Plancode] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[Sort] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Notes] [nvarchar](max) NULL,
	[PlanFrequency] [nvarchar](max) NOT NULL,
	[MonthlyPrice] [decimal](18, 2) NOT NULL,
	[YearlyPrice] [decimal](18, 2) NOT NULL,
	[ProductBucketCount] [int] NOT NULL,
	[UserBucketCount] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Plans] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 05/26/2018 12:07:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[ReferenceDetails] [nvarchar](max) NULL,
	[Notes] [nvarchar](max) NULL,
	[PaymentType] [nvarchar](max) NULL,
	[PaymentStatus] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[Sort] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Payments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderStatus]    Script Date: 05/26/2018 12:07:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Status] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[Sort] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.OrderStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Manufacturers]    Script Date: 05/26/2018 12:07:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manufacturers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NOT NULL,
	[SupplierID] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[Sort] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Manufacturers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[ExecuteStringAsQuery]    Script Date: 05/26/2018 12:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[ExecuteStringAsQuery]()
RETURNS Varchar(8000)
AS
BEGIN
DECLARE @SQLQuery AS NVARCHAR(500);
DECLARE @RESULT AS NVARCHAR(500);
--QUERY
DECLARE @DynamicPivotQuery AS NVARCHAR(MAX);
DECLARE @ColumnNamesInPivot AS NVARCHAR(MAX);
SET @DynamicPivotQuery='select * from Products';
SET @RESULT=@DynamicPivotQuery;
return @RESULT
END
GO
/****** Object:  Table [dbo].[emailrecords]    Script Date: 05/26/2018 12:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[emailrecords](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email_Sender] [nvarchar](max) NULL,
	[Email_Receiver] [nvarchar](max) NULL,
	[Send_Date] [nvarchar](max) NULL,
	[Subject] [nvarchar](max) NULL,
	[Message] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.emailrecords] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductAttributes]    Script Date: 05/26/2018 12:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductAttributes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AttributeName] [nvarchar](max) NULL,
	[AttributeDescription] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.ProductAttributes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductStatus]    Script Date: 05/26/2018 12:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[Sort] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.ProductStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[SplitString]    Script Date: 05/26/2018 12:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[SplitString]
(    
      @Input NVARCHAR(MAX),
      @Character CHAR(1)
)
RETURNS @Output TABLE (
      Item NVARCHAR(1000)
)
AS
BEGIN
      DECLARE @StartIndex INT, @EndIndex INT
 
      SET @StartIndex = 1
      IF SUBSTRING(@Input, LEN(@Input) - 1, LEN(@Input)) <> @Character
      BEGIN
            SET @Input = @Input + @Character
      END
 
      WHILE CHARINDEX(@Character, @Input) > 0
      BEGIN
            SET @EndIndex = CHARINDEX(@Character, @Input)
           
            INSERT INTO @Output(Item)
            SELECT SUBSTRING(@Input, @StartIndex, @EndIndex - 1)
           
            SET @Input = SUBSTRING(@Input, @EndIndex + 1, LEN(@Input))
      END
 
      RETURN
END
GO
/****** Object:  Table [dbo].[UnitOfMeasures]    Script Date: 05/26/2018 12:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UnitOfMeasures](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UnitOfMeasuresName] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.UnitOfMeasures] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThreshHolds]    Script Date: 05/26/2018 12:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThreshHolds](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[ProductName] [nvarchar](max) NULL,
	[LowQuantityThresHold] [int] NOT NULL,
	[HighQuantityThresHold] [int] NOT NULL,
 CONSTRAINT [PK_dbo.ThreshHolds] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Taxes]    Script Date: 05/26/2018 12:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Taxes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Descripton] [nvarchar](max) NULL,
	[Category] [nvarchar](max) NOT NULL,
	[Rate] [float] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[Sort] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Taxes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Suppliers]    Script Date: 05/26/2018 12:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suppliers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [nvarchar](max) NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Title] [nvarchar](max) NULL,
	[Address1] [nvarchar](max) NULL,
	[Address2] [nvarchar](max) NULL,
	[City] [nvarchar](max) NULL,
	[State] [nvarchar](max) NULL,
	[PostalCode] [nvarchar](max) NULL,
	[Country] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[Logo] [nvarchar](max) NULL,
	[SupplierType] [nvarchar](max) NULL,
	[ParentSupplierID] [int] NOT NULL,
	[RegisteredByID] [nvarchar](max) NULL,
	[UserID] [nvarchar](128) NULL,
	[PlanID] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[Sort] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Notes] [nvarchar](max) NULL,
	[PlanStartDate] [datetime] NOT NULL,
	[PlanEndDate] [datetime] NOT NULL,
	[ProductCount] [int] NOT NULL,
	[UserCount] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Suppliers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 05/26/2018 12:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NULL,
	[CreditCard] [nvarchar](max) NULL,
	[CreditCardType] [nvarchar](max) NULL,
	[CardExpMo] [nvarchar](max) NULL,
	[CardExpYr] [nvarchar](max) NULL,
	[UserID] [nvarchar](128) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[Sort] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 05/26/2018 12:07:48 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 05/26/2018 12:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[UserId] [nvarchar](128) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 05/26/2018 12:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[User_Id] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 05/26/2018 12:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[ParentCategory] [int] NULL,
	[CategoryImage] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[Sort] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Notes] [nvarchar](max) NULL,
	[SupplierCategoryType] [nvarchar](max) NULL,
	[Discriminator] [nvarchar](128) NOT NULL,
	[Supplier_Id] [int] NULL,
 CONSTRAINT [PK_dbo.Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 05/26/2018 12:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderNumber] [nvarchar](max) NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[ShipDate] [datetime] NOT NULL,
	[RequiredDate] [datetime] NOT NULL,
	[IsPaid] [bit] NULL,
	[TimeStamp] [nvarchar](max) NULL,
	[Isdeleted] [bit] NULL,
	[TransactionStatus] [nvarchar](max) NULL,
	[CustomerID] [int] NOT NULL,
	[OrderStatusID] [int] NOT NULL,
	[TotalDiscount] [decimal](18, 2) NOT NULL,
	[TotalPrice] [decimal](18, 2) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[Sort] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 05/26/2018 12:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](max) NOT NULL,
	[ProductCode] [nvarchar](max) NOT NULL,
	[Ranking] [nvarchar](max) NULL,
	[SKU] [nvarchar](max) NULL,
	[IDSKU] [nvarchar](max) NULL,
	[SupplierID] [int] NOT NULL,
	[ManuFacturerID] [int] NOT NULL,
	[CateogryID] [int] NOT NULL,
	[UnitOfMeasuresId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[Sort] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[InsertProduct]    Script Date: 05/26/2018 12:07:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertProduct]
      @Name nvarchar(MAX),
	  @ProductCode nvarchar(MAX),
	  @Description nvarchar(MAX),
	  --@Quantity int,
	  @categoryName nvarchar(MAX),
	  --@cost float,
	  @UnitOfMeasure nvarchar(MAX),
	  @ManuFacturer nvarchar(MAX),
	  @SupplierID int,
	   @ProductID INT OUTPUT ,
 @categoryID INT = NULL ,
 @ManufacturerID INT = NULL,
 @UnitOfMeasureID INT = NULL 
 
AS
BEGIN
    SET NOCOUNT ON;

    

	SET @categoryID = (select TOP 1 Id 
                      from dbo.Categories c 
                      where LOWER(c.CategoryName) = LOWER(@categoryName))
					SET @categoryID=@categoryID

					  if @categoryID IS NULL
					  Begin
					   INSERT dbo.Categories(CategoryName,IsActive,CreatedDate,UpdatedDate,Sort,[Description],Notes,SupplierCategoryType,Discriminator) values(@categoryName,1,GETDATE(),GETDATE(),78,'','','','Category');
						SET @categoryID = SCOPE_IDENTITY();
						   End
Else
    Begin
	print @categoryID
	End
	SET @ManufacturerID = (select TOP 1 Id 
                      from dbo.Manufacturers c 
                      where LOWER(c.Name) = LOWER(@ManuFacturer))
					SET @ManufacturerID=@ManufacturerID
	
						  if @ManufacturerID IS NULL
					  Begin
					   INSERT dbo.Manufacturers(Code,Name,SupplierID,CreatedDate,UpdatedDate,Sort,[Description],Notes) values('',@ManuFacturer,@SupplierID,GETDATE(),GETDATE(),78,'','');
						SET @ManufacturerID = SCOPE_IDENTITY();
						   End
Else
    Begin
	print @ManufacturerID
	End
	
	SET @UnitOfMeasureID = (select TOP 1 Id 
                      from dbo.UnitOfMeasures c 
                      where LOWER(c.UnitOfMeasuresName) = LOWER(@UnitOfMeasure))
					SET @UnitOfMeasureID=@UnitOfMeasureID
	
						  if @UnitOfMeasureID IS NULL
					  Begin
					   INSERT dbo.UnitOfMeasures(UnitOfMeasuresName) values(@UnitOfMeasure);
						SET @UnitOfMeasureID = SCOPE_IDENTITY();
						   End
Else
    Begin
	print @UnitOfMeasureID
	End
	
							SET @ProductID = (select TOP 1 Id 
                      from dbo.Products c 
                      where LOWER(c.ProductName) = LOWER(@Name))
					SET @ProductID=@ProductID
	
						  if @ProductID IS NULL
						 Begin
						INSERT INTO [Products]
           ([ProductName]
           ,[ProductCode]
           ,[Ranking]
           ,[SKU]
           ,[IDSKU]
           ,[SupplierID]
           ,[ManuFacturerID]
           ,[CateogryID]
           ,[UnitOfMeasuresId]
           ,[CreatedDate]
           ,[UpdatedDate]
           ,[Sort]
           ,[Description]
           ,[Notes])
     VALUES
           (@Name,@ProductCode,'','','',@SupplierID,@ManufacturerID,@categoryID,@UnitOfMeasureID,GETDATE(),GETDATE(),'234',@Description,'')
           SET @ProductID = SCOPE_IDENTITY();
           End
Else
    Begin
	print @ProductID
	End
						
		return @ProductID			
END
GO
/****** Object:  Table [dbo].[WishLists]    Script Date: 05/26/2018 12:07:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WishLists](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.WishLists] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductAttributeWithQuantities]    Script Date: 05/26/2018 12:07:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductAttributeWithQuantities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[AttributeValues] [nvarchar](max) NULL,
	[IsAvailable] [bit] NULL,
	[Discount] [decimal](18, 2) NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[UnitWeight] [decimal](18, 2) NOT NULL,
	[UnitInStock] [decimal](18, 2) NOT NULL,
	[UnitsInOrder] [decimal](18, 2) NOT NULL,
	[ProductQuantity] [int] NOT NULL,
	[ProductPrice] [decimal](18, 2) NOT NULL,
	[Weight] [decimal](18, 2) NULL,
	[IsFeatured] [bit] NOT NULL,
	[lowQuantityThreshold] [decimal](18, 2) NULL,
	[highQuantityThreshold] [decimal](18, 2) NULL,
	[StatusId] [int] NULL,
 CONSTRAINT [PK_dbo.ProductAttributeWithQuantities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_StatusId] ON [dbo].[ProductAttributeWithQuantities] 
(
	[StatusId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductAttributesRelations]    Script Date: 05/26/2018 12:07:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductAttributesRelations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SupplierID] [int] NOT NULL,
	[ProductAttributesId] [int] NOT NULL,
	[Product_Id] [int] NULL,
 CONSTRAINT [PK_dbo.ProductAttributesRelations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerReviews]    Script Date: 05/26/2018 12:07:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerReviews](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Review] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.CustomerReviews] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItems]    Script Date: 05/26/2018 12:07:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
	[TotalPrice] [decimal](18, 2) NOT NULL,
	[Discount] [decimal](18, 2) NOT NULL,
	[SupplierID] [int] NOT NULL,
	[Orders_Id] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[Sort] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.OrderItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[InsertProductAttributes]    Script Date: 05/26/2018 12:07:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertProductAttributes]
      @AttributeValues nvarchar(MAX),
	  @ProductPrice decimal,
	  @ProductQuantity decimal,
	  @ProductId int,
	  @UnitInStock decimal,
	  @UnitWeight decimal,
	  @HighQuantityThreshold decimal,
	  @LowQuantityThreshold decimal,
	  @ProductStatus nvarchar(MAX),
      @StatusID INT = NULL 
 
 
AS
BEGIN
    SET NOCOUNT ON;
    
    	SET @StatusID = (select TOP 1 Id 
                      from dbo.ProductStatus c 
                      where LOWER(c.Name) = LOWER(@ProductStatus))
					SET @StatusID=@StatusID

					  if @StatusID IS NULL
					  Begin
					   INSERT dbo.ProductStatus(Name,IsActive,CreatedDate,UpdatedDate,Sort,[Description],Notes) values(@ProductStatus,1,GETDATE(),GETDATE(),78,'','');
						SET @StatusID = SCOPE_IDENTITY();
						   End
Else
    Begin
	print @StatusID
	End

    INSERT INTO [ProductAttributeWithQuantities]
           ([AttributeValues]
           ,[IsAvailable]
           ,[Discount]
           ,[UnitPrice]
           ,[UnitWeight]
           ,[UnitInStock]
           ,[UnitsInOrder]
           ,[ProductQuantity]
           ,[ProductPrice]
           ,[Weight]
           ,[ProductId]
           ,[IsFeatured]
           ,[lowQuantityThreshold]
           ,[highQuantityThreshold]
           ,[StatusId])
     VALUES
           (@AttributeValues
           ,1
           ,'0'
           ,@ProductPrice
           ,@UnitWeight
           ,@UnitInStock
           ,'0'
           ,@ProductQuantity
           ,@ProductPrice
           ,@UnitWeight
           ,@ProductId,0,@LowQuantityThreshold,@HighQuantityThreshold,@StatusID)
						
						

						
			
END
GO
/****** Object:  Table [dbo].[ProductImages]    Script Date: 05/26/2018 12:07:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductImages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ImageName] [nvarchar](max) NULL,
	[ImagePath] [nvarchar](max) NULL,
	[Title] [nvarchar](max) NULL,
	[ProductQuantityId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[Sort] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Notes] [nvarchar](max) NULL,
	[Product_Id] [int] NULL,
 CONSTRAINT [PK_dbo.ProductImages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ProductAttribute_view]    Script Date: 05/26/2018 12:07:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ProductAttribute_view]
AS
SELECT     (CASE WHEN S.Item LIKE '%@@%' THEN LEFT(S.Item, Charindex('@@', S.Item) - 1) ELSE S.Item END) AS AttributeName, (CASE WHEN S.Item LIKE '%@@%' THEN RIGHT(S.Item, Charindex('@@', 
                      Reverse(S.Item)) - 1) END) AS AttributeValue, productId, ProductQuantity AS Quantity, ProductPrice AS Cost, Id
FROM         (SELECT     S.Item, p.productId, p.ProductQuantity, p.ProductPrice, p.Id
                       FROM          [ProductAttributeWithQuantities] AS P CROSS apply dbo.SplitString(LEFT(P.AttributeValues, len(P.AttributeValues) - 2), '##') AS S) AS S
WHERE     S.Item <> ''
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[14] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProductAttribute_view'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProductAttribute_view'
GO
/****** Object:  View [dbo].[ProductValues_view]    Script Date: 05/26/2018 12:07:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE VIEW [dbo].[ProductValues_view]
AS
SELECT        dbo.ProductAttribute_view.AttributeValue, dbo.ProductAttribute_view.Quantity, dbo.ProductAttribute_view.Id AS ProductQuantityID, dbo.ProductAttribute_view.Cost, dbo.ProductAttributes.AttributeName, 
                         dbo.ProductAttribute_view.Id, dbo.ProductAttributesRelations.SupplierID, dbo.ProductAttribute_view.productId, dbo.Products.ProductName, dbo.Suppliers.FirstName + ' ' + dbo.Suppliers.LastName AS SupplierName,
                             (SELECT        TOP (1) ImageName
                               FROM            dbo.ProductImages
                               WHERE        (dbo.ProductAttribute_view.Id = ProductQuantityId)) AS ImageName,
                             (SELECT        TOP (1) ImagePath
                               FROM            dbo.ProductImages AS ProductImages_2
                               WHERE        (dbo.ProductAttribute_view.Id = ProductQuantityId)) AS ImagePath
FROM            dbo.Products INNER JOIN
                         dbo.ProductAttribute_view ON dbo.Products.Id = dbo.ProductAttribute_view.productId INNER JOIN
                         dbo.Suppliers ON dbo.Products.SupplierID = dbo.Suppliers.Id RIGHT OUTER JOIN
                         dbo.ProductAttributesRelations INNER JOIN
                         dbo.ProductAttributes ON dbo.ProductAttributesRelations.ProductAttributesId = dbo.ProductAttributes.Id ON dbo.ProductAttribute_view.AttributeName = dbo.ProductAttributesRelations.ProductAttributesId AND 
                         dbo.Products.SupplierID = dbo.ProductAttributesRelations.SupplierID
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[56] 4[8] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -96
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Products"
            Begin Extent = 
               Top = 0
               Left = 692
               Bottom = 234
               Right = 868
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ProductAttribute_view"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 163
               Right = 198
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Suppliers"
            Begin Extent = 
               Top = 36
               Left = 1139
               Bottom = 156
               Right = 1309
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "ProductAttributesRelations"
            Begin Extent = 
               Top = 144
               Left = 390
               Bottom = 281
               Right = 574
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ProductAttributes"
            Begin Extent = 
               Top = 241
               Left = 17
               Bottom = 401
               Right = 202
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProductValues_view'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProductValues_view'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProductValues_view'
GO
/****** Object:  View [dbo].[GetAttributes]    Script Date: 05/26/2018 12:07:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[GetAttributes]
AS
SELECT DISTINCT TOP (100) PERCENT dbo.ProductAttribute_view.AttributeValue, dbo.ProductAttributes.AttributeName, dbo.ProductAttributes.Id
FROM         dbo.ProductAttributes INNER JOIN
                      dbo.ProductAttribute_view ON dbo.ProductAttributes.Id = CONVERT(numeric, dbo.ProductAttribute_view.AttributeName)
ORDER BY dbo.ProductAttributes.AttributeName
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ProductAttributes"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 111
               Right = 223
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ProductAttribute_view"
            Begin Extent = 
               Top = 6
               Left = 261
               Bottom = 126
               Right = 421
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'GetAttributes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'GetAttributes'
GO
/****** Object:  StoredProcedure [dbo].[GetProductsDetail]    Script Date: 05/26/2018 12:07:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetProductsDetail]
	 @SupplierId nvarchar(10)
AS
BEGIN
	--QUERY
DECLARE @DynamicPivotQuery AS NVARCHAR(MAX);
DECLARE @ColumnNamesInPivot AS NVARCHAR(MAX);

--Get distinct values of PIVOT Column 
SELECT   @ColumnNamesInPivot = ISNULL(@ColumnNamesInPivot + ',', '')
        + QUOTENAME([AttributeName])
FROM    ( SELECT    DISTINCT
                    [AttributeName]
          FROM      ProductValues_view where SupplierID=@SupplierId
        ) AS P



SELECT  @DynamicPivotQuery = N'Select productName,Cost,Quantity,SupplierName,'
        + @ColumnNamesInPivot + ' 
            FROM    ( SELECT * 
          FROM      ProductValues_view where SupplierID='+@SupplierId+'
        ) AS SourceTable PIVOT( MAX(AttributeValue) FOR [AttributeName] IN ('
        + @ColumnNamesInPivot + ') ) AS PVTTable'

EXEC sp_executesql @DynamicPivotQuery;
END
GO
/****** Object:  StoredProcedure [dbo].[GetProductsCount]    Script Date: 05/26/2018 12:07:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetProductsCount]
	 @SupplierId nvarchar(10)
AS
BEGIN
	--QUERY
DECLARE @DynamicPivotQuery AS NVARCHAR(MAX);
DECLARE @ColumnNamesInPivot AS NVARCHAR(MAX);

--Get distinct values of PIVOT Column 
SELECT   @ColumnNamesInPivot = ISNULL(@ColumnNamesInPivot + ',', '')
        + QUOTENAME([AttributeName])
FROM    ( SELECT    DISTINCT
                    [AttributeName]
          FROM      ProductValues_view where SupplierID=@SupplierId
        ) AS P



SELECT  @DynamicPivotQuery = N'Select Count(*) as TotalRecord  
            FROM    ( SELECT * 
          FROM      ProductValues_view where SupplierID='+@SupplierId+'
        ) AS SourceTable PIVOT( MAX(AttributeValue) FOR [AttributeName] IN ('
        + @ColumnNamesInPivot + ') ) AS PVTTable'

EXEC sp_executesql @DynamicPivotQuery;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllSortedProductsFrontFace]    Script Date: 05/26/2018 12:07:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_GetAllSortedProductsFrontFace]
@DisplayLength as Int,
@DisplayStart as Int,
@SortCol as Nvarchar(100),
@SortDir as Nvarchar(10),
@SearchText Nvarchar(255) =NULL,
@FilterText NVARCHAR(255)=Null
As
Begin
DECLARE @FirstRec int,@LastRec int;
DECLARE @DynamicPivotQuery AS NVARCHAR(MAX);
DECLARE @ColumnNamesInPivot AS NVARCHAR(MAX);
DECLARE @WhereClause as NVARCHAR(MAX);
DECLARE @AttributeWhereClause as NVARCHAR(MAX);

 

Set @FirstRec=@DisplayStart;
Set @LastRec=@DisplayStart + @DisplayLength;
SET @WhereClause='';
SET @AttributeWhereClause='';

--Get distinct values of PIVOT Column 
SELECT   @ColumnNamesInPivot = ISNULL(@ColumnNamesInPivot + ',', '')
        + QUOTENAME([AttributeName])
FROM    ( SELECT    DISTINCT
                    [AttributeName]
          FROM      ProductValues_view  where [AttributeName] is not null
        ) AS P;
 
 
 IF @SearchText IS NOT NULL
BEGIN
  SET @WhereClause=' Or productName like ''%'+@SearchText +'%''
                               or Cost like ''%'+ @SearchText+'%'' 
                               or Quantity like ''%'+ @SearchText+'%''
                               or SupplierName like ''%'+ @SearchText+'%''';
                               
                               
DECLARE @valueList varchar(8000)
DECLARE @pos INT
DECLARE @len INT
DECLARE @value varchar(8000)
DECLARE @Query varchar(8000)
DECLARE @SupplierId as nvarchar(10)
SET @valueList = @ColumnNamesInPivot;
SET @SupplierId=-1;
--the value list string must end with a comma ','
--so, if the last comma it's not there, the following IF will add a trailing comma to the value list
IF @valueList NOT LIKE '%,'
BEGIN
    set @valueList = @valueList + ',';
END


set @pos = 0;
set @len = 0;
set @AttributeWhereClause='('''+@SearchText +''' IS NULL'+ @WhereClause;

WHILE CHARINDEX(',', @valueList, @pos+1)>0
BEGIN
    set @len = CHARINDEX(',', @valueList, @pos+1) - @pos
    set @value = SUBSTRING(@valueList, @pos, @len)
    --SELECT @pos, @len, @value /*this is here for debugging*/
        
    
    SET @AttributeWhereClause=@AttributeWhereClause + ' Or ' + @value +' like ''%'+@SearchText +'%''';

    set @pos = CHARINDEX(',', @valueList, @pos+@len) +1
END
SET @AttributeWhereClause=@AttributeWhereClause + ') AND';
--PRINT @WhereClause                            
                               
END



set @Query='FROM(SELECT * FROM ProductValues_view '+CASE WHEN @SupplierId <>-1 THEN 'where SupplierID='+@SupplierId  ELSE '' END +')'

print @SupplierId
print @Query
print @ColumnNamesInPivot

--SELECT  @DynamicPivotQuery = N'Select * from (Select Row_Number() over (order by '+@SortCol+' '+@SortDir+') as RowNum,Count(*) over() as TotalCount,ImagePath,productName,Cost,Quantity,SupplierName,ImageName,'+ @ColumnNamesInPivot + @Query+ 'AS SourceTable PIVOT( MAX(AttributeValue) FOR [AttributeName] IN ('+ @ColumnNamesInPivot + ') ) AS PVTTable ) as PP where '+@AttributeWhereClause + '(RowNum > '+convert(nvarchar,@FirstRec)+' AND RowNum<='+convert(nvarchar,@LastRec)+')';
--print @DynamicPivotQuery;

SELECT  @DynamicPivotQuery = N'Select * from (Select Row_Number() over (order by '+@SortCol+' '+@SortDir+') as RowNum,Count(*) over() as TotalCount,ImagePath,productName,Cost,Quantity,SupplierName,ImageName,Id as ProductId,'
        + @ColumnNamesInPivot + ' 
            FROM    ( SELECT * 
          FROM      ProductValues_view  
        ) AS SourceTable PIVOT( MAX(AttributeValue) FOR [AttributeName] IN ('
        + @ColumnNamesInPivot + ') ) AS PVTTable ) as PP where ProductId is not null AND '+@FilterText + '(RowNum > '+convert(nvarchar,@FirstRec)+' AND RowNum<='+convert(nvarchar,@LastRec)+')';
	
EXEC sp_executesql @DynamicPivotQuery;
    
End
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllSortedProducts]    Script Date: 05/26/2018 12:07:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_GetAllSortedProducts]
@DisplayLength as Int,
@DisplayStart as Int,
@SortCol as Nvarchar(100),
@SortDir as Nvarchar(10),
@SearchText Nvarchar(255) =NULL,
@SupplierId as nvarchar(10)
As
Begin
DECLARE @FirstRec int,@LastRec int;
DECLARE @DynamicPivotQuery AS NVARCHAR(MAX);
DECLARE @ColumnNamesInPivot AS NVARCHAR(MAX);
DECLARE @WhereClause as NVARCHAR(MAX);
DECLARE @AttributeWhereClause as NVARCHAR(MAX);

Set @FirstRec=@DisplayStart;
Set @LastRec=@DisplayStart + @DisplayLength;
SET @WhereClause='';
SET @AttributeWhereClause='';

--Get distinct values of PIVOT Column 
SELECT   @ColumnNamesInPivot = ISNULL(@ColumnNamesInPivot + ',', '')
        + QUOTENAME([AttributeName])
FROM    ( SELECT    DISTINCT
                    [AttributeName]
          FROM      ProductValues_view where SupplierID=@SupplierId
        ) AS P;
 
 
 IF @SearchText IS NOT NULL
BEGIN
  SET @WhereClause=' Or productName like ''%'+@SearchText +'%''
                               or Cost like ''%'+ @SearchText+'%'' 
                               or Quantity like ''%'+ @SearchText+'%''
                               or SupplierName like ''%'+ @SearchText+'%''';
                               
                               
DECLARE @valueList varchar(8000)
DECLARE @pos INT
DECLARE @len INT
DECLARE @value varchar(8000)
DECLARE @Query varchar(8000)
SET @valueList = @ColumnNamesInPivot;

--the value list string must end with a comma ','
--so, if the last comma it's not there, the following IF will add a trailing comma to the value list
IF @valueList NOT LIKE '%,'
BEGIN
    set @valueList = @valueList + ',';
END


set @pos = 0;
set @len = 0;
set @AttributeWhereClause='('''+@SearchText +''' IS NULL'+ @WhereClause;

WHILE CHARINDEX(',', @valueList, @pos+1)>0
BEGIN
    set @len = CHARINDEX(',', @valueList, @pos+1) - @pos
    set @value = SUBSTRING(@valueList, @pos, @len)
    --SELECT @pos, @len, @value /*this is here for debugging*/
        
    
    SET @AttributeWhereClause=@AttributeWhereClause + ' Or ' + @value +' like ''%'+@SearchText +'%''';

    set @pos = CHARINDEX(',', @valueList, @pos+@len) +1
END
SET @AttributeWhereClause=@AttributeWhereClause + ') AND';
--PRINT @WhereClause                            
                               
END

set @Query='FROM(SELECT * FROM ProductValues_view '+CASE WHEN @SupplierId <>-1 THEN 'where SupplierID='+@SupplierId  ELSE '' END +')'

print @SupplierId
print @Query


SELECT  @DynamicPivotQuery = N'Select * from (Select Row_Number() over (order by '+@SortCol+' '+@SortDir+') as RowNum,Count(*) over() as TotalCount,ProductId,ProductQuantityID,ImagePath,productName,Cost,Quantity,SupplierName,ImageName,'+ @ColumnNamesInPivot + @Query+ 'AS SourceTable PIVOT( MAX(AttributeValue) FOR [AttributeName] IN ('+ @ColumnNamesInPivot + ') ) AS PVTTable ) as PP where '+@AttributeWhereClause + '(RowNum > '+convert(nvarchar,@FirstRec)+' AND RowNum<='+convert(nvarchar,@LastRec)+')';
print @DynamicPivotQuery;
EXEC sp_executesql @DynamicPivotQuery;
    
End
GO
/****** Object:  Default [DF__Plans__MonthlyPr__59FA5E80]    Script Date: 05/26/2018 12:07:47 ******/
ALTER TABLE [dbo].[Plans] ADD  DEFAULT ((0)) FOR [MonthlyPrice]
GO
/****** Object:  Default [DF__Plans__YearlyPri__5AEE82B9]    Script Date: 05/26/2018 12:07:47 ******/
ALTER TABLE [dbo].[Plans] ADD  DEFAULT ((0)) FOR [YearlyPrice]
GO
/****** Object:  Default [DF__Plans__ProductBu__5BE2A6F2]    Script Date: 05/26/2018 12:07:47 ******/
ALTER TABLE [dbo].[Plans] ADD  DEFAULT ((0)) FOR [ProductBucketCount]
GO
/****** Object:  Default [DF__Plans__UserBucke__5DCAEF64]    Script Date: 05/26/2018 12:07:47 ******/
ALTER TABLE [dbo].[Plans] ADD  DEFAULT ((0)) FOR [UserBucketCount]
GO
/****** Object:  Default [DF__Plans__IsActive__68487DD7]    Script Date: 05/26/2018 12:07:47 ******/
ALTER TABLE [dbo].[Plans] ADD  DEFAULT ((0)) FOR [IsActive]
GO
/****** Object:  Default [DF__ProductSt__Creat__6477ECF3]    Script Date: 05/26/2018 12:07:48 ******/
ALTER TABLE [dbo].[ProductStatus] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [CreatedDate]
GO
/****** Object:  Default [DF__ProductSt__Updat__656C112C]    Script Date: 05/26/2018 12:07:48 ******/
ALTER TABLE [dbo].[ProductStatus] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [UpdatedDate]
GO
/****** Object:  Default [DF__ProductSta__Sort__66603565]    Script Date: 05/26/2018 12:07:48 ******/
ALTER TABLE [dbo].[ProductStatus] ADD  DEFAULT ((0)) FOR [Sort]
GO
/****** Object:  Default [DF__Suppliers__PlanS__571DF1D5]    Script Date: 05/26/2018 12:07:48 ******/
ALTER TABLE [dbo].[Suppliers] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [PlanStartDate]
GO
/****** Object:  Default [DF__Suppliers__PlanE__5812160E]    Script Date: 05/26/2018 12:07:48 ******/
ALTER TABLE [dbo].[Suppliers] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [PlanEndDate]
GO
/****** Object:  Default [DF__Suppliers__Produ__59063A47]    Script Date: 05/26/2018 12:07:48 ******/
ALTER TABLE [dbo].[Suppliers] ADD  DEFAULT ((0)) FOR [ProductCount]
GO
/****** Object:  Default [DF__Suppliers__UserC__5CD6CB2B]    Script Date: 05/26/2018 12:07:48 ******/
ALTER TABLE [dbo].[Suppliers] ADD  DEFAULT ((0)) FOR [UserCount]
GO
/****** Object:  Default [DF__ProductAt__IsFea__5629CD9C]    Script Date: 05/26/2018 12:07:54 ******/
ALTER TABLE [dbo].[ProductAttributeWithQuantities] ADD  DEFAULT ((0)) FOR [IsFeatured]
GO
/****** Object:  ForeignKey [FK_dbo.Suppliers_dbo.AspNetUsers_UserID]    Script Date: 05/26/2018 12:07:48 ******/
ALTER TABLE [dbo].[Suppliers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Suppliers_dbo.AspNetUsers_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Suppliers] CHECK CONSTRAINT [FK_dbo.Suppliers_dbo.AspNetUsers_UserID]
GO
/****** Object:  ForeignKey [FK_dbo.Suppliers_dbo.Plans_PlanID]    Script Date: 05/26/2018 12:07:48 ******/
ALTER TABLE [dbo].[Suppliers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Suppliers_dbo.Plans_PlanID] FOREIGN KEY([PlanID])
REFERENCES [dbo].[Plans] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Suppliers] CHECK CONSTRAINT [FK_dbo.Suppliers_dbo.Plans_PlanID]
GO
/****** Object:  ForeignKey [FK_dbo.Customers_dbo.AspNetUsers_UserID]    Script Date: 05/26/2018 12:07:48 ******/
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Customers_dbo.AspNetUsers_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_dbo.Customers_dbo.AspNetUsers_UserID]
GO
/****** Object:  ForeignKey [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]    Script Date: 05/26/2018 12:07:48 ******/
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
/****** Object:  ForeignKey [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]    Script Date: 05/26/2018 12:07:48 ******/
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
/****** Object:  ForeignKey [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]    Script Date: 05/26/2018 12:07:48 ******/
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
/****** Object:  ForeignKey [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_User_Id]    Script Date: 05/26/2018 12:07:48 ******/
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_User_Id] FOREIGN KEY([User_Id])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_User_Id]
GO
/****** Object:  ForeignKey [FK_dbo.Categories_dbo.Categories_ParentCategory]    Script Date: 05/26/2018 12:07:48 ******/
ALTER TABLE [dbo].[Categories]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Categories_dbo.Categories_ParentCategory] FOREIGN KEY([ParentCategory])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[Categories] CHECK CONSTRAINT [FK_dbo.Categories_dbo.Categories_ParentCategory]
GO
/****** Object:  ForeignKey [FK_dbo.Categories_dbo.Suppliers_Supplier_Id]    Script Date: 05/26/2018 12:07:48 ******/
ALTER TABLE [dbo].[Categories]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Categories_dbo.Suppliers_Supplier_Id] FOREIGN KEY([Supplier_Id])
REFERENCES [dbo].[Suppliers] ([Id])
GO
ALTER TABLE [dbo].[Categories] CHECK CONSTRAINT [FK_dbo.Categories_dbo.Suppliers_Supplier_Id]
GO
/****** Object:  ForeignKey [FK_dbo.Orders_dbo.Customers_CustomerID]    Script Date: 05/26/2018 12:07:48 ******/
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Orders_dbo.Customers_CustomerID] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_dbo.Orders_dbo.Customers_CustomerID]
GO
/****** Object:  ForeignKey [FK_dbo.Orders_dbo.OrderStatus_OrderStatusID]    Script Date: 05/26/2018 12:07:48 ******/
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Orders_dbo.OrderStatus_OrderStatusID] FOREIGN KEY([OrderStatusID])
REFERENCES [dbo].[OrderStatus] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_dbo.Orders_dbo.OrderStatus_OrderStatusID]
GO
/****** Object:  ForeignKey [FK_dbo.Products_dbo.Categories_CateogryID]    Script Date: 05/26/2018 12:07:48 ******/
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Products_dbo.Categories_CateogryID] FOREIGN KEY([CateogryID])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_dbo.Products_dbo.Categories_CateogryID]
GO
/****** Object:  ForeignKey [FK_dbo.Products_dbo.Manufacturers_ManuFacturerID]    Script Date: 05/26/2018 12:07:48 ******/
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Products_dbo.Manufacturers_ManuFacturerID] FOREIGN KEY([ManuFacturerID])
REFERENCES [dbo].[Manufacturers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_dbo.Products_dbo.Manufacturers_ManuFacturerID]
GO
/****** Object:  ForeignKey [FK_dbo.Products_dbo.Suppliers_SupplierID]    Script Date: 05/26/2018 12:07:48 ******/
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Products_dbo.Suppliers_SupplierID] FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Suppliers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_dbo.Products_dbo.Suppliers_SupplierID]
GO
/****** Object:  ForeignKey [FK_dbo.Products_dbo.UnitOfMeasures_UnitOfMeasuresId]    Script Date: 05/26/2018 12:07:48 ******/
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Products_dbo.UnitOfMeasures_UnitOfMeasuresId] FOREIGN KEY([UnitOfMeasuresId])
REFERENCES [dbo].[UnitOfMeasures] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_dbo.Products_dbo.UnitOfMeasures_UnitOfMeasuresId]
GO
/****** Object:  ForeignKey [FK_dbo.WishLists_dbo.Customers_CustomerId]    Script Date: 05/26/2018 12:07:54 ******/
ALTER TABLE [dbo].[WishLists]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WishLists_dbo.Customers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WishLists] CHECK CONSTRAINT [FK_dbo.WishLists_dbo.Customers_CustomerId]
GO
/****** Object:  ForeignKey [FK_dbo.WishLists_dbo.Products_ProductId]    Script Date: 05/26/2018 12:07:54 ******/
ALTER TABLE [dbo].[WishLists]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WishLists_dbo.Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WishLists] CHECK CONSTRAINT [FK_dbo.WishLists_dbo.Products_ProductId]
GO
/****** Object:  ForeignKey [FK_dbo.ProductAttributeWithQuantities_dbo.Products_ProductId]    Script Date: 05/26/2018 12:07:54 ******/
ALTER TABLE [dbo].[ProductAttributeWithQuantities]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProductAttributeWithQuantities_dbo.Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductAttributeWithQuantities] CHECK CONSTRAINT [FK_dbo.ProductAttributeWithQuantities_dbo.Products_ProductId]
GO
/****** Object:  ForeignKey [FK_dbo.ProductAttributeWithQuantities_dbo.ProductStatus_StatusId]    Script Date: 05/26/2018 12:07:54 ******/
ALTER TABLE [dbo].[ProductAttributeWithQuantities]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProductAttributeWithQuantities_dbo.ProductStatus_StatusId] FOREIGN KEY([StatusId])
REFERENCES [dbo].[ProductStatus] ([Id])
GO
ALTER TABLE [dbo].[ProductAttributeWithQuantities] CHECK CONSTRAINT [FK_dbo.ProductAttributeWithQuantities_dbo.ProductStatus_StatusId]
GO
/****** Object:  ForeignKey [FK_dbo.ProductAttributesRelations_dbo.ProductAttributes_ProductAttributesId]    Script Date: 05/26/2018 12:07:54 ******/
ALTER TABLE [dbo].[ProductAttributesRelations]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProductAttributesRelations_dbo.ProductAttributes_ProductAttributesId] FOREIGN KEY([ProductAttributesId])
REFERENCES [dbo].[ProductAttributes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductAttributesRelations] CHECK CONSTRAINT [FK_dbo.ProductAttributesRelations_dbo.ProductAttributes_ProductAttributesId]
GO
/****** Object:  ForeignKey [FK_dbo.ProductAttributesRelations_dbo.Products_Product_Id]    Script Date: 05/26/2018 12:07:54 ******/
ALTER TABLE [dbo].[ProductAttributesRelations]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProductAttributesRelations_dbo.Products_Product_Id] FOREIGN KEY([Product_Id])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[ProductAttributesRelations] CHECK CONSTRAINT [FK_dbo.ProductAttributesRelations_dbo.Products_Product_Id]
GO
/****** Object:  ForeignKey [FK_dbo.ProductAttributesRelations_dbo.Suppliers_SupplierID]    Script Date: 05/26/2018 12:07:54 ******/
ALTER TABLE [dbo].[ProductAttributesRelations]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProductAttributesRelations_dbo.Suppliers_SupplierID] FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Suppliers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductAttributesRelations] CHECK CONSTRAINT [FK_dbo.ProductAttributesRelations_dbo.Suppliers_SupplierID]
GO
/****** Object:  ForeignKey [FK_dbo.CustomerReviews_dbo.Products_ProductId]    Script Date: 05/26/2018 12:07:54 ******/
ALTER TABLE [dbo].[CustomerReviews]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CustomerReviews_dbo.Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CustomerReviews] CHECK CONSTRAINT [FK_dbo.CustomerReviews_dbo.Products_ProductId]
GO
/****** Object:  ForeignKey [FK_dbo.OrderItems_dbo.Orders_Orders_Id]    Script Date: 05/26/2018 12:07:54 ******/
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK_dbo.OrderItems_dbo.Orders_Orders_Id] FOREIGN KEY([Orders_Id])
REFERENCES [dbo].[Orders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [FK_dbo.OrderItems_dbo.Orders_Orders_Id]
GO
/****** Object:  ForeignKey [FK_dbo.OrderItems_dbo.ProductAttributeWithQuantities_ProductID]    Script Date: 05/26/2018 12:07:54 ******/
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK_dbo.OrderItems_dbo.ProductAttributeWithQuantities_ProductID] FOREIGN KEY([ProductID])
REFERENCES [dbo].[ProductAttributeWithQuantities] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [FK_dbo.OrderItems_dbo.ProductAttributeWithQuantities_ProductID]
GO
/****** Object:  ForeignKey [FK_dbo.ProductImages_dbo.ProductAttributeWithQuantities_ProductQuantityId]    Script Date: 05/26/2018 12:07:54 ******/
ALTER TABLE [dbo].[ProductImages]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProductImages_dbo.ProductAttributeWithQuantities_ProductQuantityId] FOREIGN KEY([ProductQuantityId])
REFERENCES [dbo].[ProductAttributeWithQuantities] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductImages] CHECK CONSTRAINT [FK_dbo.ProductImages_dbo.ProductAttributeWithQuantities_ProductQuantityId]
GO
/****** Object:  ForeignKey [FK_dbo.ProductImages_dbo.Products_Product_Id]    Script Date: 05/26/2018 12:07:54 ******/
ALTER TABLE [dbo].[ProductImages]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProductImages_dbo.Products_Product_Id] FOREIGN KEY([Product_Id])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[ProductImages] CHECK CONSTRAINT [FK_dbo.ProductImages_dbo.Products_Product_Id]
GO
