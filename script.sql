USE [master]
GO
/****** Object:  Database [UserBase]    Script Date: 21.05.2016 18:32:26 ******/
CREATE DATABASE [UserBase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'UserBase', FILENAME = N'C:\Temp\UserBase.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'UserBase_log', FILENAME = N'C:\Temp\UserBase_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [UserBase] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [UserBase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [UserBase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [UserBase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [UserBase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [UserBase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [UserBase] SET ARITHABORT OFF 
GO
ALTER DATABASE [UserBase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [UserBase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [UserBase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [UserBase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [UserBase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [UserBase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [UserBase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [UserBase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [UserBase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [UserBase] SET  DISABLE_BROKER 
GO
ALTER DATABASE [UserBase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [UserBase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [UserBase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [UserBase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [UserBase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [UserBase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [UserBase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [UserBase] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [UserBase] SET  MULTI_USER 
GO
ALTER DATABASE [UserBase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [UserBase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [UserBase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [UserBase] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [UserBase] SET DELAYED_DURABILITY = DISABLED 
GO
USE [UserBase]
GO
/****** Object:  Table [dbo].[AccountRoles]    Script Date: 21.05.2016 18:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountRoles](
	[id] [int] NOT NULL,
	[rolename] [nchar](20) NOT NULL,
 CONSTRAINT [PK_AccountRoles] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 21.05.2016 18:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Accounts](
	[login] [varchar](40) NOT NULL,
	[passhash] [nchar](40) NOT NULL,
	[roleId] [int] NOT NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Awards]    Script Date: 21.05.2016 18:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Awards](
	[id] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[imageId] [int] NULL,
 CONSTRAINT [PK_Awards] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Images]    Script Date: 21.05.2016 18:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Images](
	[id] [int] NOT NULL,
	[content] [varbinary](max) NOT NULL,
	[contentType] [varchar](255) NOT NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserAwards]    Script Date: 21.05.2016 18:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAwards](
	[userid] [int] NULL,
	[awardid] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 21.05.2016 18:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[id] [int] NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[birthdate] [date] NOT NULL,
	[imageId] [int] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_AccountRoles] FOREIGN KEY([roleId])
REFERENCES [dbo].[AccountRoles] ([id])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_AccountRoles]
GO
ALTER TABLE [dbo].[Awards]  WITH CHECK ADD  CONSTRAINT [FK_Awards_Images] FOREIGN KEY([imageId])
REFERENCES [dbo].[Images] ([id])
GO
ALTER TABLE [dbo].[Awards] CHECK CONSTRAINT [FK_Awards_Images]
GO
ALTER TABLE [dbo].[UserAwards]  WITH CHECK ADD  CONSTRAINT [FK_UserAwards_Awards] FOREIGN KEY([awardid])
REFERENCES [dbo].[Awards] ([id])
GO
ALTER TABLE [dbo].[UserAwards] CHECK CONSTRAINT [FK_UserAwards_Awards]
GO
ALTER TABLE [dbo].[UserAwards]  WITH CHECK ADD  CONSTRAINT [FK_UserAwards_Users] FOREIGN KEY([userid])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[UserAwards] CHECK CONSTRAINT [FK_UserAwards_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Images] FOREIGN KEY([imageId])
REFERENCES [dbo].[Images] ([id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Images]
GO
/****** Object:  StoredProcedure [dbo].[Account_Add]    Script Date: 21.05.2016 18:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Account_Add]
	@Login varchar(40),
	@Passhash nchar(40),
	@RoleName nchar(40)
AS
BEGIN
	--SET NOCOUNT ON;
	INSERT INTO dbo.Accounts(login, passhash, roleId)
	SELECT TOP 1 @Login, @Passhash, id FROM AccountRoles WHERE rolename = @RoleName
END
GO
/****** Object:  StoredProcedure [dbo].[Account_AddRole]    Script Date: 21.05.2016 18:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Account_AddRole]
	@Id int,
	@Rolename nchar(40)
AS
BEGIN
	--SET NOCOUNT ON;
	INSERT INTO dbo.AccountRoles(id, rolename)
	VALUES (@Id, @Rolename)
END

GO
/****** Object:  StoredProcedure [dbo].[Account_ChangeRole]    Script Date: 21.05.2016 18:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Account_ChangeRole]
	@Login varchar(40),
	@RoleName nchar(40)
AS
BEGIN
	--SET NOCOUNT ON;
	UPDATE dbo.Accounts
	SET login = @Login, roleId = id FROM AccountRoles WHERE rolename = @RoleName
END

GO
/****** Object:  StoredProcedure [dbo].[Account_Delete]    Script Date: 21.05.2016 18:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Account_Delete]
	-- Add the parameters for the stored procedure here
	@Login varchar(40)
AS
BEGIN
	--SET NOCOUNT ON;
	DELETE
	FROM dbo.Accounts
	WHERE login = @Login
END

GO
/****** Object:  StoredProcedure [dbo].[Account_Edit]    Script Date: 21.05.2016 18:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Account_Edit]
	@Login varchar(40),
	@Passhash nchar(40),
	@RoleName nchar(40)
AS
BEGIN
	--SET NOCOUNT ON;
	UPDATE dbo.Accounts
	SET login = @Login, passhash = @Passhash, roleId = id FROM AccountRoles WHERE rolename = @RoleName
END

GO
/****** Object:  StoredProcedure [dbo].[Account_Get]    Script Date: 21.05.2016 18:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Account_Get]
@Login varchar(40)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT dbo.Accounts.login, dbo.Accounts.passhash, dbo.AccountRoles.rolename
	FROM dbo.Accounts
	JOIN dbo.AccountRoles
	ON id = roleId
	WHERE login = @Login
END

GO
/****** Object:  StoredProcedure [dbo].[Account_GetAll]    Script Date: 21.05.2016 18:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Account_GetAll]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT dbo.Accounts.login, dbo.Accounts.passhash, dbo.AccountRoles.rolename
	FROM dbo.Accounts
	JOIN dbo.AccountRoles
	ON id = roleId
END

GO
/****** Object:  StoredProcedure [dbo].[Account_GetAllRoles]    Script Date: 21.05.2016 18:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Account_GetAllRoles]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT DISTINCT rolename as r
	FROM dbo.AccountRoles
END

GO
/****** Object:  StoredProcedure [dbo].[Award_Add]    Script Date: 21.05.2016 18:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Award_Add]
	-- Add the parameters for the stored procedure here
	@Id int,
	@Name nvarchar(50),
	@ImageId int
AS
BEGIN
	--SET NOCOUNT ON;
	INSERT INTO dbo.Awards (id, name, imageId) VALUES (@Id, @Name, @ImageId)
END

GO
/****** Object:  StoredProcedure [dbo].[Award_AddImage]    Script Date: 21.05.2016 18:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Award_AddImage]
	@Id int,
	@ImageId int
AS
BEGIN
	UPDATE dbo.Awards
	SET imageId = @ImageId
	WHERE id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[Award_Delete]    Script Date: 21.05.2016 18:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Award_Delete]
	-- Add the parameters for the stored procedure here
	@Id int
AS
BEGIN
	--SET NOCOUNT ON;
	DELETE
	FROM dbo.Awards
	WHERE id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[Award_Edit]    Script Date: 21.05.2016 18:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Award_Edit]
	@Id int,
	@Name nvarchar(50),
	@ImageId int
AS
BEGIN
	--SET NOCOUNT ON;
	UPDATE dbo.Awards
	SET name = @Name, imageId = @ImageId
	WHERE id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[Award_Get]    Script Date: 21.05.2016 18:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Award_Get]
	-- Add the parameters for the stored procedure here
	@Id int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT *
	FROM dbo.Awards AS a
	WHERE a.id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[Award_GetAll]    Script Date: 21.05.2016 18:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Award_GetAll]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT *
	FROM dbo.Awards
END
GO
/****** Object:  StoredProcedure [dbo].[Image_Add]    Script Date: 21.05.2016 18:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Image_Add]
	@Id int,
	@Content varbinary(max),
	@ContentType varchar(255)
AS
BEGIN
	--SET NOCOUNT ON;
	INSERT INTO dbo.Images(id, [content], contentType)
	VALUES (@Id, @Content, @ContentType)
END

GO
/****** Object:  StoredProcedure [dbo].[Image_Delete]    Script Date: 21.05.2016 18:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Image_Delete]
	-- Add the parameters for the stored procedure here
	@Id int
AS
BEGIN
	--SET NOCOUNT ON;
	DELETE
	FROM dbo.Images
	WHERE id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[Image_Get]    Script Date: 21.05.2016 18:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Image_Get]
@Id int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT *
	FROM dbo.Images as i
	WHERE i.id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[Image_GetAll]    Script Date: 21.05.2016 18:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Image_GetAll]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT *
	FROM dbo.Images
END

GO
/****** Object:  StoredProcedure [dbo].[User_Add]    Script Date: 21.05.2016 18:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[User_Add]
	@Id int,
	@Username nvarchar(50),
	@Birthdate date,
	@ImageId int
AS
BEGIN
	--SET NOCOUNT ON;
	INSERT INTO dbo.Users(id, username, birthdate, imageId)
	VALUES (@Id, @Username, @Birthdate, @ImageId)
END

GO
/****** Object:  StoredProcedure [dbo].[User_AddImage]    Script Date: 21.05.2016 18:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[User_AddImage]
	@UserId int,
	@ImageId int
AS
BEGIN
	UPDATE dbo.Users
	SET imageId = @ImageId
	WHERE id = @UserId
END

GO
/****** Object:  StoredProcedure [dbo].[User_Delete]    Script Date: 21.05.2016 18:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[User_Delete]
	-- Add the parameters for the stored procedure here
	@Id int
AS
BEGIN
	--SET NOCOUNT ON;
	DELETE
	FROM dbo.Users
	WHERE id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[User_Edit]    Script Date: 21.05.2016 18:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[User_Edit]
	@Id int,
	@Username nvarchar(50),
	@Birthdate date,
	@ImageId int
AS
BEGIN
	--SET NOCOUNT ON;
	UPDATE dbo.Users
	SET username = @Username, birthdate = @Birthdate, imageId = @ImageId
	WHERE id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[User_Get]    Script Date: 21.05.2016 18:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[User_Get]
@Id int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT dbo.Users.id, dbo.Users.username, dbo.Users.birthdate, dbo.Users.imageId
	FROM dbo.Users
	WHERE id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[User_GetAll]    Script Date: 21.05.2016 18:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[User_GetAll]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT dbo.Users.id, dbo.Users.username, dbo.Users.birthdate, dbo.Users.imageId
	FROM dbo.Users
END

GO
/****** Object:  StoredProcedure [dbo].[User_GetAwards]    Script Date: 21.05.2016 18:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[User_GetAwards]
@Id int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ua.awardid
	FROM dbo.Users u
	JOIN dbo.UserAwards ua
	on u.id = ua.userid
	where u.id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[User_GrantAward]    Script Date: 21.05.2016 18:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[User_GrantAward]
	@UserId int,
	@AwardId int
AS
BEGIN
	INSERT INTO dbo.UserAwards(userid, awardid)
	VALUES (@UserId, @AwardId)
END

GO
/****** Object:  StoredProcedure [dbo].[User_RescindAward]    Script Date: 21.05.2016 18:32:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[User_RescindAward]
	@UserId int,
	@AwardId int
AS
BEGIN
	DELETE 
	FROM dbo.UserAwards
	WHERE userid = @UserId AND awardid = @AwardId
END

GO
USE [master]
GO
ALTER DATABASE [UserBase] SET  READ_WRITE 
GO
