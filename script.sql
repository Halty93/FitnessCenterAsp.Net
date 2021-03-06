USE [master]
GO
/****** Object:  Database [FitnessCenter]    Script Date: 01/14/2018 20:56:47 ******/
CREATE DATABASE [FitnessCenter] ON  PRIMARY 
( NAME = N'FitnessCenter', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\FitnessCenter.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'FitnessCenter_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\FitnessCenter_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [FitnessCenter] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FitnessCenter].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FitnessCenter] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [FitnessCenter] SET ANSI_NULLS OFF
GO
ALTER DATABASE [FitnessCenter] SET ANSI_PADDING OFF
GO
ALTER DATABASE [FitnessCenter] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [FitnessCenter] SET ARITHABORT OFF
GO
ALTER DATABASE [FitnessCenter] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [FitnessCenter] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [FitnessCenter] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [FitnessCenter] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [FitnessCenter] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [FitnessCenter] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [FitnessCenter] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [FitnessCenter] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [FitnessCenter] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [FitnessCenter] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [FitnessCenter] SET  DISABLE_BROKER
GO
ALTER DATABASE [FitnessCenter] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [FitnessCenter] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [FitnessCenter] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [FitnessCenter] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [FitnessCenter] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [FitnessCenter] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [FitnessCenter] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [FitnessCenter] SET  READ_WRITE
GO
ALTER DATABASE [FitnessCenter] SET RECOVERY SIMPLE
GO
ALTER DATABASE [FitnessCenter] SET  MULTI_USER
GO
ALTER DATABASE [FitnessCenter] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [FitnessCenter] SET DB_CHAINING OFF
GO
USE [FitnessCenter]
GO
/****** Object:  Table [dbo].[Room]    Script Date: 01/14/2018 20:56:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Room](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[max_capacity] [tinyint] NOT NULL,
	[description] [varchar](1000) NULL,
 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Room] ON
INSERT [dbo].[Room] ([id], [name], [max_capacity], [description]) VALUES (1, N'Aerobní hala', 30, NULL)
INSERT [dbo].[Room] ([id], [name], [max_capacity], [description]) VALUES (2, N'Sál s ringem', 25, NULL)
INSERT [dbo].[Room] ([id], [name], [max_capacity], [description]) VALUES (3, N'Workout hala', 40, NULL)
INSERT [dbo].[Room] ([id], [name], [max_capacity], [description]) VALUES (4, N'Místnost se spinnery', 15, NULL)
SET IDENTITY_INSERT [dbo].[Room] OFF
/****** Object:  Table [dbo].[Role]    Script Date: 01/14/2018 20:56:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Role](
	[id] [int] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[description] [varchar](1000) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Role] ([id], [name], [description]) VALUES (121, N'Ředitel', N'Hlavní náplní z hlediska systému je správa místností a zaměstnanců. Ovšem má také jednoduchý přehled o strojích, aktivitách a termínů.')
INSERT [dbo].[Role] ([id], [name], [description]) VALUES (212, N'Trenér', N'Řeší správu aktivit, a termínů těchto aktivit.')
INSERT [dbo].[Role] ([id], [name], [description]) VALUES (222, N'Údržbář', N'Má na starost správu strojů.')
INSERT [dbo].[Role] ([id], [name], [description]) VALUES (399, N'Zákazník', N'Má možnost se přes webovou aplikaci přihlásit do některého z vypsaných termínů od trenérů, a může nahlásit závadu na nějakém stroji')
/****** Object:  Table [dbo].[Address]    Script Date: 01/14/2018 20:56:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Address](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[town] [varchar](200) NOT NULL,
	[street] [varchar](200) NULL,
	[street_number] [varchar](50) NULL,
	[zip] [varchar](15) NOT NULL,
	[country] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Addresss] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Address] ON
INSERT [dbo].[Address] ([id], [town], [street], [street_number], [zip], [country]) VALUES (1, N'Poděbrady', NULL, NULL, N'29001', N'Česká republika')
INSERT [dbo].[Address] ([id], [town], [street], [street_number], [zip], [country]) VALUES (3, N'Hradec Králové', NULL, NULL, N'50540', N'Česká republika')
INSERT [dbo].[Address] ([id], [town], [street], [street_number], [zip], [country]) VALUES (5, N'Praha', N'Budějovická', N'6', N'50001', N'Česká republika')
INSERT [dbo].[Address] ([id], [town], [street], [street_number], [zip], [country]) VALUES (6, N'Kutná Hora', N'Masarykova', N'590', N'28101', N'Česká republika')
INSERT [dbo].[Address] ([id], [town], [street], [street_number], [zip], [country]) VALUES (8, N'Poděbrady', N'Riegrova', N'22', N'29001', N'Česká republika')
INSERT [dbo].[Address] ([id], [town], [street], [street_number], [zip], [country]) VALUES (17, N'Zlín', N'Pražská', N'95', N'24156', N'ČR')
SET IDENTITY_INSERT [dbo].[Address] OFF
/****** Object:  Table [dbo].[FitnessUser]    Script Date: 01/14/2018 20:56:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FitnessUser](
	[address_id] [int] NOT NULL,
	[role_id] [int] NOT NULL,
	[name] [varchar](200) NOT NULL,
	[surname] [varchar](200) NOT NULL,
	[birth_number] [varchar](12) NULL,
	[login] [varchar](100) NOT NULL,
	[password] [varchar](max) NOT NULL,
	[birth_date] [date] NOT NULL,
	[phone_number] [varchar](15) NULL,
	[email] [varchar](200) NOT NULL,
	[big_image_name] [varchar](100) NULL,
	[small_image_name] [varchar](100) NULL,
	[licence_list] [varchar](max) NULL,
	[id] [int] IDENTITY(12,37) NOT NULL,
 CONSTRAINT [PK_fitnessuser] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[FitnessUser] ON
INSERT [dbo].[FitnessUser] ([address_id], [role_id], [name], [surname], [birth_number], [login], [password], [birth_date], [phone_number], [email], [big_image_name], [small_image_name], [licence_list], [id]) VALUES (5, 121, N'Lukáš', N'Bareš', N'0', N'Bary', N'1000:NvRNTKE8ds+T3AA2thRJubPlr5hmxlwO:JOdwDoYz0GwAmmo4aXyNlioukS5xe3tC', CAST(0x9D180B00 AS Date), N'777888999', N'lukas.bares@bavimetovymyslet.com', NULL, NULL, NULL, 12)
INSERT [dbo].[FitnessUser] ([address_id], [role_id], [name], [surname], [birth_number], [login], [password], [birth_date], [phone_number], [email], [big_image_name], [small_image_name], [licence_list], [id]) VALUES (8, 212, N'David', N'Haltuch', N'998866552', N'Halty', N'1000:f/vrypqwUZ4c96Xhe9z30cEDKKZTaRAW:BJI8GKGt/skACIjCz+8lo9a5mY/uE3m4', CAST(0x2B1A0B00 AS Date), N'778855443', N'david.haltuch@benchmaster.com', NULL, NULL, N'dasdassdfasfsdds', 49)
INSERT [dbo].[FitnessUser] ([address_id], [role_id], [name], [surname], [birth_number], [login], [password], [birth_date], [phone_number], [email], [big_image_name], [small_image_name], [licence_list], [id]) VALUES (1, 222, N'Tomáš', N'Skořepa', N'0', N'Skoreto', N'1000:niDkEYzI4TZDciT7X0idsMW39UmI0I5K:3Tsvdps/Xg/64hbCrgeoUfVMQA3hetaR', CAST(0xC00E0B00 AS Date), N'147852369', N'tomas.skorepa@sezam.cz', NULL, NULL, NULL, 86)
INSERT [dbo].[FitnessUser] ([address_id], [role_id], [name], [surname], [birth_number], [login], [password], [birth_date], [phone_number], [email], [big_image_name], [small_image_name], [licence_list], [id]) VALUES (3, 399, N'Jan', N'Veselý ', N'0', N'Jan', N'1000:D3OWG86z3GULmc6hFpoJVsWjmXq5UmNv:UZHu+PMV5jXD39BF0VjsTtB+OX+2rYlC', CAST(0x1A190B00 AS Date), N'456852159', N'jan.vesely@veselo.com', NULL, NULL, NULL, 123)
INSERT [dbo].[FitnessUser] ([address_id], [role_id], [name], [surname], [birth_number], [login], [password], [birth_date], [phone_number], [email], [big_image_name], [small_image_name], [licence_list], [id]) VALUES (6, 399, N'Klára', N'Jirásková', N'0', N'klára', N'1000:yW3gfcvk7Z+McGgUfQhYpZBTdvqIBrz6:DuePNnBIHA/5NyTsShyxhRUX70+0KRYW', CAST(0x201E0B00 AS Date), N'786512259', N'klara@email.com', NULL, NULL, NULL, 160)
INSERT [dbo].[FitnessUser] ([address_id], [role_id], [name], [surname], [birth_number], [login], [password], [birth_date], [phone_number], [email], [big_image_name], [small_image_name], [licence_list], [id]) VALUES (17, 212, N'Aloy', N'Brave', N'9302014444', N'Aloy', N'1000:pneLHxlHq6AxSjWibn+ptX1PZmGCnUUp:Xy+yv4TvQaR456djLVzxLZRdH2O2rXJC', CAST(0xDB0F0B00 AS Date), N'775588441', N'ginger@sda.com', NULL, NULL, N'kkahfdlkjdfjaldifnxcje  dcja', 456)
SET IDENTITY_INSERT [dbo].[FitnessUser] OFF
/****** Object:  Table [dbo].[Activity]    Script Date: 01/14/2018 20:56:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Activity](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](200) NOT NULL,
	[difficultness] [int] NOT NULL,
	[description] [varchar](max) NULL,
	[big_image_name] [varchar](100) NULL,
	[small_image_name] [varchar](100) NULL,
	[price] [smallmoney] NOT NULL,
	[user_id] [int] NOT NULL,
 CONSTRAINT [PK_Activity] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_Activity] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Activity] ON
INSERT [dbo].[Activity] ([id], [name], [difficultness], [description], [big_image_name], [small_image_name], [price], [user_id]) VALUES (2, N'Kruhový trénink', 3, N'<p>Pro ty, kteri chteji zhubnout</p>', NULL, NULL, 87.0000, 49)
INSERT [dbo].[Activity] ([id], [name], [difficultness], [description], [big_image_name], [small_image_name], [price], [user_id]) VALUES (3, N'Trampolíny', 2, N'<p>Skupinove skakani na trampolinach</p>', NULL, NULL, 102.0000, 49)
INSERT [dbo].[Activity] ([id], [name], [difficultness], [description], [big_image_name], [small_image_name], [price], [user_id]) VALUES (4, N'TRX', 6, N'<p>Cvičen&iacute; s TRX</p>', N'32b1b33a-02bf-4a6e-bcff-3f8bde1ee511', N'30e87404-cd11-4e64-bba6-cdf7f5721d23', 100.0000, 49)
INSERT [dbo].[Activity] ([id], [name], [difficultness], [description], [big_image_name], [small_image_name], [price], [user_id]) VALUES (5, N'MMA', 8, N'<p>Bojov&yacute; sport</p>', N'811a0d8c-1140-411d-986e-2d77950dd909', N'b5d7d167-fc32-4f85-bcc7-36a5ba951465', 190.0000, 49)
INSERT [dbo].[Activity] ([id], [name], [difficultness], [description], [big_image_name], [small_image_name], [price], [user_id]) VALUES (6, N'Crossfit', 10, N'<p>&Scaron;&aacute;hněte si až na sam&eacute; dno va&scaron;ich sil</p>', N'2def77d2-bc18-4c4c-a0c8-7f848c3d369a', N'a2f04ae7-48f7-46a1-9768-8bf67ee3a596', 150.0000, 49)
SET IDENTITY_INSERT [dbo].[Activity] OFF
/****** Object:  Table [dbo].[Machine]    Script Date: 01/14/2018 20:56:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Machine](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[repairman_id] [int] NOT NULL,
	[name] [varchar](200) NOT NULL,
	[last_check] [date] NOT NULL,
	[next_check] [date] NOT NULL,
	[status] [varchar](50) NOT NULL,
	[description] [varchar](max) NULL,
	[user_id] [int] NULL,
	[fault_date] [date] NULL,
	[fault] [varchar](1000) NULL,
	[big_image_name] [varchar](100) NULL,
	[small_image_name] [varchar](100) NULL,
 CONSTRAINT [PK_Machine] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_Machine] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Machine] ON
INSERT [dbo].[Machine] ([id], [repairman_id], [name], [last_check], [next_check], [status], [description], [user_id], [fault_date], [fault], [big_image_name], [small_image_name]) VALUES (1, 86, N'Peck Deck', CAST(0x633D0B00 AS Date), CAST(0x633D0B00 AS Date), N'V pořádku', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Machine] ([id], [repairman_id], [name], [last_check], [next_check], [status], [description], [user_id], [fault_date], [fault], [big_image_name], [small_image_name]) VALUES (3, 86, N'Bench press', CAST(0x633D0B00 AS Date), CAST(0xD03E0B00 AS Date), N'Poškozený', NULL, 123, CAST(0xB13D0B00 AS Date), N'<p>slkdaslkdnas</p>', NULL, NULL)
INSERT [dbo].[Machine] ([id], [repairman_id], [name], [last_check], [next_check], [status], [description], [user_id], [fault_date], [fault], [big_image_name], [small_image_name]) VALUES (4, 86, N'Leg press', CAST(0x633D0B00 AS Date), CAST(0xD03E0B00 AS Date), N'V pořádku', NULL, NULL, CAST(0x00000000 AS Date), NULL, N'01f6b8af-c07e-496b-b039-0dd98dc1e4e0', N'32ddec15-938f-47bb-b417-ad3b80d978dc')
INSERT [dbo].[Machine] ([id], [repairman_id], [name], [last_check], [next_check], [status], [description], [user_id], [fault_date], [fault], [big_image_name], [small_image_name]) VALUES (5, 86, N'Klec', CAST(0x633D0B00 AS Date), CAST(0xD03E0B00 AS Date), N'V pořádku', NULL, NULL, CAST(0x00000000 AS Date), NULL, NULL, NULL)
INSERT [dbo].[Machine] ([id], [repairman_id], [name], [last_check], [next_check], [status], [description], [user_id], [fault_date], [fault], [big_image_name], [small_image_name]) VALUES (6, 86, N'Multipress', CAST(0xB13D0B00 AS Date), CAST(0xB23D0B00 AS Date), N'V pořádku', NULL, NULL, CAST(0x00000000 AS Date), NULL, NULL, NULL)
INSERT [dbo].[Machine] ([id], [repairman_id], [name], [last_check], [next_check], [status], [description], [user_id], [fault_date], [fault], [big_image_name], [small_image_name]) VALUES (7, 86, N'Stroj na dřepy', CAST(0xB13D0B00 AS Date), CAST(0x1E3F0B00 AS Date), N'V pořádku', NULL, NULL, CAST(0x00000000 AS Date), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Machine] OFF
/****** Object:  Table [dbo].[Term]    Script Date: 01/14/2018 20:56:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Term](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[room_id] [int] NOT NULL,
	[activity_id] [int] NOT NULL,
	[start_term] [datetime] NOT NULL,
	[end_term] [datetime] NOT NULL,
	[capacity] [tinyint] NOT NULL,
 CONSTRAINT [PK_Term] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Term] ON
INSERT [dbo].[Term] ([id], [user_id], [room_id], [activity_id], [start_term], [end_term], [capacity]) VALUES (2, 49, 1, 2, CAST(0x0000A85500D63BC0 AS DateTime), CAST(0x0000A85500DE7920 AS DateTime), 15)
INSERT [dbo].[Term] ([id], [user_id], [room_id], [activity_id], [start_term], [end_term], [capacity]) VALUES (26, 49, 1, 2, CAST(0x0000A85500FCAF80 AS DateTime), CAST(0x0000A8550104ECE0 AS DateTime), 15)
INSERT [dbo].[Term] ([id], [user_id], [room_id], [activity_id], [start_term], [end_term], [capacity]) VALUES (27, 49, 1, 2, CAST(0x0000A85500FF2850 AS DateTime), CAST(0x0000A855010765B0 AS DateTime), 15)
INSERT [dbo].[Term] ([id], [user_id], [room_id], [activity_id], [start_term], [end_term], [capacity]) VALUES (29, 49, 1, 2, CAST(0x0000A85500FFFB40 AS DateTime), CAST(0x0000A855010838A0 AS DateTime), 15)
INSERT [dbo].[Term] ([id], [user_id], [room_id], [activity_id], [start_term], [end_term], [capacity]) VALUES (39, 49, 1, 2, CAST(0x0000A856011826C0 AS DateTime), CAST(0x0000A85601206420 AS DateTime), 15)
INSERT [dbo].[Term] ([id], [user_id], [room_id], [activity_id], [start_term], [end_term], [capacity]) VALUES (40, 49, 1, 2, CAST(0x0000A855011BFF20 AS DateTime), CAST(0x0000A85501243C80 AS DateTime), 15)
INSERT [dbo].[Term] ([id], [user_id], [room_id], [activity_id], [start_term], [end_term], [capacity]) VALUES (41, 49, 1, 2, CAST(0x0000A8570128A180 AS DateTime), CAST(0x0000A85701391C40 AS DateTime), 15)
INSERT [dbo].[Term] ([id], [user_id], [room_id], [activity_id], [start_term], [end_term], [capacity]) VALUES (42, 49, 1, 2, CAST(0x0000A85800E43DB0 AS DateTime), CAST(0x0000A85800EC7B10 AS DateTime), 25)
INSERT [dbo].[Term] ([id], [user_id], [room_id], [activity_id], [start_term], [end_term], [capacity]) VALUES (43, 49, 2, 5, CAST(0x0000A86D010FE960 AS DateTime), CAST(0x0000A86D0128A180 AS DateTime), 15)
INSERT [dbo].[Term] ([id], [user_id], [room_id], [activity_id], [start_term], [end_term], [capacity]) VALUES (44, 49, 1, 3, CAST(0x0000A868010ED020 AS DateTime), CAST(0x0000A86801170D80 AS DateTime), 30)
SET IDENTITY_INSERT [dbo].[Term] OFF
/****** Object:  Table [dbo].[Reservation]    Script Date: 01/14/2018 20:56:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservation](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[term_id] [int] NOT NULL,
	[reservation_time] [datetime] NOT NULL,
 CONSTRAINT [PK_Reservation] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Reservation] ON
INSERT [dbo].[Reservation] ([id], [user_id], [term_id], [reservation_time]) VALUES (4, 123, 41, CAST(0x0000A85700DC46A0 AS DateTime))
INSERT [dbo].[Reservation] ([id], [user_id], [term_id], [reservation_time]) VALUES (5, 123, 43, CAST(0x0000A8680112C088 AS DateTime))
INSERT [dbo].[Reservation] ([id], [user_id], [term_id], [reservation_time]) VALUES (6, 160, 43, CAST(0x0000A86801132C03 AS DateTime))
SET IDENTITY_INSERT [dbo].[Reservation] OFF
/****** Object:  ForeignKey [FK_FitnessUser_Address]    Script Date: 01/14/2018 20:56:48 ******/
ALTER TABLE [dbo].[FitnessUser]  WITH CHECK ADD  CONSTRAINT [FK_FitnessUser_Address] FOREIGN KEY([address_id])
REFERENCES [dbo].[Address] ([id])
GO
ALTER TABLE [dbo].[FitnessUser] CHECK CONSTRAINT [FK_FitnessUser_Address]
GO
/****** Object:  ForeignKey [FK_FitnessUser_Role]    Script Date: 01/14/2018 20:56:48 ******/
ALTER TABLE [dbo].[FitnessUser]  WITH CHECK ADD  CONSTRAINT [FK_FitnessUser_Role] FOREIGN KEY([role_id])
REFERENCES [dbo].[Role] ([id])
GO
ALTER TABLE [dbo].[FitnessUser] CHECK CONSTRAINT [FK_FitnessUser_Role]
GO
/****** Object:  ForeignKey [FK_Activity_FitnessUser]    Script Date: 01/14/2018 20:56:48 ******/
ALTER TABLE [dbo].[Activity]  WITH CHECK ADD  CONSTRAINT [FK_Activity_FitnessUser] FOREIGN KEY([user_id])
REFERENCES [dbo].[FitnessUser] ([id])
GO
ALTER TABLE [dbo].[Activity] CHECK CONSTRAINT [FK_Activity_FitnessUser]
GO
/****** Object:  ForeignKey [FK_Machine_FitnessUser]    Script Date: 01/14/2018 20:56:48 ******/
ALTER TABLE [dbo].[Machine]  WITH CHECK ADD  CONSTRAINT [FK_Machine_FitnessUser] FOREIGN KEY([repairman_id])
REFERENCES [dbo].[FitnessUser] ([id])
GO
ALTER TABLE [dbo].[Machine] CHECK CONSTRAINT [FK_Machine_FitnessUser]
GO
/****** Object:  ForeignKey [FK_Machine_FitnessUser1]    Script Date: 01/14/2018 20:56:48 ******/
ALTER TABLE [dbo].[Machine]  WITH CHECK ADD  CONSTRAINT [FK_Machine_FitnessUser1] FOREIGN KEY([user_id])
REFERENCES [dbo].[FitnessUser] ([id])
GO
ALTER TABLE [dbo].[Machine] CHECK CONSTRAINT [FK_Machine_FitnessUser1]
GO
/****** Object:  ForeignKey [FK_Term_Activity]    Script Date: 01/14/2018 20:56:48 ******/
ALTER TABLE [dbo].[Term]  WITH CHECK ADD  CONSTRAINT [FK_Term_Activity] FOREIGN KEY([activity_id])
REFERENCES [dbo].[Activity] ([id])
GO
ALTER TABLE [dbo].[Term] CHECK CONSTRAINT [FK_Term_Activity]
GO
/****** Object:  ForeignKey [FK_Term_FitnessUser]    Script Date: 01/14/2018 20:56:48 ******/
ALTER TABLE [dbo].[Term]  WITH CHECK ADD  CONSTRAINT [FK_Term_FitnessUser] FOREIGN KEY([user_id])
REFERENCES [dbo].[FitnessUser] ([id])
GO
ALTER TABLE [dbo].[Term] CHECK CONSTRAINT [FK_Term_FitnessUser]
GO
/****** Object:  ForeignKey [FK_Term_Room]    Script Date: 01/14/2018 20:56:48 ******/
ALTER TABLE [dbo].[Term]  WITH CHECK ADD  CONSTRAINT [FK_Term_Room] FOREIGN KEY([room_id])
REFERENCES [dbo].[Room] ([id])
GO
ALTER TABLE [dbo].[Term] CHECK CONSTRAINT [FK_Term_Room]
GO
/****** Object:  ForeignKey [FK_Reservation_FitnessUser]    Script Date: 01/14/2018 20:56:48 ******/
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_FitnessUser] FOREIGN KEY([user_id])
REFERENCES [dbo].[FitnessUser] ([id])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_FitnessUser]
GO
/****** Object:  ForeignKey [FK_Reservation_Term]    Script Date: 01/14/2018 20:56:48 ******/
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_Term] FOREIGN KEY([term_id])
REFERENCES [dbo].[Term] ([id])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_Term]
GO
