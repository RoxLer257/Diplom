USE [VSK_Diplom]
GO
/****** Object:  Table [dbo].[ClientDrivers]    Script Date: 14.06.2025 12:17:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientDrivers](
	[ClientID] [int] NOT NULL,
	[DriverID] [int] NOT NULL,
 CONSTRAINT [PK_ClientDrivers] PRIMARY KEY CLUSTERED 
(
	[ClientID] ASC,
	[DriverID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 14.06.2025 12:17:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[ClientID] [int] IDENTITY(1,1) NOT NULL,
	[ClientTypeID] [int] NOT NULL,
	[AgentID] [int] NULL,
	[LastName] [nvarchar](50) NULL,
	[FirstName] [nvarchar](50) NULL,
	[MiddleName] [nvarchar](50) NULL,
	[CompanyName] [nvarchar](100) NULL,
	[PassportNumber] [nvarchar](20) NULL,
	[INN] [nvarchar](12) NULL,
	[Phone] [nvarchar](15) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[ClientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClientTypes]    Script Date: 14.06.2025 12:17:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientTypes](
	[ClientTypeID] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ClientTypes] PRIMARY KEY CLUSTERED 
(
	[ClientTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DriverInsuranceHistory]    Script Date: 14.06.2025 12:17:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DriverInsuranceHistory](
	[HistoryID] [int] IDENTITY(1,1) NOT NULL,
	[DriverID] [int] NOT NULL,
	[PolicyID] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[HadAccident] [bit] NOT NULL,
	[KBM] [decimal](4, 2) NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
 CONSTRAINT [PK_DriverInsuranceHistory] PRIMARY KEY CLUSTERED 
(
	[HistoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Drivers]    Script Date: 14.06.2025 12:17:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Drivers](
	[DriverID] [int] IDENTITY(1,1) NOT NULL,
	[ClientID] [int] NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,
	[DateOfBirth] [date] NOT NULL,
	[LicenseNumber] [nvarchar](20) NOT NULL,
	[DrivingExperience] [int] NOT NULL,
 CONSTRAINT [PK_Drivers] PRIMARY KEY CLUSTERED 
(
	[DriverID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 14.06.2025 12:17:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NOT NULL,
	[ManagerID] [int] NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,
	[Phone] [nvarchar](15) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HealthConditions]    Script Date: 14.06.2025 12:17:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HealthConditions](
	[HealthConditionID] [int] IDENTITY(1,1) NOT NULL,
	[ConditionName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_HealthConditions] PRIMARY KEY CLUSTERED 
(
	[HealthConditionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LifeAndHealth]    Script Date: 14.06.2025 12:17:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LifeAndHealth](
	[LifeAndHealthID] [int] IDENTITY(1,1) NOT NULL,
	[PolicyID] [int] NOT NULL,
	[Age] [int] NOT NULL,
	[Gender] [nvarchar](10) NOT NULL,
	[HealthConditionID] [int] NOT NULL,
	[Occupation] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_LifeAndHealth] PRIMARY KEY CLUSTERED 
(
	[LifeAndHealthID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Logs]    Script Date: 14.06.2025 12:17:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logs](
	[LogID] [int] IDENTITY(1,1) NOT NULL,
	[TableName] [nvarchar](50) NOT NULL,
	[Action] [nvarchar](50) NOT NULL,
	[ChangedData] [nvarchar](max) NULL,
	[ChangeDate] [datetime] NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[EmployeeID] [int] NULL,
 CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Policies]    Script Date: 14.06.2025 12:17:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Policies](
	[PolicyID] [int] IDENTITY(1,1) NOT NULL,
	[PolicyTypeID] [int] NOT NULL,
	[StatusID] [int] NOT NULL,
	[InsuranceAmount] [decimal](15, 2) NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
 CONSTRAINT [PK_Policies] PRIMARY KEY CLUSTERED 
(
	[PolicyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PolicyClients]    Script Date: 14.06.2025 12:17:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PolicyClients](
	[PolicyID] [int] NOT NULL,
	[ClientID] [int] NOT NULL,
 CONSTRAINT [PK_PolicyClients] PRIMARY KEY CLUSTERED 
(
	[PolicyID] ASC,
	[ClientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PolicyDrivers]    Script Date: 14.06.2025 12:17:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PolicyDrivers](
	[PolicyID] [int] NOT NULL,
	[DriverID] [int] NOT NULL,
 CONSTRAINT [PK_PolicyDrivers] PRIMARY KEY CLUSTERED 
(
	[PolicyID] ASC,
	[DriverID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PolicyStatuses]    Script Date: 14.06.2025 12:17:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PolicyStatuses](
	[StatusID] [int] IDENTITY(1,1) NOT NULL,
	[StatusName] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_PolicyStatuses] PRIMARY KEY CLUSTERED 
(
	[StatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PolicyTypes]    Script Date: 14.06.2025 12:17:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PolicyTypes](
	[PolicyTypeID] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PolicyTypes] PRIMARY KEY CLUSTERED 
(
	[PolicyTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Properties]    Script Date: 14.06.2025 12:17:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Properties](
	[PropertyID] [int] IDENTITY(1,1) NOT NULL,
	[PolicyID] [int] NOT NULL,
	[PropertyTypeID] [int] NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[Area] [decimal](10, 2) NOT NULL,
	[Value] [decimal](15, 2) NOT NULL,
 CONSTRAINT [PK_Properties] PRIMARY KEY CLUSTERED 
(
	[PropertyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PropertyTypes]    Script Date: 14.06.2025 12:17:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropertyTypes](
	[PropertyTypeID] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PropertyTypes] PRIMARY KEY CLUSTERED 
(
	[PropertyTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 14.06.2025 12:17:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VehicleMakes]    Script Date: 14.06.2025 12:17:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VehicleMakes](
	[MakeID] [int] IDENTITY(1,1) NOT NULL,
	[MakeName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_VehicleMakes] PRIMARY KEY CLUSTERED 
(
	[MakeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VehicleModels]    Script Date: 14.06.2025 12:17:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VehicleModels](
	[ModelID] [int] IDENTITY(1,1) NOT NULL,
	[MakeID] [int] NOT NULL,
	[ModelName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_VehicleModels] PRIMARY KEY CLUSTERED 
(
	[ModelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehicles]    Script Date: 14.06.2025 12:17:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehicles](
	[VehicleID] [int] IDENTITY(1,1) NOT NULL,
	[PolicyID] [int] NOT NULL,
	[ModelID] [int] NOT NULL,
	[VIN] [nvarchar](17) NOT NULL,
	[Year] [int] NOT NULL,
	[LicensePlate] [nvarchar](20) NOT NULL,
	[RegistrationRegion] [nvarchar](100) NULL,
	[EnginePower] [int] NULL,
 CONSTRAINT [PK_Vehicles] PRIMARY KEY CLUSTERED 
(
	[VehicleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[ClientDrivers] ([ClientID], [DriverID]) VALUES (1, 1)
INSERT [dbo].[ClientDrivers] ([ClientID], [DriverID]) VALUES (1, 2)
INSERT [dbo].[ClientDrivers] ([ClientID], [DriverID]) VALUES (2, 3)
GO
SET IDENTITY_INSERT [dbo].[Clients] ON 

INSERT [dbo].[Clients] ([ClientID], [ClientTypeID], [AgentID], [LastName], [FirstName], [MiddleName], [CompanyName], [PassportNumber], [INN], [Phone], [Email]) VALUES (1, 1, 3, N'Иванов', N'Иван', N'Иванович', NULL, N'1234 567890', N'1434567812', N'+79123435678', N'ivanov@example.com')
INSERT [dbo].[Clients] ([ClientID], [ClientTypeID], [AgentID], [LastName], [FirstName], [MiddleName], [CompanyName], [PassportNumber], [INN], [Phone], [Email]) VALUES (2, 1, 3, N'Петров', N'Пётр', N'Петрович', NULL, N'2345 678901', N'6345678901', N'+79123465679', N'petrov@example.com')
INSERT [dbo].[Clients] ([ClientID], [ClientTypeID], [AgentID], [LastName], [FirstName], [MiddleName], [CompanyName], [PassportNumber], [INN], [Phone], [Email]) VALUES (3, 2, 4, NULL, NULL, NULL, N'ООО Ромашка', NULL, N'1234567890', N'+76123456480', N'romashka@example.com')
INSERT [dbo].[Clients] ([ClientID], [ClientTypeID], [AgentID], [LastName], [FirstName], [MiddleName], [CompanyName], [PassportNumber], [INN], [Phone], [Email]) VALUES (1002, 1, NULL, N'Иванов', N'Сидр', N'Уваныч', NULL, NULL, NULL, N'+79225368722', N'gsdfg@geea.er')
INSERT [dbo].[Clients] ([ClientID], [ClientTypeID], [AgentID], [LastName], [FirstName], [MiddleName], [CompanyName], [PassportNumber], [INN], [Phone], [Email]) VALUES (1003, 1, 4, N'str', N'era', N'www', N'', N'2453453', N'2454563456', N'67456734', N'hdfghf')
INSERT [dbo].[Clients] ([ClientID], [ClientTypeID], [AgentID], [LastName], [FirstName], [MiddleName], [CompanyName], [PassportNumber], [INN], [Phone], [Email]) VALUES (2003, 1, 3, N'Ия', N'яя', N'Я', NULL, NULL, NULL, N'5643235445', N'gsdfgr@fawf.ru')
INSERT [dbo].[Clients] ([ClientID], [ClientTypeID], [AgentID], [LastName], [FirstName], [MiddleName], [CompanyName], [PassportNumber], [INN], [Phone], [Email]) VALUES (3003, 1, 4, N'text', N'test', N'Ts', NULL, N'2345345', N'234234', N'436546', N'sggfsdr@gsdg.com')
INSERT [dbo].[Clients] ([ClientID], [ClientTypeID], [AgentID], [LastName], [FirstName], [MiddleName], [CompanyName], [PassportNumber], [INN], [Phone], [Email]) VALUES (4003, 1, 4, N'test12', N'test3', N'teset3', NULL, N'12345', N'12354', N'1234123', N'12341234')
INSERT [dbo].[Clients] ([ClientID], [ClientTypeID], [AgentID], [LastName], [FirstName], [MiddleName], [CompanyName], [PassportNumber], [INN], [Phone], [Email]) VALUES (5003, 1, 5002, N'stet', N'tset', N'setset', N'', N'43234', N'234235', N'46356', N'3463')
INSERT [dbo].[Clients] ([ClientID], [ClientTypeID], [AgentID], [LastName], [FirstName], [MiddleName], [CompanyName], [PassportNumber], [INN], [Phone], [Email]) VALUES (5004, 1, 5002, N'taet', N'set', N'tsrts', NULL, N'23423', N'4534523', N'5345', N'234234')
INSERT [dbo].[Clients] ([ClientID], [ClientTypeID], [AgentID], [LastName], [FirstName], [MiddleName], [CompanyName], [PassportNumber], [INN], [Phone], [Email]) VALUES (6003, 1, 5002, N'asdfasdfa', N'sdfasdf', N'asdfasdfa', NULL, N'sasdfas', N'dfasdfa', N'1231', N'12312')
INSERT [dbo].[Clients] ([ClientID], [ClientTypeID], [AgentID], [LastName], [FirstName], [MiddleName], [CompanyName], [PassportNumber], [INN], [Phone], [Email]) VALUES (6004, 1, 5002, N'a', N'a', N'a', NULL, N'a', N'a', N'2', N'12')
INSERT [dbo].[Clients] ([ClientID], [ClientTypeID], [AgentID], [LastName], [FirstName], [MiddleName], [CompanyName], [PassportNumber], [INN], [Phone], [Email]) VALUES (7003, 1, 5002, N'hgf', N'fgh', N'fgh', NULL, N'fgh', N'fgh', N'23', N'rw32')
INSERT [dbo].[Clients] ([ClientID], [ClientTypeID], [AgentID], [LastName], [FirstName], [MiddleName], [CompanyName], [PassportNumber], [INN], [Phone], [Email]) VALUES (7004, 1, 5002, N'yrt', N'ryt', N'ryt', NULL, N'234', N'453', N'234', N'234')
INSERT [dbo].[Clients] ([ClientID], [ClientTypeID], [AgentID], [LastName], [FirstName], [MiddleName], [CompanyName], [PassportNumber], [INN], [Phone], [Email]) VALUES (7005, 1, 5002, N'zzz', N'zzz', N'zzz', NULL, N'zzz', N'zzz', N'34535', N'23423')
INSERT [dbo].[Clients] ([ClientID], [ClientTypeID], [AgentID], [LastName], [FirstName], [MiddleName], [CompanyName], [PassportNumber], [INN], [Phone], [Email]) VALUES (7006, 1, 5002, N'yery', N'etret', N'erte', N'', N'rtert', N'234234', N'23423', N'234234')
INSERT [dbo].[Clients] ([ClientID], [ClientTypeID], [AgentID], [LastName], [FirstName], [MiddleName], [CompanyName], [PassportNumber], [INN], [Phone], [Email]) VALUES (7007, 1, 5002, N'не', N'не', N'не', NULL, N'не', N'не', N'3453', N'34534')
INSERT [dbo].[Clients] ([ClientID], [ClientTypeID], [AgentID], [LastName], [FirstName], [MiddleName], [CompanyName], [PassportNumber], [INN], [Phone], [Email]) VALUES (7008, 1, 5002, N'hhh', N'hhh', N'hh', NULL, N'hhh', N'hhh', N'12412', N'12412')
INSERT [dbo].[Clients] ([ClientID], [ClientTypeID], [AgentID], [LastName], [FirstName], [MiddleName], [CompanyName], [PassportNumber], [INN], [Phone], [Email]) VALUES (7009, 1, 5002, N'test', N'test', N'test', N'', N'tset', N'test', N'setset', N'sets')
INSERT [dbo].[Clients] ([ClientID], [ClientTypeID], [AgentID], [LastName], [FirstName], [MiddleName], [CompanyName], [PassportNumber], [INN], [Phone], [Email]) VALUES (7010, 1, 5002, N'asda', N'asdas', N'asdas', NULL, N'dasda', N'sdasda', N'12', N'34234')
INSERT [dbo].[Clients] ([ClientID], [ClientTypeID], [AgentID], [LastName], [FirstName], [MiddleName], [CompanyName], [PassportNumber], [INN], [Phone], [Email]) VALUES (8009, 1, 5002, N'вапвап', N'вкпвкп', N'вкпвкп', N'', N'1231 234235', N'124124234235', N'89565468148', N'asdaw@faef.com')
INSERT [dbo].[Clients] ([ClientID], [ClientTypeID], [AgentID], [LastName], [FirstName], [MiddleName], [CompanyName], [PassportNumber], [INN], [Phone], [Email]) VALUES (8010, 1, 5002, N'ыфваф', N'фывафыв', N'фывафыв', NULL, N'1231 123456', N'121312121212', N'89775391585', N'dsfgs@asfa.com')
INSERT [dbo].[Clients] ([ClientID], [ClientTypeID], [AgentID], [LastName], [FirstName], [MiddleName], [CompanyName], [PassportNumber], [INN], [Phone], [Email]) VALUES (9009, 1, 5002, N'ваиваи', N'ваиваи', N'ваиваи', N'', N'1234 123456', N'123456789011', N'89775391625', N'sdafs@asd.com')
INSERT [dbo].[Clients] ([ClientID], [ClientTypeID], [AgentID], [LastName], [FirstName], [MiddleName], [CompanyName], [PassportNumber], [INN], [Phone], [Email]) VALUES (9010, 1, 5002, N'ИЫВА', N'ЫВАИ', N'ВАПЫ', NULL, N'1234 643565', N'454567327963', N'89656784323', N'DFGDG@GAEF.COM')
INSERT [dbo].[Clients] ([ClientID], [ClientTypeID], [AgentID], [LastName], [FirstName], [MiddleName], [CompanyName], [PassportNumber], [INN], [Phone], [Email]) VALUES (9011, 2, 5002, N'', N'', N'', N'фываыва', N'1234 123412', N'2134123534', N'89675679876', N'gsdfg@dsfgdh.lcom')
INSERT [dbo].[Clients] ([ClientID], [ClientTypeID], [AgentID], [LastName], [FirstName], [MiddleName], [CompanyName], [PassportNumber], [INN], [Phone], [Email]) VALUES (9012, 1, 5002, N'ыфв', N'фыва', N'фыва', N'', N'1234 123456', N'123456789012', N'87676788756', N'dfgsdr@gbdfg.com')
INSERT [dbo].[Clients] ([ClientID], [ClientTypeID], [AgentID], [LastName], [FirstName], [MiddleName], [CompanyName], [PassportNumber], [INN], [Phone], [Email]) VALUES (9013, 2, 5002, N'', N'', N'', N'вапывап', N'1234 123456', N'1234567890', N'89776578767', N'dfgdfh@sdfgsdfg.com')
INSERT [dbo].[Clients] ([ClientID], [ClientTypeID], [AgentID], [LastName], [FirstName], [MiddleName], [CompanyName], [PassportNumber], [INN], [Phone], [Email]) VALUES (9014, 1, 5002, N'тест', N'тест', N'тест', N'', N'1243 123456', N'123456789012', N'89264947515', N'sadfsdg@dsfg.com')
SET IDENTITY_INSERT [dbo].[Clients] OFF
GO
SET IDENTITY_INSERT [dbo].[ClientTypes] ON 

INSERT [dbo].[ClientTypes] ([ClientTypeID], [TypeName]) VALUES (1, N'Физическое лицо')
INSERT [dbo].[ClientTypes] ([ClientTypeID], [TypeName]) VALUES (2, N'Юридическое лицо')
SET IDENTITY_INSERT [dbo].[ClientTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[DriverInsuranceHistory] ON 

INSERT [dbo].[DriverInsuranceHistory] ([HistoryID], [DriverID], [PolicyID], [Year], [HadAccident], [KBM], [LastUpdated]) VALUES (1, 1, 1, 2025, 0, CAST(1.00 AS Decimal(4, 2)), CAST(N'2025-05-14T12:34:24.217' AS DateTime))
INSERT [dbo].[DriverInsuranceHistory] ([HistoryID], [DriverID], [PolicyID], [Year], [HadAccident], [KBM], [LastUpdated]) VALUES (2, 2, 11009, 2025, 0, CAST(1.00 AS Decimal(4, 2)), CAST(N'2025-05-14T12:34:24.217' AS DateTime))
INSERT [dbo].[DriverInsuranceHistory] ([HistoryID], [DriverID], [PolicyID], [Year], [HadAccident], [KBM], [LastUpdated]) VALUES (3, 3, 11009, 2025, 0, CAST(1.00 AS Decimal(4, 2)), CAST(N'2025-05-14T12:34:24.217' AS DateTime))
INSERT [dbo].[DriverInsuranceHistory] ([HistoryID], [DriverID], [PolicyID], [Year], [HadAccident], [KBM], [LastUpdated]) VALUES (4, 1, 15016, 2025, 0, CAST(1.00 AS Decimal(4, 2)), CAST(N'2025-05-14T12:34:24.217' AS DateTime))
INSERT [dbo].[DriverInsuranceHistory] ([HistoryID], [DriverID], [PolicyID], [Year], [HadAccident], [KBM], [LastUpdated]) VALUES (5, 1002, 1002, 2025, 0, CAST(1.00 AS Decimal(4, 2)), CAST(N'2025-05-14T12:34:24.217' AS DateTime))
INSERT [dbo].[DriverInsuranceHistory] ([HistoryID], [DriverID], [PolicyID], [Year], [HadAccident], [KBM], [LastUpdated]) VALUES (6, 1002, 15016, 2025, 0, CAST(1.00 AS Decimal(4, 2)), CAST(N'2025-05-14T12:34:24.217' AS DateTime))
INSERT [dbo].[DriverInsuranceHistory] ([HistoryID], [DriverID], [PolicyID], [Year], [HadAccident], [KBM], [LastUpdated]) VALUES (7, 2002, 2002, 2025, 0, CAST(1.00 AS Decimal(4, 2)), CAST(N'2025-05-14T12:34:24.217' AS DateTime))
INSERT [dbo].[DriverInsuranceHistory] ([HistoryID], [DriverID], [PolicyID], [Year], [HadAccident], [KBM], [LastUpdated]) VALUES (8, 1, 3005, 2025, 0, CAST(1.00 AS Decimal(4, 2)), CAST(N'2025-05-14T12:34:24.217' AS DateTime))
INSERT [dbo].[DriverInsuranceHistory] ([HistoryID], [DriverID], [PolicyID], [Year], [HadAccident], [KBM], [LastUpdated]) VALUES (9, 2002, 15014, 2025, 0, CAST(1.00 AS Decimal(4, 2)), CAST(N'2025-05-14T12:34:24.217' AS DateTime))
INSERT [dbo].[DriverInsuranceHistory] ([HistoryID], [DriverID], [PolicyID], [Year], [HadAccident], [KBM], [LastUpdated]) VALUES (10, 2002, 4009, 2025, 0, CAST(1.00 AS Decimal(4, 2)), CAST(N'2025-05-14T12:34:24.217' AS DateTime))
INSERT [dbo].[DriverInsuranceHistory] ([HistoryID], [DriverID], [PolicyID], [Year], [HadAccident], [KBM], [LastUpdated]) VALUES (11, 2002, 4010, 2025, 0, CAST(1.00 AS Decimal(4, 2)), CAST(N'2025-05-14T12:34:24.217' AS DateTime))
INSERT [dbo].[DriverInsuranceHistory] ([HistoryID], [DriverID], [PolicyID], [Year], [HadAccident], [KBM], [LastUpdated]) VALUES (12, 3002, 4010, 2025, 0, CAST(1.00 AS Decimal(4, 2)), CAST(N'2025-05-14T12:34:24.217' AS DateTime))
INSERT [dbo].[DriverInsuranceHistory] ([HistoryID], [DriverID], [PolicyID], [Year], [HadAccident], [KBM], [LastUpdated]) VALUES (13, 4002, 5009, 2025, 0, CAST(1.00 AS Decimal(4, 2)), CAST(N'2025-05-14T12:34:24.217' AS DateTime))
INSERT [dbo].[DriverInsuranceHistory] ([HistoryID], [DriverID], [PolicyID], [Year], [HadAccident], [KBM], [LastUpdated]) VALUES (14, 1, 6009, 2025, 0, CAST(1.00 AS Decimal(4, 2)), CAST(N'2025-05-14T12:34:24.217' AS DateTime))
INSERT [dbo].[DriverInsuranceHistory] ([HistoryID], [DriverID], [PolicyID], [Year], [HadAccident], [KBM], [LastUpdated]) VALUES (15, 4002, 6009, 2025, 0, CAST(1.00 AS Decimal(4, 2)), CAST(N'2025-05-14T12:34:24.217' AS DateTime))
INSERT [dbo].[DriverInsuranceHistory] ([HistoryID], [DriverID], [PolicyID], [Year], [HadAccident], [KBM], [LastUpdated]) VALUES (16, 1, 7009, 2005, 0, CAST(1.00 AS Decimal(4, 2)), CAST(N'2025-05-14T12:34:24.217' AS DateTime))
INSERT [dbo].[DriverInsuranceHistory] ([HistoryID], [DriverID], [PolicyID], [Year], [HadAccident], [KBM], [LastUpdated]) VALUES (17, 5002, 10009, 2005, 0, CAST(1.00 AS Decimal(4, 2)), CAST(N'2025-05-14T12:34:24.217' AS DateTime))
INSERT [dbo].[DriverInsuranceHistory] ([HistoryID], [DriverID], [PolicyID], [Year], [HadAccident], [KBM], [LastUpdated]) VALUES (2004, 8003, 12009, 2025, 0, CAST(1.17 AS Decimal(4, 2)), CAST(N'2025-05-15T22:54:32.060' AS DateTime))
INSERT [dbo].[DriverInsuranceHistory] ([HistoryID], [DriverID], [PolicyID], [Year], [HadAccident], [KBM], [LastUpdated]) VALUES (2005, 3002, 12009, 2025, 0, CAST(1.00 AS Decimal(4, 2)), CAST(N'2025-05-15T22:55:22.413' AS DateTime))
INSERT [dbo].[DriverInsuranceHistory] ([HistoryID], [DriverID], [PolicyID], [Year], [HadAccident], [KBM], [LastUpdated]) VALUES (2006, 3, 15014, 2025, 0, CAST(1.00 AS Decimal(4, 2)), CAST(N'2025-05-15T22:55:22.417' AS DateTime))
INSERT [dbo].[DriverInsuranceHistory] ([HistoryID], [DriverID], [PolicyID], [Year], [HadAccident], [KBM], [LastUpdated]) VALUES (2008, 8005, 12010, 2025, 0, CAST(1.17 AS Decimal(4, 2)), CAST(N'2025-05-15T22:59:53.527' AS DateTime))
INSERT [dbo].[DriverInsuranceHistory] ([HistoryID], [DriverID], [PolicyID], [Year], [HadAccident], [KBM], [LastUpdated]) VALUES (2009, 3002, 12013, 2025, 0, CAST(1.00 AS Decimal(4, 2)), CAST(N'2025-05-15T23:02:13.477' AS DateTime))
INSERT [dbo].[DriverInsuranceHistory] ([HistoryID], [DriverID], [PolicyID], [Year], [HadAccident], [KBM], [LastUpdated]) VALUES (2010, 5002, 12011, 2025, 0, CAST(1.00 AS Decimal(4, 2)), CAST(N'2025-05-15T23:02:13.477' AS DateTime))
INSERT [dbo].[DriverInsuranceHistory] ([HistoryID], [DriverID], [PolicyID], [Year], [HadAccident], [KBM], [LastUpdated]) VALUES (2011, 2, 15016, 2025, 0, CAST(1.00 AS Decimal(4, 2)), CAST(N'2025-05-15T23:02:13.477' AS DateTime))
INSERT [dbo].[DriverInsuranceHistory] ([HistoryID], [DriverID], [PolicyID], [Year], [HadAccident], [KBM], [LastUpdated]) VALUES (2012, 8006, 12011, 2025, 0, CAST(1.17 AS Decimal(4, 2)), CAST(N'2025-05-15T23:02:13.480' AS DateTime))
INSERT [dbo].[DriverInsuranceHistory] ([HistoryID], [DriverID], [PolicyID], [Year], [HadAccident], [KBM], [LastUpdated]) VALUES (2013, 8007, 12012, 2025, 1, CAST(1.17 AS Decimal(4, 2)), CAST(N'2025-05-15T23:08:09.870' AS DateTime))
INSERT [dbo].[DriverInsuranceHistory] ([HistoryID], [DriverID], [PolicyID], [Year], [HadAccident], [KBM], [LastUpdated]) VALUES (2014, 8008, 13014, 2025, 1, CAST(1.17 AS Decimal(4, 2)), CAST(N'2025-05-25T14:21:37.767' AS DateTime))
INSERT [dbo].[DriverInsuranceHistory] ([HistoryID], [DriverID], [PolicyID], [Year], [HadAccident], [KBM], [LastUpdated]) VALUES (3014, 10008, 14015, 2025, 0, CAST(1.17 AS Decimal(4, 2)), CAST(N'2025-05-27T13:26:37.807' AS DateTime))
SET IDENTITY_INSERT [dbo].[DriverInsuranceHistory] OFF
GO
SET IDENTITY_INSERT [dbo].[Drivers] ON 

INSERT [dbo].[Drivers] ([DriverID], [ClientID], [LastName], [FirstName], [MiddleName], [DateOfBirth], [LicenseNumber], [DrivingExperience]) VALUES (1, 1, N'Иванов', N'Иван', N'Иванович', CAST(N'1985-03-15' AS Date), N'АБ12345678', 10)
INSERT [dbo].[Drivers] ([DriverID], [ClientID], [LastName], [FirstName], [MiddleName], [DateOfBirth], [LicenseNumber], [DrivingExperience]) VALUES (2, NULL, N'Смирнов', N'Алексей', N'Иванович', CAST(N'1990-07-22' AS Date), N'ВГ98765432', 5)
INSERT [dbo].[Drivers] ([DriverID], [ClientID], [LastName], [FirstName], [MiddleName], [DateOfBirth], [LicenseNumber], [DrivingExperience]) VALUES (3, NULL, N'Козлова', N'Елена', N'Сергеевна', CAST(N'1978-11-30' AS Date), N'КЛ45678912', 15)
INSERT [dbo].[Drivers] ([DriverID], [ClientID], [LastName], [FirstName], [MiddleName], [DateOfBirth], [LicenseNumber], [DrivingExperience]) VALUES (1002, 3, N'Сидоров', N'Иван', N'Иваныч', CAST(N'2025-01-01' AS Date), N'235234534', 12)
INSERT [dbo].[Drivers] ([DriverID], [ClientID], [LastName], [FirstName], [MiddleName], [DateOfBirth], [LicenseNumber], [DrivingExperience]) VALUES (2002, 2, N'Иванов', N'Сидр', N'Уваныч', CAST(N'2024-12-30' AS Date), N'23423523', 12)
INSERT [dbo].[Drivers] ([DriverID], [ClientID], [LastName], [FirstName], [MiddleName], [DateOfBirth], [LicenseNumber], [DrivingExperience]) VALUES (3002, 2003, N'Ия', N'яя', N'Я', CAST(N'2025-02-26' AS Date), N'452345234', 12)
INSERT [dbo].[Drivers] ([DriverID], [ClientID], [LastName], [FirstName], [MiddleName], [DateOfBirth], [LicenseNumber], [DrivingExperience]) VALUES (4002, 3003, N'text', N'test', N'Ts', CAST(N'2025-01-28' AS Date), N'345457456', 41)
INSERT [dbo].[Drivers] ([DriverID], [ClientID], [LastName], [FirstName], [MiddleName], [DateOfBirth], [LicenseNumber], [DrivingExperience]) VALUES (5002, 4003, N'test12', N'test3', N'teset3', CAST(N'2005-05-12' AS Date), N'12341234', 12)
INSERT [dbo].[Drivers] ([DriverID], [ClientID], [LastName], [FirstName], [MiddleName], [DateOfBirth], [LicenseNumber], [DrivingExperience]) VALUES (6002, 5004, N'taet', N'set', N'tsrts', CAST(N'2025-04-29' AS Date), N'234', 1)
INSERT [dbo].[Drivers] ([DriverID], [ClientID], [LastName], [FirstName], [MiddleName], [DateOfBirth], [LicenseNumber], [DrivingExperience]) VALUES (7002, 6003, N'asdfasdfa', N'sdfasdf', N'asdfasdfa', CAST(N'2025-04-30' AS Date), N'2342', 21)
INSERT [dbo].[Drivers] ([DriverID], [ClientID], [LastName], [FirstName], [MiddleName], [DateOfBirth], [LicenseNumber], [DrivingExperience]) VALUES (7003, 6004, N'a', N'a', N'a', CAST(N'2025-04-29' AS Date), N'a', 2)
INSERT [dbo].[Drivers] ([DriverID], [ClientID], [LastName], [FirstName], [MiddleName], [DateOfBirth], [LicenseNumber], [DrivingExperience]) VALUES (7004, 6004, N's', N's', N's', CAST(N'2025-04-29' AS Date), N'2', 2)
INSERT [dbo].[Drivers] ([DriverID], [ClientID], [LastName], [FirstName], [MiddleName], [DateOfBirth], [LicenseNumber], [DrivingExperience]) VALUES (7005, 6004, N't', N't', N't', CAST(N'2025-04-29' AS Date), N'21', 2)
INSERT [dbo].[Drivers] ([DriverID], [ClientID], [LastName], [FirstName], [MiddleName], [DateOfBirth], [LicenseNumber], [DrivingExperience]) VALUES (8002, 7003, N'hgf', N'fgh', N'fgh', CAST(N'2025-04-29' AS Date), N'54', 1)
INSERT [dbo].[Drivers] ([DriverID], [ClientID], [LastName], [FirstName], [MiddleName], [DateOfBirth], [LicenseNumber], [DrivingExperience]) VALUES (8003, 7004, N'yrt', N'ryt', N'ryt', CAST(N'2025-04-29' AS Date), N'45', 1)
INSERT [dbo].[Drivers] ([DriverID], [ClientID], [LastName], [FirstName], [MiddleName], [DateOfBirth], [LicenseNumber], [DrivingExperience]) VALUES (8004, 7004, N'tryr', N'tyrt', N'yrty', CAST(N'2025-04-28' AS Date), N'2341231', 3)
INSERT [dbo].[Drivers] ([DriverID], [ClientID], [LastName], [FirstName], [MiddleName], [DateOfBirth], [LicenseNumber], [DrivingExperience]) VALUES (8005, 7005, N'zzz', N'zzz', N'zzz', CAST(N'2025-04-29' AS Date), N'7856785', 12)
INSERT [dbo].[Drivers] ([DriverID], [ClientID], [LastName], [FirstName], [MiddleName], [DateOfBirth], [LicenseNumber], [DrivingExperience]) VALUES (8006, 7007, N'не', N'не', N'не', CAST(N'2025-04-29' AS Date), N'некн', 3)
INSERT [dbo].[Drivers] ([DriverID], [ClientID], [LastName], [FirstName], [MiddleName], [DateOfBirth], [LicenseNumber], [DrivingExperience]) VALUES (8007, 7008, N'hhh', N'hhh', N'hh', CAST(N'1999-10-06' AS Date), N'4523234', 12)
INSERT [dbo].[Drivers] ([DriverID], [ClientID], [LastName], [FirstName], [MiddleName], [DateOfBirth], [LicenseNumber], [DrivingExperience]) VALUES (8008, 7010, N'asda', N'asdas', N'asdas', CAST(N'1990-06-13' AS Date), N'sads', 12)
INSERT [dbo].[Drivers] ([DriverID], [ClientID], [LastName], [FirstName], [MiddleName], [DateOfBirth], [LicenseNumber], [DrivingExperience]) VALUES (9008, 8010, N'ыфваф', N'фывафыв', N'фывафыв', CAST(N'1989-05-10' AS Date), N'ФЫ 12 234556', 12)
INSERT [dbo].[Drivers] ([DriverID], [ClientID], [LastName], [FirstName], [MiddleName], [DateOfBirth], [LicenseNumber], [DrivingExperience]) VALUES (10008, 9010, N'ИЫВА', N'ЫВАИ', N'ВАПЫ', CAST(N'2000-02-17' AS Date), N'ФЫ 12 123456', 1)
SET IDENTITY_INSERT [dbo].[Drivers] OFF
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([EmployeeID], [RoleID], [ManagerID], [LastName], [FirstName], [MiddleName], [Phone], [Email], [Password]) VALUES (1, 1, NULL, N'Сидоров', N'Александр', N'Иванович', N'+79123456783', N'admin@vsk.ru', N'admin1')
INSERT [dbo].[Employees] ([EmployeeID], [RoleID], [ManagerID], [LastName], [FirstName], [MiddleName], [Phone], [Email], [Password]) VALUES (2, 2, NULL, N'Петров', N'Игорь', N'Васильевич', N'+79123454679', N'manager@vsk.ru', N'manager123')
INSERT [dbo].[Employees] ([EmployeeID], [RoleID], [ManagerID], [LastName], [FirstName], [MiddleName], [Phone], [Email], [Password]) VALUES (3, 3, 2, N'Иванова', N'Елена', N'Сергеевна', N'+79123456580', N'agent1@vsk.ru', N'agent12')
INSERT [dbo].[Employees] ([EmployeeID], [RoleID], [ManagerID], [LastName], [FirstName], [MiddleName], [Phone], [Email], [Password]) VALUES (4, 3, 2, N'Смирнова', N'Ольга', N'Алексеевна', N'+79123456581', N'agent2@vsk.ru', N'agent12')
INSERT [dbo].[Employees] ([EmployeeID], [RoleID], [ManagerID], [LastName], [FirstName], [MiddleName], [Phone], [Email], [Password]) VALUES (5002, 3, NULL, N'test', N'test', N'test', N'8948498', N'dalv5002@yandex.ru', N'test1')
INSERT [dbo].[Employees] ([EmployeeID], [RoleID], [ManagerID], [LastName], [FirstName], [MiddleName], [Phone], [Email], [Password]) VALUES (6006, 3, NULL, N'тест', N'тест', N'тест', N'89264891568', N'test@test.cm', N'test1')
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
SET IDENTITY_INSERT [dbo].[HealthConditions] ON 

INSERT [dbo].[HealthConditions] ([HealthConditionID], [ConditionName]) VALUES (1, N'Здоров')
INSERT [dbo].[HealthConditions] ([HealthConditionID], [ConditionName]) VALUES (2, N'Хронические заболевания')
INSERT [dbo].[HealthConditions] ([HealthConditionID], [ConditionName]) VALUES (3, N'Инвалидность')
SET IDENTITY_INSERT [dbo].[HealthConditions] OFF
GO
SET IDENTITY_INSERT [dbo].[LifeAndHealth] ON 

INSERT [dbo].[LifeAndHealth] ([LifeAndHealthID], [PolicyID], [Age], [Gender], [HealthConditionID], [Occupation]) VALUES (1, 4, 35, N'Мужской', 1, N'Инженер')
INSERT [dbo].[LifeAndHealth] ([LifeAndHealthID], [PolicyID], [Age], [Gender], [HealthConditionID], [Occupation]) VALUES (2, 2005, 14, N'Мужской', 1, N'evth')
INSERT [dbo].[LifeAndHealth] ([LifeAndHealthID], [PolicyID], [Age], [Gender], [HealthConditionID], [Occupation]) VALUES (1002, 3006, 23, N'Мужской', 2, N'ывапывап')
INSERT [dbo].[LifeAndHealth] ([LifeAndHealthID], [PolicyID], [Age], [Gender], [HealthConditionID], [Occupation]) VALUES (1003, 3009, 43, N'Мужской', 2, N'grstg')
INSERT [dbo].[LifeAndHealth] ([LifeAndHealthID], [PolicyID], [Age], [Gender], [HealthConditionID], [Occupation]) VALUES (2003, 7010, 12, N'Мужской', 1, N'1212')
INSERT [dbo].[LifeAndHealth] ([LifeAndHealthID], [PolicyID], [Age], [Gender], [HealthConditionID], [Occupation]) VALUES (2004, 7012, 12, N'Женский', 2, N'1212')
INSERT [dbo].[LifeAndHealth] ([LifeAndHealthID], [PolicyID], [Age], [Gender], [HealthConditionID], [Occupation]) VALUES (3003, 8009, 23, N'Мужской', 1, N'123123')
INSERT [dbo].[LifeAndHealth] ([LifeAndHealthID], [PolicyID], [Age], [Gender], [HealthConditionID], [Occupation]) VALUES (3004, 8010, 12, N'Мужской', 2, N'12')
INSERT [dbo].[LifeAndHealth] ([LifeAndHealthID], [PolicyID], [Age], [Gender], [HealthConditionID], [Occupation]) VALUES (4003, 12014, 23, N'Мужской', 1, N'234234')
INSERT [dbo].[LifeAndHealth] ([LifeAndHealthID], [PolicyID], [Age], [Gender], [HealthConditionID], [Occupation]) VALUES (5003, 13015, 12, N'Мужской', 1, N'123123')
INSERT [dbo].[LifeAndHealth] ([LifeAndHealthID], [PolicyID], [Age], [Gender], [HealthConditionID], [Occupation]) VALUES (6003, 14016, 50, N'Мужской', 1, N'ghdfghd')
INSERT [dbo].[LifeAndHealth] ([LifeAndHealthID], [PolicyID], [Age], [Gender], [HealthConditionID], [Occupation]) VALUES (6004, 14017, 54, N'Мужской', 1, N'роап6овепнг6')
INSERT [dbo].[LifeAndHealth] ([LifeAndHealthID], [PolicyID], [Age], [Gender], [HealthConditionID], [Occupation]) VALUES (6005, 14018, 21, N'Мужской', 1, N'132421')
INSERT [dbo].[LifeAndHealth] ([LifeAndHealthID], [PolicyID], [Age], [Gender], [HealthConditionID], [Occupation]) VALUES (6006, 14019, 12, N'Мужской', 1, N'авпвапр')
INSERT [dbo].[LifeAndHealth] ([LifeAndHealthID], [PolicyID], [Age], [Gender], [HealthConditionID], [Occupation]) VALUES (7003, 17016, 23, N'Женский', 3, N'Шахтер')
INSERT [dbo].[LifeAndHealth] ([LifeAndHealthID], [PolicyID], [Age], [Gender], [HealthConditionID], [Occupation]) VALUES (7004, 17018, 12, N'Женский', 1, N'Работа')
INSERT [dbo].[LifeAndHealth] ([LifeAndHealthID], [PolicyID], [Age], [Gender], [HealthConditionID], [Occupation]) VALUES (7005, 17021, 25, N'Мужской', 1, N'Таксист')
SET IDENTITY_INSERT [dbo].[LifeAndHealth] OFF
GO
SET IDENTITY_INSERT [dbo].[Logs] ON 

INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (1, N'Policies', N'Insert', N'PolicyID: 1, Type: ОСАГО', CAST(N'2025-01-01T10:00:00.000' AS DateTime), N'agent1@vsk.ru', 3)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (2, N'Policies', N'Insert', N'PolicyID: 2, Type: КАСКО', CAST(N'2025-02-01T10:00:00.000' AS DateTime), N'agent1@vsk.ru', 3)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (3, N'Policies', N'Insert', N'PolicyID: 3, Type: Страхование имущества', CAST(N'2025-03-01T10:00:00.000' AS DateTime), N'agent2@vsk.ru', 4)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (4, N'Policies', N'Insert', N'PolicyID: 4, Type: Страхование жизни', CAST(N'2025-04-01T10:00:00.000' AS DateTime), N'agent1@vsk.ru', 3)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (1002, N'Drivers', N'Добавление', N'Добавлен водитель: Сидоров Иван Иваныч', CAST(N'2025-03-19T18:37:31.690' AS DateTime), N'System', NULL)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (1003, N'Policies', N'Добавление', N'Добавлен полис: 1002 для клиента 1', CAST(N'2025-03-19T18:38:25.877' AS DateTime), N'System', NULL)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (1004, N'Vehicles', N'Добавление', N'Добавлен автомобиль: 1002 для полиса 1002', CAST(N'2025-03-19T18:38:25.883' AS DateTime), N'System', NULL)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (2002, N'Drivers', N'Добавление', N'Добавлен водитель: Иванов Сидр Уваныч', CAST(N'2025-03-21T14:58:39.320' AS DateTime), N'System', NULL)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (2003, N'Clients', N'Добавление', N'Добавлен клиент: Иванов Сидр Уваныч', CAST(N'2025-03-21T14:58:39.330' AS DateTime), N'System', NULL)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (2004, N'Policies', N'Добавление', N'Добавлен полис: 2002 для клиента 2', CAST(N'2025-03-21T14:58:45.217' AS DateTime), N'System', NULL)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (2005, N'Vehicles', N'Добавление', N'Добавлен автомобиль: 2002 для полиса 2002', CAST(N'2025-03-21T14:58:45.223' AS DateTime), N'System', NULL)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (2006, N'Policies', N'Добавление', N'Добавлен полис: 2003 для клиента 1', CAST(N'2025-03-21T15:13:29.950' AS DateTime), N'System', NULL)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (2007, N'Policies', N'Добавление', N'Добавлен полис: 2004 для клиента 2', CAST(N'2025-03-21T15:22:50.810' AS DateTime), N'System', NULL)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (2008, N'Policies', N'Добавление', N'Добавлен полис: 2005 с 1 клиентами', CAST(N'2025-03-21T16:25:16.720' AS DateTime), N'System', NULL)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (2009, N'LifeAndHealth', N'Добавление', N'Добавлена запись для полиса: 2005 с состоянием здоровья 1', CAST(N'2025-03-21T16:25:16.737' AS DateTime), N'System', NULL)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (3002, N'Policies', N'Добавление', N'Добавлен полис: 3005 с 2 клиентами и 1 водителями', CAST(N'2025-03-27T11:57:51.397' AS DateTime), N'System', NULL)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (3003, N'Vehicles', N'Добавление', N'Добавлен автомобиль: 3002 для полиса 3005', CAST(N'2025-03-27T11:57:51.413' AS DateTime), N'System', NULL)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (3004, N'Policies', N'Добавление', N'Добавлен полис: 3006 с 1 клиентами', CAST(N'2025-03-27T11:58:19.500' AS DateTime), N'System', NULL)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (3005, N'LifeAndHealth', N'Добавление', N'Добавлена запись для полиса: 3006 с состоянием здоровья 2', CAST(N'2025-03-27T11:58:19.507' AS DateTime), N'System', NULL)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (3006, N'Policies', N'Добавление', N'Добавлен полис: 3007 с 1 клиентами и 2 объектами имущества', CAST(N'2025-03-27T12:27:28.623' AS DateTime), N'System', NULL)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (3007, N'Policies', N'Добавление', N'Добавлен полис: 3008 с 1 клиентами и 1 объектами имущества', CAST(N'2025-03-27T12:34:10.693' AS DateTime), N'agent2@vsk.ru', 4)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (3008, N'Policies', N'Добавление', N'Добавлен полис: 3009 с 1 клиентами', CAST(N'2025-03-27T12:39:42.463' AS DateTime), N'agent2@vsk.ru', 4)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (3009, N'LifeAndHealth', N'Добавление', N'Добавлена запись для полиса: 3009 с состоянием здоровья 2', CAST(N'2025-03-27T12:39:42.480' AS DateTime), N'agent2@vsk.ru', 4)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (3010, N'Clients', N'Добавление', N'Добавлен клиент: 1003', CAST(N'2025-03-27T12:42:38.463' AS DateTime), N'agent2@vsk.ru', 4)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (3011, N'Policies', N'Добавление', N'Добавлен полис: 3010 с 1 клиентами и 1 водителями', CAST(N'2025-03-27T12:43:19.497' AS DateTime), N'agent2@vsk.ru', 4)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (3012, N'Vehicles', N'Добавление', N'Добавлен автомобиль: 3003 для полиса 3010', CAST(N'2025-03-27T12:43:19.503' AS DateTime), N'agent2@vsk.ru', 4)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (3013, N'Employees', N'Изменение', N'Сотрудник Иванова Елена Сергеевна изменил пароль.', CAST(N'2025-03-27T13:55:22.570' AS DateTime), N'agent1@vsk.ru', 3)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (4007, N'Policies', N'Добавление', N'Добавлен полис: 4009 с 2 клиентами и 1 водителями', CAST(N'2025-03-30T12:24:57.360' AS DateTime), N'agent2@vsk.ru', 4)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (4008, N'Vehicles', N'Добавление', N'Добавлен автомобиль: 4003 для полиса 4009', CAST(N'2025-03-30T12:24:57.377' AS DateTime), N'agent2@vsk.ru', 4)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (4009, N'Clients', N'Добавление', N'Добавлен клиент: Ия яя Я', CAST(N'2025-03-30T12:55:29.800' AS DateTime), N'agent1@vsk.ru', 3)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (4010, N'Drivers', N'Добавление', N'Добавлен водитель: Ия яя Я', CAST(N'2025-03-30T12:55:29.810' AS DateTime), N'agent1@vsk.ru', 3)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (4011, N'Policies', N'Добавление', N'Добавлен полис: 4010 с 2 клиентами и 2 водителями', CAST(N'2025-03-30T12:55:35.223' AS DateTime), N'agent1@vsk.ru', 3)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (4012, N'Vehicles', N'Добавление', N'Добавлен автомобиль: 4004 для полиса 4010', CAST(N'2025-03-30T12:55:35.237' AS DateTime), N'agent1@vsk.ru', 3)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (5007, N'Clients', N'Добавление', N'Добавлен клиент: text test Ts', CAST(N'2025-03-31T16:30:53.450' AS DateTime), N'agent2@vsk.ru', 4)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (5008, N'Drivers', N'Добавление', N'Добавлен водитель: text test Ts', CAST(N'2025-03-31T16:30:53.463' AS DateTime), N'agent2@vsk.ru', 4)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (5009, N'Policies', N'Добавление', N'Добавлен полис: 5009 с 2 клиентами и 1 водителями', CAST(N'2025-03-31T16:31:02.343' AS DateTime), N'agent2@vsk.ru', 4)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (5010, N'Vehicles', N'Добавление', N'Добавлен автомобиль: 5003 для полиса 5009', CAST(N'2025-03-31T16:31:02.353' AS DateTime), N'agent2@vsk.ru', 4)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (6007, N'Employees', N'Изменение', N'Сотрудник Смирнова Ольга Алексеевна изменил пароль.', CAST(N'2025-04-07T19:04:40.503' AS DateTime), N'agent2@vsk.ru', 4)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (7007, N'Employees', N'Авторизация', N'Сотрудник Иванова Елена Сергеевна (ID: 3) вошёл в систему', CAST(N'2025-04-28T11:31:20.030' AS DateTime), N'agent1@vsk.ru', 3)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (7008, N'Employees', N'Авторизация', N'Сотрудник Иванова Елена Сергеевна (ID: 3) вошёл в систему', CAST(N'2025-04-28T11:33:23.623' AS DateTime), N'agent1@vsk.ru', 3)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (7009, N'Employees', N'Авторизация', N'Сотрудник Сидоров Александр Иванович (ID: 1) вошёл в систему', CAST(N'2025-04-28T11:33:51.847' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (7010, N'Employees', N'Авторизация', N'Сотрудник Сидоров Александр Иванович (ID: 1) вошёл в систему', CAST(N'2025-04-28T11:35:24.517' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21012, N'Policies', N'Добавление', N'Добавлен полис: 12009', CAST(N'2025-05-15T22:55:22.430' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21013, N'Vehicles', N'Добавление', N'Добавлен автомобиль: 11003', CAST(N'2025-05-15T22:55:22.433' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21014, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-15T22:57:25.080' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21015, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-15T22:58:57.143' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21016, N'Clients', N'Добавление', N'Добавлен клиент: zzz zzz zzz', CAST(N'2025-05-15T22:59:13.543' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21017, N'Drivers', N'Добавление', N'Добавлен водитель: zzz zzz zzz', CAST(N'2025-05-15T22:59:13.553' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21018, N'Clients', N'Добавление', N'Добавлен клиент: 7006', CAST(N'2025-05-15T22:59:36.083' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21019, N'Policies', N'Добавление', N'Добавлен полис: 12010', CAST(N'2025-05-15T22:59:53.540' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21020, N'Vehicles', N'Добавление', N'Добавлен автомобиль: 11004', CAST(N'2025-05-15T22:59:53.540' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21021, N'Clients', N'Добавление', N'Добавлен клиент: не не не', CAST(N'2025-05-15T23:02:09.183' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21022, N'Drivers', N'Добавление', N'Добавлен водитель: не не не', CAST(N'2025-05-15T23:02:09.187' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21023, N'Policies', N'Добавление', N'Добавлен полис: 12011', CAST(N'2025-05-15T23:02:13.483' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21024, N'Vehicles', N'Добавление', N'Добавлен автомобиль: 11005', CAST(N'2025-05-15T23:02:13.483' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21025, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-15T23:07:23.013' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21026, N'Clients', N'Добавление', N'Добавлен клиент: hhh hhh hh', CAST(N'2025-05-15T23:08:04.280' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21027, N'Drivers', N'Добавление', N'Добавлен водитель: hhh hhh hh', CAST(N'2025-05-15T23:08:04.293' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21028, N'Policies', N'Добавление', N'Добавлен полис: 12012', CAST(N'2025-05-15T23:08:09.887' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21029, N'Vehicles', N'Добавление', N'Добавлен автомобиль: 11006', CAST(N'2025-05-15T23:08:09.887' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21030, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-15T23:17:33.440' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21031, N'Policies', N'Добавление', N'Добавлен полис: 12013', CAST(N'2025-05-15T23:18:20.597' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21032, N'Vehicles', N'Добавление', N'Добавлен автомобиль: 11007', CAST(N'2025-05-15T23:18:20.600' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21033, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-15T23:28:43.487' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21034, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-15T23:34:50.160' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21035, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-15T23:35:32.477' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21036, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-15T23:39:02.653' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21037, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-15T23:39:22.527' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21038, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-15T23:45:07.497' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21039, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-15T23:46:48.887' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21040, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-15T23:48:02.247' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21041, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-15T23:48:29.130' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21042, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-15T23:48:56.100' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21043, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-15T23:49:16.810' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21044, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-15T23:49:41.510' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21045, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-15T23:50:19.410' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (21046, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-15T23:52:04.257' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22040, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-20T11:11:13.697' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22041, N'Employees', N'Авторизация', N'Сотрудник Сидоров Александр Иванович (ID: 1) вошёл в систему', CAST(N'2025-05-20T11:19:29.247' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22042, N'Employees', N'Авторизация', N'Сотрудник Сидоров Александр Иванович (ID: 1) вошёл в систему', CAST(N'2025-05-20T11:22:33.153' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22043, N'Employees', N'Добавление', N'Добавлен сотрудник: dfs sfd sdf', CAST(N'2025-05-20T11:22:41.863' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22044, N'Employees', N'Добавление', N'Добавлен сотрудник: fsadf sdf asdfs', CAST(N'2025-05-20T11:22:46.870' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22045, N'Employees', N'Удаление', N'Удалён сотрудник с ID: 6003, ФИО: fsadf sdf asdfs', CAST(N'2025-05-20T11:22:51.763' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22046, N'Employees', N'Удаление', N'Удалён сотрудник с ID: 6002, ФИО: dfs sfd sdf', CAST(N'2025-05-20T11:22:51.777' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22047, N'Employees', N'Удаление (успешно)', N'Удалено 2 сотрудника(ов) в 20.05.2025 11:22:51', CAST(N'2025-05-20T11:22:51.780' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22048, N'Employees', N'Авторизация', N'Сотрудник Сидоров Александр Иванович (ID: 1) вошёл в систему', CAST(N'2025-05-20T11:24:04.127' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22049, N'Employees', N'Добавление', N'Добавлен сотрудник: sdf sadf sadf', CAST(N'2025-05-20T11:24:15.813' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22050, N'Employees', N'Удаление', N'Удалён сотрудник с ID: 6004, ФИО: sdf sadf sadf', CAST(N'2025-05-20T11:24:22.427' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22051, N'Employees', N'Удаление (успешно)', N'Удалено 1 сотрудника(ов) в 20.05.2025 11:24:22', CAST(N'2025-05-20T11:24:22.437' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22052, N'Employees', N'Авторизация', N'Сотрудник Сидоров Александр Иванович (ID: 1) вошёл в систему', CAST(N'2025-05-20T11:29:07.267' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22053, N'Employees', N'Авторизация', N'Сотрудник Сидоров Александр Иванович (ID: 1) вошёл в систему', CAST(N'2025-05-20T11:33:49.970' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22054, N'VehicleMakes', N'Удаление', N'Удалён бренд: ID=2003, Название=test2', CAST(N'2025-05-20T11:33:55.020' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22055, N'VehicleMakes', N'Удаление (успешно)', N'Удалено 1 бренда(ов) в 20.05.2025 11:33:55', CAST(N'2025-05-20T11:33:55.037' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22056, N'Employees', N'Авторизация', N'Сотрудник Сидоров Александр Иванович (ID: 1) вошёл в систему', CAST(N'2025-05-20T11:35:02.043' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22057, N'VehicleMakes', N'Удаление', N'Удалён бренд: ID=2002, Название=testik', CAST(N'2025-05-20T11:35:10.233' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22058, N'VehicleMakes', N'Удаление (успешно)', N'Удалено 1 бренда(ов) в 20.05.2025 11:35:10', CAST(N'2025-05-20T11:35:10.250' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22059, N'VehicleModels', N'Удаление', N'Удалена модель: ID=2003, Название=testik, Бренд=', CAST(N'2025-05-20T11:35:19.080' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22060, N'VehicleModels', N'Удаление (успешно)', N'Удалено 1 модели(ей) в 20.05.2025 11:35:19', CAST(N'2025-05-20T11:35:19.087' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22061, N'PropertyTypes', N'Удаление', N'Удалён тип недвижимости: ID=2002, Название=test1', CAST(N'2025-05-20T11:35:22.857' AS DateTime), N'admin@vsk.ru', 1)
GO
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22062, N'PropertyTypes', N'Удаление (успешно)', N'Удалено 1 типа(ов) недвижимости в 20.05.2025 11:35:22', CAST(N'2025-05-20T11:35:22.863' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22063, N'HealthConditions', N'Удаление', N'Удалено состояние здоровья: ID=2002, Название=test1', CAST(N'2025-05-20T11:35:28.280' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22064, N'HealthConditions', N'Удаление', N'Удалено состояние здоровья: ID=1002, Название=test', CAST(N'2025-05-20T11:35:28.293' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22065, N'HealthConditions', N'Удаление (успешно)', N'Удалено 2 состояния(ий) здоровья в 20.05.2025 11:35:28', CAST(N'2025-05-20T11:35:28.297' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22066, N'Employees', N'Авторизация', N'Сотрудник Сидоров Александр Иванович (ID: 1) вошёл в систему', CAST(N'2025-05-20T11:55:51.820' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22067, N'VehicleModels', N'Удаление', N'Удалена модель: ID=2002, Название=test, Бренд=', CAST(N'2025-05-20T11:55:59.390' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22068, N'VehicleModels', N'Удаление (успешно)', N'Удалено 1 модели(ей) в 20.05.2025 11:55:59', CAST(N'2025-05-20T11:55:59.403' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22069, N'Employees', N'Авторизация', N'Сотрудник Сидоров Александр Иванович (ID: 1) вошёл в систему', CAST(N'2025-05-20T12:08:50.967' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22070, N'VehicleModels', N'Удаление', N'Удалена модель: ID=1003, Название=Largus, Бренд=', CAST(N'2025-05-20T12:08:58.607' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22071, N'VehicleModels', N'Удаление (успешно)', N'Удалено 1 модели(ей) в 20.05.2025 12:08:58', CAST(N'2025-05-20T12:08:58.627' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22072, N'PropertyTypes', N'Удаление', N'Удалён тип недвижимости: ID=1002, Название=test', CAST(N'2025-05-20T12:09:29.967' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22073, N'PropertyTypes', N'Удаление (успешно)', N'Удалено 1 типа(ов) недвижимости в 20.05.2025 12:09:29', CAST(N'2025-05-20T12:09:29.970' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22074, N'VehicleMakes', N'Удаление', N'Удалён бренд: ID=2004, Название=tset3', CAST(N'2025-05-20T12:09:41.903' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22075, N'VehicleMakes', N'Удаление (успешно)', N'Удалено 1 бренда(ов) в 20.05.2025 12:09:41', CAST(N'2025-05-20T12:09:41.910' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22076, N'VehicleMakes', N'Удаление', N'Удалён бренд: ID=1005, Название=test', CAST(N'2025-05-20T12:09:46.643' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22077, N'VehicleMakes', N'Удаление (успешно)', N'Удалено 1 бренда(ов) в 20.05.2025 12:09:46', CAST(N'2025-05-20T12:09:46.650' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22078, N'Employees', N'Изменение', N'Сотрудник Сидоров Александр Иванович изменил пароль.', CAST(N'2025-05-20T12:10:02.257' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22079, N'VehicleMakes', N'Добавление', N'Добавлен бренд: fgfd', CAST(N'2025-05-20T12:10:10.103' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22080, N'Employees', N'Авторизация', N'Сотрудник Сидоров Александр Иванович (ID: 1) вошёл в систему', CAST(N'2025-05-20T12:11:33.003' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22081, N'VehicleMakes', N'Удаление', N'Удалён бренд: ID=3002, Название=fgfd', CAST(N'2025-05-20T12:11:41.613' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22082, N'VehicleMakes', N'Удаление (успешно)', N'Удалено 1 бренда(ов) в 20.05.2025 12:11:41', CAST(N'2025-05-20T12:11:41.627' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22083, N'VehicleMakes', N'Добавление', N'Добавлен бренд: hgjghj', CAST(N'2025-05-20T12:11:56.473' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22084, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-20T12:12:16.257' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22085, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-20T12:13:46.203' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22086, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-20T12:15:07.007' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22087, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-20T12:17:14.490' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22088, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-20T12:18:20.237' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22089, N'Policies', N'Добавление', N'Добавлен полис: 12014 с 1 клиентами', CAST(N'2025-05-20T12:18:39.977' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22090, N'LifeAndHealth', N'Добавление', N'Добавлена запись для полиса: 12014 с состоянием здоровья 1', CAST(N'2025-05-20T12:18:39.983' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22091, N'Employees', N'Авторизация', N'Сотрудник Сидоров Александр Иванович (ID: 1) вошёл в систему', CAST(N'2025-05-20T12:43:18.397' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22092, N'Employees', N'Добавление', N'Добавлен сотрудник: dfgh dfgh dfghd', CAST(N'2025-05-20T12:43:25.337' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22093, N'VehicleMakes', N'Добавление', N'Добавлен бренд: dfghdfgh', CAST(N'2025-05-20T12:43:48.503' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22094, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-20T12:44:43.997' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22095, N'Employees', N'Авторизация', N'Сотрудник Сидоров Александр Иванович (ID: 1) вошёл в систему', CAST(N'2025-05-20T12:46:50.550' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22096, N'VehicleModels', N'Добавление', N'Добавлена модель: fgfdgh бренда hgjghj', CAST(N'2025-05-20T12:47:00.930' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22097, N'VehicleModels', N'Удаление', N'Удалена модель: ID=3002, Название=fgfdgh, Бренд=', CAST(N'2025-05-20T12:47:13.790' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22098, N'VehicleModels', N'Удаление (успешно)', N'Удалено 1 модели(ей) в 20.05.2025 12:47:13', CAST(N'2025-05-20T12:47:13.800' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (22099, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-20T12:48:21.403' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (23040, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-21T14:46:52.773' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (23041, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-21T14:48:04.767' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (23042, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-21T14:48:33.733' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (24040, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-21T15:55:42.893' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (24041, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-21T16:11:58.467' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (24042, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-21T16:13:03.363' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (25040, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-25T12:19:54.363' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (26040, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-25T12:36:42.020' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (26041, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-25T12:40:15.657' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (26042, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-25T12:45:28.070' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (26043, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-25T12:49:23.653' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (27040, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-25T12:58:59.313' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (28040, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-25T13:31:00.443' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (29040, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-25T13:39:32.810' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (29041, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-25T13:40:29.850' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (29042, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-25T13:41:49.217' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (29043, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-25T13:46:12.297' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (29044, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-25T13:51:10.997' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (29045, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-25T13:54:20.097' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (29046, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-25T13:58:59.523' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (29047, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-25T14:00:27.333' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (29048, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-25T14:01:05.110' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (29049, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-25T14:02:33.923' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (29050, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-25T14:06:13.073' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (30040, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-25T14:13:19.823' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (30041, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-25T14:16:33.410' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (30042, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-25T14:17:18.500' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (30043, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-25T14:18:40.840' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (30044, N'Clients', N'Добавление', N'Добавлен клиент: 7009', CAST(N'2025-05-25T14:19:08.633' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (30045, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-25T14:20:37.550' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (30046, N'Clients', N'Добавление', N'Добавлен клиент: asda asdas asdas', CAST(N'2025-05-25T14:21:32.793' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (30047, N'Drivers', N'Добавление', N'Добавлен водитель: asda asdas asdas', CAST(N'2025-05-25T14:21:32.803' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (30048, N'Policies', N'Добавление', N'Добавлен полис: 13014', CAST(N'2025-05-25T14:21:37.783' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (30049, N'Vehicles', N'Добавление', N'Добавлен автомобиль: 11008', CAST(N'2025-05-25T14:21:37.783' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (30050, N'Policies', N'Добавление', N'Добавлен полис: 13015 с 1 клиентами', CAST(N'2025-05-25T14:22:44.667' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (30051, N'LifeAndHealth', N'Добавление', N'Добавлена запись для полиса: 13015 с состоянием здоровья 1', CAST(N'2025-05-25T14:22:44.677' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (30052, N'Policies', N'Добавление', N'Добавлен полис: 13016 с 1 клиентами и 1 объектами имущества', CAST(N'2025-05-25T14:22:59.753' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (31040, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-25T21:28:06.533' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (31041, N'Clients', N'Добавление', N'Добавлен клиент: 8009', CAST(N'2025-05-25T21:29:33.927' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (31042, N'Clients', N'Добавление', N'Добавлен клиент: ыфваф фывафыв фывафыв', CAST(N'2025-05-25T21:36:23.217' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (31043, N'Drivers', N'Добавление', N'Добавлен водитель: ыфваф фывафыв фывафыв', CAST(N'2025-05-25T21:36:23.230' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32040, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T13:06:05.793' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32041, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T13:19:53.193' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32042, N'Policies', N'Добавление', N'Добавлен полис: 14014', CAST(N'2025-05-27T13:20:48.547' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32043, N'Vehicles', N'Добавление', N'Добавлен автомобиль: 12008', CAST(N'2025-05-27T13:20:48.550' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32044, N'Clients', N'Добавление', N'Добавлен клиент: 9009', CAST(N'2025-05-27T13:24:23.780' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32045, N'Clients', N'Добавление', N'Добавлен клиент: ИЫВА ЫВАИ ВАПЫ', CAST(N'2025-05-27T13:26:33.307' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32046, N'Drivers', N'Добавление', N'Добавлен водитель: ИЫВА ЫВАИ ВАПЫ', CAST(N'2025-05-27T13:26:33.320' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32047, N'Policies', N'Добавление', N'Добавлен полис: 14015', CAST(N'2025-05-27T13:26:37.813' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32048, N'Vehicles', N'Добавление', N'Добавлен автомобиль: 12009', CAST(N'2025-05-27T13:26:37.817' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32049, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T13:50:40.930' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32050, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T14:01:25.513' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32051, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T14:09:19.713' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32052, N'Policies', N'Добавление', N'Добавлен полис: 14016 с 1 клиентами', CAST(N'2025-05-27T14:09:49.990' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32053, N'LifeAndHealth', N'Добавление', N'Добавлена запись для полиса: 14016 с состоянием здоровья 1', CAST(N'2025-05-27T14:09:50.003' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32054, N'Clients', N'Добавление', N'Добавлен клиент: 9011', CAST(N'2025-05-27T14:11:22.890' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32055, N'Policies', N'Добавление', N'Добавлен полис: 14017 с 1 клиентами', CAST(N'2025-05-27T14:11:48.450' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32056, N'LifeAndHealth', N'Добавление', N'Добавлена запись для полиса: 14017 с состоянием здоровья 1', CAST(N'2025-05-27T14:11:48.457' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32057, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T14:12:49.923' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32058, N'Policies', N'Добавление', N'Добавлен полис: 14018 с 1 клиентами', CAST(N'2025-05-27T14:13:14.473' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32059, N'LifeAndHealth', N'Добавление', N'Добавлена запись для полиса: 14018 с состоянием здоровья 1', CAST(N'2025-05-27T14:13:14.483' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32060, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T14:34:28.940' AS DateTime), N'dalv5002@yandex.ru', 5002)
GO
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32061, N'Clients', N'Добавление', N'Добавлен клиент: 9012', CAST(N'2025-05-27T14:35:05.900' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32062, N'Policies', N'Добавление', N'Добавлен полис: 14019 с 1 клиентами', CAST(N'2025-05-27T14:35:32.023' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32063, N'LifeAndHealth', N'Добавление', N'Добавлена запись для полиса: 14019 с состоянием здоровья 1', CAST(N'2025-05-27T14:35:32.037' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32064, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T14:41:06.277' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32065, N'Clients', N'Добавление', N'Добавлен клиент: 9013', CAST(N'2025-05-27T14:41:43.757' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32066, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T14:45:08.007' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32067, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T14:51:44.873' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32068, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T14:53:38.000' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32069, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T15:00:19.110' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32070, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T15:13:36.643' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32071, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T15:17:29.283' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32072, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T15:23:35.743' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32073, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T15:27:42.010' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32074, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T15:33:55.110' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32075, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T15:42:54.570' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32076, N'Policies', N'Добавление', N'Добавлен полис: 14020 с 1 клиентами и 1 объектами имущества', CAST(N'2025-05-27T15:43:18.327' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32077, N'Employees', N'Авторизация', N'Сотрудник Сидоров Александр Иванович (ID: 1) вошёл в систему', CAST(N'2025-05-27T15:44:16.127' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32078, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T15:49:53.220' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32079, N'Policies', N'Добавление', N'Добавлен полис: 14021 с 1 клиентами и 1 объектами имущества', CAST(N'2025-05-27T15:50:33.867' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32080, N'Properties', N'Добавление', N'Добавлена запись для полиса: 14021 с 1 объектами имущества', CAST(N'2025-05-27T15:50:33.870' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32081, N'Employees', N'Авторизация', N'Сотрудник Сидоров Александр Иванович (ID: 1) вошёл в систему', CAST(N'2025-05-27T15:53:01.460' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (32082, N'Employees', N'Изменение', N'Сотрудник Сидоров Александр Иванович изменил пароль.', CAST(N'2025-05-27T15:53:15.583' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33040, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T18:16:07.110' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33041, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T18:22:24.700' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33042, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T18:32:53.983' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33043, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T18:36:13.533' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33044, N'Policies', N'Добавление', N'Добавлен полис: 15014', CAST(N'2025-05-27T18:37:01.017' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33045, N'Vehicles', N'Добавление', N'Добавлен автомобиль: 13008', CAST(N'2025-05-27T18:37:01.020' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33046, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T18:44:55.237' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33047, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T18:48:18.627' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33048, N'Policies', N'Добавление', N'Добавлен полис: 15015', CAST(N'2025-05-27T18:48:58.420' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33049, N'Vehicles', N'Добавление', N'Добавлен автомобиль: 13009', CAST(N'2025-05-27T18:48:58.427' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33050, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T18:51:35.617' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33051, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T18:57:40.683' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33052, N'Policies', N'Добавление', N'Добавлен полис: 15016', CAST(N'2025-05-27T18:58:22.310' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33053, N'Vehicles', N'Добавление', N'Добавлен автомобиль: 13010', CAST(N'2025-05-27T18:58:22.317' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33054, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T19:13:07.267' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33055, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T19:14:34.600' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33056, N'Policies', N'Редактирование', N'Отредактирован полис: 3008', CAST(N'2025-05-27T19:15:15.153' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33057, N'Properties', N'Редактирование', N'Обновлены записи имущества для полиса: 3008', CAST(N'2025-05-27T19:15:15.157' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33058, N'Policies', N'Редактирование', N'Отредактирован полис: 14021', CAST(N'2025-05-27T19:15:38.613' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33059, N'Properties', N'Редактирование', N'Обновлены записи имущества для полиса: 14021', CAST(N'2025-05-27T19:15:38.617' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33060, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T19:25:07.630' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33061, N'Policies', N'Редактирование', N'Полис 3006 обновлён', CAST(N'2025-05-27T19:25:35.093' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33062, N'LifeAndHealth', N'Редактирование', N'Обновлена запись для полиса: 3006', CAST(N'2025-05-27T19:25:35.113' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33063, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T19:31:24.740' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33064, N'Policies', N'Редактирование', N'Полис 15016 обновлён.', CAST(N'2025-05-27T19:32:08.293' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33065, N'Vehicles', N'Редактирование', N'Обновлены данные ТС для полиса 15016', CAST(N'2025-05-27T19:32:08.323' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33066, N'Policies', N'Редактирование', N'Полис 15016 обновлён.', CAST(N'2025-05-27T19:32:41.147' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33067, N'Vehicles', N'Редактирование', N'Обновлены данные ТС для полиса 15016', CAST(N'2025-05-27T19:32:41.157' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33068, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T19:35:13.803' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33069, N'Policies', N'Добавление', N'Добавлен полис: 15017', CAST(N'2025-05-27T19:35:53.560' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33070, N'Vehicles', N'Добавление', N'Добавлен автомобиль для полиса 15017', CAST(N'2025-05-27T19:35:53.567' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33071, N'Policies', N'Редактирование', N'Полис 15017 обновлён.', CAST(N'2025-05-27T19:36:17.543' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33072, N'Vehicles', N'Редактирование', N'Обновлены данные ТС для полиса 15017', CAST(N'2025-05-27T19:36:17.557' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33073, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T19:45:04.293' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33074, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T19:46:37.963' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33075, N'Policies', N'Добавление', N'Добавлен полис: 15018 с 1 клиентами и 1 объектами имущества', CAST(N'2025-05-27T19:47:22.240' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33076, N'Properties', N'Добавление', N'Добавлена запись для полиса: 15018 с 1 объектами имущества', CAST(N'2025-05-27T19:47:22.243' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33077, N'Policies', N'Редактирование', N'Отредактирован полис: 15018', CAST(N'2025-05-27T19:47:32.423' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33078, N'Properties', N'Редактирование', N'Обновлены записи имущества для полиса: 15018', CAST(N'2025-05-27T19:47:32.427' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33079, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T19:50:10.077' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33080, N'Policies', N'Добавление', N'Добавлен полис: 15019 с 1 клиентами и 1 объектами имущества', CAST(N'2025-05-27T19:50:51.137' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (33081, N'Properties', N'Добавление', N'Добавлена запись для полиса: 15019 с 1 объектами имущества', CAST(N'2025-05-27T19:50:51.140' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (34040, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T23:44:03.323' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (34041, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-27T23:49:52.820' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (34042, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T00:12:50.037' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (34043, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T00:17:37.323' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (34044, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T00:26:46.237' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (34045, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T00:31:15.213' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (34046, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T00:32:48.487' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (34047, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T00:34:25.030' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (34048, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T00:44:59.687' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (34049, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T00:52:11.043' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (34050, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T00:53:32.890' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (34051, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T00:55:43.687' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (34052, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T00:59:53.090' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (34053, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T01:04:53.440' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (34054, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T01:09:58.750' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (34055, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T01:12:24.313' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (34056, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T01:18:05.707' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (34057, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T01:22:32.097' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (34058, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T01:27:00.057' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (34059, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T01:28:18.090' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (34060, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T01:32:38.603' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (34061, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T01:35:36.340' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (34062, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T01:39:27.590' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (34063, N'Policies', N'Добавление', N'Добавлен полис: 16016', CAST(N'2025-05-28T01:40:46.543' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (34064, N'Vehicles', N'Добавление', N'Добавлен автомобиль для полиса 16016', CAST(N'2025-05-28T01:40:46.553' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (34065, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T01:44:24.937' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (34066, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T01:46:43.537' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (34067, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T01:49:09.253' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (35040, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T10:31:20.260' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (35041, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T10:34:27.427' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (35042, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T10:42:10.673' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (35043, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T10:43:31.547' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (35044, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T10:50:57.433' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (35045, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T10:53:29.217' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (35046, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T10:58:52.487' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (35047, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T11:01:46.093' AS DateTime), N'dalv5002@yandex.ru', 5002)
GO
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (35048, N'Policies', N'Редактирование', N'Полис 14014 обновлён.', CAST(N'2025-05-28T11:03:02.843' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (35049, N'Vehicles', N'Редактирование', N'Обновлены данные ТС для полиса 14014', CAST(N'2025-05-28T11:03:02.867' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (35050, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T11:20:30.933' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (35051, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T11:22:05.237' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (35052, N'Policies', N'Добавление', N'Добавлен полис: 17016', CAST(N'2025-05-28T11:22:30.777' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (35053, N'LifeAndHealth', N'Добавление', N'Добавлена запись для полиса: 17016', CAST(N'2025-05-28T11:22:30.787' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (35054, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T11:28:28.057' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (35055, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T11:32:35.737' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (35056, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T11:42:56.973' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (35057, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T11:45:21.587' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (35058, N'Employees', N'Восстановление пароля', N'Сотруднику test test test (ID: 5002) отправлен код на dalv5002@yandex.ru', CAST(N'2025-05-28T11:48:00.533' AS DateTime), N'System', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (35059, N'Employees', N'Изменение пароля', N'Сотрудник test test test (ID: 5002) изменил пароль', CAST(N'2025-05-28T11:48:25.117' AS DateTime), N'System', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (35060, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T12:04:15.457' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (35061, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T12:10:21.197' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (35062, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T12:13:26.800' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (35063, N'Policies', N'Редактирование', N'Отредактирован полис: 15018', CAST(N'2025-05-28T12:14:30.433' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (35064, N'Properties', N'Редактирование', N'Обновлены записи имущества для полиса: 15018', CAST(N'2025-05-28T12:14:30.440' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (35065, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T12:16:55.607' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (35066, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T12:22:40.490' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (35067, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T12:29:32.047' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (35068, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T12:35:59.563' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (36040, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T19:20:10.240' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (36041, N'Employees', N'Авторизация', N'Сотрудник Иванова Елена Сергеевна (ID: 3) вошёл в систему', CAST(N'2025-05-28T19:20:52.793' AS DateTime), N'agent1@vsk.ru', 3)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (36042, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-28T19:24:24.977' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (36043, N'Employees', N'Авторизация', N'Сотрудник Петров Игорь Васильевич (ID: 2) вошёл в систему', CAST(N'2025-05-28T19:25:15.960' AS DateTime), N'manager@vsk.ru', 2)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37040, N'Employees', N'Авторизация', N'Сотрудник Сидоров Александр Иванович (ID: 1) вошёл в систему', CAST(N'2025-05-29T13:00:56.497' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37041, N'VehicleMakes', N'Удаление', N'Удалён бренд: ID=3004, Название=dfghdfgh', CAST(N'2025-05-29T13:02:52.823' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37042, N'VehicleMakes', N'Удаление (успешно)', N'Удалено 1 бренда(ов) в 29.05.2025 13:02:52', CAST(N'2025-05-29T13:02:52.840' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37043, N'VehicleMakes', N'Удаление', N'Удалён бренд: ID=3003, Название=hgjghj', CAST(N'2025-05-29T13:03:03.433' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37044, N'VehicleMakes', N'Удаление (успешно)', N'Удалено 1 бренда(ов) в 29.05.2025 13:03:03', CAST(N'2025-05-29T13:03:03.443' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37045, N'Employees', N'Авторизация', N'Сотрудник Петров Игорь Васильевич (ID: 2) вошёл в систему', CAST(N'2025-05-29T13:14:30.960' AS DateTime), N'manager@vsk.ru', 2)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37046, N'Employees', N'Авторизация', N'Сотрудник Петров Игорь Васильевич (ID: 2) вошёл в систему', CAST(N'2025-05-29T13:16:03.613' AS DateTime), N'manager@vsk.ru', 2)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37047, N'Employees', N'Авторизация', N'Сотрудник Петров Игорь Васильевич (ID: 2) вошёл в систему', CAST(N'2025-05-29T13:16:43.177' AS DateTime), N'manager@vsk.ru', 2)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37048, N'Employees', N'Авторизация', N'Сотрудник Петров Игорь Васильевич (ID: 2) вошёл в систему', CAST(N'2025-05-29T13:17:15.727' AS DateTime), N'manager@vsk.ru', 2)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37049, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-29T13:20:38.193' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37050, N'Employees', N'Авторизация', N'Сотрудник Петров Игорь Васильевич (ID: 2) вошёл в систему', CAST(N'2025-05-29T13:21:08.353' AS DateTime), N'manager@vsk.ru', 2)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37051, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-29T13:24:51.427' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37052, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-29T13:24:56.317' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37053, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-29T13:25:35.283' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37054, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-29T13:26:49.073' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37055, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-29T13:28:38.273' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37056, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-29T13:29:25.280' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37057, N'Employees', N'Авторизация', N'Сотрудник Петров Игорь Васильевич (ID: 2) вошёл в систему', CAST(N'2025-05-29T13:30:04.427' AS DateTime), N'manager@vsk.ru', 2)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37058, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-30T14:16:29.797' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37059, N'Policies', N'Добавление', N'Добавлен полис: 17017', CAST(N'2025-05-30T14:17:27.110' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37060, N'Vehicles', N'Добавление', N'Добавлен автомобиль для полиса 17017', CAST(N'2025-05-30T14:17:27.120' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37061, N'Clients', N'Добавление', N'Добавлен клиент: 9014', CAST(N'2025-05-30T14:18:24.807' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37062, N'Policies', N'Добавление', N'Добавлен полис: 17018', CAST(N'2025-05-30T14:18:55.850' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37063, N'LifeAndHealth', N'Добавление', N'Добавлена запись для полиса: 17018', CAST(N'2025-05-30T14:18:55.857' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37064, N'Policies', N'Добавление', N'Добавлен полис: 17019 с 1 клиентами и 1 объектами имущества', CAST(N'2025-05-30T14:19:33.333' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37065, N'Properties', N'Добавление', N'Добавлена запись для полиса: 17019 с 1 объектами имущества', CAST(N'2025-05-30T14:19:33.340' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37066, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-05-30T14:20:20.710' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37067, N'Employees', N'Авторизация', N'Сотрудник Петров Игорь Васильевич (ID: 2) вошёл в систему', CAST(N'2025-05-30T14:22:25.347' AS DateTime), N'manager@vsk.ru', 2)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37068, N'Policies', N'Добавление', N'Добавлен полис: 17020', CAST(N'2025-05-30T15:09:16.647' AS DateTime), N'manager@vsk.ru', 2)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37069, N'Vehicles', N'Добавление', N'Добавлен автомобиль для полиса 17020', CAST(N'2025-05-30T15:09:16.650' AS DateTime), N'manager@vsk.ru', 2)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37070, N'Policies', N'Добавление', N'Добавлен полис: 17021', CAST(N'2025-05-30T15:12:42.370' AS DateTime), N'manager@vsk.ru', 2)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37071, N'LifeAndHealth', N'Добавление', N'Добавлена запись для полиса: 17021', CAST(N'2025-05-30T15:12:42.377' AS DateTime), N'manager@vsk.ru', 2)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37072, N'Policies', N'Добавление', N'Добавлен полис: 17022 с 1 клиентами и 1 объектами имущества', CAST(N'2025-05-30T15:15:30.813' AS DateTime), N'manager@vsk.ru', 2)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37073, N'Properties', N'Добавление', N'Добавлена запись для полиса: 17022 с 1 объектами имущества', CAST(N'2025-05-30T15:15:30.817' AS DateTime), N'manager@vsk.ru', 2)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37074, N'Policies', N'Редактирование', N'Полис 17020 обновлён.', CAST(N'2025-05-30T15:24:13.437' AS DateTime), N'manager@vsk.ru', 2)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37075, N'Vehicles', N'Редактирование', N'Обновлены данные ТС для полиса 17020', CAST(N'2025-05-30T15:24:13.450' AS DateTime), N'manager@vsk.ru', 2)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37076, N'Employees', N'Авторизация', N'Сотрудник Сидоров Александр Иванович (ID: 1) вошёл в систему', CAST(N'2025-05-30T15:34:54.573' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37077, N'Employees', N'Удаление', N'Удалён сотрудник с ID: 6005, ФИО: dfgh dfgh dfghd', CAST(N'2025-05-30T15:38:32.947' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37078, N'Employees', N'Удаление (успешно)', N'Удалено 1 сотрудника(ов) в 30.05.2025 15:38:32', CAST(N'2025-05-30T15:38:32.967' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37079, N'Employees', N'Добавление', N'Добавлен сотрудник: тест тест тест', CAST(N'2025-05-30T15:40:29.113' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37080, N'VehicleModels', N'Добавление', N'Добавлена модель: Largus бренда LADA', CAST(N'2025-05-30T15:43:22.107' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37081, N'HealthConditions', N'Добавление', N'Добавлено состояние здоровья: тест', CAST(N'2025-05-30T15:57:28.113' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37082, N'HealthConditions', N'Удаление', N'Удалено состояние здоровья: ID=4, Название=тест', CAST(N'2025-05-30T15:57:34.973' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (37083, N'HealthConditions', N'Удаление (успешно)', N'Удалено 1 состояния(ий) здоровья в 30.05.2025 15:57:34', CAST(N'2025-05-30T15:57:34.983' AS DateTime), N'admin@vsk.ru', 1)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (38058, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-06-03T11:14:01.440' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (38059, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-06-03T11:14:43.130' AS DateTime), N'dalv5002@yandex.ru', 5002)
INSERT [dbo].[Logs] ([LogID], [TableName], [Action], [ChangedData], [ChangeDate], [UserName], [EmployeeID]) VALUES (39058, N'Employees', N'Авторизация', N'Сотрудник test test test (ID: 5002) вошёл в систему', CAST(N'2025-06-13T11:20:44.887' AS DateTime), N'dalv5002@yandex.ru', 5002)
SET IDENTITY_INSERT [dbo].[Logs] OFF
GO
SET IDENTITY_INSERT [dbo].[Policies] ON 

INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (1, 1, 1, CAST(5000.00 AS Decimal(15, 2)), CAST(N'2025-01-01' AS Date), CAST(N'2026-01-01' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (2, 2, 1, CAST(15000.00 AS Decimal(15, 2)), CAST(N'2025-02-01' AS Date), CAST(N'2026-02-01' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (3, 3, 1, CAST(20000.00 AS Decimal(15, 2)), CAST(N'2025-03-01' AS Date), CAST(N'2026-03-01' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (4, 4, 1, CAST(10000.00 AS Decimal(15, 2)), CAST(N'2025-04-01' AS Date), CAST(N'2026-04-01' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (1002, 1, 1, CAST(12312.00 AS Decimal(15, 2)), CAST(N'2025-03-05' AS Date), CAST(N'2025-03-28' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (2002, 1, 1, CAST(1231.00 AS Decimal(15, 2)), CAST(N'2025-03-04' AS Date), CAST(N'2025-03-29' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (2003, 3, 1, CAST(123.00 AS Decimal(15, 2)), CAST(N'2025-03-05' AS Date), CAST(N'2025-03-30' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (2004, 4, 1, CAST(34234234.00 AS Decimal(15, 2)), CAST(N'2025-03-13' AS Date), CAST(N'2025-03-29' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (2005, 4, 1, CAST(34234.00 AS Decimal(15, 2)), CAST(N'2025-02-25' AS Date), CAST(N'2025-03-29' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (3005, 1, 1, CAST(234.00 AS Decimal(15, 2)), CAST(N'2025-03-05' AS Date), CAST(N'2025-03-28' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (3006, 4, 1, CAST(123421.00 AS Decimal(15, 2)), CAST(N'2025-05-27' AS Date), CAST(N'2025-08-01' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (3007, 3, 3, CAST(1231.00 AS Decimal(15, 2)), CAST(N'2025-02-26' AS Date), CAST(N'2025-04-04' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (3008, 3, 2, CAST(345.00 AS Decimal(15, 2)), CAST(N'2025-05-27' AS Date), CAST(N'2025-06-27' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (3009, 4, 2, CAST(123.00 AS Decimal(15, 2)), CAST(N'2025-03-06' AS Date), CAST(N'2025-03-30' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (3010, 2, 1, CAST(235252.00 AS Decimal(15, 2)), CAST(N'2025-03-04' AS Date), CAST(N'2025-03-30' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (4009, 1, 1, CAST(3245234.00 AS Decimal(15, 2)), CAST(N'2025-02-25' AS Date), CAST(N'2025-04-05' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (4010, 2, 1, CAST(3453245.00 AS Decimal(15, 2)), CAST(N'2025-03-05' AS Date), CAST(N'2025-04-05' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (5009, 1, 1, CAST(324.00 AS Decimal(15, 2)), CAST(N'2025-02-26' AS Date), CAST(N'2025-03-29' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (6009, 1, 1, CAST(124.00 AS Decimal(15, 2)), CAST(N'2025-05-06' AS Date), CAST(N'2025-05-30' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (7009, 1, 1, CAST(123123.00 AS Decimal(15, 2)), CAST(N'2005-12-12' AS Date), CAST(N'2005-12-13' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (7010, 3, 1, CAST(123412.00 AS Decimal(15, 2)), CAST(N'2005-02-12' AS Date), CAST(N'3005-03-12' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (7011, 4, 1, CAST(12.00 AS Decimal(15, 2)), CAST(N'2005-12-12' AS Date), CAST(N'2006-12-12' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (7012, 3, 1, CAST(2134.00 AS Decimal(15, 2)), CAST(N'2005-12-12' AS Date), CAST(N'2006-12-12' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (8009, 4, 1, CAST(12312.00 AS Decimal(15, 2)), CAST(N'2025-04-30' AS Date), CAST(N'2025-05-29' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (8010, 4, 1, CAST(123.00 AS Decimal(15, 2)), CAST(N'2005-12-12' AS Date), CAST(N'2025-05-23' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (9009, 1, 1, CAST(11479.55 AS Decimal(15, 2)), CAST(N'2025-05-05' AS Date), CAST(N'2025-05-22' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (10009, 1, 1, CAST(18554.47 AS Decimal(15, 2)), CAST(N'2025-05-07' AS Date), CAST(N'2026-05-07' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (11009, 1, 1, CAST(33770.16 AS Decimal(15, 2)), CAST(N'2025-04-30' AS Date), CAST(N'2025-06-06' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (12009, 1, 1, CAST(33770.16 AS Decimal(15, 2)), CAST(N'2025-05-05' AS Date), CAST(N'2025-05-23' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (12010, 1, 1, CAST(19514.27 AS Decimal(15, 2)), CAST(N'2025-04-29' AS Date), CAST(N'2025-06-07' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (12011, 1, 1, CAST(28363.88 AS Decimal(15, 2)), CAST(N'2025-05-08' AS Date), CAST(N'2025-05-31' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (12012, 2, 1, CAST(18449.86 AS Decimal(15, 2)), CAST(N'2025-05-05' AS Date), CAST(N'2025-06-07' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (12013, 1, 1, CAST(21834.15 AS Decimal(15, 2)), CAST(N'2025-04-28' AS Date), CAST(N'2025-05-29' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (12014, 4, 1, CAST(234.00 AS Decimal(15, 2)), CAST(N'2025-05-01' AS Date), CAST(N'2025-05-21' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (13014, 1, 1, CAST(34274.67 AS Decimal(15, 2)), CAST(N'2025-05-06' AS Date), CAST(N'2025-05-22' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (13015, 4, 1, CAST(1231.00 AS Decimal(15, 2)), CAST(N'2025-04-29' AS Date), CAST(N'2025-05-21' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (13016, 3, 1, CAST(1231.00 AS Decimal(15, 2)), CAST(N'2025-04-28' AS Date), CAST(N'2025-05-26' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (14014, 1, 1, CAST(26632.56 AS Decimal(15, 2)), CAST(N'2025-05-28' AS Date), CAST(N'2025-06-05' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (14015, 1, 1, CAST(30829.98 AS Decimal(15, 2)), CAST(N'2025-05-27' AS Date), CAST(N'2025-06-06' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (14016, 4, 1, CAST(1500.00 AS Decimal(15, 2)), CAST(N'2025-05-27' AS Date), CAST(N'2025-06-05' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (14017, 4, 1, CAST(1235.00 AS Decimal(15, 2)), CAST(N'2025-05-26' AS Date), CAST(N'2025-06-07' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (14018, 4, 1, CAST(321.00 AS Decimal(15, 2)), CAST(N'2025-05-27' AS Date), CAST(N'2025-06-06' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (14019, 4, 1, CAST(23141.00 AS Decimal(15, 2)), CAST(N'2025-05-28' AS Date), CAST(N'2025-06-07' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (14020, 3, 1, CAST(12312.00 AS Decimal(15, 2)), CAST(N'2025-05-27' AS Date), CAST(N'2025-06-07' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (14021, 3, 3, CAST(1231.00 AS Decimal(15, 2)), CAST(N'2025-05-27' AS Date), CAST(N'2025-06-04' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (15014, 1, 1, CAST(29294.59 AS Decimal(15, 2)), CAST(N'2025-05-27' AS Date), CAST(N'2025-06-05' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (15015, 1, 1, CAST(26887.28 AS Decimal(15, 2)), CAST(N'2025-05-27' AS Date), CAST(N'2025-06-07' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (15016, 1, 1, CAST(29294.59 AS Decimal(15, 2)), CAST(N'2025-05-27' AS Date), CAST(N'2025-06-13' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (15017, 1, 2, CAST(26632.56 AS Decimal(15, 2)), CAST(N'2025-05-27' AS Date), CAST(N'2025-07-05' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (15018, 3, 1, CAST(123412.00 AS Decimal(15, 2)), CAST(N'2025-05-28' AS Date), CAST(N'2025-09-19' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (15019, 3, 1, CAST(234532.00 AS Decimal(15, 2)), CAST(N'2025-05-27' AS Date), CAST(N'2025-06-04' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (16016, 1, 1, CAST(42803.67 AS Decimal(15, 2)), CAST(N'2025-05-28' AS Date), CAST(N'2025-06-04' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (17016, 4, 1, CAST(16547.00 AS Decimal(15, 2)), CAST(N'2025-06-05' AS Date), CAST(N'2025-06-08' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (17017, 1, 1, CAST(20506.22 AS Decimal(15, 2)), CAST(N'2025-05-30' AS Date), CAST(N'2025-11-15' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (17018, 4, 1, CAST(1234.00 AS Decimal(15, 2)), CAST(N'2025-05-30' AS Date), CAST(N'2025-08-22' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (17019, 3, 1, CAST(654356.00 AS Decimal(15, 2)), CAST(N'2025-05-30' AS Date), CAST(N'2025-06-07' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (17020, 1, 1, CAST(29294.59 AS Decimal(15, 2)), CAST(N'2025-05-30' AS Date), CAST(N'2026-04-03' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (17021, 4, 1, CAST(12312.00 AS Decimal(15, 2)), CAST(N'2025-05-30' AS Date), CAST(N'2025-06-08' AS Date))
INSERT [dbo].[Policies] ([PolicyID], [PolicyTypeID], [StatusID], [InsuranceAmount], [StartDate], [EndDate]) VALUES (17022, 3, 1, CAST(12312.00 AS Decimal(15, 2)), CAST(N'2025-05-30' AS Date), CAST(N'2025-07-18' AS Date))
SET IDENTITY_INSERT [dbo].[Policies] OFF
GO
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (2005, 1)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (3005, 1)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (3005, 2)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (3006, 2)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (3007, 1002)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (3008, 2)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (3009, 1002)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (3010, 1003)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (4009, 2)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (4009, 1003)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (4010, 1002)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (4010, 2003)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (5009, 2003)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (5009, 3003)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (6009, 3003)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (7009, 2)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (7009, 4003)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (7010, 4003)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (7011, 4003)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (7012, 2)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (8009, 1)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (8010, 2)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (9009, 2003)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (10009, 1002)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (11009, 1)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (12009, 1002)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (12010, 7006)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (12011, 7006)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (12012, 3)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (12013, 5004)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (12014, 3)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (13014, 1002)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (13015, 2)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (13016, 1003)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (14014, 2)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (14015, 9009)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (14016, 2)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (14017, 9011)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (14018, 6004)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (14019, 9012)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (14020, 1003)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (14021, 1003)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (15014, 1002)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (15015, 3)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (15016, 1003)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (15017, 5003)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (15018, 1002)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (15019, 2003)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (16016, 7009)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (17016, 1)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (17017, 2)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (17018, 9014)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (17019, 1002)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (17020, 1)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (17021, 1002)
INSERT [dbo].[PolicyClients] ([PolicyID], [ClientID]) VALUES (17022, 2)
GO
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (1, 1)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (1, 2)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (2, 3)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (1002, 1)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (1002, 1002)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (2002, 1002)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (2002, 2002)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (3005, 1)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (3010, 2002)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (4009, 2002)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (4010, 2002)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (4010, 3002)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (5009, 4002)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (6009, 1)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (6009, 4002)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (7009, 1)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (7009, 5002)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (9009, 2)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (10009, 1)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (10009, 2)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (10009, 5002)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (11009, 2)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (11009, 3)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (11009, 2002)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (12009, 3)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (12009, 3002)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (12010, 8005)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (12011, 2)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (12011, 3002)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (12011, 5002)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (12011, 8006)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (12012, 8007)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (12013, 3002)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (13014, 1)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (13014, 3)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (13014, 8008)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (14014, 2)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (14014, 2002)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (14015, 1)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (14015, 2)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (14015, 10008)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (15014, 3)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (15014, 2002)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (15015, 2)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (15015, 1002)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (15016, 1)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (15016, 2)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (15017, 2)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (15017, 1002)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (15017, 5002)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (16016, 2)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (16016, 8006)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (16016, 8007)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (17017, 1)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (17017, 3)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (17017, 1002)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (17020, 1)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (17020, 2)
INSERT [dbo].[PolicyDrivers] ([PolicyID], [DriverID]) VALUES (17020, 2002)
GO
SET IDENTITY_INSERT [dbo].[PolicyStatuses] ON 

INSERT [dbo].[PolicyStatuses] ([StatusID], [StatusName]) VALUES (1, N'Активен')
INSERT [dbo].[PolicyStatuses] ([StatusID], [StatusName]) VALUES (2, N'Истек')
INSERT [dbo].[PolicyStatuses] ([StatusID], [StatusName]) VALUES (3, N'Отменен')
SET IDENTITY_INSERT [dbo].[PolicyStatuses] OFF
GO
SET IDENTITY_INSERT [dbo].[PolicyTypes] ON 

INSERT [dbo].[PolicyTypes] ([PolicyTypeID], [TypeName]) VALUES (1, N'ОСАГО')
INSERT [dbo].[PolicyTypes] ([PolicyTypeID], [TypeName]) VALUES (2, N'КАСКО')
INSERT [dbo].[PolicyTypes] ([PolicyTypeID], [TypeName]) VALUES (3, N'Страхование имущества')
INSERT [dbo].[PolicyTypes] ([PolicyTypeID], [TypeName]) VALUES (4, N'Страхование жизни')
SET IDENTITY_INSERT [dbo].[PolicyTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Properties] ON 

INSERT [dbo].[Properties] ([PropertyID], [PolicyID], [PropertyTypeID], [Address], [Area], [Value]) VALUES (1, 3, 1, N'г. Москва, ул. Ленина, д. 10, кв. 5', CAST(60.50 AS Decimal(10, 2)), CAST(5000000.00 AS Decimal(15, 2)))
INSERT [dbo].[Properties] ([PropertyID], [PolicyID], [PropertyTypeID], [Address], [Area], [Value]) VALUES (2, 3007, 1, N'rgsergs', CAST(23.00 AS Decimal(10, 2)), CAST(235235.00 AS Decimal(15, 2)))
INSERT [dbo].[Properties] ([PropertyID], [PolicyID], [PropertyTypeID], [Address], [Area], [Value]) VALUES (3, 3007, 3, N'пирваре', CAST(234.00 AS Decimal(10, 2)), CAST(654.00 AS Decimal(15, 2)))
INSERT [dbo].[Properties] ([PropertyID], [PolicyID], [PropertyTypeID], [Address], [Area], [Value]) VALUES (5, 7011, 1, N'2345234', CAST(523.00 AS Decimal(10, 2)), CAST(23452345.00 AS Decimal(15, 2)))
INSERT [dbo].[Properties] ([PropertyID], [PolicyID], [PropertyTypeID], [Address], [Area], [Value]) VALUES (1005, 13016, 1, N'123', CAST(123.00 AS Decimal(10, 2)), CAST(1231.00 AS Decimal(15, 2)))
INSERT [dbo].[Properties] ([PropertyID], [PolicyID], [PropertyTypeID], [Address], [Area], [Value]) VALUES (2005, 14020, 1, N'выапыв', CAST(123.00 AS Decimal(10, 2)), CAST(1234.00 AS Decimal(15, 2)))
INSERT [dbo].[Properties] ([PropertyID], [PolicyID], [PropertyTypeID], [Address], [Area], [Value]) VALUES (3005, 3008, 2, N'ывпквп', CAST(345.00 AS Decimal(10, 2)), CAST(2134.00 AS Decimal(15, 2)))
INSERT [dbo].[Properties] ([PropertyID], [PolicyID], [PropertyTypeID], [Address], [Area], [Value]) VALUES (3006, 14021, 1, N'вапывапв', CAST(1231.00 AS Decimal(10, 2)), CAST(1231.00 AS Decimal(15, 2)))
INSERT [dbo].[Properties] ([PropertyID], [PolicyID], [PropertyTypeID], [Address], [Area], [Value]) VALUES (3009, 15019, 1, N'выапвапыв', CAST(123123.00 AS Decimal(10, 2)), CAST(12312.00 AS Decimal(15, 2)))
INSERT [dbo].[Properties] ([PropertyID], [PolicyID], [PropertyTypeID], [Address], [Area], [Value]) VALUES (4005, 15018, 1, N'вапвап', CAST(1231.00 AS Decimal(10, 2)), CAST(123.00 AS Decimal(15, 2)))
INSERT [dbo].[Properties] ([PropertyID], [PolicyID], [PropertyTypeID], [Address], [Area], [Value]) VALUES (4006, 17019, 2, N'где-то здесь', CAST(123.00 AS Decimal(10, 2)), CAST(10000000.00 AS Decimal(15, 2)))
INSERT [dbo].[Properties] ([PropertyID], [PolicyID], [PropertyTypeID], [Address], [Area], [Value]) VALUES (4007, 17022, 1, N'тут есть', CAST(123.00 AS Decimal(10, 2)), CAST(12341234.00 AS Decimal(15, 2)))
SET IDENTITY_INSERT [dbo].[Properties] OFF
GO
SET IDENTITY_INSERT [dbo].[PropertyTypes] ON 

INSERT [dbo].[PropertyTypes] ([PropertyTypeID], [TypeName]) VALUES (1, N'Квартира')
INSERT [dbo].[PropertyTypes] ([PropertyTypeID], [TypeName]) VALUES (2, N'Дом')
INSERT [dbo].[PropertyTypes] ([PropertyTypeID], [TypeName]) VALUES (3, N'Гараж')
SET IDENTITY_INSERT [dbo].[PropertyTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (1, N'Администратор')
INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (2, N'Менеджер')
INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (3, N'Страховой агент')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[VehicleMakes] ON 

INSERT [dbo].[VehicleMakes] ([MakeID], [MakeName]) VALUES (1, N'Toyota')
INSERT [dbo].[VehicleMakes] ([MakeID], [MakeName]) VALUES (2, N'BMW')
INSERT [dbo].[VehicleMakes] ([MakeID], [MakeName]) VALUES (3, N'LADA')
INSERT [dbo].[VehicleMakes] ([MakeID], [MakeName]) VALUES (1006, N'test1')
SET IDENTITY_INSERT [dbo].[VehicleMakes] OFF
GO
SET IDENTITY_INSERT [dbo].[VehicleModels] ON 

INSERT [dbo].[VehicleModels] ([ModelID], [MakeID], [ModelName]) VALUES (1, 1, N'Camry')
INSERT [dbo].[VehicleModels] ([ModelID], [MakeID], [ModelName]) VALUES (2, 1, N'Corolla')
INSERT [dbo].[VehicleModels] ([ModelID], [MakeID], [ModelName]) VALUES (3, 2, N'X5')
INSERT [dbo].[VehicleModels] ([ModelID], [MakeID], [ModelName]) VALUES (4, 2, N'3 Series')
INSERT [dbo].[VehicleModels] ([ModelID], [MakeID], [ModelName]) VALUES (5, 3, N'Granta')
INSERT [dbo].[VehicleModels] ([ModelID], [MakeID], [ModelName]) VALUES (6, 3, N'Vesta')
INSERT [dbo].[VehicleModels] ([ModelID], [MakeID], [ModelName]) VALUES (1002, 1006, N'test2')
INSERT [dbo].[VehicleModels] ([ModelID], [MakeID], [ModelName]) VALUES (1003, 3, N'Largus')
SET IDENTITY_INSERT [dbo].[VehicleModels] OFF
GO
SET IDENTITY_INSERT [dbo].[Vehicles] ON 

INSERT [dbo].[Vehicles] ([VehicleID], [PolicyID], [ModelID], [VIN], [Year], [LicensePlate], [RegistrationRegion], [EnginePower]) VALUES (1, 1, 1, N'JTDBE40KX9J123456', 2019, N'А123БВ77', NULL, NULL)
INSERT [dbo].[Vehicles] ([VehicleID], [PolicyID], [ModelID], [VIN], [Year], [LicensePlate], [RegistrationRegion], [EnginePower]) VALUES (2, 2, 3, N'WBAXH5C5XCC123456', 2020, N'В456ГД77', NULL, NULL)
INSERT [dbo].[Vehicles] ([VehicleID], [PolicyID], [ModelID], [VIN], [Year], [LicensePlate], [RegistrationRegion], [EnginePower]) VALUES (1002, 1002, 2, N'апиравпр453', 1294, N'ыпвап234', NULL, NULL)
INSERT [dbo].[Vehicles] ([VehicleID], [PolicyID], [ModelID], [VIN], [Year], [LicensePlate], [RegistrationRegion], [EnginePower]) VALUES (2002, 2002, 4, N'dfgsdftg2342f23', 1231, N'fd234f2', NULL, NULL)
INSERT [dbo].[Vehicles] ([VehicleID], [PolicyID], [ModelID], [VIN], [Year], [LicensePlate], [RegistrationRegion], [EnginePower]) VALUES (3002, 3005, 3, N'ghdfhf5y', 2012, N'rge435', NULL, NULL)
INSERT [dbo].[Vehicles] ([VehicleID], [PolicyID], [ModelID], [VIN], [Year], [LicensePlate], [RegistrationRegion], [EnginePower]) VALUES (3003, 3010, 6, N'hdfgy4t5h4', 2014, N'sg4g43', NULL, NULL)
INSERT [dbo].[Vehicles] ([VehicleID], [PolicyID], [ModelID], [VIN], [Year], [LicensePlate], [RegistrationRegion], [EnginePower]) VALUES (4003, 4009, 1, N'sgdfgsdfgs234', 2019, N'gsd3234f', NULL, NULL)
INSERT [dbo].[Vehicles] ([VehicleID], [PolicyID], [ModelID], [VIN], [Year], [LicensePlate], [RegistrationRegion], [EnginePower]) VALUES (4004, 4010, 4, N'gdfg3', 2019, N'gfg234', NULL, NULL)
INSERT [dbo].[Vehicles] ([VehicleID], [PolicyID], [ModelID], [VIN], [Year], [LicensePlate], [RegistrationRegion], [EnginePower]) VALUES (5003, 5009, 1, N'hdfghd5', 2018, N'dfgh435h', NULL, NULL)
INSERT [dbo].[Vehicles] ([VehicleID], [PolicyID], [ModelID], [VIN], [Year], [LicensePlate], [RegistrationRegion], [EnginePower]) VALUES (6003, 6009, 1002, N'ertger243', 2019, N'fw3f', NULL, NULL)
INSERT [dbo].[Vehicles] ([VehicleID], [PolicyID], [ModelID], [VIN], [Year], [LicensePlate], [RegistrationRegion], [EnginePower]) VALUES (7003, 7009, 1002, N'hd5hd5h45h45h', 2019, N'23fwerf4', NULL, NULL)
INSERT [dbo].[Vehicles] ([VehicleID], [PolicyID], [ModelID], [VIN], [Year], [LicensePlate], [RegistrationRegion], [EnginePower]) VALUES (8003, 9009, 1, N'fghdfh', 2019, N'asdfsdf', NULL, NULL)
INSERT [dbo].[Vehicles] ([VehicleID], [PolicyID], [ModelID], [VIN], [Year], [LicensePlate], [RegistrationRegion], [EnginePower]) VALUES (9003, 10009, 5, N'fdhft56', 2010, N'gsd5g', NULL, NULL)
INSERT [dbo].[Vehicles] ([VehicleID], [PolicyID], [ModelID], [VIN], [Year], [LicensePlate], [RegistrationRegion], [EnginePower]) VALUES (10003, 11009, 3, N'bdfghbdf5', 2017, N'gsdg4', NULL, NULL)
INSERT [dbo].[Vehicles] ([VehicleID], [PolicyID], [ModelID], [VIN], [Year], [LicensePlate], [RegistrationRegion], [EnginePower]) VALUES (11003, 12009, 1, N'wer', 2010, N'rwe', NULL, NULL)
INSERT [dbo].[Vehicles] ([VehicleID], [PolicyID], [ModelID], [VIN], [Year], [LicensePlate], [RegistrationRegion], [EnginePower]) VALUES (11004, 12010, 1, N'dth5h', 2020, N'gs', NULL, NULL)
INSERT [dbo].[Vehicles] ([VehicleID], [PolicyID], [ModelID], [VIN], [Year], [LicensePlate], [RegistrationRegion], [EnginePower]) VALUES (11005, 12011, 1, N'1231', 2015, N'3453', NULL, NULL)
INSERT [dbo].[Vehicles] ([VehicleID], [PolicyID], [ModelID], [VIN], [Year], [LicensePlate], [RegistrationRegion], [EnginePower]) VALUES (11006, 12012, 3, N'34534rt', 2022, N'fsf', NULL, NULL)
INSERT [dbo].[Vehicles] ([VehicleID], [PolicyID], [ModelID], [VIN], [Year], [LicensePlate], [RegistrationRegion], [EnginePower]) VALUES (11007, 12013, 3, N'34534', 2001, N'fer', NULL, NULL)
INSERT [dbo].[Vehicles] ([VehicleID], [PolicyID], [ModelID], [VIN], [Year], [LicensePlate], [RegistrationRegion], [EnginePower]) VALUES (11008, 13014, 1, N'fdfghdfgh', 2020, N'dfasd', NULL, NULL)
INSERT [dbo].[Vehicles] ([VehicleID], [PolicyID], [ModelID], [VIN], [Year], [LicensePlate], [RegistrationRegion], [EnginePower]) VALUES (12008, 14014, 1, N'AFSEF321F231E2F1F', 2019, N'А213ЫВ323', N'Новосибирск', 125)
INSERT [dbo].[Vehicles] ([VehicleID], [PolicyID], [ModelID], [VIN], [Year], [LicensePlate], [RegistrationRegion], [EnginePower]) VALUES (12009, 14015, 3, N'FASEFA3R2231R1F2E', 2023, N'В235ПР765', NULL, NULL)
INSERT [dbo].[Vehicles] ([VehicleID], [PolicyID], [ModelID], [VIN], [Year], [LicensePlate], [RegistrationRegion], [EnginePower]) VALUES (13008, 15014, 1, N'SDFGSD4GDRGDRT34G', 2020, N'Ф123ИП345', NULL, NULL)
INSERT [dbo].[Vehicles] ([VehicleID], [PolicyID], [ModelID], [VIN], [Year], [LicensePlate], [RegistrationRegion], [EnginePower]) VALUES (13009, 15015, 5, N'FDSGFG4W4W2RFG32F', 2020, N'Ф435АП654', NULL, NULL)
INSERT [dbo].[Vehicles] ([VehicleID], [PolicyID], [ModelID], [VIN], [Year], [LicensePlate], [RegistrationRegion], [EnginePower]) VALUES (13010, 15016, 1, N'DFGBDFG5T3G23RG23', 2020, N'Ф123ЫВ234', N'System.Windows.Controls.ComboBoxItem: Москва', 125)
INSERT [dbo].[Vehicles] ([VehicleID], [PolicyID], [ModelID], [VIN], [Year], [LicensePlate], [RegistrationRegion], [EnginePower]) VALUES (13011, 15017, 2, N'DFHGDFGTHR5YH6H34', 2020, N'А123АВ543', N'Новосибирск', 128)
INSERT [dbo].[Vehicles] ([VehicleID], [PolicyID], [ModelID], [VIN], [Year], [LicensePlate], [RegistrationRegion], [EnginePower]) VALUES (14010, 16016, 4, N'SGDFG4TG4GT235G23', 2022, N'ФЫ123ЫВ123', N'Москва', 250)
INSERT [dbo].[Vehicles] ([VehicleID], [PolicyID], [ModelID], [VIN], [Year], [LicensePlate], [RegistrationRegion], [EnginePower]) VALUES (14011, 17017, 1, N'SADFASF3FD1E2ED21', 2020, N'А123АА123', N'Москва', 150)
INSERT [dbo].[Vehicles] ([VehicleID], [PolicyID], [ModelID], [VIN], [Year], [LicensePlate], [RegistrationRegion], [EnginePower]) VALUES (14012, 17020, 4, N'FDASLKJFHLKJH1L2K', 2020, N'В156АВ152', N'Москва', 150)
SET IDENTITY_INSERT [dbo].[Vehicles] OFF
GO
ALTER TABLE [dbo].[DriverInsuranceHistory] ADD  DEFAULT ((0)) FOR [HadAccident]
GO
ALTER TABLE [dbo].[DriverInsuranceHistory] ADD  DEFAULT ((1.0)) FOR [KBM]
GO
ALTER TABLE [dbo].[DriverInsuranceHistory] ADD  DEFAULT (getdate()) FOR [LastUpdated]
GO
ALTER TABLE [dbo].[Logs] ADD  DEFAULT (getdate()) FOR [ChangeDate]
GO
ALTER TABLE [dbo].[ClientDrivers]  WITH CHECK ADD  CONSTRAINT [FK_ClientDrivers_Client] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Clients] ([ClientID])
GO
ALTER TABLE [dbo].[ClientDrivers] CHECK CONSTRAINT [FK_ClientDrivers_Client]
GO
ALTER TABLE [dbo].[ClientDrivers]  WITH CHECK ADD  CONSTRAINT [FK_ClientDrivers_Driver] FOREIGN KEY([DriverID])
REFERENCES [dbo].[Drivers] ([DriverID])
GO
ALTER TABLE [dbo].[ClientDrivers] CHECK CONSTRAINT [FK_ClientDrivers_Driver]
GO
ALTER TABLE [dbo].[Clients]  WITH CHECK ADD  CONSTRAINT [FK_Clients_Agent] FOREIGN KEY([AgentID])
REFERENCES [dbo].[Employees] ([EmployeeID])
GO
ALTER TABLE [dbo].[Clients] CHECK CONSTRAINT [FK_Clients_Agent]
GO
ALTER TABLE [dbo].[Clients]  WITH CHECK ADD  CONSTRAINT [FK_Clients_ClientType] FOREIGN KEY([ClientTypeID])
REFERENCES [dbo].[ClientTypes] ([ClientTypeID])
GO
ALTER TABLE [dbo].[Clients] CHECK CONSTRAINT [FK_Clients_ClientType]
GO
ALTER TABLE [dbo].[DriverInsuranceHistory]  WITH CHECK ADD  CONSTRAINT [FK_DriverInsuranceHistory_Driver] FOREIGN KEY([DriverID])
REFERENCES [dbo].[Drivers] ([DriverID])
GO
ALTER TABLE [dbo].[DriverInsuranceHistory] CHECK CONSTRAINT [FK_DriverInsuranceHistory_Driver]
GO
ALTER TABLE [dbo].[DriverInsuranceHistory]  WITH CHECK ADD  CONSTRAINT [FK_DriverInsuranceHistory_Policy] FOREIGN KEY([PolicyID])
REFERENCES [dbo].[Policies] ([PolicyID])
GO
ALTER TABLE [dbo].[DriverInsuranceHistory] CHECK CONSTRAINT [FK_DriverInsuranceHistory_Policy]
GO
ALTER TABLE [dbo].[Drivers]  WITH CHECK ADD  CONSTRAINT [FK_Drivers_Clients] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Clients] ([ClientID])
GO
ALTER TABLE [dbo].[Drivers] CHECK CONSTRAINT [FK_Drivers_Clients]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Manager] FOREIGN KEY([ManagerID])
REFERENCES [dbo].[Employees] ([EmployeeID])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Manager]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([RoleID])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Role]
GO
ALTER TABLE [dbo].[LifeAndHealth]  WITH CHECK ADD  CONSTRAINT [FK_LifeAndHealth_HealthCondition] FOREIGN KEY([HealthConditionID])
REFERENCES [dbo].[HealthConditions] ([HealthConditionID])
GO
ALTER TABLE [dbo].[LifeAndHealth] CHECK CONSTRAINT [FK_LifeAndHealth_HealthCondition]
GO
ALTER TABLE [dbo].[LifeAndHealth]  WITH CHECK ADD  CONSTRAINT [FK_LifeAndHealth_Policy] FOREIGN KEY([PolicyID])
REFERENCES [dbo].[Policies] ([PolicyID])
GO
ALTER TABLE [dbo].[LifeAndHealth] CHECK CONSTRAINT [FK_LifeAndHealth_Policy]
GO
ALTER TABLE [dbo].[Logs]  WITH CHECK ADD  CONSTRAINT [FK_Logs_Employees] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employees] ([EmployeeID])
GO
ALTER TABLE [dbo].[Logs] CHECK CONSTRAINT [FK_Logs_Employees]
GO
ALTER TABLE [dbo].[Policies]  WITH CHECK ADD  CONSTRAINT [FK_Policies_PolicyType] FOREIGN KEY([PolicyTypeID])
REFERENCES [dbo].[PolicyTypes] ([PolicyTypeID])
GO
ALTER TABLE [dbo].[Policies] CHECK CONSTRAINT [FK_Policies_PolicyType]
GO
ALTER TABLE [dbo].[Policies]  WITH CHECK ADD  CONSTRAINT [FK_Policies_Status] FOREIGN KEY([StatusID])
REFERENCES [dbo].[PolicyStatuses] ([StatusID])
GO
ALTER TABLE [dbo].[Policies] CHECK CONSTRAINT [FK_Policies_Status]
GO
ALTER TABLE [dbo].[PolicyClients]  WITH CHECK ADD  CONSTRAINT [FK_PolicyClients_Client] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Clients] ([ClientID])
GO
ALTER TABLE [dbo].[PolicyClients] CHECK CONSTRAINT [FK_PolicyClients_Client]
GO
ALTER TABLE [dbo].[PolicyClients]  WITH CHECK ADD  CONSTRAINT [FK_PolicyClients_Policy] FOREIGN KEY([PolicyID])
REFERENCES [dbo].[Policies] ([PolicyID])
GO
ALTER TABLE [dbo].[PolicyClients] CHECK CONSTRAINT [FK_PolicyClients_Policy]
GO
ALTER TABLE [dbo].[PolicyDrivers]  WITH CHECK ADD  CONSTRAINT [FK_PolicyDrivers_Drivers] FOREIGN KEY([DriverID])
REFERENCES [dbo].[Drivers] ([DriverID])
GO
ALTER TABLE [dbo].[PolicyDrivers] CHECK CONSTRAINT [FK_PolicyDrivers_Drivers]
GO
ALTER TABLE [dbo].[PolicyDrivers]  WITH CHECK ADD  CONSTRAINT [FK_PolicyDrivers_Policies] FOREIGN KEY([PolicyID])
REFERENCES [dbo].[Policies] ([PolicyID])
GO
ALTER TABLE [dbo].[PolicyDrivers] CHECK CONSTRAINT [FK_PolicyDrivers_Policies]
GO
ALTER TABLE [dbo].[Properties]  WITH CHECK ADD  CONSTRAINT [FK_Properties_Policy] FOREIGN KEY([PolicyID])
REFERENCES [dbo].[Policies] ([PolicyID])
GO
ALTER TABLE [dbo].[Properties] CHECK CONSTRAINT [FK_Properties_Policy]
GO
ALTER TABLE [dbo].[Properties]  WITH CHECK ADD  CONSTRAINT [FK_Properties_PropertyType] FOREIGN KEY([PropertyTypeID])
REFERENCES [dbo].[PropertyTypes] ([PropertyTypeID])
GO
ALTER TABLE [dbo].[Properties] CHECK CONSTRAINT [FK_Properties_PropertyType]
GO
ALTER TABLE [dbo].[VehicleModels]  WITH CHECK ADD  CONSTRAINT [FK_VehicleModels_Make] FOREIGN KEY([MakeID])
REFERENCES [dbo].[VehicleMakes] ([MakeID])
GO
ALTER TABLE [dbo].[VehicleModels] CHECK CONSTRAINT [FK_VehicleModels_Make]
GO
ALTER TABLE [dbo].[Vehicles]  WITH CHECK ADD  CONSTRAINT [FK_Vehicles_Model] FOREIGN KEY([ModelID])
REFERENCES [dbo].[VehicleModels] ([ModelID])
GO
ALTER TABLE [dbo].[Vehicles] CHECK CONSTRAINT [FK_Vehicles_Model]
GO
ALTER TABLE [dbo].[Vehicles]  WITH CHECK ADD  CONSTRAINT [FK_Vehicles_Policy] FOREIGN KEY([PolicyID])
REFERENCES [dbo].[Policies] ([PolicyID])
GO
ALTER TABLE [dbo].[Vehicles] CHECK CONSTRAINT [FK_Vehicles_Policy]
GO
