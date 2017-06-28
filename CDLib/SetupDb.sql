/****** To Generate: Take an up to date Db, remove _Migration and AspNet* tables, 
Tasks | Generate Scripts .. | Select Specific Database Objects, Select All | Next | Save to Query Window | Advanced | 
Script USE DATABASE = False | OK | Next | Next ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UserId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
	[Address1] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Address2] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[City] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[State] [nvarchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PostalCode] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PhoneNumber] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[V_CompanyEmail]    Script Date: 6/27/2017 2:01:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_CompanyEmail]
AS
SELECT        dbo.Company.Id, dbo.AspNetUsers.Email
FROM            dbo.Company INNER JOIN
                         dbo.AspNetUsers ON dbo.Company.UserId = dbo.AspNetUsers.Id

GO
/****** Object:  Table [dbo].[Offer]    Script Date: 6/27/2017 2:01:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Offer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[CompanyId] [int] NOT NULL,
	[Description] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Url] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Category] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Token] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Offer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OfferCode]    Script Date: 6/27/2017 2:01:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OfferCode](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OfferId] [int] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[Code] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ClaimingUser] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ClaimedOn] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Company_UserId]    Script Date: 6/27/2017 2:01:04 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Company_UserId] ON [dbo].[Company]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Offer] ADD  CONSTRAINT [DF_Offer_Token]  DEFAULT (newid()) FOR [Token]
GO
/****** Object:  StoredProcedure [dbo].[Company_Add]    Script Date: 6/27/2017 2:01:04 PM ******/
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
/****** Object:  StoredProcedure [dbo].[Company_Delete]    Script Date: 6/27/2017 2:01:04 PM ******/
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
/****** Object:  StoredProcedure [dbo].[Company_Get]    Script Date: 6/27/2017 2:01:04 PM ******/
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
/****** Object:  StoredProcedure [dbo].[Company_GetAll]    Script Date: 6/27/2017 2:01:04 PM ******/
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
/****** Object:  StoredProcedure [dbo].[Company_GetByUserId]    Script Date: 6/27/2017 2:01:04 PM ******/
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
/****** Object:  StoredProcedure [dbo].[Company_Update]    Script Date: 6/27/2017 2:01:04 PM ******/
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
/****** Object:  StoredProcedure [dbo].[Offer_Add]    Script Date: 6/27/2017 2:01:04 PM ******/
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
/****** Object:  StoredProcedure [dbo].[Offer_ClaimNextCode]    Script Date: 6/27/2017 2:01:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Offer_ClaimNextCode]
@OfferId INT,
@UserId NVARCHAR(128)

AS

SET NOCOUNT ON;
DECLARE @Id INT
DECLARE @Code NVARCHAR(30)
BEGIN TRANSACTION
SELECT        TOP (1) @Id = Id, @Code = Code
FROM            OfferCode
WHERE        (ClaimingUser IS NULL) AND (OfferId = @OfferId)
ORDER BY Id

UPDATE OfferCode
SET ClaimingUser = @UserId,
ClaimedOn = GETDATE()
WHERE Id = @Id
COMMIT TRANSACTION
SELECT @Code as 'Code'
RETURN

GO
/****** Object:  StoredProcedure [dbo].[Offer_Delete]    Script Date: 6/27/2017 2:01:04 PM ******/
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
/****** Object:  StoredProcedure [dbo].[Offer_Get]    Script Date: 6/27/2017 2:01:04 PM ******/
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
/****** Object:  StoredProcedure [dbo].[Offer_GetAll]    Script Date: 6/27/2017 2:01:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Offer_GetAll]
@CompanyId INT

AS

SET NOCOUNT ON;
SELECT        Offer.Id, Offer.Title, Offer.CreatedOn, Offer.UpdatedOn, Offer.CompanyId, Offer.Description, Offer.Url, Offer.Category, Offer.Token, drvCodeTotals.TotalCodes, drvCodeTotals.TotalClaimed
FROM            Offer LEFT OUTER JOIN
                             (SELECT        drvAllCodes.OfferId, drvAllCodes.TotalCodes, drvClaimedCodes.TotalClaimed
                               FROM            (SELECT        OfferId, COUNT(Id) AS TotalClaimed
                                                         FROM            (SELECT        OfferId, Id
                                                                                   FROM            OfferCode AS OfferCode_1
                                                                                   WHERE        (ClaimingUser IS NOT NULL)) AS drvClaimedCodes_1
                                                         GROUP BY OfferId) AS drvClaimedCodes RIGHT OUTER JOIN
                                                             (SELECT        OfferId, COUNT(Id) AS TotalCodes
                                                               FROM            OfferCode
                                                               GROUP BY OfferId) AS drvAllCodes ON drvClaimedCodes.OfferId = drvAllCodes.OfferId) AS drvCodeTotals ON Offer.Id = drvCodeTotals.OfferId
WHERE CompanyId = @CompanyId




RETURN

GO
/****** Object:  StoredProcedure [dbo].[Offer_GetByCompanyId]    Script Date: 6/27/2017 2:01:04 PM ******/
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
/****** Object:  StoredProcedure [dbo].[Offer_GetByToken]    Script Date: 6/27/2017 2:01:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Offer_GetByToken]
@Token NVARCHAR(128) 

AS

SET NOCOUNT ON;
SELECT * FROM Offer 
WHERE Token = @Token

RETURN

GO
/****** Object:  StoredProcedure [dbo].[Offer_Update]    Script Date: 6/27/2017 2:01:04 PM ******/
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
/****** Object:  StoredProcedure [dbo].[OfferCode_Add]    Script Date: 6/27/2017 2:01:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[OfferCode_Add]
@Id INT OUTPUT,
@OfferId INT,
@Code NVARCHAR(30),
@ClaimingUser NVARCHAR(128) NULL

AS

SET NOCOUNT ON;

INSERT INTO OfferCode
(OfferId, CreatedOn, Code, ClaimingUser)
VALUES 
(
@OfferId,
GETDATE(),
@Code,
@ClaimingUser
 )

SET @Id = @@IDENTITY

RETURN

GO
/****** Object:  StoredProcedure [dbo].[OfferCode_Delete]    Script Date: 6/27/2017 2:01:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[OfferCode_Delete]
@Id INT 

AS

SET NOCOUNT ON;
DELETE OfferCode
WHERE ID = @ID


RETURN

GO
/****** Object:  StoredProcedure [dbo].[OfferCode_Get]    Script Date: 6/27/2017 2:01:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[OfferCode_Get]
@Id INT 

AS

SET NOCOUNT ON;
SELECT * FROM OfferCode
WHERE ID = @ID


RETURN

GO
/****** Object:  StoredProcedure [dbo].[OfferCode_GetAll]    Script Date: 6/27/2017 2:01:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[OfferCode_GetAll]
@OfferId INT

AS

SET NOCOUNT ON;
SELECT        dbo.OfferCode.Id, dbo.OfferCode.OfferId, dbo.OfferCode.CreatedOn, dbo.OfferCode.UpdatedOn,
 dbo.OfferCode.Code, dbo.OfferCode.ClaimedOn, 
                         dbo.AspNetUsers.Email AS BuyerEmail
FROM            dbo.OfferCode LEFT OUTER JOIN
                         dbo.AspNetUsers ON dbo.OfferCode.ClaimingUser = dbo.AspNetUsers.Id

WHERE OfferId = @OfferId




RETURN

GO
/****** Object:  StoredProcedure [dbo].[OfferCode_Update]    Script Date: 6/27/2017 2:01:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[OfferCode_Update]
@Id INT,
@Code NVARCHAR(30) NULL

AS

SET NOCOUNT ON;
UPDATE OfferCode
SET
	Code = @Code,
	UpdatedOn = GETDATE(),
	ClaimedOn = GETDATE()
	
WHERE Id = @Id


RETURN

GO
