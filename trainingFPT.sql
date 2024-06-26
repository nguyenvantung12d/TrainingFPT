USE [master]
GO
/****** Object:  Database [TrainingFPT]    Script Date: 09/04/2024 2:39:50 CH ******/
CREATE DATABASE [TrainingFPT]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TrainingFPT', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\TrainingFPT.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TrainingFPT_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\TrainingFPT_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [TrainingFPT] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TrainingFPT].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TrainingFPT] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TrainingFPT] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TrainingFPT] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TrainingFPT] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TrainingFPT] SET ARITHABORT OFF 
GO
ALTER DATABASE [TrainingFPT] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [TrainingFPT] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TrainingFPT] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TrainingFPT] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TrainingFPT] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TrainingFPT] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TrainingFPT] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TrainingFPT] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TrainingFPT] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TrainingFPT] SET  ENABLE_BROKER 
GO
ALTER DATABASE [TrainingFPT] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TrainingFPT] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TrainingFPT] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TrainingFPT] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TrainingFPT] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TrainingFPT] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TrainingFPT] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TrainingFPT] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TrainingFPT] SET  MULTI_USER 
GO
ALTER DATABASE [TrainingFPT] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TrainingFPT] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TrainingFPT] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TrainingFPT] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TrainingFPT] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TrainingFPT] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [TrainingFPT] SET QUERY_STORE = OFF
GO
USE [TrainingFPT]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 09/04/2024 2:39:50 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[PosterImage] [varchar](max) NOT NULL,
	[ParentId] [int] NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
	[DeletedAt] [datetime] NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Courses]    Script Date: 09/04/2024 2:39:50 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Courses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[NameCourse] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[Image] [varchar](max) NULL,
	[Status] [varchar](50) NOT NULL,
	[LikeCourse] [int] NULL,
	[StarCourse] [int] NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
	[DeletedAt] [datetime] NULL,
 CONSTRAINT [PK_Courses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 09/04/2024 2:39:50 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](200) NULL,
	[Status] [varchar](50) NOT NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
	[DeletedAt] [datetime] NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Topics]    Script Date: 09/04/2024 2:39:50 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Topics](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NameTopic] [nvarchar](200) NOT NULL,
	[CouresId] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Video] [varchar](max) NULL,
	[Audio] [varchar](max) NULL,
	[DocumentTopic] [varchar](max) NULL,
	[LikeTopic] [int] NULL,
	[StarTopic] [int] NULL,
	[Status] [varchar](50) NOT NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
	[DeletedAt] [datetime] NULL,
 CONSTRAINT [PK_Topics] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TraineeCourse]    Script Date: 09/04/2024 2:39:50 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TraineeCourse](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
 CONSTRAINT [PK_TraineeCourse] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrainerTopic]    Script Date: 09/04/2024 2:39:50 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrainerTopic](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[TopicId] [int] NOT NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
 CONSTRAINT [PK_TrainerTopic] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 09/04/2024 2:39:50 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Phone] [varchar](50) NOT NULL,
	[Address] [nvarchar](500) NOT NULL,
	[Birthday] [datetime] NOT NULL,
	[Gender] [varchar](10) NOT NULL,
	[ExtraCode] [varchar](50) NOT NULL,
	[Avatar] [varchar](max) NULL,
	[Education] [varchar](200) NULL,
	[ProgramingLang] [varchar](50) NULL,
	[ToeicScore] [int] NULL,
	[Skills] [varchar](max) NULL,
	[IPClient] [varchar](50) NULL,
	[LastLogin] [datetime] NULL,
	[LastLogout] [datetime] NULL,
	[Status] [varchar](50) NOT NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
	[DeletedAt] [datetime] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Name], [Description], [PosterImage], [ParentId], [Status], [CreatedAt], [UpdatedAt], [DeletedAt]) VALUES (1, N'Tin tức ABC nhé', N'oke nhé', N'77d3472a-a7db-4d83-9051-140ed0675747-unnamed.jpg', 0, N'Active', CAST(N'2024-03-17T14:04:55.000' AS DateTime), CAST(N'2024-03-17T14:19:02.000' AS DateTime), CAST(N'2024-04-09T13:58:13.000' AS DateTime))
INSERT [dbo].[Categories] ([Id], [Name], [Description], [PosterImage], [ParentId], [Status], [CreatedAt], [UpdatedAt], [DeletedAt]) VALUES (2, N'TIn tức thôi', N'oke ', N'4accf80a-56f3-4ff4-aed8-598d5a330976-cdbb038a2e8fb80179913c6cebdbde26.jpg', 0, N'Active', CAST(N'2024-03-17T14:07:21.000' AS DateTime), CAST(N'2024-03-17T14:19:11.000' AS DateTime), CAST(N'2024-04-09T13:58:16.000' AS DateTime))
INSERT [dbo].[Categories] ([Id], [Name], [Description], [PosterImage], [ParentId], [Status], [CreatedAt], [UpdatedAt], [DeletedAt]) VALUES (3, N'Bóng dá', N'oke nhé', N'de99435b-4b7c-4332-8226-74db8d60969c-Capture.PNG', 0, N'Active', CAST(N'2024-03-17T14:12:13.000' AS DateTime), NULL, CAST(N'2024-03-17T14:29:22.000' AS DateTime))
INSERT [dbo].[Categories] ([Id], [Name], [Description], [PosterImage], [ParentId], [Status], [CreatedAt], [UpdatedAt], [DeletedAt]) VALUES (4, N'Danh mick1', N'oke nhé', N'd7f4939c-0469-4c35-b8a3-a760b1936fec-11.jpg', 0, N'Active', CAST(N'2024-03-17T21:38:11.000' AS DateTime), CAST(N'2024-03-17T21:38:21.000' AS DateTime), CAST(N'2024-03-17T21:38:25.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Courses] ON 

INSERT [dbo].[Courses] ([Id], [CategoryId], [NameCourse], [Description], [Image], [Status], [LikeCourse], [StarCourse], [CreatedAt], [UpdatedAt], [DeletedAt]) VALUES (1, 1, N'Khóa học A1', N'oke ', N'efd01b59-cbd5-49c3-8c1f-32eab719917f-unnamed.jpg', N'Active', NULL, 3, CAST(N'2024-03-17T14:59:58.000' AS DateTime), CAST(N'2024-03-17T15:08:55.000' AS DateTime), CAST(N'2024-04-09T13:58:18.000' AS DateTime))
INSERT [dbo].[Courses] ([Id], [CategoryId], [NameCourse], [Description], [Image], [Status], [LikeCourse], [StarCourse], [CreatedAt], [UpdatedAt], [DeletedAt]) VALUES (2, 2, N'Course12222', N'ko cs', N'6c62cde8-9974-4ea9-b2b6-f76727bbf5b5-3-495-1702756596-977-width740height495.jpg', N'Active', NULL, 2, CAST(N'2024-03-17T21:38:51.000' AS DateTime), CAST(N'2024-03-17T21:38:57.000' AS DateTime), CAST(N'2024-03-17T21:39:00.000' AS DateTime))
INSERT [dbo].[Courses] ([Id], [CategoryId], [NameCourse], [Description], [Image], [Status], [LikeCourse], [StarCourse], [CreatedAt], [UpdatedAt], [DeletedAt]) VALUES (3, 1, N'khóa học B', N'avcb', N'99712407-6b56-4d93-8bc7-7f9d6f3158bf-1690969270_1689527300_avtar-2.jpeg', N'Active', NULL, 2, CAST(N'2024-04-08T00:36:17.000' AS DateTime), NULL, CAST(N'2024-04-09T13:58:20.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Courses] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [Name], [Description], [Status], [CreatedAt], [UpdatedAt], [DeletedAt]) VALUES (1, N'Admin', N'Admin', N'1', CAST(N'2024-03-17T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Roles] ([Id], [Name], [Description], [Status], [CreatedAt], [UpdatedAt], [DeletedAt]) VALUES (2, N'Manage Training Staff', N'Manage Training Staff', N'1', CAST(N'2024-03-17T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Roles] ([Id], [Name], [Description], [Status], [CreatedAt], [UpdatedAt], [DeletedAt]) VALUES (3, N'Manage Trainer', N'Manage Trainer', N'1', CAST(N'2024-03-17T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Roles] ([Id], [Name], [Description], [Status], [CreatedAt], [UpdatedAt], [DeletedAt]) VALUES (4, N'Trainee', N'Trainee', N'1', CAST(N'2024-03-17T00:00:00.000' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Topics] ON 

INSERT [dbo].[Topics] ([Id], [NameTopic], [CouresId], [Description], [Video], [Audio], [DocumentTopic], [LikeTopic], [StarTopic], [Status], [CreatedAt], [UpdatedAt], [DeletedAt]) VALUES (2, N'Topc 0111', 1, N'abc', N'd5dbe5ad-b409-41a1-b3b4-51ea31de181b-184069 (720p).mp4', N'db34eb7b-96f2-4e39-aad3-d51bb91d5c97-anhthanhnien_huyr.mp3', N'1bb1f6a7-496c-48cf-a957-bb03d8f1dcc5-KTLTCS.MauBaoCao.doc', NULL, NULL, N'Active', NULL, CAST(N'2024-04-09T13:12:51.660' AS DateTime), CAST(N'2024-04-09T13:58:05.887' AS DateTime))
INSERT [dbo].[Topics] ([Id], [NameTopic], [CouresId], [Description], [Video], [Audio], [DocumentTopic], [LikeTopic], [StarTopic], [Status], [CreatedAt], [UpdatedAt], [DeletedAt]) VALUES (3, N'Topic1903', 1, N'oke', N'5dfb5de8-05a7-4e00-8ada-490cb3bfbff6-184069 (720p).mp4', N'ca45d10f-305e-4c82-9873-24f3665440df-cogaivang_huyr.mp3', N'2223d5b3-6026-4091-ad90-54b523be7016-Nhom8_HSK_S2.docx', NULL, NULL, N'Active', NULL, NULL, CAST(N'2024-04-09T13:58:03.013' AS DateTime))
INSERT [dbo].[Topics] ([Id], [NameTopic], [CouresId], [Description], [Video], [Audio], [DocumentTopic], [LikeTopic], [StarTopic], [Status], [CreatedAt], [UpdatedAt], [DeletedAt]) VALUES (4, N'demotopic', 3, N'demo', N'21c6e584-583e-44c1-9ab8-e91d241866f5-199788-911378451_tiny.mp4', N'af95269d-1384-4712-a97f-24415c0c5bf1-3ur3m6szcf.mp3', N'3be393d6-bcc4-461c-b64f-be0da0af6cc8-ASM1_1ST_ComputingResearchProject_PhongDH_BH00286.pdf', NULL, NULL, N'Active', NULL, NULL, CAST(N'2024-04-09T13:58:00.690' AS DateTime))
INSERT [dbo].[Topics] ([Id], [NameTopic], [CouresId], [Description], [Video], [Audio], [DocumentTopic], [LikeTopic], [StarTopic], [Status], [CreatedAt], [UpdatedAt], [DeletedAt]) VALUES (5, N'demotopic1', 1, N'demo2', N'b2224839-260f-4d01-910d-a507e4490e4e-demovideo.mp4', N'b42cce63-f284-47a4-a83f-24ab7b18338a-demo2.mp3', N'f06fed8e-2cfa-4951-988a-233b75ff6ec1-ASM1_1ST_ComputingResearchProject_NguyenVanTung_BH00299.docx', NULL, NULL, N'Active', NULL, CAST(N'2024-04-09T13:31:34.477' AS DateTime), CAST(N'2024-04-09T13:58:08.190' AS DateTime))
INSERT [dbo].[Topics] ([Id], [NameTopic], [CouresId], [Description], [Video], [Audio], [DocumentTopic], [LikeTopic], [StarTopic], [Status], [CreatedAt], [UpdatedAt], [DeletedAt]) VALUES (6, N'demotopic2', 1, N'áds', N'', N'', N'e41f6a74-914e-4435-a309-267e5e3e35a0-ASM1_1ST_ComputingResearchProject_NguyenVanTung_BH00299.pdf', NULL, NULL, N'Active', NULL, NULL, CAST(N'2024-04-09T13:57:58.570' AS DateTime))
SET IDENTITY_INSERT [dbo].[Topics] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [RoleId], [Username], [Password], [Email], [Phone], [Address], [Birthday], [Gender], [ExtraCode], [Avatar], [Education], [ProgramingLang], [ToeicScore], [Skills], [IPClient], [LastLogin], [LastLogout], [Status], [CreatedAt], [UpdatedAt], [DeletedAt]) VALUES (4, 3, N'tung', N'123456', N'thanhpham1245@gmail.com', N'0997752125', N'Hà Nội', CAST(N'1999-01-01T00:00:00.000' AS DateTime), N'Male', N'1567', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, N'Active', CAST(N'2024-03-17T14:25:36.000' AS DateTime), CAST(N'2024-03-17T21:40:17.000' AS DateTime), NULL)
INSERT [dbo].[Users] ([Id], [RoleId], [Username], [Password], [Email], [Phone], [Address], [Birthday], [Gender], [ExtraCode], [Avatar], [Education], [ProgramingLang], [ToeicScore], [Skills], [IPClient], [LastLogin], [LastLogout], [Status], [CreatedAt], [UpdatedAt], [DeletedAt]) VALUES (7, 1, N'admin', N'123456', N'admin@gmail.com', N'0975612521', N'Hà Nội', CAST(N'1999-01-02T00:00:00.000' AS DateTime), N'Male', N'129876', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, N'Active', CAST(N'2024-03-17T11:06:26.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Users] ([Id], [RoleId], [Username], [Password], [Email], [Phone], [Address], [Birthday], [Gender], [ExtraCode], [Avatar], [Education], [ProgramingLang], [ToeicScore], [Skills], [IPClient], [LastLogin], [LastLogout], [Status], [CreatedAt], [UpdatedAt], [DeletedAt]) VALUES (9, 4, N'demo1', N'123456', N'demo1@gmail.com', N'0123456789', N'hanoi', CAST(N'2024-04-03T00:00:00.000' AS DateTime), N'Male', N'dm1', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, N'Active', CAST(N'2024-04-08T21:39:53.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Users] ([Id], [RoleId], [Username], [Password], [Email], [Phone], [Address], [Birthday], [Gender], [ExtraCode], [Avatar], [Education], [ProgramingLang], [ToeicScore], [Skills], [IPClient], [LastLogin], [LastLogout], [Status], [CreatedAt], [UpdatedAt], [DeletedAt]) VALUES (13, 2, N'trungtq', N'123456', N'trungtq@gmail.com', N'0123467895', N'ha noi ', CAST(N'2003-04-24T00:00:00.000' AS DateTime), N'Male', N'trungtq', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, N'Active', CAST(N'2024-04-09T14:14:23.000' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Categories] ADD  CONSTRAINT [DF_Categories_ParentId]  DEFAULT ((0)) FOR [ParentId]
GO
ALTER TABLE [dbo].[Categories] ADD  CONSTRAINT [DF_Categories_Status]  DEFAULT ('Active') FOR [Status]
GO
ALTER TABLE [dbo].[Courses] ADD  CONSTRAINT [DF_Courses_Status]  DEFAULT ('Active') FOR [Status]
GO
ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Roles_Status]  DEFAULT ('Active') FOR [Status]
GO
ALTER TABLE [dbo].[Topics] ADD  CONSTRAINT [DF_Topics_Status]  DEFAULT ('Active') FOR [Status]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Status]  DEFAULT ('Active') FOR [Status]
GO
ALTER TABLE [dbo].[Courses]  WITH CHECK ADD  CONSTRAINT [FK_Courses_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[Courses] CHECK CONSTRAINT [FK_Courses_Categories]
GO
ALTER TABLE [dbo].[Topics]  WITH CHECK ADD  CONSTRAINT [FK_Topics_Courses] FOREIGN KEY([CouresId])
REFERENCES [dbo].[Courses] ([Id])
GO
ALTER TABLE [dbo].[Topics] CHECK CONSTRAINT [FK_Topics_Courses]
GO
ALTER TABLE [dbo].[TraineeCourse]  WITH CHECK ADD  CONSTRAINT [FK_TraineeCourse_Courses] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Courses] ([Id])
GO
ALTER TABLE [dbo].[TraineeCourse] CHECK CONSTRAINT [FK_TraineeCourse_Courses]
GO
ALTER TABLE [dbo].[TraineeCourse]  WITH CHECK ADD  CONSTRAINT [FK_TraineeCourse_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[TraineeCourse] CHECK CONSTRAINT [FK_TraineeCourse_Users]
GO
ALTER TABLE [dbo].[TrainerTopic]  WITH CHECK ADD  CONSTRAINT [FK_TrainerTopic_Topics] FOREIGN KEY([TopicId])
REFERENCES [dbo].[Topics] ([Id])
GO
ALTER TABLE [dbo].[TrainerTopic] CHECK CONSTRAINT [FK_TrainerTopic_Topics]
GO
ALTER TABLE [dbo].[TrainerTopic]  WITH CHECK ADD  CONSTRAINT [FK_TrainerTopic_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[TrainerTopic] CHECK CONSTRAINT [FK_TrainerTopic_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
USE [master]
GO
ALTER DATABASE [TrainingFPT] SET  READ_WRITE 
GO
