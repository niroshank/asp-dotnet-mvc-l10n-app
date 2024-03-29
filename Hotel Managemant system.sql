USE [master]
GO
/****** Object:  Database [hotelmanagement]    Script Date: 11/28/2016 5:48:51 AM ******/
CREATE DATABASE [hotelmanagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CCCC', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\CCCC.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'CCCC_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\CCCC_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [hotelmanagement] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [hotelmanagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [hotelmanagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [hotelmanagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [hotelmanagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [hotelmanagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [hotelmanagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [hotelmanagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [hotelmanagement] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [hotelmanagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [hotelmanagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [hotelmanagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [hotelmanagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [hotelmanagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [hotelmanagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [hotelmanagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [hotelmanagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [hotelmanagement] SET  DISABLE_BROKER 
GO
ALTER DATABASE [hotelmanagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [hotelmanagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [hotelmanagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [hotelmanagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [hotelmanagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [hotelmanagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [hotelmanagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [hotelmanagement] SET RECOVERY FULL 
GO
ALTER DATABASE [hotelmanagement] SET  MULTI_USER 
GO
ALTER DATABASE [hotelmanagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [hotelmanagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [hotelmanagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [hotelmanagement] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'hotelmanagement', N'ON'
GO
USE [hotelmanagement]
GO
/****** Object:  Table [dbo].[feedbacks]    Script Date: 11/28/2016 5:48:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[feedbacks](
	[feedbackid] [int] IDENTITY(1,1) NOT NULL,
	[email] [varchar](50) NULL,
	[message] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[login]    Script Date: 11/28/2016 5:48:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[login](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[activation] [int] NOT NULL,
 CONSTRAINT [PK_login] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[reservation]    Script Date: 11/28/2016 5:48:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[reservation](
	[reservationid] [int] IDENTITY(1,1) NOT NULL,
	[firstname] [varchar](max) NOT NULL,
	[lastname] [varchar](max) NOT NULL,
	[identificationtype] [varchar](50) NOT NULL,
	[identifynumber] [varchar](50) NOT NULL,
	[addresses] [varchar](max) NOT NULL,
	[telephone] [varchar](50) NOT NULL,
	[email] [varchar](max) NULL,
	[creditcardno] [varchar](50) NOT NULL,
	[checkin] [date] NOT NULL,
	[checkout] [date] NOT NULL,
	[roomid] [int] NOT NULL,
	[username] [varchar](50) NULL,
 CONSTRAINT [PK_reservation] PRIMARY KEY CLUSTERED 
(
	[reservationid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rooms]    Script Date: 11/28/2016 5:48:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rooms](
	[roomid] [int] IDENTITY(1,1) NOT NULL,
	[roomno] [int] NOT NULL,
	[roomtype] [varchar](50) NULL,
 CONSTRAINT [PK_rooms] PRIMARY KEY CLUSTERED 
(
	[roomid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[login] ADD  CONSTRAINT [DF_login_activation]  DEFAULT ((0)) FOR [activation]
GO
ALTER TABLE [dbo].[rooms] ADD  CONSTRAINT [DF_rooms_roomtype]  DEFAULT ('single') FOR [roomtype]
GO
USE [master]
GO
ALTER DATABASE [hotelmanagement] SET  READ_WRITE 
GO
