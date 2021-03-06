/****** Object:  Database [CD_Test]    Script Date: 6/5/2017 5:22:05 PM ******/
CREATE DATABASE [CD_Test]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CD_Test', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\CD_Test.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CD_Test_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\CD_Test_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [CD_Test] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CD_Test].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CD_Test] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CD_Test] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CD_Test] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CD_Test] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CD_Test] SET ARITHABORT OFF 
GO
ALTER DATABASE [CD_Test] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CD_Test] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CD_Test] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CD_Test] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CD_Test] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CD_Test] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CD_Test] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CD_Test] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CD_Test] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CD_Test] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CD_Test] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CD_Test] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CD_Test] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CD_Test] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CD_Test] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CD_Test] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CD_Test] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CD_Test] SET RECOVERY FULL 
GO
ALTER DATABASE [CD_Test] SET  MULTI_USER 
GO
ALTER DATABASE [CD_Test] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CD_Test] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CD_Test] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CD_Test] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CD_Test] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'CD_Test', N'ON'
GO
ALTER DATABASE [CD_Test] SET QUERY_STORE = OFF
GO
USE [CD_Test]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 6/5/2017 5:22:06 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 6/5/2017 5:22:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 6/5/2017 5:22:06 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 6/5/2017 5:22:06 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 6/5/2017 5:22:06 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 6/5/2017 5:22:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
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
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 6/5/2017 5:22:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[UserId] [nvarchar](128) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
	[Address1] [nvarchar](50) NULL,
	[Address2] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](2) NULL,
	[PostalCode] [nvarchar](10) NULL,
	[PhoneNumber] [nvarchar](20) NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Offer]    Script Date: 6/5/2017 5:22:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Offer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[CompanyId] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Url] [nvarchar](200) NOT NULL,
	[Category] [nvarchar](30) NULL,
	[Token] [nvarchar](128) NULL,
 CONSTRAINT [PK_Offer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 6/5/2017 5:22:06 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 6/5/2017 5:22:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 6/5/2017 5:22:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RoleId]    Script Date: 6/5/2017 5:22:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 6/5/2017 5:22:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 6/5/2017 5:22:06 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Company_UserId]    Script Date: 6/5/2017 5:22:06 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Company_UserId] ON [dbo].[Company]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Offer] ADD  CONSTRAINT [DF_Offer_Token]  DEFAULT (newid()) FOR [Token]
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
/****** Object:  StoredProcedure [dbo].[Company_Add]    Script Date: 6/5/2017 5:22:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Company_Add]
@Id INT OUTPUT,
@Name NVARCHAR(50),
@UserId NVARCHAR(128) NULL,
@Address1 NVARCHAR(50) NULL,
@Address2 NVARCHAR(50) NULL,
@City NVARCHAR(50) NULL,
@State NVARCHAR(2) NULL,
@PostalCode NVARCHAR(10) NULL,
@PhoneNumber NVARCHAR(20) NULL

AS

SET NOCOUNT ON;

INSERT INTO Company 
(Name,UserId ,CreatedOn, Address1, Address2, City, State, PostalCode, PhoneNumber)
VALUES 
(
@Name, 
@UserId,
GETDATE(),
@Address1,
@Address2,
@City,
@State,
@PostalCode,
@PhoneNumber

 )

SET @Id = @@IDENTITY

RETURN
GO
/****** Object:  StoredProcedure [dbo].[Company_Delete]    Script Date: 6/5/2017 5:22:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Company_Delete]
@Id INT 

AS

SET NOCOUNT ON;
DELETE Company 
WHERE ID = @ID


RETURN
GO
/****** Object:  StoredProcedure [dbo].[Company_Get]    Script Date: 6/5/2017 5:22:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Company_Get]
@Id INT 

AS

SET NOCOUNT ON;
SELECT * FROM Company 
WHERE ID = @ID


RETURN
GO
/****** Object:  StoredProcedure [dbo].[Company_GetAll]    Script Date: 6/5/2017 5:22:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Company_GetAll]


AS

SET NOCOUNT ON;
SELECT * FROM Company 



RETURN
GO
/****** Object:  StoredProcedure [dbo].[Company_GetByUserId]    Script Date: 6/5/2017 5:22:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Company_GetByUserId]
@UserId nvarchar(128) 

AS

SET NOCOUNT ON;
SELECT * FROM Company 
WHERE UserId = @UserId


RETURN
GO
/****** Object:  StoredProcedure [dbo].[Company_Update]    Script Date: 6/5/2017 5:22:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Company_Update]
@Id INT ,
@Name NVARCHAR(50),
@UserId NVARCHAR(128) NULL,
@Address1 NVARCHAR(50) NULL,
@Address2 NVARCHAR(50) NULL,
@City NVARCHAR(50) NULL,
@State NVARCHAR(2) NULL,
@PostalCode NVARCHAR(10) NULL,
@PhoneNumber NVARCHAR(20) NULL

AS

SET NOCOUNT ON;
UPDATE Company
SET
	Name = @Name,
	UserId = @UserId,
	UpdatedOn = GETDATE(),
	Address1 = @Address1,
	Address2 = @Address2,
	City = @City,
	State = @State,
	PostalCode = @PostalCode,
	PhoneNumber = @PhoneNumber
WHERE Id = @Id


RETURN
GO
/****** Object:  StoredProcedure [dbo].[Offer_Add]    Script Date: 6/5/2017 5:22:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Offer_Add]
@Id INT OUTPUT,
@Title NVARCHAR(100),
@CompanyId INT OUTPUT,
@Description NVARCHAR(MAX) NULL,
@Url NVARCHAR(200) NULL,
@Category NVARCHAR(128) NULL

AS

SET NOCOUNT ON;

INSERT INTO Offer 
(Title ,CreatedOn, CompanyId, Description, Url, Category)
VALUES 
(
@Title,
GETDATE(),
@CompanyId,
@Description,
@Url,
@Category
 )

SET @Id = @@IDENTITY

RETURN
GO
/****** Object:  StoredProcedure [dbo].[Offer_Delete]    Script Date: 6/5/2017 5:22:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Offer_Delete]
@Id INT 

AS

SET NOCOUNT ON;
DELETE Offer
WHERE ID = @ID


RETURN
GO
/****** Object:  StoredProcedure [dbo].[Offer_Get]    Script Date: 6/5/2017 5:22:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Offer_Get]
@Id INT 

AS

SET NOCOUNT ON;
SELECT * FROM Offer
WHERE ID = @ID


RETURN
GO
/****** Object:  StoredProcedure [dbo].[Offer_GetAll]    Script Date: 6/5/2017 5:22:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Offer_GetAll]
@CompanyId INT

AS

SET NOCOUNT ON;
SELECT * FROM Offer
WHERE CompanyId = @CompanyId




RETURN
GO
/****** Object:  StoredProcedure [dbo].[Offer_GetByCompanyId]    Script Date: 6/5/2017 5:22:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Offer_GetByCompanyId]
@CompanyId INT 

AS

SET NOCOUNT ON;
SELECT * FROM Offer 
WHERE CompanyId = @CompanyId


RETURN
GO
/****** Object:  StoredProcedure [dbo].[Offer_Update]    Script Date: 6/5/2017 5:22:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Offer_Update]
@Id INT,
@Title NVARCHAR(100),
@Description NVARCHAR(MAX) NULL,
@Url NVARCHAR(200) NULL,
@Category NVARCHAR(128) NULL

AS

SET NOCOUNT ON;
UPDATE Offer
SET
	Title = @Title,
	UpdatedOn = GETDATE(),
	Description = @Description,
	Url = @Url,
	Category = @Category
WHERE Id = @Id


RETURN
GO
ALTER DATABASE [CD_Test] SET  READ_WRITE 
GO
