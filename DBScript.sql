USE [master]
GO
/****** Object:  Database [db_a96e41_parkinglot]    Script Date: 8/9/2023 9:25:35 PM ******/
CREATE DATABASE [db_a96e41_parkinglot]

GO
ALTER DATABASE [db_a96e41_parkinglot] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [db_a96e41_parkinglot].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [db_a96e41_parkinglot] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [db_a96e41_parkinglot] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [db_a96e41_parkinglot] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [db_a96e41_parkinglot] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [db_a96e41_parkinglot] SET ARITHABORT OFF 
GO
ALTER DATABASE [db_a96e41_parkinglot] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [db_a96e41_parkinglot] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [db_a96e41_parkinglot] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [db_a96e41_parkinglot] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [db_a96e41_parkinglot] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [db_a96e41_parkinglot] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [db_a96e41_parkinglot] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [db_a96e41_parkinglot] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [db_a96e41_parkinglot] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [db_a96e41_parkinglot] SET  ENABLE_BROKER 
GO
ALTER DATABASE [db_a96e41_parkinglot] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [db_a96e41_parkinglot] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [db_a96e41_parkinglot] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [db_a96e41_parkinglot] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [db_a96e41_parkinglot] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [db_a96e41_parkinglot] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [db_a96e41_parkinglot] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [db_a96e41_parkinglot] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [db_a96e41_parkinglot] SET  MULTI_USER 
GO
ALTER DATABASE [db_a96e41_parkinglot] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [db_a96e41_parkinglot] SET DB_CHAINING OFF 
GO
ALTER DATABASE [db_a96e41_parkinglot] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [db_a96e41_parkinglot] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [db_a96e41_parkinglot] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [db_a96e41_parkinglot] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [db_a96e41_parkinglot] SET QUERY_STORE = OFF
GO
USE [db_a96e41_parkinglot]
GO
/****** Object:  Table [dbo].[ParkingLots]    Script Date: 8/9/2023 9:25:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ParkingLots](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NULL,
	[Address] [nvarchar](150) NULL,
	[TotalSpaces] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ParkingLotSpaces]    Script Date: 8/9/2023 9:25:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ParkingLotSpaces](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ParkingLotId] [int] NULL,
	[SpaceNumber] [nvarchar](50) NULL,
	[Occupied] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ParkingLotTransactions]    Script Date: 8/9/2023 9:25:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ParkingLotTransactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CheckinTime] [datetime] NULL,
	[CheckoutTime] [datetime] NULL,
	[TagNumber] [nvarchar](50) NULL,
	[ParkingLotSpaceId] [int] NULL,
	[AmountPaid] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ParkingLots] ON 

INSERT [dbo].[ParkingLots] ([Id], [Name], [Address], [TotalSpaces]) VALUES (1, N'Main branch', N'123 Main st', 15)
SET IDENTITY_INSERT [dbo].[ParkingLots] OFF
GO
SET IDENTITY_INSERT [dbo].[ParkingLotSpaces] ON 

INSERT [dbo].[ParkingLotSpaces] ([Id], [ParkingLotId], [SpaceNumber], [Occupied]) VALUES (1, 1, N'A1', 0)
INSERT [dbo].[ParkingLotSpaces] ([Id], [ParkingLotId], [SpaceNumber], [Occupied]) VALUES (2, 1, N'A2', 0)
INSERT [dbo].[ParkingLotSpaces] ([Id], [ParkingLotId], [SpaceNumber], [Occupied]) VALUES (3, 1, N'A3', 0)
INSERT [dbo].[ParkingLotSpaces] ([Id], [ParkingLotId], [SpaceNumber], [Occupied]) VALUES (4, 1, N'A4', 0)
INSERT [dbo].[ParkingLotSpaces] ([Id], [ParkingLotId], [SpaceNumber], [Occupied]) VALUES (5, 1, N'A5', 0)
INSERT [dbo].[ParkingLotSpaces] ([Id], [ParkingLotId], [SpaceNumber], [Occupied]) VALUES (6, 1, N'B1', 0)
INSERT [dbo].[ParkingLotSpaces] ([Id], [ParkingLotId], [SpaceNumber], [Occupied]) VALUES (7, 1, N'B2', 0)
INSERT [dbo].[ParkingLotSpaces] ([Id], [ParkingLotId], [SpaceNumber], [Occupied]) VALUES (8, 1, N'B3', 0)
INSERT [dbo].[ParkingLotSpaces] ([Id], [ParkingLotId], [SpaceNumber], [Occupied]) VALUES (9, 1, N'B4', 0)
INSERT [dbo].[ParkingLotSpaces] ([Id], [ParkingLotId], [SpaceNumber], [Occupied]) VALUES (10, 1, N'B5', 0)
INSERT [dbo].[ParkingLotSpaces] ([Id], [ParkingLotId], [SpaceNumber], [Occupied]) VALUES (11, 1, N'C1', 0)
INSERT [dbo].[ParkingLotSpaces] ([Id], [ParkingLotId], [SpaceNumber], [Occupied]) VALUES (12, 1, N'C2', 0)
INSERT [dbo].[ParkingLotSpaces] ([Id], [ParkingLotId], [SpaceNumber], [Occupied]) VALUES (13, 1, N'C3', 0)
INSERT [dbo].[ParkingLotSpaces] ([Id], [ParkingLotId], [SpaceNumber], [Occupied]) VALUES (14, 1, N'C4', 0)
INSERT [dbo].[ParkingLotSpaces] ([Id], [ParkingLotId], [SpaceNumber], [Occupied]) VALUES (15, 1, N'C5', 0)
SET IDENTITY_INSERT [dbo].[ParkingLotSpaces] OFF
GO
ALTER TABLE [dbo].[ParkingLots] ADD  DEFAULT ((0)) FOR [TotalSpaces]
GO
ALTER TABLE [dbo].[ParkingLotSpaces] ADD  DEFAULT ((0)) FOR [Occupied]
GO
ALTER TABLE [dbo].[ParkingLotTransactions] ADD  DEFAULT ((0)) FOR [AmountPaid]
GO
USE [master]
GO
ALTER DATABASE [db_a96e41_parkinglot] SET  READ_WRITE 
GO
