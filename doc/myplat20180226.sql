USE [master]
GO
/****** Object:  Database [myplat]    Script Date: 2018/2/26 14:29:18 ******/
CREATE DATABASE [myplat]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'mycms', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.OSERVER\MSSQL\DATA\mycms.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'mycms_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.OSERVER\MSSQL\DATA\mycms_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [myplat] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [myplat].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [myplat] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [myplat] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [myplat] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [myplat] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [myplat] SET ARITHABORT OFF 
GO
ALTER DATABASE [myplat] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [myplat] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [myplat] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [myplat] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [myplat] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [myplat] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [myplat] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [myplat] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [myplat] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [myplat] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [myplat] SET  DISABLE_BROKER 
GO
ALTER DATABASE [myplat] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [myplat] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [myplat] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [myplat] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [myplat] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [myplat] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [myplat] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [myplat] SET RECOVERY FULL 
GO
ALTER DATABASE [myplat] SET  MULTI_USER 
GO
ALTER DATABASE [myplat] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [myplat] SET DB_CHAINING OFF 
GO
ALTER DATABASE [myplat] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [myplat] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'myplat', N'ON'
GO
USE [myplat]
GO
/****** Object:  Table [dbo].[Core]    Script Date: 2018/2/26 14:29:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Core](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](512) NOT NULL,
	[Intro] [nvarchar](2048) NOT NULL,
	[Cover] [nvarchar](2048) NOT NULL,
	[Imgs] [nvarchar](max) NOT NULL,
	[ShowTime] [datetime] NOT NULL,
	[Author] [nvarchar](50) NOT NULL,
	[ViewCount] [int] NOT NULL,
	[DingCount] [int] NOT NULL,
	[OriginalLink] [nvarchar](2048) NOT NULL,
	[HContent] [nvarchar](max) NOT NULL,
	[FrameLink] [nvarchar](2048) NOT NULL,
	[RedirectLink] [nvarchar](2048) NOT NULL,
	[Type] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Core] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Manager]    Script Date: 2018/2/26 14:29:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manager](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Limit] [nvarchar](2048) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Des] [nvarchar](2048) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Manager] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Core] ADD  CONSTRAINT [DF_Core_Title]  DEFAULT ('') FOR [Title]
GO
ALTER TABLE [dbo].[Core] ADD  CONSTRAINT [DF_Core_Intro]  DEFAULT ('') FOR [Intro]
GO
ALTER TABLE [dbo].[Core] ADD  CONSTRAINT [DF_Core_ImgList]  DEFAULT ('[]') FOR [Imgs]
GO
ALTER TABLE [dbo].[Core] ADD  CONSTRAINT [DF_Table_2_showtime]  DEFAULT (getdate()) FOR [ShowTime]
GO
ALTER TABLE [dbo].[Core] ADD  CONSTRAINT [DF_Core_Author]  DEFAULT ('') FOR [Author]
GO
ALTER TABLE [dbo].[Core] ADD  CONSTRAINT [DF_Table_2_viewcount]  DEFAULT ((0)) FOR [ViewCount]
GO
ALTER TABLE [dbo].[Core] ADD  CONSTRAINT [DF_Core_DingCount]  DEFAULT ((0)) FOR [DingCount]
GO
ALTER TABLE [dbo].[Core] ADD  CONSTRAINT [DF_Core_OriginalLink]  DEFAULT ('') FOR [OriginalLink]
GO
ALTER TABLE [dbo].[Core] ADD  CONSTRAINT [DF_Core_FrameLink]  DEFAULT ('') FOR [FrameLink]
GO
ALTER TABLE [dbo].[Core] ADD  CONSTRAINT [DF_Core_RedirectLink]  DEFAULT ('') FOR [RedirectLink]
GO
ALTER TABLE [dbo].[Core] ADD  CONSTRAINT [DF_Core_Type]  DEFAULT ((1)) FOR [Type]
GO
ALTER TABLE [dbo].[Core] ADD  CONSTRAINT [DF_Core_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Core] ADD  CONSTRAINT [DF_Core_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[Manager] ADD  CONSTRAINT [DF_Manager_Limit]  DEFAULT ('[]') FOR [Limit]
GO
ALTER TABLE [dbo].[Manager] ADD  CONSTRAINT [DF_Manager_Des]  DEFAULT ('') FOR [Des]
GO
ALTER TABLE [dbo].[Manager] ADD  CONSTRAINT [DF_Manager_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'内部访问链接' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Core', @level2type=N'COLUMN',@level2name=N'FrameLink'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'跳转链接' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Core', @level2type=N'COLUMN',@level2name=N'RedirectLink'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据类型，默认1，普通类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Core', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态：1正常， 2已删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Core', @level2type=N'COLUMN',@level2name=N'Status'
GO
USE [master]
GO
ALTER DATABASE [myplat] SET  READ_WRITE 
GO
